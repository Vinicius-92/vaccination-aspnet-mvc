using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Vaccination.Data;
using Vaccination.Models;
using Vaccination.Models.DTO;
using Vaccination.Services.Exceptions;

namespace Vaccination.Services
{
    public class VaccineBatchService
    {
        private readonly ApplicationDbContext _context;

        public VaccineBatchService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task RemoveFromStockAsync(int id)
        {
            var batch = _context.VaccineBatches.FirstOrDefault(x => x.Id == id);
            batch.AmountInStock -= 1;
            await _context.SaveChangesAsync();
        }   

         public async Task InsertAsync(VaccineBatchDTO dto)
        {
            var model = new VaccineBatch {
                IdentificationCode = dto.IdentificationCode,
                AmountInStock = dto.AmountReceived,
                AmountReceived = dto.AmountReceived,
                DeliveryDate = dto.DeliveryDate,
                ExpirationDate = dto.ExpirationDate,
                Vaccine = _context.Vaccines.First(x => x.Id == dto.VaccineDTO)
            };
            _context.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task<List<VaccineDTO>> TypeVaccines()
        {
            var list = await _context.Vaccines.ToListAsync();
            var listDTO = new List<VaccineDTO>();
            foreach (var vaccine in list)
            {
                listDTO.Add(new VaccineDTO {
                    Id = vaccine.Id,
                    Laboratory = vaccine.Laboratory,
                    IntervalBetweenDoses = vaccine.IntervalBetweenDoses,
                    Name = vaccine.Name,
                    Posology = vaccine.Posology
                });
            }
            return listDTO;
        }

        public async Task<List<VaccineBatchDTO>> FindAllAsync()
        {
            var listModel = await _context.VaccineBatches
                                    .Include(x => x.Vaccine)
                                    .Where(x => x.AmountInStock > 0)
                                    .Where(x => x.ExpirationDate > DateTime.Now).ToListAsync();
            var listDto = new List<VaccineBatchDTO>();
            foreach (var model in listModel)
            {
                listDto.Add(new VaccineBatchDTO {
                    Id = model.Id,
                    IdentificationCode = model.IdentificationCode,
                    AmountReceived = model.AmountReceived,
                    AmountInStock = model.AmountInStock,
                    DeliveryDate = model.DeliveryDate,
                    ExpirationDate = model.ExpirationDate,
                    VaccineDTO = model.Vaccine.Id,
                    Vaccine = new VaccineDTO {
                        Id = model.Vaccine.Id,
                        Name = model.Vaccine.Name,
                        Posology = model.Vaccine.Posology,
                        IntervalBetweenDoses = model.Vaccine.IntervalBetweenDoses,
                        Laboratory = model.Vaccine.Laboratory
                    }
                });
            }
            return listDto;
        }

        public async Task<VaccineBatchDTO> FindByIdAsync(int id)
        {
            var model = await _context.VaccineBatches.Include(x => x.Vaccine).FirstOrDefaultAsync(obj => obj.Id == id);
            var dto = new VaccineBatchDTO {
                Id = model.Id,
                    IdentificationCode = model.IdentificationCode,
                    AmountReceived = model.AmountReceived,
                    AmountInStock = model.AmountInStock,
                    DeliveryDate = model.DeliveryDate,
                    ExpirationDate = model.ExpirationDate,
                    VaccineDTO = model.Vaccine.Id,
                    Vaccine = new VaccineDTO {
                        Id = model.Vaccine.Id,
                        Name = model.Vaccine.Name,
                        Posology = model.Vaccine.Posology,
                        IntervalBetweenDoses = model.Vaccine.IntervalBetweenDoses,
                        Laboratory = model.Vaccine.Laboratory
                }
            };
            return dto;
        }

        public async Task<List<VaccineBatchDTO>> FindByVaccine(int VaccineId)
        {
            var listModel = await _context.VaccineBatches
                                    .Include(x => x.Vaccine)
                                    .Where(x => x.AmountInStock > 0)
                                    .Where(x => x.ExpirationDate > DateTime.Now)
                                    .Where(x => x.Vaccine.Id == VaccineId)
                                    .ToListAsync();
            var vaccine = await _context.Vaccines.FirstOrDefaultAsync(x => x.Id == VaccineId);

                                                
            var listDto = new List<VaccineBatchDTO>();
            foreach (var model in listModel)
            {
                if(model.Vaccine == vaccine)
                {
                listDto.Add(new VaccineBatchDTO {
                    Id = model.Id,
                    IdentificationCode = model.IdentificationCode,
                    AmountReceived = model.AmountReceived,
                    AmountInStock = model.AmountInStock,
                    DeliveryDate = model.DeliveryDate,
                    ExpirationDate = model.ExpirationDate,
                    VaccineDTO = model.Vaccine.Id,
                    Vaccine = new VaccineDTO {
                        Id = model.Vaccine.Id,
                        Name = model.Vaccine.Name,
                        Posology = model.Vaccine.Posology,
                        IntervalBetweenDoses = model.Vaccine.IntervalBetweenDoses,
                        Laboratory = model.Vaccine.Laboratory
                    }
                });
                }
            }
            return listDto;
        }

        public async Task<List<VaccineBatchDTO>> FindBatchesToExpireAsync()
        {
            var expireIn = DateTime.Now.AddDays(30);
            var listModel = await _context.VaccineBatches
                                    .Include(x => x.Vaccine)
                                    .Where(x => x.AmountInStock > 0)
                                    .Where(x => x.ExpirationDate > DateTime.Now && x.ExpirationDate < expireIn)
                                    .ToListAsync();
            
            var listDto = new List<VaccineBatchDTO>();
            foreach (var model in listModel)
            {
                listDto.Add(new VaccineBatchDTO {
                    Id = model.Id,
                    IdentificationCode = model.IdentificationCode,
                    AmountReceived = model.AmountReceived,
                    AmountInStock = model.AmountInStock,
                    DeliveryDate = model.DeliveryDate,
                    ExpirationDate = model.ExpirationDate,
                    VaccineDTO = model.Vaccine.Id,
                    Vaccine = new VaccineDTO {
                        Id = model.Vaccine.Id,
                        Name = model.Vaccine.Name,
                        Posology = model.Vaccine.Posology,
                        IntervalBetweenDoses = model.Vaccine.IntervalBetweenDoses,
                        Laboratory = model.Vaccine.Laboratory
                    }
                });
            }
            return listDto;
        }

        public async Task<string> CheckSingleDoseVaccine(int id)
        {
            var vaccineBatch = await FindByIdAsync(id);
            var posology = vaccineBatch.Vaccine.Posology.ToString();
            return posology;
        }

        public async Task RemoveAsync(int id)
        {
            var obj = await _context.VaccineBatches.FirstOrDefaultAsync(x => x.Id == id);
            if (obj == null)
                throw new NotFoundException("Id not found");
            try
            {
                _context.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch  (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }

        public async Task UpdateAsync(VaccineBatchDTO dto)
        {
            var model = await _context.VaccineBatches.FirstOrDefaultAsync(obj => obj.Id == dto.Id);
            if (model == null)
                throw new NotFoundException("Id not found");
            try
            {
                model.IdentificationCode = dto.IdentificationCode;
                model.AmountInStock = dto.AmountReceived;
                model.AmountReceived = dto.AmountReceived;
                model.DeliveryDate = dto.DeliveryDate;
                model.ExpirationDate = dto.ExpirationDate;
                model.Vaccine = _context.Vaccines.First(x => x.Id == dto.VaccineDTO);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}