namespace dotnet_crud_api.Models;

public partial class Coupon
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public DateOnly? Expiration { get; set; }
}
