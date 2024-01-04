
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
    }
}