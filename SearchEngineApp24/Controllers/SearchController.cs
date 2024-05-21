using Microsoft.AspNetCore.Mvc;
using SearchEngineApp24.Data;

namespace SearchEngineApp24.Controllers
{
    public class SearchController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly GooglePlacesService _googlePlacesService;
        
        public SearchController(ApplicationDbContext context, GooglePlacesService googlePlacesService)
        {
            _context = context;
            _googlePlacesService = googlePlacesService;
        }

       // [HttpPost]
        public async Task<IActionResult> Index(string query)
        {
             
            var newQuery = query + " " + "Pizza Store";
            var localResults = _context.PizzaStores
                .Where(p => p.Name.Contains(query) || p.Address.Contains(query) || p.City.Contains(query))
                .ToList();

            var googleResults = await _googlePlacesService.SearchPlacesAsync(newQuery);

            var results = localResults.Concat(googleResults);

            return View(results);
        }
    }

}
