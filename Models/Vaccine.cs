using Vaccination.Models.Enums;

namespace Vaccination.Models
{
    public class Vaccine
    {
        public Vaccine()
        {
        }
        public Vaccine(int id, string name, string laboratory, Posology posology, int intervalBetweenDoses)
        {
            Id = id;
            Name = name;
            Laboratory = laboratory;
            Posology = posology;
            IntervalBetweenDoses = intervalBetweenDoses;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Laboratory { get; set; }
        public Posology Posology { get; set; }
        public int IntervalBetweenDoses { get; set; }
    }
}