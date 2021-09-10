using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Vaccination.Models.ViewModel;
using Vaccination.Services;

namespace Vaccination.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PersonService _personService;
        private readonly VaccinationRecordService _vaccinationRecordService;

        public HomeController(ILogger<HomeController> logger,
                              PersonService personService,
                              VaccinationRecordService vaccinationRecordService)
        {
            _logger = logger;
            _personService = personService;
            _vaccinationRecordService = vaccinationRecordService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.AllPeople = await _personService.FindAllAsync();
            ViewBag.NotFullVaccineted = await _personService.FindAllWitoutCompletedVaccinesAsync();
            ViewBag.FullyVaccineted = await _personService.FindAllCompletedVaccinetedAsync();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
