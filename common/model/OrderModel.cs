namespace FinalCoffee1.Common.model;
using FinalCoffee1.Modules.Coffee.model;

public class OrderModel
{
    public string Email { get; set; }
    public decimal TotalPrice { get; set; }
    public List<CommonModel> CoffeeData { get; set; }
}