using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Organization;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/organization")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationRepository _organizationRepo;
        public OrganizationController(IOrganizationRepository organizationRepo)
        {
            _organizationRepo = organizationRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(){
            var organizations = await _organizationRepo.GetAllAsync();
            var organizationsDto =  organizations.Select(s => s.ToOrganizationDto());

            return Ok(organizationsDto);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id){
            var organization = await _organizationRepo.GetByIdAsync(id);
            
            if(organization == null) return NotFound();

            return Ok(organization.ToOrganizationDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrganizationRequestDto organizationDto){
            var organizationModel = organizationDto.ToOrganizationFromCreateDto();
            await _organizationRepo.CreateAsync(organizationModel);

            return CreatedAtAction(nameof(GetById), new { id = organizationModel.Id }, organizationModel.ToOrganizationDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateOrganizationRequestDto organizationDto){
            var organizationModel = await _organizationRepo.UpdateAsync(id, organizationDto);

            if(organizationModel == null) return NotFound();

            return Ok(organizationModel.ToOrganizationDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id){
            var organizationModel = await _organizationRepo.DeleteAsync(id);

            if(organizationModel == null) return NotFound();

            return NoContent();
        }
    }
}