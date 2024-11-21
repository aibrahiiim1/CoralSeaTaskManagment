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
    public class GlocationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;
        public GlocationController(ApplicationDbContext dbContext, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._context = dbContext;
            this._unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {

            var glocationDomain = await _unitOfWork.Glocation.GetAll(Includeword: "Hotels");

            var glocationDto = mapper.Map<List<GlocationDto>>(glocationDomain);

            // Return DTO
            return Ok(glocationDto);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {

            var glocationDomain = _unitOfWork.Glocation.GetFirstorDefault(predicate: x => x.Id == id, Includeword: "Hotels");
            if (glocationDomain == null)
            {
                return NotFound();
            }

            var glocationDto = mapper.Map<GlocationDto>(glocationDomain);

            return Ok(glocationDto);
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] GlocationAddDto glocationAddDto)
        {
            var glocationDomain = mapper.Map<Glocation>(glocationAddDto);

            _unitOfWork.Glocation.Add(glocationDomain);
            _unitOfWork.Complete();

            var glocationDto = mapper.Map<GlocationDto>(glocationDomain);

            return CreatedAtAction(nameof(GetById), new { id = glocationDomain.Id }, glocationDto);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] GlocationUpdateDto glocationUpdateDto)
        {

            var glocationDomain = _unitOfWork.Glocation.GetFirstorDefault(predicate: x => x.Id == id, Includeword: "Hotels");
            if (glocationDomain == null)
            {
                return NotFound();
            }

            glocationDomain.Name = glocationUpdateDto.Name;
            glocationDomain.NameAr = glocationUpdateDto.NameAr;
            glocationDomain.HotelId = glocationUpdateDto.HotelId;

            _unitOfWork.Complete();

            var glocationDto = mapper.Map<GlocationDto>(glocationDomain);

            return Ok(glocationDto);
        }
 
        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var glocationDomain = _unitOfWork.Glocation.GetFirstorDefault(predicate: x => x.Id == id, Includeword: "Hotels");
            if (glocationDomain == null)
            {
                return NotFound();
            }
            _unitOfWork.Glocation.Remove(glocationDomain);
            _unitOfWork.Complete();

            var glocationDto = mapper.Map<GlocationDto>(glocationDomain);
            return Ok(glocationDto);
        }

    }
}
