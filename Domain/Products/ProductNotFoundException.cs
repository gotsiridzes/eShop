namespace Domain.Products;

public sealed class ProductNotFoundException : Exception
{
    public ProductNotFoundException(ProductId id) : base($"Product with Id {id.Value} was not found")
    { }
}