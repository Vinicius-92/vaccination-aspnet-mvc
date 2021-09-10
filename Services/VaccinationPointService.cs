using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Vaccination.Data;
using Vaccination.Models;
using Vaccination.Models.DTO;
using Vaccination.Services.Exceptions;

namespace Vaccination.Services
{
    public class VaccinationPointService
    {
        private readonly ApplicationDbContext _context;

        public VaccinationPointService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<VaccinationPointDTO>> FindAllAsync()
        {
            var listModel = await _context.VaccinationPoints.Include(x => x.Address).ToListAsync();
            var listDto = new List<VaccinationPointDTO>();
            foreach (var model in listModel)
            {
                listDto.Add(new VaccinationPointDTO {
                    Id = model.Id,
                    Name = model.Name,
                    AddressDTO = new AddressDTO {
                        Id = model.Address.Id,
                        CEP = model.Address.CEP,
                        Street = model.Address.Street,
                        Number = model.Address.Number,
                        Complement = model.Address.Complement,
                        City = model.Address.City,
                        State = model.Address.State
                    }
                });
            }
            return listDto;
        }

        public async Task<VaccinationPointDTO> FindByIdAsync(int id)
        {
            var model = await _context.VaccinationPoints.Include(x => x.Address).FirstOrDefaultAsync(obj => obj.Id == id);
            var dto = new VaccinationPointDTO {
                Id = model.Id,
                Name = model.Name,
                AddressDTO = new AddressDTO {
                        Id = model.Address.Id,
                        CEP = model.Address.CEP,
                        Street = model.Address.Street,
                        Number = model.Address.Number,
                        Complement = model.Address.Complement,
                        City = model.Address.City,
                        State = model.Address.State
                }
            };
            return dto;
        }

        public async Task InsertAsync(VaccinationPointDTO DTO)
        {
            var model = new VaccinationPoint {
                Id = DTO.Id,
                Name = DTO.Name,
                Address = new Address {
                    Id = DTO.AddressDTO.Id,
                    CEP = DTO.AddressDTO.CEP,
                    Street = DTO.AddressDTO.Street,
                    Number = DTO.AddressDTO.Number,
                    Complement = DTO.AddressDTO.Complement,
                    City = DTO.AddressDTO.City,
                    State = DTO.AddressDTO.State
                }
            };
            _context.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var obj = await _context.VaccinationPoints.Include(x => x.Address).FirstOrDefaultAsync(obj => obj.Id == id);
            if (obj == null)
                throw new NotFoundException("Id not found.");          
            try
            {
                _context.Remove(obj);
                _context.Addresses.Remove(obj.Address);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }

        public async Task UpdateAsync(VaccinationPointDTO dto)
        {
            var model = await _context.VaccinationPoints.Include(x => x.Address).FirstOrDefaultAsync(obj => obj.Id == dto.Id);
            if(model == null)
                throw new NotFoundException("Id not found.");
            
            try
            {
                model.Name = dto.Name;
                model.Address.CEP = dto.AddressDTO.CEP;
                model.Address.Street = dto.AddressDTO.Street;
                model.Address.Number = dto.AddressDTO.Number;
                model.Address.Complement = dto.AddressDTO.Complement;
                model.Address.City = dto.AddressDTO.City;
                model.Address.State = dto.AddressDTO.State;
                _context.Update(model);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}