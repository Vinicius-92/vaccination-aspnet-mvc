using Vaccination.Models.DTO;

namespace Vaccination.Models
{
    public class Address
    {
        public Address()
        {
        }
        public Address(int id, string cep, string street, int number, string complement, string city, string state)
        {
            Id = id;
            CEP = cep;
            Street = street;
            Number = number;
            Complement = complement;
            City = city;
            State = state;
        }
        public AddressDTO ToDTO()
        {
            return new AddressDTO {
                Id = this.Id,
                Street = this.Street,
                State = this.State,
                CEP = this.CEP,
                City = this.City,
                Complement = this.Complement,
                Number = this.Number
            };
        }
        public int Id { get; set; }
        public string CEP { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string Complement { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}