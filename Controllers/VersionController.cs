using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using VostrikovaLab.Models;

namespace VostrikovaLab.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class VersionController : Controller
    {
        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            var versionInfo = new VersionModel
            {
                Company = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyCompanyAttribute>().Company,
                Product = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyProductAttribute>().Product,
                ProductVersion = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion
            };

            return Ok(versionInfo);
        }
    }
}
