using AutoMapper;
using CoralSeaTaskManagment.Api.Models.DTO;
using CoralSeaTaskManagment.Api.Models.ViewModel;
using CoralSeaTaskManagment.Data.Data;
using CoralSeaTaskManagment.Model.Models.Domain;
using CoralSeaTaskManagment.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoralSeaTaskManagment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtypeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;
        public OtypeController(ApplicationDbContext dbContext, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._context = dbContext;
            this._unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //// Get Data From Database (Domain Model)
            var otypeDomain = await _unitOfWork.Otype.GetAll(Includeword: "Hotels");

            // Using Auto Mapper from Domain Model To DTO
            var otypeDto = mapper.Map<List<OtypeDto>>(otypeDomain);
            // Return DTO
            return Ok(otypeDto);
        }
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            // Get Data From Database (Domain Model)
            var otypeDomain = _unitOfWork.Otype.GetFirstorDefault(predicate: x => x.Id == id, Includeword: "Hotels");
            if (otypeDomain == null)
            {
                return NotFound();
            }

            // Using Auto Mapper
            var otypeDto = mapper.Map<OtypeDto>(otypeDomain);

            // Return DTO
            return Ok(otypeDto);
        }
        [HttpPost]
        public IActionResult Create([FromBody] OtypeAddDto otypeAddDto)
        {
            //// Map or Convert DTO to Domain Model Using Auto Mapper
            var otypeDomain = mapper.Map<Otype>(otypeAddDto);

            // Use Domain Model To Create 
            _unitOfWork.Otype.Add(otypeDomain);
            _unitOfWork.Complete();

            // Map Domain Model Back To DTO Using Auto Mapper
            var otypeDto = mapper.Map<OtypeDto>(otypeDomain);

            return CreatedAtAction(nameof(GetById), new { id = otypeDomain.Id }, otypeDto);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] OtypeUpdateDto otypeUpdateDto)
        {
            // check for id is excest or not
            var otypeDomain = _unitOfWork.Otype.GetFirstorDefault(predicate: x => x.Id == id, Includeword: "Hotels");
            if (otypeDomain == null)
            {
                return NotFound();
            }

            // Map DTO to Domain Model Using Normal Manual Mapping
            otypeDomain.Name = otypeUpdateDto.Name;
            otypeDomain.HotelId = otypeUpdateDto.HotelId;
            otypeDomain.Fronthousflag = otypeUpdateDto.Fronthousflag;

            // Save and update
            _unitOfWork.Complete();

            // Convert Domain Model To DTO Using Auto Mapper
            var otypeDto = mapper.Map<OtypeDto>(otypeDomain);

            return Ok(otypeDto);
        }
        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            // check for id is excest or not
            var otypeDomain = _unitOfWork.Otype.GetFirstorDefault(predicate: x => x.Id == id, Includeword: "Hotels");
            if (otypeDomain == null)
            {
                return NotFound();
            }
            // Delete Hotel
            _unitOfWork.Otype.Remove(otypeDomain);
            _unitOfWork.Complete();

            // you can return deleted hotel value 
            // Using Auto Mapper
            var otypeDto = mapper.Map<OtypeDto>(otypeDomain);
            return Ok(otypeDto);
        }
        [HttpGet("Enum")]
        public IActionResult EnumFronthouse()
        {
            var hotels = Enum.GetValues(typeof(FronthousEnum))
                             .Cast<FronthousEnum>()
                             .Select(h => new
                             {
                                 Value = (int)h,
                                 Text = h.ToString()
                             })
                             .ToList();

            return Ok(hotels);
        }
    }
}
