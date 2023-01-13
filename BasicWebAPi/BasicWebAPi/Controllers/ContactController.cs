using BasicWebAPi.Data;
using BasicWebAPi.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BasicWebAPi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<CountryDTO>> GetContacts()
        {
            return Ok(ContactsData.contactsList);
        }
        [HttpGet("id")]
        public ContactDTO FilterContact(int id)
        {
            return ContactsData.contactsList.FirstOrDefault(u => u.ContactId == id);

        }


        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public ActionResult<IEnumerable<ContactDTO>> CreateContacts([FromBody] ContactDTO contactDTO)
        {
            
            if (contactDTO == null)
            {
                return BadRequest(contactDTO);
            }


            contactDTO.ContactId = ContactsData.contactsList.OrderByDescending(u => u.ContactId).FirstOrDefault().ContactId + 1;
            ContactsData.contactsList.Add(contactDTO);

            return Ok(contactDTO);
        }


        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}")]
        public IActionResult DeleteContact(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var contact = ContactsData.contactsList.FirstOrDefault(u => u.ContactId == id);
            if (contact == null)
            {
                return NotFound();
            }
            ContactsData.contactsList.Remove(contact);
            return NoContent();
        }



        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id:int}")]
        public IActionResult UpdateContact(int id, [FromBody] ContactDTO contactDTO)
        {
            if (contactDTO == null || id != contactDTO.ContactId)
            {
                return BadRequest(contactDTO);
            }
            var contact = ContactsData.contactsList.FirstOrDefault(u => u.ContactId == id);
            contact.ContactName = contactDTO.ContactName;

            return NoContent();
        }

        
    }
}
