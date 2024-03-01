using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComercialHermanosCastro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        // POST: DefaultController/Create
        [HttpGet]
        public string Get ()
        {
            return "Aplicacion corriendo...";
        }
    }
}
