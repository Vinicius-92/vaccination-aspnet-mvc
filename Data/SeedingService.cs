using System;
using System.Linq;
using Vaccination.Models;
using Vaccination.Models.Enums;

namespace Vaccination.Data
{
    public class SeedingService
    {
        private readonly ApplicationDbContext _context;

        public SeedingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if(_context.People.Any() || 
               _context.Vaccines.Any() ||
               _context.VaccineBatches.Any() ||
               _context.VaccinationPoints.Any() || 
               _context.VaccinationRecords.Any() ||
               _context.Addresses.Any())
               {
                   return;
               }
               var a1 = new Address(1, "13480001", "Street One", 100, "House", "Starter City", "GFT");
               var a2 = new Address(2, "13480002", "Street Two", 200, "Commercial", "Starter City", "GFT");
               var a3 = new Address(3, "13480003", "Street Three", 300, "Apartment", "Starter City", "GFT");
               var a4 = new Address(4, "13480004", "Street Four", 400, "House", "Starter City", "GFT");
               var a5 = new Address(5, "13480005", "Street Five", 500, "Apartment", "Starter City", "GFT");
               var a6 = new Address(6, "13480006", "Street Six", 600, "Commercial", "Starter City", "GFT");
               var a7 = new Address(7, "13480007", "Street Seven", 700, "House", "Starter City", "GFT");
               var a8 = new Address(8, "13480008", "Street Eight", 800, "House", "Starter City", "GFT");
               var a9 = new Address(9, "13480009", "Street Nine", 900, "House", "Starter City", "GFT");
               var a10 = new Address(10, "13480010", "Street Ten", 101, "Commercial", "Starter City", "GFT");
               var a11 = new Address(11, "13480011", "Street Eleven", 102, "House", "Starter City", "GFT");
               var a12 = new Address(12, "13480012", "Street Twelve", 103, "Apartment", "Starter City", "GFT");
               var a13 = new Address(13, "13480013", "Street Thirteen", 104, "House", "Starter City", "GFT");
               var a14 = new Address(14, "13480014", "Street Fourteen", 105, "House", "Starter City", "GFT");
               var a15 = new Address(15, "13480015", "Street Fifteen", 106, "Apartment", "Starter City", "GFT");
               _context.Addresses.AddRange(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15);
               
               var vp1 = new VaccinationPoint(1, "Vaccionation point 1", a1);
               var vp2 = new VaccinationPoint(2, "Vaccionation point 2", a2);
               var vp3 = new VaccinationPoint(3, "Vaccionation point 3", a3);
               var vp4 = new VaccinationPoint(4, "Vaccionation point 4", a4);
               var vp5 = new VaccinationPoint(5, "Vaccionation point 5", a5);
               _context.VaccinationPoints.AddRange(vp1, vp2, vp3, vp4, vp5);
               
               var p1 = new Person(1, "89831066030", "Kael Basílio Espargosa", new DateTime(1990, 01, 01), a1);
               var p2 = new Person(2, "36195074063", "Hoorain Maciel Prado", new DateTime(1991, 02, 02), a2);
               var p3 = new Person(3, "43589070080", "Kevyn César Pegado", new DateTime(1992, 03, 03), a3);
               var p4 = new Person(4, "31724938061", "Cloe Ginjeira Godinho", new DateTime(1993, 04, 04), a4);
               var p5 = new Person(5, "67129293004", "Isaque Quinterno Serro", new DateTime(1994, 05, 05), a5);
               var p6 = new Person(6, "24169780043", "Theo Capucho Carneiro", new DateTime(1995, 06, 06), a6);
               var p7 = new Person(7, "58458399040", "Santiago Gama Pires", new DateTime(1980, 07, 07), a7);
               var p8 = new Person(8, "39948098005", "Melinda Mourato Caeiro", new DateTime(1981, 08, 08), a8);
               var p9 = new Person(9, "74027497031", "Andreia Simas Sarmento", new DateTime(1982, 09, 09), a9);
               var p10 = new Person(10, "51853478059", "India Baião Vilarinho", new DateTime(1983, 10, 14), a10);
               var p11 = new Person(11, "76888495000", "Mercês Negromonte Lira", new DateTime(1984, 11, 22), a11);
               var p12 = new Person(12, "63154633049", "Lourenço Bingre Pederneiras", new DateTime(1985, 12, 30), a12);
               var p13 = new Person(13, "18426197051", "Charlotte Hipólito Homem", new DateTime(1986, 01, 25), a13);
               var p14 = new Person(14, "22105176054", "Hélio Caiado Carmo", new DateTime(1987, 02, 14), a14);
               var p15 = new Person(15, "72015287000", "Martina Grilo Bernardes", new DateTime(1988, 03, 19), a15);
               var p16 = new Person(16, "53421811008", "Diva Pimentel Baião", new DateTime(1989, 04, 23), a1);
               var p17 = new Person(17, "84567138058", "Lorenzo Loio Lagoa", new DateTime(1979, 05, 14), a2);
               var p18 = new Person(18, "41782550020", "Ianis Milheirão Bettencourt", new DateTime(1978, 06, 13), a3);
               var p19 = new Person(19, "03205568087", "Maísa Lagoa Domingos", new DateTime(1977, 07, 29), a4);
               var p20 = new Person(20, "60708238009", "Linda Feitosa Freire", new DateTime(1976, 08, 28), a5);
               var p21 = new Person(21, "23372143035", "Tiara Carmo Gomide", new DateTime(1991, 08, 05), a5);
               var p22 = new Person(22, "26177512011", "Taísa Gadelha Bandeira", new DateTime(1986, 08, 28), a5);
               var p23 = new Person(23, "96820350074", "Nour Valcanaia Zagalo", new DateTime(1995, 08, 18), a5);
               _context.People.AddRange(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23);

