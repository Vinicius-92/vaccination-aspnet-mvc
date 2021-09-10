using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Vaccination.Services;
using Vaccination.Models.DTO;
using Vaccination.Services.Exceptions;
using Vaccination.Models.ViewModel;
using System.Diagnostics;
using System;

namespace Vaccination.Controllers
{
    public class VaccineBatchController : Controller
    {
        private readonly VaccineBatchService _vaccineBatchService;
        private readonly VaccineService _vaccineService;

        public VaccineBatchController(VaccineBatchService vaccineBatchService, VaccineService vaccineService)
        {
            _vaccineBatchService = vaccineBatchService;
            _vaccineService = vaccineService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _vaccineBatchService.FindAllAsync();
            ViewBag.Vaccine = await _vaccineService.FindAllAsync();
            return View(list);
        }

        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var dto = await _vaccineBatchService.FindByIdAsync(id);
            ViewBag.Vaccine = await _vaccineService.FindByIdAsync(dto.VaccineDTO);
            return View(dto);
        }

        [Authorize]
        public async Task<IActionResult> Create()
        {
            ViewBag.Vaccines = await _vaccineBatchService.TypeVaccines();
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _vaccineBatchService.RemoveAsync(id);
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
            var obj = await _vaccineBatchService.FindByIdAsync(id.Value);
            if (obj == null)
                return RedirectToAction(nameof(Error), new { message = "Id not found"});
            ViewBag.Vaccines = await _vaccineBatchService.TypeVaccines();
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VaccineBatchDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);
            if (id != dto.Id)
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            try
            {
                await _vaccineBatchService.UpdateAsync(dto);
                return RedirectToAction(nameof(Index));
            } catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message});
            } 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VaccineBatchDTO dto)
        {
            if(!ModelState.IsValid)
                return View();
            await _vaccineBatchService.InsertAsync(dto);
            Response.StatusCode = 200;
            return RedirectToAction(nameof(Index));
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