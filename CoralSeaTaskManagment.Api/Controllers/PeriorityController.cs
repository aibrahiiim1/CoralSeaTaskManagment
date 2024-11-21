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
    public class PeriorityController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;
        public PeriorityController(ApplicationDbContext dbContext, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._context = dbContext;
            this._unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            var Domain = await _unitOfWork.Periority.GetAll();

            var Dto = mapper.Map<List<PeriorityDto>>(Domain);

            return Ok(Dto);
        }
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var Domain = _unitOfWork.Periority.GetFirstorDefault(x => x.Id == id);
            if (Domain == null)
            {
                return NotFound();
            }
            var hotelDto = mapper.Map<PeriorityDto>(Domain);
            return Ok(hotelDto);
        }
        [HttpPost("Create")]
        public IActionResult Create([FromBody] PeriorityAddDto Dto)
        {
            var Domain = mapper.Map<Periority>(Dto);

            _unitOfWork.Periority.Add(Domain);
            _unitOfWork.Complete();

            var hotelDto = mapper.Map<PeriorityDto>(Domain);

            return CreatedAtAction(nameof(GetById), new { id = Domain.Id }, hotelDto);

        }
        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] PeriorityUpdateDto UpdateDto)
        {
            var Domain = _unitOfWork.Periority.GetFirstorDefault(x => x.Id == id);
            if (Domain == null)
            {
                return NotFound();
            }
            Domain.Name = UpdateDto.Name;
            Domain.NameAr = UpdateDto.NameAr;
            _unitOfWork.Complete();

            var hotelDto = mapper.Map<PeriorityDto>(Domain);

            return Ok(hotelDto);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var Domain = _unitOfWork.Periority.GetFirstorDefault(x => x.Id == id);
            if (Domain == null)
            {
                return NotFound();
            }
            _unitOfWork.Periority.Remove(Domain);
            _unitOfWork.Complete();

            var hotelDto = mapper.Map<PeriorityDto>(Domain);
            return Ok(hotelDto);
        }
    }
}
