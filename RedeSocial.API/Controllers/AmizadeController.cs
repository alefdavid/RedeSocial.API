using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RedeSocial.Domain.DTOs;
using RedeSocial.Domain.Interfaces.Services;

namespace RedeSocial.API.Controllers
{
    [ApiController]
    [Route("api/amizade")]
    public class AmizadeController : ControllerBase
    {
        private readonly IAmizadeService _amizadeService;

        private readonly IMapper _mapper;

        public AmizadeController(IAmizadeService amizadeService,
                                IMapper mapper)
        {
            _amizadeService = amizadeService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var returnById = await _amizadeService.GetById(id);

                if (returnById == null)
                    return NotFound();

                return Ok(returnById);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var returnAll = await _amizadeService.GetAll();

                if (returnAll == null)
                    return NotFound();

                return Ok(returnAll);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AmizadeDTO amizadeDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var lista = ModelState.Where(x => x.Value.ValidationState == ModelValidationState.Invalid)
                        .SelectMany(item => item.Value.Errors.ToList(), (item, errors) => new { item, errors })
                        .Select(x => x.errors.ErrorMessage)
                        .ToList();

                    return BadRequest(lista);
                }

                var amizadeCreateDTO = _mapper.Map<AmizadeDTO>(amizadeDTO);

                var returnAmizade = await _amizadeService.Post(amizadeCreateDTO);

                if (returnAmizade == null)
                    return NotFound();

                return Ok(returnAmizade);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }        

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var returnAmizade = await _amizadeService.Delete(id);

                if (returnAmizade == null)
                    return NotFound();

                return Ok(returnAmizade);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
