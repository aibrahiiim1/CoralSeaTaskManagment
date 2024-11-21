using AutoMapper;
using CoralSeaTaskManagment.Api.Models.DTO;
using CoralSeaTaskManagment.Api.Response;
using CoralSeaTaskManagment.Data.Data;
using CoralSeaTaskManagment.Model.Models.Domain;
using CoralSeaTaskManagment.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoralSeaTaskManagment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;
        public DepartmentController(ApplicationDbContext dbContext, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._context = dbContext;
            this._unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        // Get All
        // Get: https://localhost:portnumber/api/departments
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //// Get Data From Database (Domain Model)
            var departmentDomain = await _unitOfWork.Department.GetAll(Includeword:"Hotels");


            // Using Auto Mapper from Domain Model To DTO
            var departmentDto = mapper.Map<List<DepartmentDto>>(departmentDomain);
            // Return DTO
            return Ok(departmentDto);
        }
        // Get single department by ID
        // Get: https://localhost:portnumber/api/departments/{id}
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            // Get Data From Database (Domain Model)
            var departmentDomain = _unitOfWork.Department.GetFirstorDefault(predicate:x => x.Id == id,Includeword:"Hotels");
            if (departmentDomain == null)
            {
                return NotFound();
            }

            // Using Auto Mapper
            var departmentDto = mapper.Map<DepartmentDto>(departmentDomain);

            // Return DTO
            return Ok(departmentDto);
        }
        // Post to Create New Department
        // Post: https://localhost:portnumber/api/Departments
        [HttpPost("Create")]
        public IActionResult Create([FromBody] DepartmentAddDto departmentAddDto)
        {
            //// Map or Convert DTO to Domain Model Using Auto Mapper
            var departmentDomain = mapper.Map<Department>(departmentAddDto);

            // Use Domain Model To Create 
            _unitOfWork.Department.Add(departmentDomain);
            _unitOfWork.Complete();

            // Map Domain Model Back To DTO Using Auto Mapper
            var departmentDto = mapper.Map<DepartmentDto>(departmentDomain);

            return CreatedAtAction(nameof(GetById), new { id = departmentDomain.Id }, departmentDto);
        }
        // Update department
        // Put: https://localhost:portnumber/api/department/{id}
        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] DepartmentUpdateDto departmentUpdateDto)
        {
            // check for id is excest or not
            var departmentDomain = _unitOfWork.Department.GetFirstorDefault(predicate:x => x.Id == id, Includeword: "Hotels");
            if (departmentDomain == null)
            {
                return NotFound();
            }

            // Map DTO to Domain Model Using Normal Manual Mapping
            departmentDomain.Name = departmentUpdateDto.Name;
            departmentDomain.NameAr = departmentUpdateDto.NameAr;
            departmentDomain.HotelId = departmentUpdateDto.HotelId;

            // Save and update
            _unitOfWork.Complete();

            // Convert Domain Model To DTO Using Auto Mapper
            var departmentDto = mapper.Map<DepartmentDto>(departmentDomain);

            return Ok(departmentDto);
        }
        // Delete department from Database
        // Delete:https://localhost:portnumber/api/department/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            // check for id is excest or not
            var departmentDomain = _unitOfWork.Department.GetFirstorDefault(x => x.Id == id);
            if (departmentDomain == null)
            {
                return NotFound();
            }
            // Delete Hotel
            _unitOfWork.Department.Remove(departmentDomain);
            _unitOfWork.Complete();

            // you can return deleted hotel value 
            // Using Auto Mapper
            var departmentDto = mapper.Map<DepartmentDto>(departmentDomain);
            return Ok(departmentDto);
        }
    }
}
