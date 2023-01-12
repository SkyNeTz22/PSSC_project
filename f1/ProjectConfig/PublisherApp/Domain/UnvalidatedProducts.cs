namespace PublisherApp.Domain
{
    public class UnvalidatedProducts
    {
        public record UnvalidatedProductsList(string Category, string Name, decimal Price);
    }
}
