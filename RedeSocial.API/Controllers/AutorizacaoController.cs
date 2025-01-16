using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RedeSocial.Domain.DTOs;
using RedeSocial.Domain.Interfaces.Services;

namespace RedeSocial.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/autorizacao")]
    public class AutorizacaoController : ControllerBase
    {
        private readonly IAutorizacaoService _autorizacaoService;

        private readonly IMapper _mapper;

        public AutorizacaoController(IAutorizacaoService autorizacaoService,
                                IMapper mapper)
        {
            _autorizacaoService = autorizacaoService;
            _mapper = mapper;
        }
     
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDTO loginUsuarioDTO)
        {
            try
            {
                var retorno = await _autorizacaoService.Login(loginUsuarioDTO);

                if (retorno == null)
                    return NotFound();

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }  
    }
}
