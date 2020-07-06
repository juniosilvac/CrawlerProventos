using CrawlerProventos.Core.Dtos;
using CrawlerProventos.Core.Dtos.Requests;
using CrawlerProventos.Core.Interfaces;
using CrawlerProventos.Core.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CrawlerProventos.Core.Services.ProventoUseCases
{
    public class DeletarProventosHandler : IRequestHandler<DeletarProventosRequest, BaseResponseDto<bool>>
    {

        private readonly IRepository<Provento> _repository;
        private readonly ILogger<DeletarProventosHandler> _logger;

        public DeletarProventosHandler(IRepository<Provento> repository, ILogger<DeletarProventosHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }


        public async Task<BaseResponseDto<bool>> Handle(DeletarProventosRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseResponseDto<bool>();
            try
            {
                await _repository.DeleteAllAsync();
                response.Data = true;

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
