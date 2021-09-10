using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vaccination.Services;
using Vaccination.Models.DTO;
using Vaccination.Models.ViewModel;

namespace Vaccination.Controllers
{
    [Authorize]
    public class VaccinationRecordController : Controller
    {
        private readonly VaccinationRecordService _vaccinationRecordService;
        private readonly VaccinationPointService _vacinationPointService;
        private readonly VaccineBatchService _vaccineBatchService;
        private readonly PersonService _personService;
        private readonly VaccineService _vaccineService;

        public VaccinationRecordController(VaccinationRecordService vaccinationRecordService, 
                                           VaccinationPointService vacinationPointService, 
                                           VaccineBatchService vaccineBatchService, 
                                           PersonService personService,
                                           VaccineService vaccineService)
        {
            _vaccinationRecordService = vaccinationRecordService;
            _vacinationPointService = vacinationPointService;
            _vaccineBatchService = vaccineBatchService;
            _personService = personService;
            _vaccineService = vaccineService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _vaccinationRecordService.FindAllAsync();
            ViewBag.Person = await _personService.FindAllWitoutCompletedVaccinesAsync();
            return View(list);
        }

        public async Task<IActionResult> CreateRecord(int id)
        {
            var person = await _personService.FindByIdModelAsync(id);
            var intervalBetweenDoses = await _vaccinationRecordService.DosesIntervalConfirmation(id);

            if (person.Records.Count == 0)
            {   
                ViewBag.Points = await _vacinationPointService.FindAllAsync();
                ViewBag.Batch = await _vaccineBatchService.FindAllAsync();
                ViewBag.Vaccine = await _vaccineService.FindAllAsync();
                ViewBag.Person = person;
                ViewBag.Dose = 1;
                return View();
            }
            else
            {
                var personVaccineInterval = intervalBetweenDoses;
                if (personVaccineInterval < person.Records[0].VaccineBatch.Vaccine.IntervalBetweenDoses)
                {
                    return RedirectToAction(nameof(Error), new { message = $"Interval between doses not reached, return in {personVaccineInterval} days"});
                }
                var vaccine = person.Records[0].VaccineBatch.Vaccine;
                ViewBag.Points = await _vacinationPointService.FindAllAsync();
                ViewBag.Batch = await _vaccineBatchService.FindByVaccine(vaccine.Id);
                ViewBag.Vaccine = await _vaccineService.FindByIdAsync(vaccine.Id);
                ViewBag.Person = person;
                ViewBag.Dose = 2;
                return View();
            }
        }
        
        public async Task<IActionResult> CreateFor()
        {
            ViewBag.People = await _personService.FindAllWitoutCompletedVaccinesAsync();
            ViewBag.Vaccines = await _vaccineService.FindAllAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VaccinationRecordDTO Record)
        {
            var person = await _personService.FindByIdModelAsync(Record.PersonId);
            var posology = await _vaccineBatchService.CheckSingleDoseVaccine(Record.VaccineBatchId);

            if (person.Records.Count > 1)
            {
                var dto = new VaccinationRecordDTO();
                dto.Date = DateTime.Now;
                dto.Dose = Record.Dose;
                dto.PersonId = Record.PersonId;
                dto.VaccinationPointId = Record.VaccinationPointId;
                dto.VaccineBatchId = Record.VaccineBatchId;
                dto.VaccinationDoneStatus = true;
                                                
                await _vaccinationRecordService.InsertAsync(dto);
                await _vaccineBatchService.RemoveFromStockAsync(dto.VaccineBatchId);
                Response.StatusCode = 200;
                return RedirectToAction(nameof(Index));
            }
            else if (posology.ToUpper() == "DOUBLE")
            {
                var dto = new VaccinationRecordDTO();
                dto.Date = DateTime.Now;
                dto.Dose = Record.Dose;
                dto.PersonId = Record.PersonId;
                dto.VaccinationPointId = Record.VaccinationPointId;
                dto.VaccineBatchId = Record.VaccineBatchId;
                dto.VaccinationDoneStatus = false;
                                                
                await _vaccinationRecordService.InsertAsync(dto);
                await _vaccineBatchService.RemoveFromStockAsync(dto.VaccineBatchId);
                Response.StatusCode = 200;
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var dto = new VaccinationRecordDTO();
                dto = new VaccinationRecordDTO();
                dto.Date = DateTime.Now;
                dto.Dose = Record.Dose;
                dto.PersonId = Record.PersonId;
                dto.VaccinationPointId = Record.VaccinationPointId;
                dto.VaccineBatchId = Record.VaccineBatchId;
                dto.VaccinationDoneStatus = true;
                                             
            await _vaccinationRecordService.InsertAsync(dto);
            await _vaccineBatchService.RemoveFromStockAsync(dto.VaccineBatchId);
            Response.StatusCode = 200;
            return RedirectToAction(nameof(Index));
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