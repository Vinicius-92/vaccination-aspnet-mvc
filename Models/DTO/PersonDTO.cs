using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Vaccination.Models.DTO
{
    public class PersonDTO
    {
        [Required(ErrorMessage = "{0} required.")]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "{0} required")]
        [StringLength(11, ErrorMessage = "{0} size must be {1} characters.")]
        public string Cpf { get; set; }
        
        [Required(ErrorMessage = "{0} required")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} size be between {2} and {1}")]
        public string Name { get; set; }
        
        [Display(Name = "Birth date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyy}")]
        public DateTime BirthDate { get; set; }
        
        [Required]
        public AddressDTO AddressDTO { get; set; }
        public IList<VaccinationRecordDTO> Records { get; set; }
        public int VaccineId { get; set; }
    }
}