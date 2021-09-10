using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Vaccination.Data;
using Vaccination.Models;
using Vaccination.Models.DTO;
using System.Linq;

namespace Vaccination.Services
{
    public class VaccinationRecordService
    {
        private readonly ApplicationDbContext _context;
        private readonly PersonService _personService;
        private readonly VaccinationPointService _vaccinationPointService;
        private readonly VaccineBatchService _vaccineBatchService;
        private readonly VaccineService _vaccineService;
        public VaccinationRecordService(ApplicationDbContext context, 
                                        PersonService personService, 
                                        VaccinationPointService vaccinationPointService, 
                                        VaccineBatchService vaccineBatchService,
                                        VaccineService vaccineService)
        {
            _context = context;
            _personService = personService;
            _vaccinationPointService = vaccinationPointService;
            _vaccineBatchService = vaccineBatchService;
            _vaccineService = vaccineService;
        }

        public async Task<List<VaccinationRecordDTO>> FindAllAsync()
        {
            var list = await _context.VaccinationRecords
                                      .Include(x => x.Person)
                                      .Include(x => x.VaccinationPoint)
                                      .Include(x => x.VaccineBatch)
                                      .ToListAsync();
            var listDto = new List<VaccinationRecordDTO>();
            foreach (var record in list)
            {
                listDto.Add(new VaccinationRecordDTO {
                    Id = record.Id,
                    Dose = record.Dose,
                    Date = record.Date,
                    VaccinationDoneStatus = record.VaccinationDoneStatus,
                    VaccinationPointId = record.VaccinationPoint.Id,
                    VaccineBatchId = record.VaccineBatch.Id,
                    PersonId = record.Person.Id,
                    PersonDTO = await _personService.FindByIdAsync(record.Person.Id),
                    VaccinationPointDTO = await _vaccinationPointService.FindByIdAsync(record.VaccinationPoint.Id),
                    VaccineBatchDTO = await _vaccineBatchService.FindByIdAsync(record.VaccineBatch.Id)
                });
            }
            return listDto;
        }

        public async Task<List<VaccinationRecordDTO>> FindByIdAsync(int id)
        {
            var list = await _context.VaccinationRecords
                                      .Include(x => x.Person)
                                      .Include(x => x.VaccinationPoint)
                                      .Include(x => x.VaccineBatch)
                                      .Where(x => x.Person.Id == id)
                                      .ToListAsync();
            var listDto = new List<VaccinationRecordDTO>();
            foreach (var record in list)
            {
                listDto.Add(new VaccinationRecordDTO {
                    Id = record.Id,
                    Dose = record.Dose,
                    Date = record.Date,
                    VaccinationDoneStatus = record.VaccinationDoneStatus,
                    VaccinationPointId = record.VaccinationPoint.Id,
                    VaccineBatchId = record.VaccineBatch.Id,
                    PersonId = record.Person.Id,
                    PersonDTO = await _personService.FindByIdAsync(record.Person.Id),
                    VaccinationPointDTO = await _vaccinationPointService.FindByIdAsync(record.VaccinationPoint.Id),
                    VaccineBatchDTO = await _vaccineBatchService.FindByIdAsync(record.VaccineBatch.Id)
                });
            }
            return listDto;
        }
        public async Task InsertAsync(VaccinationRecordDTO dto)
        {
            var model = new VaccinationRecord {
                Id = dto.Id,
                Date = dto.Date,
                Dose = dto.Dose,
                VaccineBatch = await _context.VaccineBatches.FirstOrDefaultAsync(x => x.Id == dto.VaccineBatchId),
                Person = await _context.People.FirstOrDefaultAsync(x => x.Id == dto.PersonId),
                VaccinationPoint = await _context.VaccinationPoints.FirstOrDefaultAsync(x => x.Id == dto.VaccinationPointId),
                VaccinationDoneStatus = dto.VaccinationDoneStatus
            };
            _context.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task<int> DosesIntervalConfirmation(int id)
        {
            var person = await _personService.FindByIdModelAsync(id);
            if (person.Records.Count == 0)
                return 0;
            var date = person.Records[0].Date;
            var dateToCompare = DateTime.Now - date;
            return (int)dateToCompare.TotalDays;
        }
    }
}