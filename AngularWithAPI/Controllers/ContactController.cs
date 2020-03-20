using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularWithAPI.Models;
using AngularWithAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngularWithAPI.Controllers
{
    [Route("api/Persone")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository repository;
        private readonly IPersoneRepository personeRepository;

        public ContactController(IContactRepository repository, IPersoneRepository personeRepository)
        {
            this.repository = repository;
            this.personeRepository = personeRepository;
        }

        [Route("{personeId}/Contact")]
        [HttpGet]
        public IActionResult Get(int personeId)
        {
            var result = repository.Persones(personeId);
            if (result == null) { return NotFound(); }
            return Ok(result);
        }

        [Route("{personeId}/Contact/{contactId}")]
        [HttpGet]
        public IActionResult Get(int personeId,int contactId)
        {
            var result = repository.GetPersoneById(personeId, contactId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [Route("{personeId}/Contact")]
        [HttpPost]
        public IActionResult AddContactToPersone(int personeId,[FromBody] Contact contact)
        {
            if (contact == null)
            {
                return BadRequest(contact);
            }
            var findPersone = personeRepository.PersoneById(personeId);

            if(findPersone == null) { return NotFound(); }

            repository.AddContact(personeId, contact);

            return NoContent();
        }

        [Route("{personeId}/Contact/{contactId}")]
        [HttpPut]
        public IActionResult put(int personeId,int contactId,[FromBody] Contact newContact)
        {
            var findPersone = repository.GetPersoneById(personeId, contactId);

            if (findPersone == null) { return NotFound(); }

            if (newContact == null) { return BadRequest(); }

            if (ModelState.IsValid) {

            
                repository.UpdateContact(personeId, contactId, newContact);

            }
            return NoContent();
        }

        [Route("{personeId}/Contact/{contactId}")]
        [HttpDelete]
        public IActionResult Delete(int personeId, int contactId)
        {
            var findPersone = repository.GetPersoneById(personeId, contactId);
            if (findPersone != null) 
            {
                repository.DeleteContact(personeId, contactId);
                return NoContent();
            
            }

            return NotFound();

        }

    }
}