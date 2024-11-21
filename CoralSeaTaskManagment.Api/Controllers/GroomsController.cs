using AutoMapper;
using CoralSeaTaskManagment.Api.Models.DTO;
using CoralSeaTaskManagment.Data.Data;
using CoralSeaTaskManagment.Model.Models.Domain;
using CoralSeaTaskManagment.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CoralSeaTaskManagment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroomsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;
        public GroomsController(ApplicationDbContext dbContext, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._context = dbContext;
            this._unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var groomsDomain = await _unitOfWork.Grooms.GetAll(Includeword: "Hotels");

            var groomsDto = mapper.Map<List<GroomDto>>(groomsDomain);

            return Ok(groomsDto);
        }
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var groomsDomain = _unitOfWork.Grooms.GetFirstorDefault(predicate: x => x.Id == id, Includeword: "Hotels");
            if (groomsDomain == null)
            {
                return NotFound();
            }
            var groomsDto = mapper.Map<GroomDto>(groomsDomain);

            return Ok(groomsDto);
        }
        [HttpPost("Create")]
        public IActionResult Create([FromBody] GroomAddDto groomsAddDto)
        {
            var groomsDomain = mapper.Map<Grooms>(groomsAddDto);
            _unitOfWork.Grooms.Add(groomsDomain);
            _unitOfWork.Complete();

            var groomsDto = mapper.Map<GroomDto>(groomsDomain);

            return CreatedAtAction(nameof(GetById), new { id = groomsDomain.Id }, groomsDto);
        }
        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] GroomUpdateDto groomUpdateDto)
        {
            var groomsDomain = _unitOfWork.Grooms.GetFirstorDefault(predicate:x => x.Id == id, Includeword: "Hotels");
            if (groomsDomain == null)
            {
                return NotFound();
            }
            groomsDomain.Name = groomUpdateDto.Name;
            groomsDomain.Building = groomUpdateDto.Building;
            groomsDomain.HotelId = groomUpdateDto.HotelId;

            _unitOfWork.Complete();
            var groomsDto = mapper.Map<GroomDto>(groomsDomain);

            return Ok(groomsDto);
        }
        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var groomsDomain = _unitOfWork.Grooms.GetFirstorDefault(predicate:x => x.Id == id, Includeword: "Hotels");
            if (groomsDomain == null)
            {
                return NotFound();
            }
            _unitOfWork.Grooms.Remove(groomsDomain);
            _unitOfWork.Complete();
            var groomsDto = mapper.Map<GroomDto>(groomsDomain);
            return Ok(groomsDto);
        }
    }
}
