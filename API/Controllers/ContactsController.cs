using System;
using Contracts;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[contacts]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly ILoggerManager _logger;

        public ContactsController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;

        }

        [HttpGet]
        public IActionResult GetAllContacts()
        {
            try
            {
                var contacts = _repository.Contact.GetAllContactsAsync();

                _logger.LogInfo($"Returned all contacts from database.");

                return Ok(contacts);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllContactsAsync action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "ContactById")]
        public IActionResult GetContactById(int contactId)
        {
            try
            {
                var contact = _repository.Contact.GetContactByIdAsync(contactId);

                if (contact == null)
                {
                    _logger.LogError($"Contact with id: {contactId}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned contact with id: {contactId}");
                    return Ok(contact);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetContactByIdAsync action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult AddContact([FromBody]PhoneBook contact)
        {
            try
            {
                if (contact== null)
                {
                    _logger.LogError("Contact object sent from client is null.");
                    return BadRequest("Contact object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid contact object sent from client.");
                    return BadRequest("Invalid model object");
                }

                _repository.Contact.AddContact(contact);

                return CreatedAtRoute("ContactById", new { id = contact.Id }, contact);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside AddContact action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateContact([FromBody]PhoneBook contact)

        {
            try
            {
                if (contact == null)
                {
                    _logger.LogError("Contact object sent from client is null.");
                    return BadRequest("Contact object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid contact object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var dbContact = _repository.Contact.GetContactByIdAsync(contact.Id);
                if (dbContact == null)
                {
                    _logger.LogError($"Contact with id: {contact}, hasn't been found in db.");
                    return NotFound();
                }
                
                _repository.Contact.UpdateContact(contact);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateContact action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteContact(PhoneBook contact,int contactId)
        {
            try
            {
                var dbContact = _repository.Contact.GetContactByIdAsync(contactId);
                if (dbContact == null)
                {
                    _logger.LogError($"Employee with id: {contactId}, hasn't been found in db.");
                    return NotFound();
                }
                if (contactId != 0)
                {
                    _repository.Contact.DeleteContact(contact);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteContact action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
            return Ok();
        }
    }
}