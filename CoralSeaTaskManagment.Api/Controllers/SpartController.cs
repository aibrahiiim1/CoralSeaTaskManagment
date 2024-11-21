using AutoMapper;
using CoralSeaTaskManagment.Api.Models.DTO;
using CoralSeaTaskManagment.Data.Data;
using CoralSeaTaskManagment.Model.Models.Domain;
using CoralSeaTaskManagment.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CoralSeaTaskManagment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpartController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;
        public SpartController(ApplicationDbContext dbContext, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._context = dbContext;
            this._unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var Domain = await _unitOfWork.Spart.GetAll(Includeword: "Hotels,Items");
            var Dto = mapper.Map<List<SpartDto>>(Domain);
            return Ok(Dto);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var Domain = _unitOfWork.Spart.GetFirstorDefault(predicate: x => x.Id == id, Includeword: "Hotels,Items");
            if (Domain == null)
            {
                return NotFound();
            }
            var Dto = mapper.Map<SpartDto>(Domain);
            return Ok(Dto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] SpartAddDto AddDto)
        {
            var Domain = mapper.Map<Spart>(AddDto);

            _unitOfWork.Spart.Add(Domain);
            _unitOfWork.Complete();
            var Dto = mapper.Map<SpartDto>(Domain);

            return CreatedAtAction(nameof(GetById), new { id = Domain.Id }, Dto);
        }
        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] SpartUpdateDto UpdateDto)
        {
            // check for id is excest or not
            var Domain = _unitOfWork.Spart.GetFirstorDefault(predicate: x => x.Id == id, Includeword: "Hotels,Items");
            if (Domain == null)
            {
                return NotFound();
            }

            Domain.Q = UpdateDto.Q;
            Domain.CreatedTime = DateTime.Now;
            Domain.Name = UpdateDto.Name;
            Domain.HotelId = UpdateDto.HotelId;
            Domain.ItemId = UpdateDto.ItemId;
            Domain.Price = UpdateDto.Price;
            Domain.Unit = UpdateDto.Unit;
            Domain.ApplicationUserId = UpdateDto.ApplicationUserId;
            Domain.OrderId = UpdateDto.OrderId;

            _unitOfWork.Complete();
            var Dto = mapper.Map<SpartDto>(Domain);

            return Ok(Dto);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var Domain = _unitOfWork.Spart.GetFirstorDefault(x => x.Id == id);
            if (Domain == null)
            {
                return NotFound();
            }
            _unitOfWork.Spart.Remove(Domain);
            _unitOfWork.Complete();
            var Dto = mapper.Map<SpartDto>(Domain);
            return Ok(Dto);
        }

    }
}
