using BizLand.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BizLand.UI.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();
            HomeViewModel homeViewModel = new HomeViewModel();


            var categories = await client.GetFromJsonAsync<List<CategoryGetViewModel>>("https://localhost:7104/api/Categories");
            var features = await client.GetFromJsonAsync<List<FeatureGetViewModel>>("https://localhost:7104/api/Features");
            var workers = await client.GetFromJsonAsync<List<WorkerGetViewModel>>("https://localhost:7104/api/Workers");
            var portfolios = await client.GetFromJsonAsync<List<PortfolioGetViewModel>>("https://localhost:7104/api/Portfolios");

            if (categories != null && features != null && workers != null && portfolios != null)
            {
                homeViewModel = new HomeViewModel()
                {
                    Categories = categories,
                    Features = features,
                    Workers = workers,
                    Portfolios = portfolios,
                };
            }


            return View(homeViewModel);
        }
    }
}