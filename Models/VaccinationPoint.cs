namespace Vaccination.Models
{
    public class VaccinationPoint
    {
        public VaccinationPoint()
        {
        }
        public VaccinationPoint(int id, string name, Address address)
        {
            Id = id;
            Name = name;
            Address = address;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }

    }
}