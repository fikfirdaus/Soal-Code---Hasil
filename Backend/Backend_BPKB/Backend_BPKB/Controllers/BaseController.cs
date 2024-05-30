using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Backend_BPKB.Controllers
{
    public class BaseController : Controller
    {
        protected readonly string _connectionString;
        public BaseController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
    }
}
