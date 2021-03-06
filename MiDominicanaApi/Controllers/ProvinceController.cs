using Microsoft.AspNetCore.Mvc;
using MiDominicanaApi.Models;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
//using System.Text.Json;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MiDominicanaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvinceController : ControllerBase
    {
        private readonly IHttpContextAccessor _env;
        public ProvinceController(IHttpContextAccessor env)
        {
            _env = env;
        }
        // GET: api/<ProvinceController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string fileName =  String.Concat($"{Directory.GetCurrentDirectory()}",@"\Data\Province.json").Replace(@"\Controllers", "");
            await using FileStream openStream = System.IO.File.OpenRead(fileName);
            List<Province> provinces = JsonConvert.DeserializeObject<List<Province>>(System.IO.File.ReadAllText(fileName));
            var scheme = _env.HttpContext.Request.Scheme;
            var host = _env.HttpContext.Request.Host.Value;
            provinces
                .Where(x => x.Code > 0)
                .ToList()
                .ForEach(y => y.ImagePath = string.Concat(scheme, "://", host, $"/provinceImage/{y.ImagePath}"));
            
            return Ok(provinces);
        }

        // GET api/<ProvinceController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

       
    }
}
