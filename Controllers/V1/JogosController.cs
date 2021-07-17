using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using catJogos.Expcetions;
using catJogos.InputModel;
using catJogos.Services;
using catJogos.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace catJogos.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class JogosController : ControllerBase
    {
        private readonly IJogoService _jogosService;
        public JogosController(IJogoService jogosService)
        {
            _jogosService = jogosService;
        }


        [HttpGet]
        public async Task<ActionResult<List<JogoViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var jogos = await _jogosService.Obter(pagina,quantidade);
            if (jogos.Count() == 0)
            {
                return NoContent();
            }
            return Ok(jogos);
        }

        [HttpGet("{idJogo:guid}")]
        public async Task<ActionResult<JogoViewModel>> Obter([FromRoute] Guid idJogo)
        {
            var jogo = await _jogosService.Obter(idJogo);
            if (jogo == null)
            {
                return NoContent();
            }
            return Ok(jogo);
        }

        [HttpPost]
        public async Task<ActionResult<JogoViewModel>> InserirJogo([FromBody] JogoInputModel jogoInputModel)
        {
            try
            {
                var jogo = await _jogosService.Inserir(jogoInputModel);
                return Ok(jogo);
            }
            catch (JogoJaCadastradoException ex)
            {
                return UnprocessableEntity("Já existe um jogo com este nome para esta produtora");
            }
        }

        [HttpPut("{idJogo:guid}")]
        public async Task<ActionResult> AtualizarJogo([FromRoute] Guid idJogo, [FromBody] JogoInputModel jogoInputModel)
        {
            try
            {
                await _jogosService.Atualizar(idJogo, jogoInputModel);
                return Ok();
            }
            catch (JogoJaCadastradoException ex)
            {
                return NotFound("Jogo não existe");
            }
        }

        [HttpPatch("{idJogo:guid}/preco/{preco:double}")]
        public async Task<ActionResult> AtualizarJogo([FromRoute] Guid idJogo, [FromRoute] double preco)
        {
            try
            {
                await _jogosService.Atualizar(idJogo, preco);
                return Ok();
            }
            catch (JogoJaCadastradoException ex)
            {
                return NotFound("Jogo não existe");
            }
        }

        [HttpDelete("{idJogo:guid}")]
        public async Task<ActionResult> ApagarJogo([FromRoute] Guid idJogo)
        {
            try
                {
                    await _jogosService.Remover(idJogo);
                    return Ok();
                }
                catch (JogoJaCadastradoException ex)
                {
                    return NotFound("Jogo não existe");
                }
        }

    }
}