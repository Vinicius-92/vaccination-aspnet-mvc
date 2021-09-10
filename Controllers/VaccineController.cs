using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Vaccination.Models.DTO;
using Vaccination.Models.ViewModel;
using Vaccination.Services;
using Vaccination.Services.Exceptions;

namespace Vaccination.Controllers
{
    public class VaccineController : Controller
    {

        private readonly VaccineService _vaccineService;

        public VaccineController(VaccineService vaccineService)
        {
            _vaccineService = vaccineService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var list = await _vaccineService.FindAllAsync();
            return View(list);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VaccineDTO dto)
        {
            if(!ModelState.IsValid)
                return View();
            await _vaccineService.InsertAsync(dto);
            Response.StatusCode = 200;
            return RedirectToAction(nameof(Index));
        }
        
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { message = "Id not provided"});
            var obj = await _vaccineService.FindByIdAsync(id.Value);
            if (obj == null)
                return RedirectToAction(nameof(Error), new { message = "Id not found"});
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VaccineDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);
            if (id != dto.Id)
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            try
            {
                await _vaccineService.UpdateAsync(dto);
                return RedirectToAction(nameof(Index));
            } catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message});
            } 
        }

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { message = "Id not provided"});
            var obj = await _vaccineService.FindByIdAsync(id.Value);
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
                await _vaccineService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
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