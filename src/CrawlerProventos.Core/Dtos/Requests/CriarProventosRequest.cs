using System.Collections.Generic;
using MediatR;

namespace CrawlerProventos.Core.Dtos.Requests
{
    public class CriarProventosRequest : IRequest<BaseResponseDto<bool>>
    {
        public List<ProventoDto> proventos { get; set; }
    }
}
