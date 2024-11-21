using AutoMapper;
using CoralSeaTaskManagment.Api.Models.DTO;
using CoralSeaTaskManagment.Model.Models.Domain;
using CoralSeaTaskManagment.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoralSeaTaskManagment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OutgoingController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OutgoingController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var modelDomain = await _unitOfWork.Outgoing.GetAll(Includeword:"Hotels,Items");
            var Dto = _mapper.Map<List<OutgoingDto>>(modelDomain);
            return Ok(Dto);
        }
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var modelDomain = _unitOfWork.Outgoing.GetFirstorDefault(predicate: x => x.Id == id, Includeword:"Hotels,Items");
            if (modelDomain == null)
            {
                return NotFound();
            }

            var Dto = _mapper.Map<OutgoingDto>(modelDomain);
            return Ok(Dto);
        }
        [HttpPost]
        public IActionResult Create([FromBody] OutgoingAddDto outgoingAddDto)
        {
            var mapDtoTDomain = _mapper.Map<Outgoing>(outgoingAddDto);

            _unitOfWork.Outgoing.Add(mapDtoTDomain);
            _unitOfWork.Complete();
            var Dto = _mapper.Map<OutgoingDto>(mapDtoTDomain);

            return CreatedAtAction(nameof(GetById), new { id = mapDtoTDomain.Id }, Dto);
        }
        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] OutgoingUpdateDto outgoingUpdateDto)
        {
            // check for id is excest or not
            var Domain = _unitOfWork.Outgoing.GetFirstorDefault(predicate: x => x.Id == id, Includeword: "Hotels,Items");
            if (Domain == null)
            {
                return NotFound();
            }

            Domain.No = outgoingUpdateDto.No;
            Domain.HotelId = outgoingUpdateDto.HotelId;
            Domain.CreatedTime = outgoingUpdateDto.CreatedTime;
            Domain.CompanyName = outgoingUpdateDto.CompanyName;
            Domain.OrderId = outgoingUpdateDto.OrderId;
            Domain.ApplicationUserId = outgoingUpdateDto.ApplicationUserId;
            Domain.ReturnDate = outgoingUpdateDto.ReturnDate;
            Domain.Remark = outgoingUpdateDto.Remark;
            Domain.ItemId = outgoingUpdateDto.ItemId;
            Domain.Price = outgoingUpdateDto.Price;


            _unitOfWork.Complete();
            var Dto = _mapper.Map<OutgoingDto>(Domain);

            return Ok(Dto);
        }
        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var Domain = _unitOfWork.Outgoing.GetFirstorDefault(x => x.Id == id);
            if (Domain == null)
            {
                return NotFound();
            }
            _unitOfWork.Outgoing.Remove(Domain);
            _unitOfWork.Complete();
            var Dto = _mapper.Map<OutgoingDto>(Domain);
            return Ok(Dto);
        }
    }
}
