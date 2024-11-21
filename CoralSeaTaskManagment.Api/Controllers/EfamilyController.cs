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
    public class EfamilyController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;
        public EfamilyController(ApplicationDbContext dbContext, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._context = dbContext;
            this._unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //// Get Data From Database (Domain Model)
            var efamilyDomain = await _unitOfWork.Efamily.GetAll(Includeword: "Hotels");


            // Using Auto Mapper from Domain Model To DTO
            var efamilyDto = mapper.Map<List<EfamilyDto>>(efamilyDomain);
            // Return DTO
            return Ok(efamilyDto);
        }
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            // Get Data From Database (Domain Model)
            var efamilyDomain = _unitOfWork.Efamily.GetFirstorDefault(predicate: x => x.Id == id, Includeword: "Hotels");
            if (efamilyDomain == null)
            {
                return NotFound();
            }

            // Using Auto Mapper
            var efamilyDto = mapper.Map<EfamilyDto>(efamilyDomain);

            // Return DTO
            return Ok(efamilyDto);
        }
        [HttpPost]
        public IActionResult Create([FromBody] EfamilyAddDto efamilyAddDto)
        {
            //// Map or Convert DTO to Domain Model Using Auto Mapper
            var efamilyDomain = mapper.Map<Efamily>(efamilyAddDto);

            // Use Domain Model To Create 
            _unitOfWork.Efamily.Add(efamilyDomain);
            _unitOfWork.Complete();

            // Map Domain Model Back To DTO Using Auto Mapper
            var efamilyDto = mapper.Map<EfamilyDto>(efamilyDomain);

            return CreatedAtAction(nameof(GetById), new { id = efamilyDomain.Id }, efamilyDto);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] EfamilyUpdateDto efamilyUpdateDto)
        {
            // check for id is excest or not
            var efamilyDomain = _unitOfWork.Efamily.GetFirstorDefault(predicate: x => x.Id == id, Includeword: "Hotels");
            if (efamilyDomain == null)
            {
                return NotFound();
            }

            // Map DTO to Domain Model Using Normal Manual Mapping
            efamilyDomain.Name = efamilyUpdateDto.Name;
            efamilyDomain.HotelId = efamilyUpdateDto.HotelId;

            // Save and update
            _unitOfWork.Complete();

            // Convert Domain Model To DTO Using Auto Mapper
            var efamilyDto = mapper.Map<EfamilyDto>(efamilyDomain);

            return Ok(efamilyDto);
        }
        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            // check for id is excest or not
            var efamilyDomain = _unitOfWork.Efamily.GetFirstorDefault(predicate: x => x.Id == id, Includeword: "Hotels");
            if (efamilyDomain == null)
            {
                return NotFound();
            }
            // Delete Hotel
            _unitOfWork.Efamily.Remove(efamilyDomain);
            _unitOfWork.Complete();

            // you can return deleted hotel value 
            // Using Auto Mapper
            var efamilyDto = mapper.Map<EfamilyDto>(efamilyDomain);
            return Ok(efamilyDto);
        }
    }
}
