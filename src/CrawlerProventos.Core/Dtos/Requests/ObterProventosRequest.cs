using MediatR;
using System.Collections.Generic;

namespace CrawlerProventos.Core.Dtos.Requests
{
    public class ObterProventosRequest : IRequest<BaseResponseDto<List<ProventoDto>>>
    {
    }
}
