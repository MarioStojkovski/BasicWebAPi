using BasicWebAPi.Data;
using BasicWebAPi.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BasicWebAPi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountryController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<CountryDTO>> GetCountries()
        {
            return Ok(CountriesData.countriesList);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public ActionResult<CountryDTO> CreateContry([FromBody]CountryDTO countryDTO)
        {
            if (CountriesData.countriesList.FirstOrDefault(u => u.CountryName.ToLower() == countryDTO.CountryName.ToLower()) != null)
            {
                ModelState.AddModelError("", "The Country already exists!");
                return BadRequest(ModelState);
            }
            if (countryDTO == null)
            {
                return BadRequest(countryDTO);
            }

            countryDTO.CountryId = CountriesData.countriesList.OrderByDescending(u => u.CountryId).FirstOrDefault().CountryId + 1;
            CountriesData.countriesList.Add(countryDTO);

            return Ok(countryDTO);
        }


        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}")]
        public IActionResult DeleteCountry(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var country = CountriesData.countriesList.FirstOrDefault(u => u.CountryId == id);
            if (country == null)
            {
                return NotFound();
            }
            CountriesData.countriesList.Remove(country);
            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id:int}")]
        public IActionResult UpdateCountry(int id, [FromBody] CountryDTO countryDTO)
        {
            if (countryDTO == null || id != countryDTO.CountryId)
            {
                return BadRequest(countryDTO);
            }
            var country = CountriesData.countriesList.FirstOrDefault(u => u.CountryId == id);
            country.CountryName = countryDTO.CountryName;

            return NoContent();
        }


    }
}
