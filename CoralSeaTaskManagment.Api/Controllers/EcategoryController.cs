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
    public class EcategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;
        public EcategoryController(ApplicationDbContext dbContext, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._context = dbContext;
            this._unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //// Get Data From Database (Domain Model)
            var ecategoryDomain = await _unitOfWork.Ecategory.GetAll(Includeword: "Hotels");


            // Using Auto Mapper from Domain Model To DTO
            var ecategoryDto = mapper.Map<List<EcategoryDto>>(ecategoryDomain);
            // Return DTO
            return Ok(ecategoryDto);
        }
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            // Get Data From Database (Domain Model)
            var ecategoryDomain = _unitOfWork.Ecategory.GetFirstorDefault(predicate: x => x.Id == id, Includeword: "Hotels");
            if (ecategoryDomain == null)
            {
                return NotFound();
            }

            // Using Auto Mapper
            var ecategoryDto = mapper.Map<EcategoryDto>(ecategoryDomain);

            // Return DTO
            return Ok(ecategoryDto);
        }
        [HttpPost]
        public IActionResult Create([FromBody] EcategoryAddDto ecategoryAddDto)
        {
            //// Map or Convert DTO to Domain Model Using Auto Mapper
            var ecategoryDomain = mapper.Map<Ecategory>(ecategoryAddDto);

            // Use Domain Model To Create 
            _unitOfWork.Ecategory.Add(ecategoryDomain);
            _unitOfWork.Complete();

            // Map Domain Model Back To DTO Using Auto Mapper
            var ecategoryDto = mapper.Map<EcategoryDto>(ecategoryDomain);

            return CreatedAtAction(nameof(GetById), new { id = ecategoryDomain.Id }, ecategoryDto);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] EcategoryUpdateDto ecategoryUpdateDto)
        {
            // check for id is excest or not
            var ecategoryDomain = _unitOfWork.Ecategory.GetFirstorDefault(predicate: x => x.Id == id, Includeword: "Hotels");
            if (ecategoryDomain == null)
            {
                return NotFound();
            }

            // Map DTO to Domain Model Using Normal Manual Mapping
            ecategoryDomain.Name = ecategoryUpdateDto.Name;
            ecategoryDomain.HotelId = ecategoryUpdateDto.HotelId;

            // Save and update
            _unitOfWork.Complete();

            // Convert Domain Model To DTO Using Auto Mapper
            var ecategoryDto = mapper.Map<EcategoryDto>(ecategoryDomain);

            return Ok(ecategoryDto);
        }
        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            // check for id is excest or not
            var ecategoryDomain = _unitOfWork.Ecategory.GetFirstorDefault(predicate: x => x.Id == id, Includeword: "Hotels");
            if (ecategoryDomain == null)
            {
                return NotFound();
            }
            // Delete Hotel
            _unitOfWork.Ecategory.Remove(ecategoryDomain);
            _unitOfWork.Complete();

            // you can return deleted hotel value 
            // Using Auto Mapper
            var ecategoryDto = mapper.Map<EcategoryDto>(ecategoryDomain);
            return Ok(ecategoryDto);
        }
    }
}
