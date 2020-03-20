using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularWithAPI.Models;
using AngularWithAPI.Pagination;
using AngularWithAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AngularWithAPI.Controllers
{
    [Route("api/Persone")]
    [ApiController]
    public class PersoneController : ControllerBase
    {
        private readonly IPersoneRepository repository;

        public PersoneController(IPersoneRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult GetAllPersone(int PageNumber=1, int pageSize = 3)
        {
            var result = repository.GetPersones(PageNumber, pageSize);
          
            var metadataPagination = new
            {
                result.TotalCount,
                result.PageSize,
                result.PageNumber,
                result.TotalPages,
                result.HasNext,
                result.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadataPagination));

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id, bool includeContact) 
        {
            var persone = repository.AllPersoneWithContact(id, includeContact);
            if (persone == null){return NotFound();}
            return Ok(persone);
        }   
        
        [HttpPost]
        public IActionResult Post([FromBody] Persone persone)
        {
            if (persone == null) { return BadRequest(); }

            if(!ModelState.IsValid)
            {
                return BadRequest(persone);
            }
            repository.AddPersone(persone);
            return CreatedAtAction("GetById", new { id=persone.Id },persone);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id,[FromBody] Persone persone)
        {
            if(persone==null) { return BadRequest(); }

            Persone personeExist = repository.PersoneById(id);

            if (personeExist == null) { return NotFound(); }

            repository.UpdatePersone(id, persone);

            return CreatedAtAction("GetById", new { id = personeExist.Id }, personeExist);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var personeDeleted = repository.PersoneById(id);

            if(personeDeleted==null) { return NotFound(); }

            repository.DeletePersone(personeDeleted);

            return NoContent();
        }
    }
}
