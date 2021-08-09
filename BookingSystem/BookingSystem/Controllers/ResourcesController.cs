using BookingSystem.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
namespace BookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourcesController : Controller
    {
        private readonly ResourcesContext _context;
        public ResourcesController(ResourcesContext context)
        {
            _context = context;
        }
        [Route("GetAllResources")]
        [HttpGet]
        public async Task<IActionResult> GetAllResources()
        {
            var resources = await _context.Resources.ToListAsync();
            return Ok(resources);
        }
        [Route("GetResourceByName")]
        [HttpGet]
        public async Task<IActionResult> GetResourceByName(string name)
        {
            var resource = await _context.Resources.FirstOrDefaultAsync(x => x.Name == name);
            return Ok(resource);
        }
        [Route("CreateResource")]
        [HttpPost]
        public async Task<IActionResult> CreateResource(Resource data)
        {
            if (ModelState.IsValid)
            {
                    await _context.Resources.AddAsync(data);
                    await _context.SaveChangesAsync();
            }
            return Ok("Resource created Sucessfully");
        }
        
    }
}
