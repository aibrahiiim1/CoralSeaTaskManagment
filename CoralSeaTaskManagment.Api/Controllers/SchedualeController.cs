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
    public class SchedualeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;
        public SchedualeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var Domain = await _unitOfWork.Scheduale.GetAll(Includeword: "Hotels");
            var Dto = mapper.Map<List<SchedualeDto>>(Domain);
            return Ok(Dto);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var Domain = _unitOfWork.Scheduale.GetFirstorDefault(predicate: x => x.Id == id, Includeword: "Hotels");
            if (Domain == null)
            {
                return NotFound();
            }
            var Dto = mapper.Map<SchedualeDto>(Domain);
            return Ok(Dto);
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] SchedualeAddDto schedualeAddDto)
        {
            var Domain = mapper.Map<Scheduale>(schedualeAddDto);

            _unitOfWork.Scheduale.Add(Domain);
            _unitOfWork.Complete();
            var Dto = mapper.Map<SchedualeDto>(Domain);

            return CreatedAtAction(nameof(GetById), new { id = Domain.Id }, Dto);
        }
        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] SchedualeUpdateDto schedualeUpdateDto)
        {
            // check for id is excest or not
            var Domain = _unitOfWork.Scheduale.GetFirstorDefault(predicate: x => x.Id == id, Includeword: "Hotels");
            if (Domain == null)
            {
                return NotFound();
            }
            Domain.CreatedTime = DateTime.Now;
            Domain.Reason = schedualeUpdateDto.Reason;
            Domain.HotelId = schedualeUpdateDto.HotelId;
            Domain.OrderId = schedualeUpdateDto.OrderId;
            Domain.ReturnDate = schedualeUpdateDto.ReturnDate;
            Domain.ApplicationUserId = schedualeUpdateDto.ApplicationUserId;

            _unitOfWork.Complete();
            var Dto = mapper.Map<SchedualeDto>(Domain);

            return Ok(Dto);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var Domain = _unitOfWork.Scheduale.GetFirstorDefault(x => x.Id == id);
            if (Domain == null)
            {
                return NotFound();
            }
            _unitOfWork.Scheduale.Remove(Domain);
            _unitOfWork.Complete();
            var Dto = mapper.Map<SchedualeDto>(Domain);
            return Ok(Dto);
        }

    }
}
