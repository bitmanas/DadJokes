using ConsumeWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace ConsumeWebApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            JokeViewModel data = await GetJoksData(1);           
            if (data!=null)
            {
                return View(data.body);
            }
            else
            {
                //no data
                data = new JokeViewModel();
                data.success = true;
                data.body = new List<JokeModel>().ToArray();
                return View(data.body);
            }
        }

        private async Task<JokeViewModel> GetJoksData(int count = 1)
        {
            if (count < 1)
            {
                count = 1;
            }
            if (count > 5)
            {
                count = 5;
            }

            var jokes = new JokeViewModel();
            var client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://dad-jokes.p.rapidapi.com/random/joke?count=" + count),
                Headers =
                    {
                        { "X-RapidAPI-Key", "1211a6cfc0msh4dca38f6f625161p1fd9bfjsn97f21995c1c7" },
                        { "X-RapidAPI-Host", "dad-jokes.p.rapidapi.com" },
                    },
            };

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                jokes = JsonConvert.DeserializeObject<JokeViewModel>(body);
            }

            return jokes;
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