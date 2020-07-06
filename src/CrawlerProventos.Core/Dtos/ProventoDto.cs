using CrawlerProventos.Core.Models.Enums;
using System;

namespace CrawlerProventos.Core.Dtos
{
    public class ProventoDto
    {  
        public Guid Id { get; set; }
        public DateTime Aprovacao { get; set; }
        public decimal Valor { get; set; }
        public decimal Preco { get; set; }
        public int ProventoPorUnidade { get; set; }
        public TipoProventoEnum TipoProvento { get; set; }

        public TipoAtivoEnum TipoAtivo { get; set; }

        public CotacaoPorLoteMilDto CotacaoPorLoteMilDto { get; set; }
    }
}