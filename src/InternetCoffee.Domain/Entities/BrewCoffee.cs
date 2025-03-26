namespace InternetCoffee.Domain.Entities
{
    /// <summary>
    /// Brew coffee entity
    /// If we need to persist the data, 
    /// we can add a constructor to this class 
    /// and use it to map the data from the database
    /// </summary>
    public class BrewCoffee
    {
        public int StatusCode { get; }
        public object Content { get; }

        public BrewCoffee(int statusCode, object content)
        {
            StatusCode = statusCode;
            Content = content;
        }
    }
}
