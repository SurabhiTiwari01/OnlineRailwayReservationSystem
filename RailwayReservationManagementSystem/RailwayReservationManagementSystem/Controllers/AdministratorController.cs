using Microsoft.AspNetCore.Mvc;
using RailwayReservationManagementSystem.Models;
using RailwayReservationManagementSystem.Interfaces;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RailwayReservationManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministratorController : ControllerBase
    {
        private readonly IAdministratorRepo _administratorRepository;

        public AdministratorController(IAdministratorRepo administratorRepository)
        {
            _administratorRepository = administratorRepository;
        }

        // GET: api/Administrator/{username}
        [HttpGet("{username}")]
        public async Task<ActionResult<Administrator>> GetAdministratorByUsername(string username)
        {
            var administrator = await _administratorRepository.GetAdministratorByUsernameAsync(username);
            if (administrator == null)
            {
                return NotFound();
            }
            return Ok(administrator);
        }

        // POST: api/Administrator
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Administrator>> CreateAdministrator(Administrator administrator)
        {
            await _administratorRepository.AddAdministratorAsync(administrator);
            return CreatedAtAction(nameof(GetAdministratorByUsername), new { username = administrator.Username }, administrator);
        }

        // PUT: api/Administrator/{id}
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateAdministrator(int id, Administrator administrator)
        {
            if (id != administrator.AdminId)
            {
                return BadRequest();
            }

            await _administratorRepository.UpdateAdministratorAsync(administrator);
            return NoContent();
        }

    }
}
