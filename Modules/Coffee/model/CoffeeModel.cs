using FinalCoffee1.Common.model;

namespace FinalCoffee1.Modules.Coffee.model;

public class CoffeeModel :BaseModel
{
    public string? Password { get; set; }
    public string? Name { get; set; } // Name of the coffee, e.g., "Espresso", "Latte", "Cappuccino", etc.
    public string? CoffeeType { get; set; } // Type of the coffee, e.g., "Espresso", "Drip", "Cold Brew", etc.
    public string? Size { get; set; } // Size of the coffee, e.g., "Small", "Medium", "Large", etc.
    public decimal Price { get; set; } // Price of the coffee, e.g., 3.50, 4.00, 4.50, etc.
}