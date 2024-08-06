using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Certificate;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/certificate")]
    [ApiController]
    public class CertificateController : ControllerBase
    {
        private readonly ICertificateRepository _certificateRepo;

        public CertificateController(ICertificateRepository certificateRepo)
        {
            _certificateRepo = certificateRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? organizer, [FromQuery] string? eventName, [FromQuery] int? eventId, [FromQuery] string? code)
        {
            List<Certificate> certificates;

            if (!string.IsNullOrEmpty(organizer) || !string.IsNullOrEmpty(eventName) || !string.IsNullOrEmpty(code) || eventId != null)
            {
                certificates = await _certificateRepo.GetCertificatesByOrganizerEventCodeAsync(organizer, eventName, code, eventId);
            }
            else
            {
                certificates = await _certificateRepo.GetAllAsync();
            }
            var certificatesDto = certificates.Select(c => c.ToCertificateDto());
            return Ok(certificatesDto);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var certificate = await _certificateRepo.GetByIdAsync(id);

            if (certificate == null) return NotFound();

            return Ok(certificate.ToCertificateDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCertificateRequestDto certificateDto)
        {
            try
            {
                var createdCertificates = await _certificateRepo.CreateMultipleAsync(certificateDto);
                var createdCertificatesDto = createdCertificates.Select(c => c.ToCertificateDto());
                return CreatedAtAction(nameof(GetById), new { id = createdCertificates.First().Id }, createdCertificatesDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("download/{code}")]
        public async Task<IActionResult> DownloadCertificate(string code){
            var pdfBytes = await _certificateRepo.GetCertificatePdfByCodeAsync(code);

            if(pdfBytes == null){
                return NotFound();
            }

            return File(pdfBytes, "application/pdf", $"{code}.pdf");
        }

        [HttpPost("download-zip")]
        public async Task<IActionResult> DownloadCertificatesAsZip([FromBody] List<string> certificateCodes){
            if(certificateCodes == null || certificateCodes.Count == 0){
                return BadRequest("No certificate codes provided");
            }

            var zipBytes = await _certificateRepo.GenerateZipFromCertificateCodes(certificateCodes);

            if(zipBytes == null || zipBytes.Length == 0){
                return NotFound("No certificates found or PDF generation failed");
            }

            return File(zipBytes, "application/zip", "certificates.zip");
        }


    }
}