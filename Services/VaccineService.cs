using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Vaccination.Data;
using Vaccination.Models;
using Vaccination.Models.DTO;
using Vaccination.Services.Exceptions;

namespace Vaccination.Services
{
    public class VaccineService
    {
        private readonly ApplicationDbContext _context;

        public VaccineService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<VaccineDTO>> FindAllAsync()
        {
            var listModel = await _context.Vaccines.ToListAsync();
            var listDto = new List<VaccineDTO>();

            foreach (var model in listModel)
            {
                listDto.Add(new VaccineDTO {
                    Id = model.Id,
                    Name = model.Name,
                    Laboratory = model.Laboratory,
                    Posology = model.Posology,
                    IntervalBetweenDoses = model.IntervalBetweenDoses
                });
            }
            return listDto;
        }

        public async Task<VaccineDTO> FindByIdAsync(int id)
        {
            var model = await _context.Vaccines.FirstOrDefaultAsync(obj => obj.Id == id);
            var dto = new VaccineDTO {
                Id = model.Id,
                Name = model.Name,
                Laboratory = model.Laboratory,
                Posology = model.Posology,
                IntervalBetweenDoses = model.IntervalBetweenDoses
            };
            return dto;
        }

        public async Task InsertAsync(VaccineDTO dto)
        {
            var model = new Vaccine {
                Id = dto.Id,
                Name = dto.Name,
                Laboratory = dto.Laboratory,
                Posology = dto.Posology,
                IntervalBetweenDoses = dto.IntervalBetweenDoses
            };
            _context.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var obj = await _context.Vaccines.FirstOrDefaultAsync(x => x.Id == id);
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

        public async Task UpdateAsync(VaccineDTO dto)
        {
            var model = await _context.Vaccines.FirstOrDefaultAsync(obj => obj.Id == dto.Id);
            if (model == null)
                throw new NotFoundException("Id not found");
            try
            {
                model.Name = dto.Name;
                model.Laboratory = dto.Laboratory;
                model.Posology = dto.Posology;
                model.IntervalBetweenDoses = dto.IntervalBetweenDoses;
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