using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Vaccination.Models.ViewModel;
using Vaccination.Models.DTO;
using Vaccination.Services;
using Vaccination.Services.Exceptions;

namespace Vaccination.Controllers
{
    [Authorize]
    public class AddressController : Controller
    {
        private readonly AddressService _addressService;

        public AddressController(AddressService addressService)
        {
            _addressService = addressService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _addressService.FindAllAsync();
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddressDTO dto)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            await _addressService.InsertAsync(dto);
            Response.StatusCode = 200;
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { message = "Id not provided"});
            var obj = await _addressService.FindByIdAsync(id.Value);
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
                await _addressService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { message = "Id not provided"});
            var obj = await _addressService.FindByIdAsync(id.Value);
            if (obj == null)
                return RedirectToAction(nameof(Error), new { message = "Id not found"});
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AddressDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);
            if (id != dto.Id)
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            try
            {
                await _addressService.UpdateAsync(dto);
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