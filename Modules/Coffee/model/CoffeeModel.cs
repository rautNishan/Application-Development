namespace FinalCoffee1.Modules.Coffee.model;

public class CoffeeModel
{
       public int Id { get; set; } // Unique identifier for each coffee, e.g., 1, 2, 3, etc.
    public string? Name { get; set; } // Name of the coffee, e.g., "Espresso", "Latte", "Cappuccino", etc.
    public string? CoffeeType { get; set; } // Type of the coffee, e.g., "Espresso", "Drip", "Cold Brew", etc.
    public string? Size{ get; set; } // Size of the coffee, e.g., "Small", "Medium", "Large", etc.
    public decimal Price { get; set; } // Price of the coffee, e.g., 3.50, 4.00, 4.50, etc.
}