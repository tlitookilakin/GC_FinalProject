using FinalProjectBackend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly OpenFoodService openFoodService;

        public TestController(OpenFoodService openFoodService)
        {
            this.openFoodService = openFoodService;
        }

        [HttpGet("{upc}")]
        public async Task<IActionResult> GetByUpc(string upc) 
        { 
            return Ok(await openFoodService.GetByUPC(upc));
        }
    }

    
}
