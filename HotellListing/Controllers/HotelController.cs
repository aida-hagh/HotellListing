using AutoMapper;
using Cor.HotelListing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CountryController> _logger;
        private readonly IMapper _mapper;
        public HotelController(IUnitOfWork unitOfWork, ILogger<CountryController> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHotels()
        {
            try
            {
                var hotels = await _unitOfWork.Hotels.GetAll();
                var res = _mapper.Map<IList<HotelDTO>>(hotels);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"wrong in the{GetHotels}");
                return StatusCode(500,"Internal Error");
            }
     
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHotel(int id)
        {
            try
            {
                var hotel = await _unitOfWork.Hotels.Get(x=>x.Id==id,new List<string> { "Country" } );
                var res = _mapper.Map<HotelDTO>(hotel);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"wrong in the{GetHotel}");
                return StatusCode(500, "Internal Error");
            }

        }



    }
}
