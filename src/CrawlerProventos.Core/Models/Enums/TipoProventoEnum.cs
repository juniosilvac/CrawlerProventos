using System.ComponentModel;

namespace CrawlerProventos.Core.Models.Enums
{
    public enum TipoProventoEnum
    {
        [Description("DIVIDENDO")]
        DIVIDENDO = 1,
        [Description("JRS CAP PROPRIO")]
        JRSCAPPROPRIO = 2,
        [Description("BONIFICAÇÃO")]
        BONIFICACAO = 3,
        [Description("DIREITOS DE SUBSCRIÇÃO")]
        DIREITOSUBSCRICAO = 4
    }
}