using System;

namespace CrawlerProventos.Core.Models
{
    public class CotacaoPorLoteMil : EntityBase
    {
        public DateTime UltimoDia {get; set;}
        public DateTime UltimoDiaPreco {get; set;}
        public decimal UltimoPreco {get; set;}
        public int PrecoPorUnidade {get; set;}
    }
}