               var v1 = new Vaccine(1, "Coronavac", "Butantan", Posology.Double, 21);
               var v2 = new Vaccine(2, "Covishield", "AstraZeneca/Oxford", Posology.Double, 28);
               var v3 = new Vaccine(3, "Cominarty", "Pfizer/BioNTech", Posology.Double, 84);
               var v4 = new Vaccine(4, "Janssen", "Jhonson&Jhonson", Posology.Single, 0);
               var v5 = new Vaccine(5, "Covaxin", "Bharat Biotech", Posology.Double, 28);
               var v6 = new Vaccine(6, "Sputnik", "Gamaleya", Posology.Double, 21);
               _context.Vaccines.AddRange(v1, v2, v3, v4, v5, v6);

               var b1 = new VaccineBatch(1, v1, "CORONA1", 150, 10, new DateTime(2021, 08, 25), new DateTime(2021, 12, 31));
               var b2 = new VaccineBatch(2, v2, "ASTRZ50", 80, 49, new DateTime(2021, 07, 15), new DateTime(2021, 10, 25));
               var b3 = new VaccineBatch(3, v3, "PFAIZ22", 100, 80, new DateTime(2021, 04, 14), new DateTime(2022, 02, 11));
               var b4 = new VaccineBatch(4, v4, "BEBE505", 40, 37, new DateTime(2021, 07, 29), new DateTime(2021, 09, 15));
               var b5 = new VaccineBatch(5, v5, "BHARAT9", 15, 10, new DateTime(2021, 06, 01), new DateTime(2021, 09, 10));
               var b6 = new VaccineBatch(6, v6, "SPUTNK6", 10, 8, new DateTime(2021, 08, 10), new DateTime(2021, 12, 25));
               var b7 = new VaccineBatch(7, v1, "CORONA0", 150, 0, new DateTime(2021, 04, 15), new DateTime(2021, 08, 31));
               _context.VaccineBatches.AddRange(b1, b2, b3, b4, b5, b6, b7);

               var r1 = new VaccinationRecord(1, new DateTime(2021, 06, 25), p1, b1, vp1, 1, false);
               var r3 = new VaccinationRecord(3, new DateTime(2021, 04, 05), p2, b2, vp3, 1, false);
               var r5 = new VaccinationRecord(5, new DateTime(2021, 05, 01), p3, b3, vp2, 1, false);
               var r7 = new VaccinationRecord(7, new DateTime(2021, 07, 12), p4, b5, vp1, 1, false);
               var r9 = new VaccinationRecord(9, new DateTime(2021, 07, 27), p5, b6, vp3, 1, false);
               var r11 = new VaccinationRecord(11, new DateTime(2021, 04, 09), p6, b7, vp4, 1, false);
               var r13 = new VaccinationRecord(13, new DateTime(2021, 06, 10), p7, b2, vp5, 1, false);
               var r15 = new VaccinationRecord(15, new DateTime(2021, 04, 15), p8, b3, vp1, 1, false);
               var r17 = new VaccinationRecord(17, new DateTime(2021, 08, 08), p9, b3, vp2, 1, false);               
               var r18 = new VaccinationRecord(18, new DateTime(2021, 07, 08), p10, b6, vp2, 1, true);
               var r19 = new VaccinationRecord(19, new DateTime(2021, 07, 18), p11, b5, vp3, 1, false);
               var r23 = new VaccinationRecord(23, new DateTime(2021, 08, 08), p16, b4, vp1, 1, false);
               var r24 = new VaccinationRecord(24, new DateTime(2021, 08, 08), p17, b4, vp2, 1, false);
               var r25 = new VaccinationRecord(25, new DateTime(2021, 08, 08), p18, b4, vp3, 1, false);
               var r26 = new VaccinationRecord(26, new DateTime(2021, 08, 08), p19, b4, vp4, 1, false);
               var r27 = new VaccinationRecord(27, new DateTime(2021, 08, 08), p20, b4, vp5, 1, false);
               var r20 = new VaccinationRecord(20, new DateTime(2021, 08, 01), p21, b5, vp3, 1, false);
               var r21 = new VaccinationRecord(21, new DateTime(2021, 06, 30), p22, b2, vp4, 1, false);
               var r22 = new VaccinationRecord(22, new DateTime(2021, 07, 28), p23, b3, vp5, 1, false);
               _context.VaccinationRecords.AddRange(r1, r3, r5, r7, r9, r11,r13, r15, r17, r18, r19, r20, r21, r22);
               _context.SaveChanges();

               var r2 = new VaccinationRecord(2, new DateTime(2021, 07, 16), p1, b1, vp1, 2, true);
               var r4 = new VaccinationRecord(4, new DateTime(2021, 05, 03), p2, b2, vp4, 2, true);
               var r6 = new VaccinationRecord(6, new DateTime(2021, 08, 28), p3, b3, vp5, 2, true);
               var r8 = new VaccinationRecord(8, new DateTime(2021, 07, 30), p4, b5, vp3, 2, true);
               var r10 = new VaccinationRecord(10, new DateTime(2021, 08, 18), p5, b6, vp2, 2, true);
               var r12 = new VaccinationRecord(12, new DateTime(2021, 04, 30), p6, b7, vp4, 2, true);
               var r14 = new VaccinationRecord(14, new DateTime(2021, 07, 04), p7, b2, vp5, 2, true);
               var r16 = new VaccinationRecord(16, new DateTime(2021, 07, 28), p8, b3, vp3, 2, true);
               _context.VaccinationRecords.AddRange(r2, r4, r6, r8, r10, r12, r14, r16);         
               _context.SaveChanges();
        }
    }
}