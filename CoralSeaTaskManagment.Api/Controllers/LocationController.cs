using AutoMapper;
using CoralSeaTaskManagment.Api.Models.DTO;
using CoralSeaTaskManagment.Data.Data;
using CoralSeaTaskManagment.Model.Models.Domain;
using CoralSeaTaskManagment.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoralSeaTaskManagment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;
        public LocationController(ApplicationDbContext dbContext, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._context = dbContext;
            this._unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        // Get All
        // Get: https://localhost:portnumber/api/hotels
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //// Get Data From Database (Domain Model)
            var locationDomain =await _unitOfWork.Location.GetAll(Includeword: "Hotels,Departments,Departments.Hotels");


            // Using Auto Mapper from Domain Model To DTO
            var locationDto = mapper.Map<List<LocationDto>>(locationDomain);

            // Return DTO
            return Ok(locationDto);
        }
        // Get single department by ID
        // Get: https://localhost:portnumber/api/departments/{id}
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            // Get Data From Database (Domain Model)
            var locationDomain = _unitOfWork.Location.GetFirstorDefault(predicate: x => x.Id == id, Includeword: "Hotels,Departments,Departments.Hotels");
            if (locationDomain == null)
            {
                return NotFound();
            }

            // Using Auto Mapper
            var locationDto = mapper.Map<LocationDto>(locationDomain);

            // Return DTO
            return Ok(locationDto);
        }
        // Post to Create New Location
        // Post: https://localhost:portnumber/api/Locations
        [HttpPost("Create")]
        public IActionResult Create([FromBody] LocationAddDto locationAddDto)
        {
            //// Map or Convert DTO to Domain Model Using Auto Mapper
            var locationDomain = mapper.Map<Location>(locationAddDto);

            // Use Domain Model To Create 
            _unitOfWork.Location.Add(locationDomain);
            _unitOfWork.Complete();

            // Map Domain Model Back To DTO Using Auto Mapper
            var locationDto = mapper.Map<LocationDto>(locationDomain);

            return CreatedAtAction(nameof(GetById), new { id = locationDomain.Id }, locationDto);
        }
        // Update department
        // Put: https://localhost:portnumber/api/department/{id}
        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] LocationUpdateDto departmentUpdateDto)
        {
            // check for id is excest or not
            var locationDomain = _unitOfWork.Location.GetFirstorDefault(x => x.Id == id);
            if (locationDomain == null)
            {
                return NotFound();
            }

            // Map DTO to Domain Model Using Normal Manual Mapping
            locationDomain.Name = departmentUpdateDto.Name;
            locationDomain.NameAr = departmentUpdateDto.NameAr;
            locationDomain.HotelId = departmentUpdateDto.HotelId;
            locationDomain.DepartmentId = departmentUpdateDto.DepartmentId;

            // Save and update
            _unitOfWork.Complete();

            // Convert Domain Model To DTO Using Auto Mapper
            var locationDto = mapper.Map<LocationDto>(locationDomain);

            return Ok(locationDto);
        }
        // Delete department from Database
        // Delete:https://localhost:portnumber/api/department/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            // check for id is excest or not
            var locationDomain = _unitOfWork.Location.GetFirstorDefault(x => x.Id == id);
            if (locationDomain == null)
            {
                return NotFound();
            }
            // Delete Hotel
            _unitOfWork.Location.Remove(locationDomain);
            _unitOfWork.Complete();

            // you can return deleted hotel value 
            // Using Auto Mapper
            var locationDto = mapper.Map<LocationDto>(locationDomain);
            return Ok(locationDto);
        }
        [HttpGet("GetByDep/{id}")]
        public async Task<IActionResult> GetByDepId([FromRoute] int id)
        {
            // Get Data From Database (Domain Model)
            var locationDomain =await _unitOfWork.Location.GetAll(predicate: x => x.DepartmentId == id, Includeword: "Hotels,Departments,Departments.Hotels");
            if (locationDomain == null)
            {
                return NotFound();
            }

            // Using Auto Mapper
            var locationDto =  mapper.Map< IEnumerable<LocationDto>>(locationDomain);

            // Return DTO
            return Ok(locationDto);
        }
    }
}
