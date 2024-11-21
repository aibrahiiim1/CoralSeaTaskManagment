using AutoMapper;
using CoralSeaTaskManagment.Api.Models.DTO;
using CoralSeaTaskManagment.Data.Data;
using CoralSeaTaskManagment.Model.Models.Domain;
using CoralSeaTaskManagment.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CoralSeaTaskManagment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;
        public HotelController(ApplicationDbContext dbContext,IUnitOfWork unitOfWork,IMapper mapper)
        {
            this._context = dbContext;
            this._unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        // Get All
        // Get: https://localhost:portnumber/api/hotels
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //// Get Data From Database (Domain Model)
            //var hotelDomain=await _context.Hotels.ToListAsync();
            var hotelDomain=await _unitOfWork.Hotel.GetAll();

            //// Map Domain Model to DTO Using Normal Manual Mapping
            //var hotelDto = new List<HotelDto>();
            //foreach (var hotel in hotelDomain)
            //{
            //    hotelDto.Add(new HotelDto
            //    {
            //        Id = hotel.Id,
            //        Name = hotel.Name
            //    });
            //}

            // Using Auto Mapper from Domain Model To DTO
            var hotelDto = mapper.Map< List <HotelDto>>(hotelDomain);

            // Return DTO
            return Ok(hotelDto);
        }
        // Get single hotel by ID
        // Get: https://localhost:portnumber/api/hotels/{id}
        [HttpGet]
        [Route("{id:int}")]
        public  IActionResult GetById([FromRoute] int id)
        {
            // Get Data From Database (Domain Model)
            //var hotelDomain = _context.Hotels.Find(id);
            //// OR you can use FirstOrDefault
            var hotelDomain =  _unitOfWork.Hotel.GetFirstorDefault(x=>x.Id==id);
            if (hotelDomain == null)
            {
                return NotFound();
            }

            //// Map Domain Model to DTO Using Normal Manual Mapping
            //var hotelDto = new HotelDto
            //{
            //    Id = hotelDomain.Id,
            //    Name = hotelDomain.Name
            //};

            // Using Auto Mapper
            var hotelDto = mapper.Map<HotelDto>(hotelDomain);

            // Return DTO
            return Ok(hotelDto);
        }
        // Post to Create New Hotel
        // Post: https://localhost:portnumber/api/hotels
     
        [HttpPost("Create")]
        public IActionResult Create([FromBody] HotelAddDto hotelAddDto)
        {
            //// Map or Convert DTO to Domain Model Using Normal Manual Mapping
            //var hotelDomain = new Hotel
            //{
            //    Name = hotelAddDto.Name
            //};
            var hotelDomain = mapper.Map<Hotel>(hotelAddDto);

            // Use Domain Model To Create 
             _unitOfWork.Hotel.Add(hotelDomain);
             _unitOfWork.Complete();

            //// Map Domain Model Back To DTO Using Normal Manual Mapping
            //var hotelDto = new HotelDto
            //{
            //    Id = hotelDomain.Id,
            //    Name = hotelDomain.Name
            //};

            // Using Auto Mapper
            var hotelDto = mapper.Map<HotelDto>(hotelDomain);

            return CreatedAtAction(nameof(GetById), new {id=hotelDomain.Id},hotelDto);

        }

        // Update Hotel
        // Put: https://localhost:portnumber/api/hotels/{id}
        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] HotelUpdateDto hotelUpdateDto)
        {
            // check for id is excest or not
            var hotelDomain = _unitOfWork.Hotel.GetFirstorDefault(x => x.Id == id);
            if (hotelDomain == null)
            {
                return NotFound();
            }

            // Map DTO to Domain Model Using Normal Manual Mapping
            hotelDomain.Name = hotelUpdateDto.Name;

            // Save and update
            _unitOfWork.Complete();

            //// Convert Domain Model To DTO Using Normal Manual Mapping
            //var hotelDto = new HotelDto
            //{
            //    Id = hotelDomain.Id,
            //    Name = hotelDomain.Name
            //};

            // Convert Domain Model To DTO Using Auto Mapper
            var hotelDto=mapper.Map<HotelDto>(hotelDomain);

            return Ok(hotelDto);
        }
        // Delete Hotel from Database
        // Delete:https://localhost:portnumber/api/hotels/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            // check for id is excest or not
            var hotelDomain = _unitOfWork.Hotel.GetFirstorDefault(x => x.Id == id);
            if (hotelDomain == null)
            {
                return NotFound();
            }
            // Delete Hotel
            _unitOfWork.Hotel.Remove(hotelDomain);
             _unitOfWork.Complete();

            // you can return deleted hotel value 

            ////Map Domain Model To DTO Using Normal Manual Mapping
            //var hotelDto = new HotelDto
            //{
            //    Id = hotelDomain.Id,
            //    Name = hotelDomain.Name
            //};

            // Using Auto Mapper
            var hotelDto = mapper.Map<HotelDto>(hotelDomain);
            return Ok(hotelDto);
        }
    }
}
