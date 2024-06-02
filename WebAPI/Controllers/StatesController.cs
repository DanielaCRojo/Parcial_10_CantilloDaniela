using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.DAL.Entities;
using WebAPI.Domain.Interfaces;
using WebAPI.Domain.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatesController : Controller

    {
        private readonly IStatesService _statesService;

        public StatesController(IStatesService statesService)
        {
            _statesService = statesService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateState([FromBody] CreateStateRequest request)
        {
            try
            {
                var newState = await _statesService.CreateState(request.CountryId, request.Name);
                return CreatedAtAction(nameof(CreateState), new { id = newState.Id }, newState);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet, ActionName("Get")]
        [Route("GetStatesByIdCountry")]

        public async Task<ActionResult<List<State>>> GetStateByCountryIdAsync(Guid countryId)
        {
            var listStates = await _statesService.GetStateByCountryIdAsync(countryId);
            return Ok(listStates);
        }
        [HttpGet, ActionName("Get")]
        [Route("GetStatesByName")]

        public async Task<ActionResult<State>> GetStateByName(string name)
        {
            var State = await _statesService.GetStateByName(name);
            return State;
        }

        [HttpPut, ActionName("Edit")]
        [Route("EditState")]
        public async Task<ActionResult<Country>> EditState(Guid id)
        {
            try
            {
                var editedState = await _statesService.EditState(id);
                if (editedState == null) return NotFound();
                return Ok(editedState);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }



        [HttpDelete, ActionName("Delete")]
        [Route("DeleteState")]
        public async Task<ActionResult<Country>> DeleteTaskAsync(Guid id)
        {
            try
            {
                if (id == null) return BadRequest();

                var deletedState = await _statesService.DeleteTaskAsync(id);

                if (deletedState == null) return NotFound();

                return Ok("Borrado satisfactoriamente");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }


        }

    }
}
public class CreateStateRequest
{
    public Guid CountryId { get; set; }
    public string Name { get; set; }
}



