using Microsoft.AspNetCore.Mvc;

using MSLab.Server.Models;
using MSLab.Server.Services;

using System;

namespace MSLab.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ConcertsController : ControllerBase
    {
        private readonly IConcertStore _concertStore;

        public ConcertsController(IConcertStore concertStore)
        {
            _concertStore = concertStore ?? throw new ArgumentNullException(nameof(concertStore));
        }

        [HttpGet]
        public IActionResult GetConcerts([FromQuery] string filter)
        {
            return new JsonResult(_concertStore.GetConcerts(filter));
        }

        [HttpGet("{id}")]
        public IActionResult GetConcertById([FromRoute] int id)
        {
            try
            {
                var concert = _concertStore.GetConcertById(id);
                return new JsonResult(concert);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult CreateConcert([FromBody] ConcertDetailedData newConcert)
        {
            _concertStore.CreateNewConcert(newConcert);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateConcert([FromRoute] int id, [FromBody] ConcertDetailedData modifiedConcert)
        {
            try
            {
                _concertStore.UpdateConcert(id, modifiedConcert);
                return Ok();
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteConcertById([FromRoute] int id)
        {
            try
            {
                _concertStore.DeleteConcert(id);
                return Ok();
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }
    }
}