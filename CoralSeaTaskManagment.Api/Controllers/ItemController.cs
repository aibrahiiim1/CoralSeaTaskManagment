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
    public class ItemController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;
        public ItemController(ApplicationDbContext dbContext, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._context = dbContext;
            this._unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var Domain =await _unitOfWork.Item.GetAll(Includeword: "Hotels,Departments,Hotels,Ecategories,Locations,Efamilies,Eclasses");
            var Dto = mapper.Map<List<ItemDto>>(Domain);
            return Ok(Dto);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var Domain = _unitOfWork.Item.GetFirstorDefault(predicate: x => x.Id == id, Includeword: "Hotels,Departments,Hotels,Ecategories,Locations,Efamilies,Eclasses");
            if (Domain == null)
            {
                return NotFound();
            }
            var locationDto = mapper.Map<ItemDto>(Domain);
            return Ok(locationDto);
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] ItemAddDto itemAddDto)
        {
            var Domain = mapper.Map<Item>(itemAddDto);

            _unitOfWork.Item.Add(Domain);
            _unitOfWork.Complete();
            var Dto = mapper.Map<ItemDto>(Domain);

            return CreatedAtAction(nameof(GetById), new { id = Domain.Id }, Dto);
        }
        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] ItemUpdateDto itemUpdateDto)
        {
            // check for id is excest or not
            var Domain = _unitOfWork.Item.GetFirstorDefault(predicate: x => x.Id == id,Includeword: "Hotels,Departments,Hotels,Ecategories,Locations,Efamilies,Eclasses,Locations.Hotels,Locations.Departments,Departments.Hotels");
            if (Domain == null)
            {
                return NotFound();
            }

            Domain.Name = itemUpdateDto.Name;
            Domain.Namear = itemUpdateDto.Namear;
            Domain.HotelId = itemUpdateDto.HotelId;
            Domain.Code = itemUpdateDto.Code;
            Domain.Cost = itemUpdateDto.Cost;
            Domain.EcategoryId = itemUpdateDto.EcategoryId;
            Domain.EclassId = itemUpdateDto.EclassId;
            Domain.EfamilyId = itemUpdateDto.EfamilyId;
            Domain.DepartmentId = itemUpdateDto.DepartmentId;
            Domain.LocationId = itemUpdateDto.LocationId;
            Domain.Manufacturer = itemUpdateDto.Manufacturer;
            Domain.InstallDate = itemUpdateDto.InstallDate;
            Domain.Warranty = itemUpdateDto.Warranty;
            Domain.WarrantyYear = itemUpdateDto.WarrantyYear;
            Domain.WarrantyStart = itemUpdateDto.WarrantyStart;
            Domain.Agent = itemUpdateDto.Agent;
            Domain.Pic = itemUpdateDto.Pic;
            Domain.CreatedTime = itemUpdateDto.CreatedTime;
            Domain.WarrantyFlag = itemUpdateDto.WarrantyFlag;
            Domain.CreatedTime = itemUpdateDto.CreatedTime;
            Domain.statusFlag = itemUpdateDto.statusFlag;

            _unitOfWork.Complete();
            var Dto = mapper.Map<ItemDto>(Domain);

            return Ok(Dto);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var Domain = _unitOfWork.Item.GetFirstorDefault(x => x.Id == id);
            if (Domain == null)
            {
                return NotFound();
            }
            _unitOfWork.Item.Remove(Domain);
            _unitOfWork.Complete();
            var Dto = mapper.Map<ItemDto>(Domain);
            return Ok(Dto);
        }

    }
}
