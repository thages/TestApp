namespace TestApp.Domain.DataTransferObjects
{
    public class SumIndicatorDetails
    {
        public SumIndicatorDetails(DateTimeOffset date, decimal value)
        {
            Date = date;
            Value = value;
        }

        public DateTimeOffset Date { get; private set; }
        public decimal Value { get; private set; }
    }
}