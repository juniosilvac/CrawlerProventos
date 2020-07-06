using CrawlerProventos.Core.Dtos;
using CrawlerProventos.Core.Dtos.Requests;
using CrawlerProventos.Core.Interfaces;
using CrawlerProventos.Core.Models;
using CrawlerProventos.Core.Services.ProventoUseCases;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using CrawlerProventos.Core.Models.Enums;

namespace CrawlerProventos.Core.Services.CrawlerProventosUseCases
{
    public class ObterProventosHandler : IRequestHandler<ObterProventosRequest, BaseResponseDto<List<ProventoDto>>>
    {
        private readonly IRepository<Provento> _repository;
        private readonly ILogger<ObterProventosHandler> _logger;
        public ObterProventosHandler(IRepository<Provento> repository, ILogger<ObterProventosHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<BaseResponseDto<List<ProventoDto>>> Handle(ObterProventosRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseResponseDto<List<ProventoDto>>();
            try
            {
                var proventos = (await _repository.GetAllAsync()).Select(p => new ProventoDto()
                {
                    TipoAtivo = (TipoAtivoEnum)p.TipoAtivoId,
                    Aprovacao =p.Aprovacao,
                    Valor = p.Valor,
                    ProventoPorUnidade = p.ProventoPorUnidade,
                    TipoProvento = (TipoProventoEnum)p.TipoProventoId,
                    CotacaoPorLoteMilDto = new CotacaoPorLoteMilDto()
                    {
                        UltimoDia = p.CotacaoPorLoteMil.UltimoDia,
                        UltimoDiaPreco = p.CotacaoPorLoteMil.UltimoDiaPreco,
                        UltimoPreco = p.CotacaoPorLoteMil.UltimoPreco,
                        PrecoPorUnidade = p.CotacaoPorLoteMil.PrecoPorUnidade,
                    },
                    Preco = p.Preco
                }).ToList();
                response.Data = proventos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response.Errors.Add("Falha ao listar os proventos.");
            }

            return response;
        }
    }
}
