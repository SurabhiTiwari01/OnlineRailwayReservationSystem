using Microsoft.AspNetCore.Mvc;
using RailwayReservationManagementSystem.Interfaces;
using RailwayReservationManagementSystem.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RailwayReservationManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainController : ControllerBase
    {
        // GET: api/<TrainDetailsController>

        private readonly ITrainRepo _trainRepository;

        public TrainController(ITrainRepo trainRepository)
        {
            _trainRepository = trainRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTrains()
        {
            var trains = await _trainRepository.GetAllTrainsAsync();
            return Ok(trains);
        }

        // GET: api/Train/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Train>> GetTrainById(int id)
        {
            var train = await _trainRepository.GetTrainByIdAsync(id);
            if (train == null)
            {
                return NotFound();
            }
            return Ok(train);
        }


        // POST: api/Train
        [HttpPost]
        public async Task<ActionResult<Train>> CreateTrain(Train train)
        {
            await _trainRepository.AddTrainAsync(train);
            return CreatedAtAction(nameof(GetTrainById), new { id = train.TrainId }, train);
        }



        // PUT: api/Train/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTrain(int id, Train train)
        {
            if (id != train.TrainId)
            {
                return BadRequest();
            }

            await _trainRepository.UpdateTrainAsync(train);
            return NoContent();
        }


        // DELETE: api/Train/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrain(int id)
        {
            await _trainRepository.DeleteTrainAsync(id);
            return NoContent();
        }
    


}
}
