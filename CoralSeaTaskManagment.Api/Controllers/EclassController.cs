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
    public class EclassController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;
        public EclassController(ApplicationDbContext dbContext, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._context = dbContext;
            this._unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //// Get Data From Database (Domain Model)
            var eclassDomain = await _unitOfWork.Eclass.GetAll(Includeword: "Hotels");


            // Using Auto Mapper from Domain Model To DTO
            var eclassDto = mapper.Map<List<EclassDto>>(eclassDomain);
            // Return DTO
            return Ok(eclassDto);
        }
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            // Get Data From Database (Domain Model)
            var eclassDomain = _unitOfWork.Eclass.GetFirstorDefault(predicate: x => x.Id == id, Includeword: "Hotels");
            if (eclassDomain == null)
            {
                return NotFound();
            }

            // Using Auto Mapper
            var eclassDto = mapper.Map<EclassDto>(eclassDomain);

            // Return DTO
            return Ok(eclassDto);
        }
        [HttpPost]
        public IActionResult Create([FromBody] EclassAddDto eclassAddDto)
        {
            //// Map or Convert DTO to Domain Model Using Auto Mapper
            var eclassDomain = mapper.Map<Eclass>(eclassAddDto);

            // Use Domain Model To Create 
            _unitOfWork.Eclass.Add(eclassDomain);
            _unitOfWork.Complete();

            // Map Domain Model Back To DTO Using Auto Mapper
            var eclassDto = mapper.Map<EclassDto>(eclassDomain);

            return CreatedAtAction(nameof(GetById), new { id = eclassDomain.Id }, eclassDto);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] EclassUpdateDto eclassUpdateDto)
        {
            // check for id is excest or not
            var eclassDomain = _unitOfWork.Eclass.GetFirstorDefault(predicate: x => x.Id == id, Includeword: "Hotels");
            if (eclassDomain == null)
            {
                return NotFound();
            }

            // Map DTO to Domain Model Using Normal Manual Mapping
            eclassDomain.Name = eclassUpdateDto.Name;
            eclassDomain.HotelId = eclassUpdateDto.HotelId;

            // Save and update
            _unitOfWork.Complete();

            // Convert Domain Model To DTO Using Auto Mapper
            var eclassDto = mapper.Map<EclassDto>(eclassDomain);

            return Ok(eclassDto);
        }
        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            // check for id is excest or not
            var eclassDomain = _unitOfWork.Eclass.GetFirstorDefault(predicate: x => x.Id == id, Includeword: "Hotels");
            if (eclassDomain == null)
            {
                return NotFound();
            }
            // Delete Hotel
            _unitOfWork.Eclass.Remove(eclassDomain);
            _unitOfWork.Complete();

            // you can return deleted hotel value 
            // Using Auto Mapper
            var eclassDto = mapper.Map<EclassDto>(eclassDomain);
            return Ok(eclassDto);
        }
    }
}
