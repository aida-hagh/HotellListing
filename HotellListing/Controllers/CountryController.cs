using AutoMapper;
using Cor.HotelListing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CountryController> _logger;
        private readonly IMapper _mapper;

        public CountryController(IUnitOfWork unitOfWork, ILogger<CountryController> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCountries()
        {
            try
            {
                var countries = await _unitOfWork.Countries.GetAll();
                var res = _mapper.Map<IList<CountryDTO>>(countries);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"wrong in the{GetCountries}");
                return StatusCode(500, "internal error");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCountry(int id)
        {
            try
            {
                var country = await _unitOfWork.Countries.Get(x=>x.Id==id,new List<string> { "Hotels" });
                var res = _mapper.Map<CountryDTO>(country);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"wrong in the{GetCountry}");
                return StatusCode(500, "internal error");
            }
        }
    }
}
