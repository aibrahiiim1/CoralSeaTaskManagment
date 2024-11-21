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
    public class GorderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;
        public GorderController(ApplicationDbContext dbContext, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._context = dbContext;
            this._unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var Domain = await _unitOfWork.Gorder.GetAll(Includeword: "Hotels,Departments,GdepartmentFrom,Glocations,Gitems,Gitems.Glocations,Otypes,Grooms,Grooms.Hotels");
            var Dto = mapper.Map<List<GorderDto>>(Domain);
            return Ok(Dto);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var Domain = _unitOfWork.Gorder.GetFirstorDefault(predicate: x => x.Id == id, Includeword: "Hotels,Departments,GdepartmentFrom,Glocations,Gitems,Otypes,Grooms");
            if (Domain == null)
            {
                return NotFound();
            }
            var Dto = mapper.Map<GorderDto>(Domain);
            return Ok(Dto);
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] GorderAddDto orderAddDto)
        {
            orderAddDto.OstatusId = OstatusEnum.Open;
            orderAddDto.AssignFlag = AssignEnum.NotAssigned;
            var Domain = mapper.Map<Gorder>(orderAddDto);
            _unitOfWork.Gorder.Add(Domain);

            _unitOfWork.Complete();
            var Dto = mapper.Map<GorderDto>(Domain);

            return CreatedAtAction(nameof(GetById), new { id = Domain.Id }, Dto);
        }
        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] GorderUpdateDto orderUpdateDto)
        {
            var Domain = _unitOfWork.Gorder.GetFirstorDefault(predicate: x => x.Id == id, Includeword: "Hotels,Departments,GdepartmentFrom,Glocations,Gitems,Gitems.Glocations,Otypes,Grooms,Grooms.Hotels");
            if (Domain == null)
            {
                return NotFound();
            }

            Domain.OtypeId = orderUpdateDto.OtypeId;
            Domain.GlocationId = orderUpdateDto.GlocationId;
            Domain.DepartmentId = orderUpdateDto.DepartmentId;
            Domain.GdepartmentFId = orderUpdateDto.GdepartmentFId;
            Domain.OstatusId = orderUpdateDto.OstatusId;
            Domain.AssignFlag = orderUpdateDto.AssignFlag;
            Domain.HotelId = orderUpdateDto.HotelId;
            Domain.Comment = orderUpdateDto.Comment;
            Domain.GitemId = orderUpdateDto.GitemId;
            Domain.GroomsId = orderUpdateDto.GroomsId;
            Domain.ApplicationUserId = orderUpdateDto.ApplicationUserId;

            _unitOfWork.Complete();
            var Dto = mapper.Map<GorderDto>(Domain);

            return Ok(Dto);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var Domain = _unitOfWork.Gorder.GetFirstorDefault(x => x.Id == id);
            if (Domain == null)
            {
                return NotFound();
            }
            _unitOfWork.Gorder.Remove(Domain);
            _unitOfWork.Complete();
            var Dto = mapper.Map<GorderDto>(Domain);
            return Ok(Dto);
        }
    }
}
