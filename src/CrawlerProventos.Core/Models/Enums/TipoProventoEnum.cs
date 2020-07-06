using System.ComponentModel;

namespace CrawlerProventos.Core.Models.Enums
{
    public enum TipoProventoEnum
    {
        [Description("DIVIDENDO")]
        DIVIDENDO = 1,
        [Description("JRS CAP PROPRIO")]
        JRSCAPPROPRIO = 2,
        [Description("BONIFICA��O")]
        BONIFICACAO = 3,
        [Description("DIREITOS DE SUBSCRI��O")]
        DIREITOSUBSCRICAO = 4
    }
}