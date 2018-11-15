namespace Semptra.SalesforceDotNet.Models.Results
{
    public class CommandResponce<T>
    {
        public int Status { get; set; }

        public T Result { get; set; }
    }
}
