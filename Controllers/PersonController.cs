using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vaccination.Models.DTO;
using Vaccination.Models.ViewModel;
using Vaccination.Services;
using Vaccination.Services.Exceptions;

namespace Vaccination.Controllers
{
    [Authorize]
    public class PersonController : Controller
    {
        private readonly PersonService _personService;
        private readonly AddressService _addressService;
        private readonly VaccinationRecordService _vaccinationRecordService;

        public PersonController(PersonService personService, 
                                AddressService addressService, 
                                VaccinationRecordService vaccinationRecordService)
        {
            _personService = personService;
            _addressService = addressService;
            _vaccinationRecordService = vaccinationRecordService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _personService.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Details(int id)
        {
            var dto = await _personService.FindByIdAsync(id);
            ViewBag.Records = await _vaccinationRecordService.FindByIdAsync(id);
            return View(dto);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PersonDTO dto)
        {
            if(!ModelState.IsValid)
                return View();
            else if(await _personService.CheckCpfInDatabase(dto.Cpf))
                return RedirectToAction(nameof(Error), new { message = "CPF already exists in database"});
            else
            {
                await _personService.InsertAsync(dto);
            }
            Response.StatusCode = 200;
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { message = "Id not provided"});
            var obj = await _personService.FindByIdAsync(id.Value);
            if (obj == null)
                return RedirectToAction(nameof(Error), new { message = "Id not found"});
            return View(obj);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _personService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { message = "Id not provided"});
            var obj = await _personService.FindByIdAsync(id.Value);
            if (obj == null)
                return RedirectToAction(nameof(Error), new { message = "Id not found"});
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PersonDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);
            if (id != dto.Id)
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            try
            {
                await _personService.UpdateAsync(dto);
                return RedirectToAction(nameof(Index));
            } catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message});
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}