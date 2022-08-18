using Exchange.ClientApp.ExternalServices.Interfaces;
using Exchange.ClientApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;

namespace Exchange.ClientApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IExchangeClient _exchangeClient;

        public HomeController(ILogger<HomeController> logger,
                             IExchangeClient exchangeClient)
        {
            _logger = logger;
            _exchangeClient = exchangeClient;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var container = new DataContainer();
            var responce = await _exchangeClient.GetHistoryAsync();
            if (responce.ErrorCode == (int)HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Index", "login");
            }

            if (!responce.IsSuccess)
            {
                return RedirectToAction("ErrorResponce", new ResponseModel() { IsSuccess = responce.IsSuccess, ErrorCode = responce.ErrorCode, ErrorMessage = responce.ErrorMessage });
            }

            container.HistoryListModel = responce.Histories;

            return View(container);
        }


        [HttpPost]
        public async Task<IActionResult> Search(ConvertModel createCurrencyDto)
        {
            var result = await _exchangeClient.ConvertAsync(createCurrencyDto);
            if (result.ErrorCode == (int)HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Index", "login");
            }

            if (!result.IsSuccess)
            {
                return RedirectToAction("ErrorResponce", new ResponseModel() { IsSuccess = result.IsSuccess, ErrorCode = result.ErrorCode, ErrorMessage = result.ErrorMessage });
            }

            return RedirectToAction("Index");

        }
     
        public IActionResult ErrorResponce(ResponseModel model)
        {
            return View(model);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}