using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using TSense.Models;
using TSense_API.Models;

namespace TSense.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Tweet()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Tweeted(string tweetLink)
        {
            using (var client = new HttpClient())
            {
                Twitter twitter = new Twitter(tweetLink);
                var myJson = JsonConvert.SerializeObject(twitter);
                client.BaseAddress = new Uri("http://localhost:57797/api/tweet");
                var responseTask = client.GetAsync("tweet");
                responseTask.Wait();

                var result = responseTask.Result;
            }

                ViewData["TwitterLink"] = tweetLink;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
