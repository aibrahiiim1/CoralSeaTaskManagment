using AutoMapper;
using CoralSeaTaskManagment.Api.Models.DTO;
using CoralSeaTaskManagment.Data.Data;
using CoralSeaTaskManagment.Model.Models.Domain;
using CoralSeaTaskManagment.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace CoralSeaTaskManagment.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;
        public OrderController(ApplicationDbContext dbContext, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._context = dbContext;
            this._unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var Domain = await _unitOfWork.Order.GetAll(Includeword: "Hotels,Departments,DepartmentFrom,Locations,Items,Otypes,Periorities");
            var Dto = mapper.Map<List<OrderDto>>(Domain);
            return Ok(Dto);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var Domain = _unitOfWork.Order.GetFirstorDefault(predicate: x => x.Id == id, Includeword: "Hotels,Departments,DepartmentFrom,Locations,Items,Otypes,Periorities");
            if (Domain == null)
            {
                return NotFound();
            }
            var Dto = mapper.Map<OrderDto>(Domain);
            return Ok(Dto);
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] OrderAddDto orderAddDto)
        {
            orderAddDto.OstatusId = OstatusEnum.Open;
            orderAddDto.AssignFlag = AssignEnum.NotAssigned;
            var Domain = mapper.Map<Order>(orderAddDto);
            _unitOfWork.Order.Add(Domain);

            _unitOfWork.Complete();
            var Dto = mapper.Map<OrderDto>(Domain);

            return CreatedAtAction(nameof(GetById), new { id = Domain.Id }, Dto);
        }
        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] OrderUpdateDto orderUpdateDto)
        {
            var Domain = _unitOfWork.Order.GetFirstorDefault(predicate: x => x.Id == id, Includeword: "Hotels,Departments,DepartmentFrom,Locations,Items,Otypes,Periorities");
            if (Domain == null)
            {
                return NotFound();
            }

            Domain.OtypeId = orderUpdateDto.OtypeId;
            Domain.LocationId = orderUpdateDto.LocationId;
            Domain.DepartmentId = orderUpdateDto.DepartmentId;
            Domain.DepartmentFId = orderUpdateDto.DepartmentFId;
            Domain.OstatusId = orderUpdateDto.OstatusId;
            Domain.AssignFlag = orderUpdateDto.AssignFlag;
            Domain.PeriorityId = orderUpdateDto.PeriorityId;
            Domain.Pic = orderUpdateDto.Pic;
            Domain.HotelId = orderUpdateDto.HotelId;
            Domain.Comment = orderUpdateDto.Comment;
            Domain.ItemId = orderUpdateDto.ItemId;
            Domain.ApplicationUserId = orderUpdateDto.ApplicationUserId;
            Domain.AssignFlag = orderUpdateDto.AssignFlag;

            _unitOfWork.Complete();
            var Dto = mapper.Map<OrderDto>(Domain);

            return Ok(Dto);
        }

       
        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var Domain = _unitOfWork.Order.GetFirstorDefault(x => x.Id == id);
            if (Domain == null)
            {
                return NotFound();
            }
            _unitOfWork.Order.Remove(Domain);
            _unitOfWork.Complete();
            var Dto = mapper.Map<OrderDto>(Domain);
            return Ok(Dto);
        }
        [HttpPut("Assign/{OrderId}")]
        public IActionResult Assign(int OrderId)
        {
            var Domain = _unitOfWork.Order.GetFirstorDefault(x => x.Id == OrderId);
            if (Domain == null)
            {
                return NotFound();
            }
            var orderDto = new OrderDto();


            Domain.AssignFlag = AssignEnum.Assigned;
            _unitOfWork.Complete();
            orderDto = mapper.Map<OrderDto>(Domain);

            return Ok(orderDto);
        }        

    }
}
