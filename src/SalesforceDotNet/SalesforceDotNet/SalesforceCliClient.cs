using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Semptra.SalesforceDotNet.Exceptions;
using Semptra.SalesforceDotNet.Models.Entities;
using Semptra.SalesforceDotNet.Models.Results;

namespace Semptra.SalesforceDotNet
{
    public class SalesforceCliClient
    {
        private FileInfo cli { get; }

        public SalesforceCliClient(string cliPath)
        {
            if (!File.Exists(cliPath))
            {
                throw new FileNotFoundException("Cannot find path to CLI.", cliPath);
            }

            var cliFileInfo = new FileInfo(cliPath);

            if (cliFileInfo.Extension != ".cmd")
            {
                throw new IOException("CLI extension must be .cmd");
            }

            this.cli = cliFileInfo;
        }

        public string WebAuthorize()
        {
            var arguments = this.BuildArgumentsList(CliCommands.Auth.Web.Login, 
                CliCommands.Json);

            var executionResult = this.RunCli(arguments);

            if (!executionResult.Success)
            {
                throw new SalesforceException(executionResult.Error);
            }

            return executionResult.Output;
        }

        public T GetRecordByQuery<T>(string query, string sObjectType = null) 
            where T : Entity
        {
            var arguments = this.BuildArgumentsList(CliCommands.Data.Record.Get,
                "-s", sObjectType ?? typeof(T).Name, 
                "-w", $"\"{query}\"", 
                CliCommands.Json);

            return this.RunCli<T>(arguments);
        }

        public T GetRecordById<T>(string recordId, string sObjectType = null) 
            where T : Entity
        {
            var arguments = this.BuildArgumentsList(CliCommands.Data.Record.Get,
                "-s", sObjectType ?? typeof(T).Name,
                "-i", recordId,
                CliCommands.Json);

            return this.RunCli<T>(arguments);
        }

        public UpdateResult UpdateRecord(string sObjectType, string recordId, IDictionary<string, string> values)
        {
            var arguments = this.BuildArgumentsList(CliCommands.Data.Record.Update,
                "-s", sObjectType, 
                "-i", recordId, 
                "-v", this.EntityValuesToString(values), 
                CliCommands.Json);

            return this.RunCli<UpdateResult>(arguments);
        }

        public SoqlResult<T> Soql<T>(string query) 
            where T : Entity
        {
            string selectFields = string.Join(", ", JsonHelpers.GetJsonProperties(typeof(T)));
            query = query.Replace("SELECT * FROM", $"SELECT {selectFields} FROM");

            var arguments = this.BuildArgumentsList(CliCommands.Data.Soql.Query,
                "-q", $"\"{query}\"", 
                CliCommands.Json);

            return this.RunCli<SoqlResult<T>>(arguments);
        }

        private T RunCli<T>(params string[] arguments)
        {
            CliExecutionResult executionResult = this.RunCli(arguments);

            if (!executionResult.Success)
            {
                throw new SalesforceException(executionResult.Error);
            }

            var result = JsonConvert.DeserializeObject<CommandResponce<T>>(executionResult.Output);

            if (result.Status != 0)
            {
                throw new SalesforceException($"Result statis is {result.Status}");
            }

            return result.Result;
        }

        private CliExecutionResult RunCli(params string[] arguments)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = this.cli.FullName,
                Arguments = string.Join(" ", arguments),
                CreateNoWindow = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true
            };

            var process = new Process
            {
                StartInfo = startInfo
            };

            process.Start();
            process.WaitForExit((int)new TimeSpan(0, 0, 10).TotalMilliseconds);

            string error = process.StandardError.ReadToEnd();

            return new CliExecutionResult
            {
                Output = process.StandardOutput.ReadToEnd(),
                Error = error,
                Success = string.IsNullOrEmpty(error)
            };
        }

        private string[] BuildArgumentsList(params string[] arguments)
        {
            return arguments.Where(x => !string.IsNullOrEmpty(x)).ToArray();
        }

        private string EntityValuesToString(IDictionary<string, string> values)
        {
            if (values?.Count == 0)
            {
                return string.Empty;
            }

            var stringBuilder = new StringBuilder();
            stringBuilder.Append("\"");
            
            foreach(var keyValue in values)
            {
                if (string.IsNullOrEmpty(keyValue.Key))
                {
                    throw new ArgumentException("Key is null or empty", nameof(values));
                }

                stringBuilder.Append($"{keyValue.Key}='{keyValue.Value} '");
            }

            stringBuilder.Remove(stringBuilder.Length - 1, 1);
            stringBuilder.Append("\"");

            return stringBuilder.ToString();
        }
    }
}
