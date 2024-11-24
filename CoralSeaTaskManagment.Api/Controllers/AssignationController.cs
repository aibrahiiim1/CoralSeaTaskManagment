using AutoMapper;
using CoralSeaTaskManagment.Api.Models.DTO;
using CoralSeaTaskManagment.Data.Data;
using CoralSeaTaskManagment.Model.Models.Domain;
using CoralSeaTaskManagment.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoralSeaTaskManagment.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AssignationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;
        public AssignationController(ApplicationDbContext dbContext, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var Domain = await _unitOfWork.Assignation.GetAll(Includeword: "Hotels,Assigns");
            var Dto = mapper.Map<List<AssignationDto>>(Domain);
            return Ok(Dto);
        }
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var Domain = _unitOfWork.Assignation.GetFirstorDefault(predicate: x => x.Id == id, Includeword: "Hotels,Assigns");
            if (Domain == null)
            {
                return NotFound();
            }
            var Dto = mapper.Map<AssignationDto>(Domain);
            return Ok(Dto);
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] AssignationAddDto assignationAddDto)
        {
            var mapDtoTDomain = mapper.Map<Assignation>(assignationAddDto);

            _unitOfWork.Assignation.Add(mapDtoTDomain);
            _unitOfWork.Complete();
            var Dto = mapper.Map<AssignationDto>(mapDtoTDomain);

            return CreatedAtAction(nameof(GetById), new { id = mapDtoTDomain.Id }, Dto);
        }
        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] AssignationUpdateDto assignationUpdateDto)
        {
            // check for id is excest or not
            var Domain = _unitOfWork.Assignation.GetFirstorDefault(predicate: x => x.Id == id, Includeword: "Hotels,Assigns");
            if (Domain == null)
            {
                return NotFound();
            }

            Domain.AssignId = assignationUpdateDto.AssignId;
            Domain.HotelId = assignationUpdateDto.HotelId;
            Domain.CreatedTime = assignationUpdateDto.CreatedTime;
            Domain.TechName = assignationUpdateDto.TechName;
            Domain.OrderId = assignationUpdateDto.OrderId;
            Domain.ApplicationUserId = assignationUpdateDto.ApplicationUserId;

            _unitOfWork.Complete();
            var Dto = mapper.Map<AssignationDto>(Domain);

            return Ok(Dto);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var Domain = _unitOfWork.Assignation.GetFirstorDefault(x => x.Id == id);
            if (Domain == null)
            {
                return NotFound();
            }
            _unitOfWork.Assignation.Remove(Domain);
            _unitOfWork.Complete();
            var Dto = mapper.Map<AssignationDto>(Domain);
            return Ok(Dto);
        }
    }
}
