using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Vaccination.Data;
using Vaccination.Models;
using Vaccination.Models.DTO;
using Vaccination.Services.Exceptions;

namespace Vaccination.Services
{
    public class AddressService
    {
        private readonly ApplicationDbContext _context;

        public AddressService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<AddressDTO>> FindAllAsync()
        {
            var listModel = await _context.Addresses.ToListAsync();
            var listDTO = new List<AddressDTO>();
            foreach(var model in listModel)
            {
                listDTO.Add(new AddressDTO {
                    Id = model.Id,
                    CEP = model.CEP,
                    Street = model.Street,
                    Number = model.Number,
                    Complement = model.Complement,
                    City = model.City,
                    State = model.State
                });
            }
            return listDTO;
        }

        public async Task<AddressDTO> FindByIdAsync(int id)
        {
            var address = await _context.Addresses.FirstOrDefaultAsync(obj => obj.Id == id);
            var dto = new AddressDTO {
                Id = address.Id,
                CEP = address.CEP,
                Street = address.Street,
                Number = address.Number,
                Complement = address.Complement,
                City = address.City,
                State = address.State
            };
            return dto;
        }

        public async Task InsertAsync(AddressDTO dto)
        {
            var model = new Address {
                Id = dto.Id,
                CEP = dto.CEP,
                Street = dto.Street,
                Number = dto.Number,
                Complement = dto.Complement,
                City = dto.City,
                State = dto.State
            };
            _context.Addresses.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Addresses.FindAsync(id);
                _context.Addresses.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }

        public async Task UpdateAsync(AddressDTO dto)
        {
            var exists =  await _context.Addresses.AnyAsync(x => x.Id == dto.Id);
            if(!exists)
                throw new NotFoundException("Id not found.");
            try
            {
                var model = new Address{
                    Id = dto.Id,
                    CEP = dto.CEP,
                    Street = dto.Street,
                    Number = dto.Number,
                    Complement = dto.Complement,
                    City = dto.City,
                    State = dto.State
                };
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