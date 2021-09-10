using System;
using System.ComponentModel.DataAnnotations;

namespace Vaccination.Models.DTO
{
    public class VaccineBatchDTO
    {
        [Required(ErrorMessage = "{0} required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} required.")]
        public int VaccineDTO { get; set; }
        public VaccineDTO Vaccine { get; set; }

        [Required(ErrorMessage = "{0} required.")]
        [StringLength(8, MinimumLength = 6, ErrorMessage = "{0} size be between {2} and {1}")]
        [Display(Name = "Code")]
        public string IdentificationCode { get; set; }

        [Required(ErrorMessage = "{0} required.")]
        [Display(Name = "Amount Received")]
        [Range(0, int.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public int AmountReceived { get; set; }

        [Required(ErrorMessage = "{0} required.")]
        [Display(Name = "Amount in stock")]
        [Range(0, int.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public int AmountInStock { get; set; }

        [Required(ErrorMessage = "{0} required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyy}")]
        [Display(Name = "Delivery date")]
        public DateTime DeliveryDate { get; set; }

        [Required(ErrorMessage = "{0} required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyy}")]
        [Display(Name = "Expiration date")]
        public DateTime ExpirationDate { get; set; }
    }
}