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
    public class PersonService
    {
        private readonly ApplicationDbContext _context;

        public PersonService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<PersonDTO>> FindAllAsync()
        {
            var listModel = await _context.People.Include(x => x.Address).ToListAsync();
            var listDto = new List<PersonDTO>();
            foreach (var model in listModel)
            {
                listDto.Add(new PersonDTO {
                    Id = model.Id,
                    Name = model.Name,
                    BirthDate = model.BirthDate,
                    Cpf = model.Cpf,
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

        public async Task<List<PersonDTO>> FindAllCompletedVaccinetedAsync()
        {
            var listModel = await _context.People
                                          .Include(x => x.Address)
                                          .Include(x => x.Records)
                                          .ToListAsync();
                       
            var listDto = new List<PersonDTO>();
            foreach (var model in listModel)
            {
                if (model.Records.Count == 2)
                {
                     listDto.Add(new PersonDTO {
                        Id = model.Id,
                        Name = model.Name,
                        BirthDate = model.BirthDate,
                        Cpf = model.Cpf,
                        AddressDTO = new AddressDTO {
                            Id = model.Address.Id,
                            CEP = model.Address.CEP,
                            Street = model.Address.Street,
                            Number = model.Address.Number,
                            Complement = model.Address.Complement,
                            City = model.Address.City,
                            State = model.Address.State,
                        }
                    });
                }
                else if (model.Records.Count == 1 && model.Records[0].VaccinationDoneStatus == true)
                {
                     listDto.Add(new PersonDTO {
                        Id = model.Id,
                        Name = model.Name,
                        BirthDate = model.BirthDate,
                        Cpf = model.Cpf,
                        AddressDTO = new AddressDTO {
                            Id = model.Address.Id,
                            CEP = model.Address.CEP,
                            Street = model.Address.Street,
                            Number = model.Address.Number,
                            Complement = model.Address.Complement,
                            City = model.Address.City,
                            State = model.Address.State,
                        }
                    });
                }
            }
            return listDto;
        }

        public async Task<List<PersonDTO>> FindAllWitoutCompletedVaccinesAsync()
        {
            var listModel = await _context.People
                                          .Include(x => x.Address)
                                          .Include(x => x.Records)
                                          .Where(x => x.Records.Count < 2)
                                          .ToListAsync();
                       
            var listDto = new List<PersonDTO>();
            foreach (var model in listModel)
            {
                if (model.Records.Count == 0)
                {
                     listDto.Add(new PersonDTO {
                        Id = model.Id,
                        Name = model.Name,
                        BirthDate = model.BirthDate,
                        Cpf = model.Cpf,
                        AddressDTO = new AddressDTO {
                            Id = model.Address.Id,
                            CEP = model.Address.CEP,
                            Street = model.Address.Street,
                            Number = model.Address.Number,
                            Complement = model.Address.Complement,
                            City = model.Address.City,
                            State = model.Address.State,
                        }
                    });
                }
                else if (model.Records[0].VaccinationDoneStatus == false)
                {
                     listDto.Add(new PersonDTO {
                        Id = model.Id,
                        Name = model.Name,
                        BirthDate = model.BirthDate,
                        Cpf = model.Cpf,
                        AddressDTO = new AddressDTO {
                            Id = model.Address.Id,
                            CEP = model.Address.CEP,
                            Street = model.Address.Street,
                            Number = model.Address.Number,
                            Complement = model.Address.Complement,
                            City = model.Address.City,
                            State = model.Address.State,
                        }
                    });
                }
            }
            return listDto;
        }

        public async Task<PersonDTO> FindByIdAsync(int id)
        {
            var model = await _context.People.Include(x => x.Address).FirstOrDefaultAsync(obj => obj.Id == id);
            var dto = new PersonDTO {
                    Id = model.Id,
                    Name = model.Name,
                    BirthDate = model.BirthDate,
                    Cpf = model.Cpf,
                    AddressDTO = model.Address.ToDTO()
            };
            return dto;
         }

         public async Task<Person> FindByIdModelAsync(int id)
        {
            var model = await _context.People
                                      .Include(x => x.Address)
                                      .Include("Records.VaccineBatch.Vaccine")
                                      .FirstOrDefaultAsync(obj => obj.Id == id);
            return model;
         }

        public async Task<bool> CheckCpfInDatabase(string cpf)
        {
            var model = await _context.People.FirstOrDefaultAsync(x => x.Cpf.Equals(cpf));
            if (model == null)
                return false;
            else
                return true;
        }

        public async Task InsertAsync(PersonDTO dto)
        {
            var model = new Person {
                Id = dto.Id,
                Name = dto.Name,
                BirthDate = dto.BirthDate,
                Cpf = dto.Cpf,
                Address = new Address {
                    Id = dto.AddressDTO.Id,
                    CEP = dto.AddressDTO.CEP,
                    Street = dto.AddressDTO.Street,
                    Number = dto.AddressDTO.Number,
                    Complement = dto.AddressDTO.Complement,
                    City = dto.AddressDTO.City,
                    State = dto.AddressDTO.State
                }
            };
            _context.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var obj = await _context.People.Include(x => x.Address).FirstOrDefaultAsync(obj => obj.Id == id);
            if(obj == null)
                throw new NotFoundException("Id not found");
            try
            {
                _context.Remove(obj);
                _context.Remove(obj.Address);
                await _context.SaveChangesAsync();
            }
            catch  (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }

        public async Task UpdateAsync(PersonDTO dto)
        {
            var model = await _context.People.Include(x => x.Address).FirstOrDefaultAsync(obj => obj.Id == dto.Id);
            if (model == null)
                throw new NotFoundException("Id not found.");
            try
            {
                model.Name = dto.Name;
                model.Cpf = dto.Cpf;
                model.BirthDate = dto.BirthDate;
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