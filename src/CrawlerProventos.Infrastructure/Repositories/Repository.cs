using CrawlerProventos.Core.Interfaces;
using CrawlerProventos.Core.Models;
using CrawlerProventos.Core.Models.Enums;
using CrawlerProventos.Infrastructure.Contexts;
using CrawlerProventos.Infrastructure.Extensions;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;


namespace CrawlerProventos.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        private readonly AppDbContext _context;
        private DbSet<T> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task CreateAsync(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAllAsync()
        {
            _context.ExecuteCommand("DELETE FROM provento");
            _context.ExecuteCommand("DELETE FROM cotacaoporlotemil");
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Provento>> GetAllAsync()
        {
            var client = new HttpClient();
            var response = await client.GetAsync("http://bvmf.bmfbovespa.com.br/Cias-Listadas/Empresas-Listadas/ResumoProventosDinheiro.aspx?codigoCvm=21610&tab=3.1&idioma=pt-br");
            var pageContents = await response.Content.ReadAsStringAsync();

            var pageDocument = new HtmlDocument();
            pageDocument.LoadHtml(pageContents);

            var rows = pageDocument
                .DocumentNode
                .SelectNodes("//table[@id='ctl00_contentPlaceHolderConteudo_grdProventos_ctl01']//tbody//tr");

            var proventos = new List<Provento>();

            if(rows != null && rows.Count > 0)
            {
                foreach (var row in rows)
                {
                    var cols = row.SelectNodes("td");
                    proventos.Add(new Provento()
                    {
                        TipoAtivoId = (int)cols[0].InnerText.GetEnumValue<TipoAtivoEnum>(),
                        Aprovacao = Convert.ToDateTime(cols[1].InnerText),
                        Valor = Convert.ToDecimal(cols[2].InnerText),
                        ProventoPorUnidade = Convert.ToInt32(cols[3].InnerText),
                        TipoProventoId = (int)cols[4].InnerText.GetEnumValue<TipoProventoEnum>(),
                        CotacaoPorLoteMil = new CotacaoPorLoteMil()
                        {
                            UltimoDia = Convert.ToDateTime(cols[5].InnerText),
                            UltimoDiaPreco = Convert.ToDateTime(cols[6].InnerText),
                            UltimoPreco = Convert.ToDecimal(cols[7].InnerText),
                            PrecoPorUnidade = Convert.ToInt32(cols[8].InnerText),
                        },
                        Preco = Convert.ToDecimal(cols[9].InnerText)
                    });
                }
            }

            return proventos;
        }

        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
