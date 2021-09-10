using System;

namespace Vaccination.Models
{
    public class VaccinationRecord
    {
        public VaccinationRecord()
        {
        }
        public VaccinationRecord(int id, DateTime date, Person person, VaccineBatch vaccineBatch, VaccinationPoint vaccinationPoint, int dose, bool vaccinationDoneStatus)
        {
            Id = id;
            Date = date;
            Person = person;
            VaccineBatch = vaccineBatch;
            VaccinationPoint = vaccinationPoint;
            Dose = dose;
            VaccinationDoneStatus = vaccinationDoneStatus;
        }
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Person Person { get; set; }
        public VaccineBatch VaccineBatch { get; set; }
        public VaccinationPoint VaccinationPoint { get; set; }
        public int Dose { get; set; }
        public bool VaccinationDoneStatus { get; set; }
    }
}