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

        [HttpPost("{id}/curtida")]
        public async Task<IActionResult> PostCurtida(int id, [FromBody] CurtidaDTO curtidaDTO)
        {
            curtidaDTO.PublicacaoId = id;
            var curtidaCriada = await _publicacaoService.PostCurtida(curtidaDTO);

            if (curtidaCriada == null)
            {
                return BadRequest("Erro ao adicionar curtida.");
            }

            return Ok(curtidaCriada);
        }

        [HttpDelete("{id}/curtida/{curtidaId}")]
        public async Task<IActionResult> DeleteCurtida(int curtidaId)
        {
            var result = await _publicacaoService.DeleteCurtida(curtidaId);

            if (!result)
            {
                return NotFound("Curtida não encontrada ou erro ao removê-la.");
            }

            return NoContent();
        }

        [HttpPost("{id}/comentario")]
        public async Task<IActionResult> PostComentario(int id, [FromBody] ComentarioDTO comentarioDTO)
        {
            comentarioDTO.PublicacaoId = id;
            var comentarioCriado = await _publicacaoService.PostComentario(comentarioDTO);

            if (comentarioCriado == null)
            {
                return BadRequest("Erro ao adicionar comentário.");
            }

            return Ok(comentarioCriado);
        }

        [HttpPut("{id}/comentario/{comentarioId}")]
        public async Task<IActionResult> PutComentario(int id, ComentarioDTO comentarioDTO)
        {
            var buscaComentario = await _publicacaoService.GetComentarioById(id);

            if (buscaComentario == null)
            {
                return NotFound();
            }

            var comentarioAtualizadoDTO = _mapper.Map<ComentarioDTO>(comentarioDTO);

            var retornoComentario = await _publicacaoService.PutComentario(comentarioAtualizadoDTO, id);

            return Ok(retornoComentario);
        }

        [HttpDelete("{id}/comentario/{comentarioId}")]
        public async Task<IActionResult> DeleteComentario(int id)
        {
            var comentario = await _publicacaoService.DeleteComentario(id);

            if (comentario == null)
            {
                return NotFound("Comentário não encontrado ou erro ao removê-lo.");
            }

            return NoContent();
        }
    }
}
