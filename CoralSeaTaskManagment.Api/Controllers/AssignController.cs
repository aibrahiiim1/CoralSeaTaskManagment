using AutoMapper;
using CoralSeaTaskManagment.Api.Models.DTO;
using CoralSeaTaskManagment.Data.Data;
using CoralSeaTaskManagment.Model.Models.Domain;
using CoralSeaTaskManagment.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoralSeaTaskManagment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;
        public AssignController(ApplicationDbContext dbContext, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._context = dbContext;
            this._unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //// Get Data From Database (Domain Model)
            var assignDomain = await _unitOfWork.Assign.GetAll(Includeword: "Hotels,Departments,Departments.Hotels");


            // Using Auto Mapper from Domain Model To DTO
            var assignDto = mapper.Map<List<AssignDto>>(assignDomain);
            // Return DTO
            return Ok(assignDto);
        }
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            // Get Data From Database (Domain Model)
            var assignDomain = _unitOfWork.Assign.GetFirstorDefault(predicate: x => x.Id == id, Includeword: "Hotels,Departments,Departments.Hotels");
            if (assignDomain == null)
            {
                return NotFound();
            }

            // Using Auto Mapper
            var assignDto = mapper.Map<AssignDto>(assignDomain);

            // Return DTO
            return Ok(assignDto);
        }
        [HttpPost]
        public IActionResult Create([FromBody] AssignAddDto assignAddDto)
        {
            //// Map or Convert DTO to Domain Model Using Auto Mapper
            var assignDomain = mapper.Map<Assign>(assignAddDto);

            // Use Domain Model To Create 
            _unitOfWork.Assign.Add(assignDomain);
            _unitOfWork.Complete();

            // Map Domain Model Back To DTO Using Auto Mapper
            var assignDto = mapper.Map<AssignDto>(assignDomain);

            return CreatedAtAction(nameof(GetById), new { id = assignDomain.Id }, assignDto);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] AssignUpdateDto assignUpdateDto)
        {
            // check for id is excest or not
            var assignDomain = _unitOfWork.Assign.GetFirstorDefault(predicate: x => x.Id == id, Includeword: "Departments,Hotels");
            if (assignDomain == null)
            {
                return NotFound();
            }

            // Map DTO to Domain Model Using Normal Manual Mapping
            assignDomain.Name = assignUpdateDto.Name;
            assignDomain.NameAr = assignUpdateDto.NameAr;


            // Save and update
            _unitOfWork.Complete();

            // Convert Domain Model To DTO Using Auto Mapper
            var assignDto = mapper.Map<AssignDto>(assignDomain);

            return Ok(assignDto);
        }
        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            // check for id is excest or not
            var assignDomain = _unitOfWork.Assign.GetFirstorDefault(predicate: x => x.Id == id, Includeword: "Hotels,Departments,Departments.Hotels");
            if (assignDomain == null)
            {
                return NotFound();
            }
            // Delete Hotel
            _unitOfWork.Assign.Remove(assignDomain);
            _unitOfWork.Complete();

            // you can return deleted hotel value 
            // Using Auto Mapper
            var assignDto = mapper.Map<AssignDto>(assignDomain);
            return Ok(assignDto);
        }
    }
}
