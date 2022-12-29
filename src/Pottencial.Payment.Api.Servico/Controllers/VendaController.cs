using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pottencial.Payment.Api.Aplicacao.Commands.AtualizarStatusDaVenda;
using Pottencial.Payment.Api.Aplicacao.Commands.CadastrarVenda;
using Pottencial.Payment.Api.Aplicacao.Queries.BuscarVendaPorId;
using Pottencial.Payment.Api.Aplicacao.Queries.ListarTodasAsVendas;
using Pottencial.Payment.Api.Dominio.Enum;
using Pottencial.Payment.Api.Dominio.Excecoes;
using Pottencial.Payment.Api.Infraestrutura.Repositorios;

namespace Pottencial.Payment.Api.Servico.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VendaController : ControllerBase
    {
        public static IWebHostEnvironment Enviroment;

        public IMediator Mediator;

        private readonly ILogger<VendaController> _logger;

        public VendaController(IMediator mediator, ILogger<VendaController> logger)
        {
            Mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Criação de um novo registro.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<ActionResult> CadastraVenda([FromBody] CadastrarVendaRequest request)
        {
            if (ModelState.IsValid)
            {
                CadastrarVendaResponse response = await Mediator.Send(request);

                return CreatedAtAction(nameof(BuscarVendaPorId), new { Id = response.Venda.Id }, response );
            }
            else
            {
                _logger.LogInformation("Modelo inválido", ModelState);
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Listar todas as vendas.
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public async Task<ActionResult> ListarTodasAsVendas()
        {
            ListarTodasAsVendasQuery request = new ListarTodasAsVendasQuery();

            ListarTodasAsVendasResponse listaDeVendas = await Mediator.Send(request);

            return Ok(listaDeVendas);
        }

        /// <summary>
        /// Busca venda por id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult> BuscarVendaPorId([FromRoute] Guid id)
        {
            try
            {
                BuscarVendaPorIdQuery request = new BuscarVendaPorIdQuery();
                request.Id = id;

                BuscarVendaPorIdResponse venda = await Mediator.Send(request);
                return Ok(venda);
            }
            catch (VendaNaoLocalizadaException ex)
            {
                return BadRequest("Erro: " + ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro Genérico: " + ex.Message);
            }
        }

        /// <summary>
        /// Atualiza status da venda.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> AtualizarStatusDaVenda([FromRoute] Guid id, [FromBody] AtualizarStatusDaVendaRequest request)
        {
            if (id == request.Id)
            {
                try
                {
                    AtualizarStatusDaVendaResponse venda = await Mediator.Send(request);
                    return Ok(venda);
                }
                catch (VendaNaoLocalizadaException ex)
                {
                    _logger.LogError($"Erro: " + ex.Message);
                    return BadRequest("Erro: " + ex.Message);
                }
                catch (AtualizacaoDeStatusIncompativelException ex)
                {
                    _logger.LogError($"ERRO: Atualização de Status da Venda não Permitida!");
                    return BadRequest("Erro: " + ex.Message);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"ERRO: Não tratado - {ex.Message}");
                    return BadRequest("Erro Genérico");
                }
            }
            else
            {
                _logger.LogError($"ERRO: IDs divergentes - Route: {id} - Body {request.Id}");
                return BadRequest("Erro: Dados do ID divergentes!");
            }
        }
    }
}
