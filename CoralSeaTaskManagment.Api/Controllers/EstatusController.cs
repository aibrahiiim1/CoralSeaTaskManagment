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
    public class EstatusController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;
        public EstatusController(ApplicationDbContext dbContext, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._context = dbContext;
            this._unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //// Get Data From Database (Domain Model)
            var estatusDomain = await _unitOfWork.Estatus.GetAll(Includeword: "Hotels");


            // Using Auto Mapper from Domain Model To DTO
            var estatusDto = mapper.Map<List<EstatusDto>>(estatusDomain);
            // Return DTO
            return Ok(estatusDto);
        }
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            // Get Data From Database (Domain Model)
            var estatusDomain = _unitOfWork.Estatus.GetFirstorDefault(predicate: x => x.Id == id, Includeword: "Hotels");
            if (estatusDomain == null)
            {
                return NotFound();
            }

            // Using Auto Mapper
            var estatusDto = mapper.Map<EstatusDto>(estatusDomain);

            // Return DTO
            return Ok(estatusDto);
        }
        [HttpPost]
        public IActionResult Create([FromBody] EstatusAddDto estatusAddDto)
        {
            //// Map or Convert DTO to Domain Model Using Auto Mapper
            var estatusDomain = mapper.Map<Estatus>(estatusAddDto);

            // Use Domain Model To Create 
            _unitOfWork.Estatus.Add(estatusDomain);
            _unitOfWork.Complete();

            // Map Domain Model Back To DTO Using Auto Mapper
            var estatusDto = mapper.Map<EstatusDto>(estatusDomain);

            return CreatedAtAction(nameof(GetById), new { id = estatusDomain.Id }, estatusDto);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] EstatusUpdateDto estatusUpdateDto)
        {
            // check for id is excest or not
            var estatusDomain = _unitOfWork.Estatus.GetFirstorDefault(predicate: x => x.Id == id, Includeword: "Hotels");
            if (estatusDomain == null)
            {
                return NotFound();
            }

            // Map DTO to Domain Model Using Normal Manual Mapping
            estatusDomain.Name = estatusUpdateDto.Name;
            estatusDomain.HotelId = estatusUpdateDto.HotelId;

            // Save and update
            _unitOfWork.Complete();

            // Convert Domain Model To DTO Using Auto Mapper
            var estatusDto = mapper.Map<EstatusDto>(estatusDomain);

            return Ok(estatusDto);
        }
        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            // check for id is excest or not
            var estatusDomain = _unitOfWork.Estatus.GetFirstorDefault(predicate: x => x.Id == id, Includeword: "Hotels");
            if (estatusDomain == null)
            {
                return NotFound();
            }
            // Delete Hotel
            _unitOfWork.Estatus.Remove(estatusDomain);
            _unitOfWork.Complete();

            // you can return deleted hotel value 
            // Using Auto Mapper
            var estatusDto = mapper.Map<EstatusDto>(estatusDomain);
            return Ok(estatusDto);
        }
    }
}
