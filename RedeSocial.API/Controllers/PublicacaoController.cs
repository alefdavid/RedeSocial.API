using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RedeSocial.Domain.DTOs;
using RedeSocial.Domain.Entities;
using RedeSocial.Domain.Interfaces.Services;

namespace RedeSocial.API.Controllers
{
    [ApiController]
    [Route("api/publicacao")]
    public class PublicacaoController : ControllerBase
    {
        private readonly IPublicacaoService _publicacaoService;

        private readonly IMapper _mapper;
        public PublicacaoController(IPublicacaoService publicacaoService, IMapper mapper)
        {
            _publicacaoService = publicacaoService;
            _mapper = mapper;            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var publicacao = await _publicacaoService.GetById(id);

            if (publicacao == null) 
            { 
                return NotFound();
            }

            return Ok(publicacao);
        }

        [HttpGet("usuario/{usuarioId}")]
        public async Task<IActionResult> GetByUsuario(int usuarioId)
        {
            var publicacoes = await _publicacaoService.GetPublicacoesByUsuario(usuarioId);

            if (publicacoes == null || publicacoes.Publicacoes == null || !publicacoes.Publicacoes.Any())
            { 
                return NotFound();
            }

            return Ok(publicacoes);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PublicacaoDTO publicacaoDTO)
        {
            if (!ModelState.IsValid)
            {
                var lista = ModelState.Where(x => x.Value.ValidationState == ModelValidationState.Invalid)
                    .SelectMany(item => item.Value.Errors.ToList(), (item, errors) => new { item, errors })
                    .Select(x => x.errors.ErrorMessage)
                    .ToList();

                return BadRequest(lista);
            }

            var publicacao = _mapper.Map<Publicacao>(publicacaoDTO);

            var publicacaoCriada = await _publicacaoService.Post(publicacaoDTO);

            if (publicacaoCriada == null)
            {
                return BadRequest();
            }

            return Ok(publicacaoCriada);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(PublicacaoDTO publicacaoDTO, int id)
        {
            var buscaPublicacao = await _publicacaoService.GetById(id);

            if (buscaPublicacao == null)
            {
                return NotFound();
            }

            var publicacaoAtualizadaDTO = _mapper.Map<PublicacaoDTO>(publicacaoDTO);

            var retornoPublicacao = await _publicacaoService.Put(publicacaoAtualizadaDTO, id);

            if (retornoPublicacao == null)
            {
                return NotFound();
            }

            return Ok(retornoPublicacao);            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var publicacao = await _publicacaoService.Delete(id);

            if (publicacao == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
