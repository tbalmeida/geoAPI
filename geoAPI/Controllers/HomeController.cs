using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using geoAPI.App.Repositories.Interfaces;
using geoAPI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace geoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<ActionResult<CompanyVM>> Create([FromBody] CompanyCreateVM data)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data");

            try
            {
                var result = await _companyRepository.Create(data);
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<CompanyVM>>> GetAll()
        {
            try
            {
                var results = await _companyRepository.GetAll();
                return Ok(results);
            }
            catch
            {
                return StatusCode(500);

            }
        }
    }

}
