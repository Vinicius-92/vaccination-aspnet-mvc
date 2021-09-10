using System.ComponentModel.DataAnnotations;
using Vaccination.Models.Enums;

namespace Vaccination.Models.DTO
{
    public class VaccineDTO
    {
        [Required(ErrorMessage = "{0} required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} required.")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} size be between {2} and {1}")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "{0} required.")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} size be between {2} and {1}")]
        public string Laboratory { get; set; }

        [Required(ErrorMessage = "{0} required.")]
        public Posology Posology { get; set; }

       [Range(0, 90, ErrorMessage = "The field {0} must be greater than {1} and lower than {2}.")]
       [Display(Name = "Interval between doses")]
        public int IntervalBetweenDoses { get; set; }

    }
}