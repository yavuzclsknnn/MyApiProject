using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApiProject_Application.Services;
using MyApiProject_Core.Entities;

namespace MyApiProject.Controllers
{

    [Authorize(Roles = "Admin")] // Korumayı geri açtık 
    [Route("api/[controller]")]
    [ApiController]
    public class UrunController : ControllerBase
    {


        private readonly UrunService _service;

        public UrunController(UrunService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<Urun>> GetAll()
        {
             
            var isInRole = User.IsInRole("admin");
            var claims = User.Claims.Select(c => $"{c.Type}: {c.Value}").ToList();

            var list = _service.GetAll();
            return Ok(list);  
        }

         
    }
}
