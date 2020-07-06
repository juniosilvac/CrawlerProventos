using System.Collections.Generic;
using System.Linq;

namespace CrawlerProventos.Core.Dtos
{
    public class BaseResponseDto<T>
    {
        public BaseResponseDto()
        {
            Errors = new List<string>();
        }
        public bool HasError => Errors.Any();
        public List<string> Errors {get; set;}
        public int Total {get; set;}
        public T Data {get; set;}        
    }
}