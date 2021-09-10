using System;
using System.Collections.Generic;

namespace Vaccination.Models
{
    public class Person
    {
        public Person()
        {
        }
        public Person(int id, string cpf, string name, DateTime birthDate, Address address)
        {
            Id = id;
            Cpf = cpf;
            Name = name;
            BirthDate = birthDate;
            Address = address;
        }
        public int Id { get; set; }
        public string Cpf { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public Address Address { get; set; }
        public IList<VaccinationRecord> Records { get; set; }
    }
}