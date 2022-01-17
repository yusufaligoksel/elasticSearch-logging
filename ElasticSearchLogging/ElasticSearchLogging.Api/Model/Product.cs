namespace ElasticSearchLogging.Api.Model;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsActive{ get; set; }
    public bool IsDeleted { get; set; } = false;
    public decimal Price { get; set; }
    public int Stock { get; set; }
}