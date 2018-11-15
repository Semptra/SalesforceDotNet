namespace Semptra.SalesforceDotNet
{
    internal static class CliCommands
    {
        internal const string Json = "--json";

        internal static class Auth
        {
            internal static class Web
            {
                internal const string Login = "force:auth:web:login";
            }
        }

        internal static class Data
        {
            internal static class Record
            {
                internal const string Get = "force:data:record:get";
                internal const string Update = "force:data:record:update";
            }

            internal static class Soql
            {
                internal const string Query = "force:data:soql:query";
            }
        }
    }
}
