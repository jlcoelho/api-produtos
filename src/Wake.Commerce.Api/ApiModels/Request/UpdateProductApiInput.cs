namespace Wake.Commerce.Api.ApiModels.Request;

public class UpdateProductApiInput
{
    public string? Name { get; set; }
    public int? Stock { get; set; }
    public decimal? Price { get; set; }

    public UpdateProductApiInput(
        string? name = null, 
        int? stock = null, 
        decimal? price = null
    ) {
        Name = name;
        Stock = stock;
        Price = price;
    }
}