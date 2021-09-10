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
    public class VaccinationPointController : Controller
    {
        private readonly AddressService _addressService;
        private readonly VaccinationPointService _vaccinationPointService;

        public VaccinationPointController(AddressService addressService, VaccinationPointService vaccinationPointService)
        {
            _addressService = addressService;
            _vaccinationPointService = vaccinationPointService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _vaccinationPointService.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Details(int id)
        {
            var dto = await _vaccinationPointService.FindByIdAsync(id);
            return View(dto);
        }
        
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VaccinationPointDTO dto)
        {
            if(!ModelState.IsValid)
                return View();
            await _vaccinationPointService.InsertAsync(dto);
            Response.StatusCode = 200;
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { message = "Id not provided"});
            var obj =  await _vaccinationPointService.FindByIdAsync(id.Value);
            if (obj == null)
                return RedirectToAction(nameof(Error), new { message = "Id not found"});
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _vaccinationPointService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { message = "Id not provided"});
            var obj = await _vaccinationPointService.FindByIdAsync(id.Value);
            if (obj == null)
                return RedirectToAction(nameof(Error), new { message = "Id not found"});
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VaccinationPointDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);
            if (id != dto.Id)
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            try
            {
                await _vaccinationPointService.UpdateAsync(dto);
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