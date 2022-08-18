using Exchange.ClientApp.ExternalServices.Interfaces;
using Exchange.ClientApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Exchange.ClientApp.Controllers
{
    public class LoginController : Controller
    {
        private IAccessToken _accessToken;
        public LoginController(IAccessToken accessToken)
        {
            _accessToken = accessToken;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginRequest([Bind("UserName,Password")] LoginModel loginModel)
        {
            if (string.IsNullOrEmpty(loginModel.UserName) ||
                string.IsNullOrEmpty(loginModel.Password))
            {
                return RedirectToAction("Index", "Login");
            }
            string token = await _accessToken.GetNewAccessTokenAsync(loginModel.UserName, loginModel.Password);
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Index", "Login");
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            _accessToken.ForgetAccessToken();
            return RedirectToAction("Index", "Home");
        }
    }
}
