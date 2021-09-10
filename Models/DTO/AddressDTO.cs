using System;
using System.ComponentModel.DataAnnotations;

namespace Vaccination.Models.DTO
{
    public class AddressDTO
    {
        [Required(ErrorMessage = "{0} required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [StringLength(8, ErrorMessage = "CEP can't contain more than {1} numbers")]
        public string CEP { get; set; }
        
        [Required(ErrorMessage = "{0} required")]
        [StringLength(60, MinimumLength = 7, ErrorMessage = "{0} size be between {2} and {1}")]
        public string Street { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Range(0, int.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public int Number { get; set; }
        
        [StringLength(60, MinimumLength = 4, ErrorMessage = "{0} size be between {2} and {1}")]
        [Required(ErrorMessage = "{0} required")]
        public string Complement { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} size be between {2} and {1}")]
        public string City { get; set; }

        [Required(ErrorMessage = "{0} required")]
        public string State { get; set; }
    }
}