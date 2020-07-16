using CrawlerProventos.Core.Dtos.Requests;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CrawlerProventos.Core.Services.CrawlerProventosUseCases
{
    public class ServiceProventosHandler : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        public ServiceProventosHandler(ILogger<ServiceProventosHandler> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("O serviço está sendo executado.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
            TimeSpan.FromSeconds(86400)); // 24 horas

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            try
            {
                _logger.LogInformation("--- O serviço está trabalhando. ---\n\n\n");
                _logger.LogInformation("O serviço está realizando a busca dos proventos.");
                var proventosRequest = _mediator.Send(new ObterProventosRequest());               

                if (proventosRequest.Result.HasError)
                {
                    _logger.LogError(proventosRequest.Result.Errors.ToString());
                }
                else
                {
                    _logger.LogInformation("Removendo os registros de Cotação e Proventos.");
                    var proventosDeletados = _mediator.Send(new DeletarProventosRequest());
                   
                    if (proventosDeletados.Result.Data)
                    {
                        _logger.LogInformation("Inserindo novos registros de Cotação e Proventos.");
                        var criarProventos = _mediator.Send(new CriarProventosRequest()
                        {
                            proventos = proventosRequest.Result.Data
                        });

                        if (criarProventos.Result.HasError)
                        {
                            _logger.LogError(criarProventos.Result.Errors.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("O serviço está sendo parado.");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
