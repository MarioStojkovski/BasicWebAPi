using BasicWebAPi.Data;
using BasicWebAPi.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BasicWebAPi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<CompanyDTO>> GetCompanies()
        {
            return Ok(CompaniesData.companiesList);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public ActionResult<CompanyDTO> CreateCompany([FromBody]CompanyDTO companyDTO)
        {
            if(CompaniesData.companiesList.FirstOrDefault(u => u.CompanyName.ToLower() == companyDTO.CompanyName.ToLower()) != null){
                ModelState.AddModelError("", "The company already exists!");
                return BadRequest(ModelState);
            }
            if(companyDTO == null)
            {
                return BadRequest(companyDTO);
            }
           
            companyDTO.CompanyId = CompaniesData.companiesList.OrderByDescending(u => u.CompanyId).FirstOrDefault().CompanyId + 1 ;
            CompaniesData.companiesList.Add(companyDTO);

            return Ok(companyDTO);
        }


        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}")]
        public IActionResult DeleteCompany(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var company = CompaniesData.companiesList.FirstOrDefault(u =>u.CompanyId == id);
            if(company == null)
            {
                return NotFound();
            }
            CompaniesData.companiesList.Remove(company);
            return NoContent();
        }


        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id:int}")]
        public IActionResult UpdateCompany(int id, [FromBody]CompanyDTO companyDTO)
        {
            if(companyDTO == null || id != companyDTO.CompanyId)
            {
                return BadRequest(companyDTO);
            }
            var company = CompaniesData.companiesList.FirstOrDefault(u => u.CompanyId == id);
            company.CompanyName = companyDTO.CompanyName;

            return NoContent();
        }

    }


    
}
