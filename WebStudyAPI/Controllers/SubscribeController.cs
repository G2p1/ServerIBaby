using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStudyAPI.DBContext;

namespace WebStudyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubscribeController : Controller
    {
        private readonly FiendContext _context;
        public SubscribeController(FiendContext context)
        {
            _context = context;
        }
        [HttpPost(Name = "PostSubscribe")]
        [AllowAnonymous]
        public void Post()
        {

        }
    }
}
