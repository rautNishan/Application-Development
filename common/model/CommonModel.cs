
using System.ComponentModel.DataAnnotations;

namespace FinalCoffee1.Common.model
{
    public class CommonModel
    {

        public int Id { get; set; }
        public UserType userType { get; set; }


        [StringLength(20, MinimumLength = 4, ErrorMessage = "User must be at least 4 characters long")]
        public string? Username { get; set; }

        [StringLength(20, MinimumLength = 4, ErrorMessage = "Password must be at least 6 characters long")]
        public string? Password { get; set; }
        public string? Name { get; set; } // Name of the coffee, e.g., "Espresso", "Latte", "Cappuccino", etc.
        public string? CoffeeType { get; set; } // Type of the coffee, e.g., "Espresso", "Drip", "Cold Brew", etc.
        public string? Size { get; set; } // Size of the coffee, e.g., "Small", "Medium", "Large", etc.
        public decimal Price { get; set; } // Price of the coffee, e.g., 3.50, 4.00, 4.50, etc.
    }
}