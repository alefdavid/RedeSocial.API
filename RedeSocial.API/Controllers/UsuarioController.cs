using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RedeSocial.Domain.DTOs;
using RedeSocial.Domain.Entities;
using RedeSocial.Domain.Interfaces.Services;
using RedeSocial.Infrastructure.Context;

namespace RedeSocial.API.Controllers
{
    [ApiController]
    [Route("api/usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        private readonly IMapper _mapper;

        public UsuarioController(IUsuarioService usuarioService,
                                IMapper mapper)
        {
            _usuarioService = usuarioService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var retornoById = await _usuarioService.GetById(id);

                if (retornoById == null)
                    return NotFound();

                return Ok(retornoById);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string? nome)
        {
            try
            {
                var retornoAll = await _usuarioService.GetAll(nome);

                if (retornoAll == null)
                    return NotFound();

                return Ok(retornoAll);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UsuarioDTO usuarioDTO)
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

                var usuarioCriadoDTO = _mapper.Map<UsuarioDTO>(usuarioDTO);

                var retornoUsuario = await _usuarioService.Post(usuarioCriadoDTO);

                if (retornoUsuario == null)
                    return NotFound();

                return Ok(retornoUsuario);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(UsuarioDTO usuarioDTO, int id)
        {
            try
            {
                var buscaUsuario = await _usuarioService.GetById(id);

                if (buscaUsuario == null)
                    return NotFound();

                var usuarioAtualizadoDTO = _mapper.Map<UsuarioDTO>(usuarioDTO);

                var retornoUsuario = await _usuarioService.Put(usuarioAtualizadoDTO, id);

                if (retornoUsuario == null)
                    return NotFound();

                return Ok(retornoUsuario);
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
                var retornoUsuario = await _usuarioService.Delete(id);

                if (retornoUsuario == null)
                    return NotFound();

                return Ok(retornoUsuario);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
