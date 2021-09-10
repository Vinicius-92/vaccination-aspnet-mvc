using System.ComponentModel.DataAnnotations;

namespace Vaccination.Models.DTO
{
    public class VaccinationPointDTO
    {
        [Required(ErrorMessage = "{0} required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} required.")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} size be between {2} and {1}")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} required.")]
        public AddressDTO AddressDTO { get; set; }
    }
}