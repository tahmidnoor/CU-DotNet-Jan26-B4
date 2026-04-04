using Microsoft.AspNetCore.Mvc;
using Vagabond.Service.Models;
using Vagabond.Service.Repositories;

namespace Vagabond.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DestinationsController : ControllerBase
    {
        private readonly IDestinationRepository _repository;

        public DestinationsController(IDestinationRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _repository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _repository.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(Destination destination)
        {
            await _repository.AddAsync(destination);
            return Ok(destination);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Destination destination)
        {
            destination.Id = id;
            await _repository.UpdateAsync(destination);
            return Ok(destination);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return Ok();
        }
    }
}