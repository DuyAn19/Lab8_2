using AppView.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using System.Text;

namespace AppView.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _client;

        public HomeController()
        {
            _client = new HttpClient { BaseAddress = new Uri("https://localhost:7244/api/Calculator/") };
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CalculateCompoundInterest(double money, int n, double p)
        {
            var response = await _client.GetAsync($"calculate-compound-interest?money={money}&n={n}&p={p}");
            if (response.IsSuccessStatusCode)
            {
                response.EnsureSuccessStatusCode();
                var interest = await response.Content.ReadAsStringAsync();
                ViewBag.BMIResult = interest;
                return View("Index");
            }
            else
            {
                ModelState.AddModelError("", "Failed to calculate compound interest.");
            }
            return View("Index");
        }


        [HttpPost]
        public async Task<ActionResult> FindThirdLargestNumber(int[] numbers)
        {
            var jsonContent = JsonConvert.SerializeObject(numbers);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("find-third-largest", content);

            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            ViewBag.ThirdLargestLargestResult = responseBody;

            return View("Index");
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
