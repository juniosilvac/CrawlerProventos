using CrawlerProventos.Core.Dtos;
using CrawlerProventos.Core.Dtos.Requests;
using CrawlerProventos.Core.Interfaces;
using CrawlerProventos.Core.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CrawlerProventos.Core.Services.ProventoUseCases
{
    public class CriarProventosHandler : IRequestHandler<CriarProventosRequest, BaseResponseDto<bool>>
    {
        private readonly IRepository<Provento> _repository;
        private readonly ILogger<CriarProventosHandler> _logger;

        public CriarProventosHandler(IRepository<Provento> repository, ILogger<CriarProventosHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task<BaseResponseDto<bool>> Handle(CriarProventosRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseResponseDto<bool>();
            try
            {
                var proventos = request.proventos.Select(p => new Provento()
                {
                    Id = Guid.NewGuid(),
                    Aprovacao = p.Aprovacao,
                    Preco = p.Preco,
                    ProventoPorUnidade = p.ProventoPorUnidade,
                    TipoAtivoId = (int)p.TipoAtivo,
                    TipoProventoId = (int)p.TipoProvento,
                    Valor = p.Valor,
                    CotacaoPorLoteMil = new CotacaoPorLoteMil()
                    {
                        Id = Guid.NewGuid(),
                        PrecoPorUnidade = p.CotacaoPorLoteMilDto.PrecoPorUnidade,
                        UltimoDia = p.CotacaoPorLoteMilDto.UltimoDia,
                        UltimoDiaPreco = p.CotacaoPorLoteMilDto.UltimoDiaPreco,
                        UltimoPreco = p.CotacaoPorLoteMilDto.UltimoPreco
                    }

                }).ToList();

                await _repository.CreateAsync(proventos);
                response.Data = true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response.Errors.Add("Falha ao listar os proventos.");
            }

            return response;
        }

        public List<ProventoDto> CarregarProventos()
        {
            return new List<ProventoDto>()
            {
                //new ProventoDto()
                //{
                //    Aprovacao = DateTime.UtcNow,
                //    Preco = decimal.Round(1,2),
                //    ProventoPorUnidade = 1,
                //    TipoAtivo =  TipoAtivo.ON.GetDescription(),
                //    TipoProvento = TipoProvento.JRSCAPPROPRIO.GetDescription(),
                //    Valor = decimal.Round(1,2),
                //    CotacaoPorLoteMilDto = new CotacaoPorLoteMilDto()
                //    {
                //        PrecoPorUnidade = 1,
                //        UltimoDia = DateTime.UtcNow,
                //        UltimoDiaPreco = DateTime.UtcNow,
                //        UltimoPreco = decimal.Round(1,2),
                //    }

                //},
                //new ProventoDto()
                //{
                //    Aprovacao = DateTime.UtcNow,
                //    Preco = decimal.Round(1,2),
                //    ProventoPorUnidade = 1,
                //    TipoAtivo = TipoAtivo.ON.GetDescription(),
                //    TipoProvento = TipoProvento.JRSCAPPROPRIO.GetDescription(),
                //    Valor = decimal.Round(1,2),
                //    CotacaoPorLoteMilDto = new CotacaoPorLoteMilDto()
                //    {
                //        PrecoPorUnidade = 1,
                //        UltimoDia = DateTime.UtcNow,
                //        UltimoDiaPreco = DateTime.UtcNow,
                //        UltimoPreco = decimal.Round(1,2),
                //    }

                //},
                //new ProventoDto()
                //{
                //    Aprovacao = DateTime.UtcNow,
                //    Preco = decimal.Round(1,2),
                //    ProventoPorUnidade = 1,
                //    TipoAtivo = TipoAtivo.ON.GetDescription(),
                //    TipoProvento = TipoProvento.JRSCAPPROPRIO.GetDescription(),
                //    Valor = decimal.Round(1,2),
                //    CotacaoPorLoteMilDto = new CotacaoPorLoteMilDto()
                //    {
                //        PrecoPorUnidade = 1,
                //        UltimoDia = DateTime.UtcNow,
                //        UltimoDiaPreco = DateTime.UtcNow,
                //        UltimoPreco = decimal.Round(1,2),
                //    }

                //},
                //new ProventoDto()
                //{
                //    Aprovacao = DateTime.UtcNow,
                //    Preco = decimal.Round(1,2),
                //    ProventoPorUnidade = 1,
                //    TipoAtivo = TipoAtivo.ON.GetDescription(),
                //    TipoProvento = TipoProvento.DIVIDENDO.GetDescription(),
                //    Valor =decimal.Round(1,2),
                //    CotacaoPorLoteMilDto = new CotacaoPorLoteMilDto()
                //    {
                //        PrecoPorUnidade = 1,
                //        UltimoDia = DateTime.UtcNow,
                //        UltimoDiaPreco = DateTime.UtcNow,
                //        UltimoPreco = decimal.Round(1,2),
                //    }

                //},
                //new ProventoDto()
                //{
                //    Aprovacao = DateTime.UtcNow,
                //    Preco = decimal.Round(1,2),
                //    ProventoPorUnidade = 1,
                //    TipoAtivo = TipoAtivo.ON.GetDescription(),
                //    TipoProvento = TipoProvento.DIVIDENDO.GetDescription(),
                //    Valor = decimal.Round(1,2),
                //    CotacaoPorLoteMilDto = new CotacaoPorLoteMilDto()
                //    {
                //        PrecoPorUnidade = 1,
                //        UltimoDia = DateTime.UtcNow,
                //        UltimoDiaPreco = DateTime.UtcNow,
                //        UltimoPreco = decimal.Round(1,2),
                //    }

                //}
            };
        }
    }
}