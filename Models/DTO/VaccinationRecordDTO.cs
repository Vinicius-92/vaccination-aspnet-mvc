using System;
using System.ComponentModel.DataAnnotations;

namespace Vaccination.Models.DTO
{
    public class VaccinationRecordDTO
    {        
        [Required(ErrorMessage = "{0} required.")]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyy}")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "{0} required.")]
        [Display(Name = "Person")]
        public int PersonId { get; set; }
        public PersonDTO PersonDTO { get; set; }

        [Required(ErrorMessage = "{0} required.")]
        public int VaccineBatchId { get; set; }
        public VaccineBatchDTO VaccineBatchDTO { get; set; }

        [Required(ErrorMessage = "{0} required.")]
        public int VaccinationPointId { get; set; }
        public VaccinationPointDTO VaccinationPointDTO { get; set; }

        [Required(ErrorMessage = "{0} required.")]
        [Range(1, 2, ErrorMessage = "The field {0} can be 1 or 2.")]
        public int Dose { get; set; }

        [Required(ErrorMessage = "{0} required.")]
        public bool VaccinationDoneStatus { get; set; }
    }
}