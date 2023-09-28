namespace Domain.Products;

public record Sku
{
    private const int DefaultLength = 8;
    public string Value { get; set; }
 
    private Sku(string value) => Value = value;

    public static Sku? Create(string value)
    {
        if (string.IsNullOrEmpty(value))
            return null;

        if (value.Length != DefaultLength)
            return null;

        return new Sku(value);
    }
}