using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vaccination.Services;

namespace Vaccination.Controllers
{
    public class ReportController : Controller
    {
        private readonly VaccinationRecordService _vaccinationRecordService;
        private readonly PersonService _personService;
        private readonly VaccineBatchService _vaccineBatchService;

        public ReportController(VaccinationRecordService vaccinationRecordService,
                                PersonService personService,
                                VaccineBatchService vaccineBatchService)
        {
            _vaccinationRecordService = vaccinationRecordService;
            _personService = personService;
            _vaccineBatchService = vaccineBatchService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ListForSecondDose()
        {
            var list = await _personService.FindAllWitoutCompletedVaccinesAsync();
            return View(list);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> BatchesToExpire()
        {
            var list = await _vaccineBatchService.FindBatchesToExpireAsync();
            return View(list);
        }
    }
}