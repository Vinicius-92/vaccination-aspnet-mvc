using System;

namespace Vaccination.Models
{
    public class VaccineBatch
    {
        public VaccineBatch()
        {
        }
        public VaccineBatch(int id, Vaccine vaccine, string identificationCode, int amountReceived, int amountInStock, DateTime deliveryDate, DateTime expirationDate)
        {
            Id = id;
            Vaccine = vaccine;
            IdentificationCode = identificationCode;
            AmountReceived = amountReceived;
            AmountInStock = amountInStock;
            DeliveryDate = deliveryDate;
            ExpirationDate = expirationDate;
        }

        public int Id { get; set; }
        public Vaccine Vaccine { get; set; }
        public string IdentificationCode { get; set; }
        public int AmountReceived { get; set; }
        public int AmountInStock { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime ExpirationDate { get; set; }

    }
}