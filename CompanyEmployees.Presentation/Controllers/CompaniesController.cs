﻿using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace CompanyEmployees.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public CompaniesController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [HttpGet]
        public IActionResult GetCompanies()
        {
            var companies = _serviceManager.CompanyService.GetAllCompanies(trackChanges: false);
            return Ok(companies);
        }

        [HttpGet("{companyId:guid}")]
        public IActionResult GetCompany(Guid companyId)
        {
            var company = _serviceManager.CompanyService.GetCompany(companyId, trackChanges: false);
            return Ok(company);
        }
    }
}