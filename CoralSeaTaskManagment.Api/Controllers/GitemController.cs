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
    public class GitemController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;
        public GitemController(ApplicationDbContext dbContext, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._context = dbContext;
            this._unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var gitemDomain = await _unitOfWork.Gitem.GetAll(Includeword: "Hotels,Departments,Glocations");

            var gitemDto = mapper.Map<List<GitemDto>>(gitemDomain);

            return Ok(gitemDto);
        }
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var gitemDomain = _unitOfWork.Gitem.GetFirstorDefault(predicate: x => x.Id == id, Includeword: "Hotels,Departments,Glocation");
            if (gitemDomain == null)
            {
                return NotFound();
            }
            var gitemDto = mapper.Map<GitemDto>(gitemDomain);

            return Ok(gitemDto);
        }
        [HttpPost("Create")]
        public IActionResult Create([FromBody] GitemAddDto gitemAddDto)
        {
            var gitemDomain = mapper.Map<Gitem>(gitemAddDto);

            _unitOfWork.Gitem.Add(gitemDomain);
            _unitOfWork.Complete();

            var gitemDto = mapper.Map<GitemDto>(gitemDomain);

            return CreatedAtAction(nameof(GetById), new { id = gitemDomain.Id }, gitemDto);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] GitemUpdateDto gitemUpdateDto)
        {

            var gitemDomain = _unitOfWork.Gitem.GetFirstorDefault(predicate: x => x.Id == id, Includeword: "Hotels,Departments,Glocation");
            if (gitemDomain == null)
            {
                return NotFound();
            }

            gitemDomain.Name = gitemUpdateDto.Name;
            gitemDomain.Namear = gitemUpdateDto.Namear;
            gitemDomain.HotelId = gitemUpdateDto.HotelId;
            gitemDomain.DepartmentId = gitemUpdateDto.DepartmentId;
            gitemDomain.GlocationId = gitemUpdateDto.GlocationId;

            _unitOfWork.Complete();

            var gitemDto = mapper.Map<GitemDto>(gitemDomain);

            return Ok(gitemDto);
        }
        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var gitemDomain = _unitOfWork.Gitem.GetFirstorDefault(predicate:x => x.Id == id, Includeword: "Hotels,Departments,Glocation");
            if (gitemDomain == null)
            {
                return NotFound();
            }
            _unitOfWork.Gitem.Remove(gitemDomain);
            _unitOfWork.Complete();

            var gitemDto = mapper.Map<GitemDto>(gitemDomain);
            return Ok(gitemDto);
        }
        [HttpGet("GetByLoc/{id}")]
        public async Task<IActionResult> GetByLocId([FromRoute] int id)
        {
            // Get Data From Database (Domain Model)
            var itemDomain = await _unitOfWork.Gitem.GetAll(predicate: x => x.GlocationId == id, Includeword: "Hotels,Glocations");
            if (itemDomain == null)
            {
                return NotFound();
            }

            // Using Auto Mapper
            var itemDto = mapper.Map<IEnumerable<GitemDto>>(itemDomain);

            // Return DTO
            return Ok(itemDto);
        }
    }
}
