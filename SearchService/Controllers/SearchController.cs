using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;
using SearchService.Dto;
using SearchService.RequestHelpers;

namespace SearchService.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Product>>> SearchProducts([FromQuery] SearchParam searchParam)
        {
            var query = DB.PagedSearch<Product, Product>();

            query.Sort(x => x.Ascending(a => a.Brand));

            // add searchParam
            if (!string.IsNullOrEmpty(searchParam.SearchTerms) )
            {
                query.Match(Search.Full, searchParam.SearchTerms).SortByTextScore();
            }

            query = searchParam.OrderBy switch
            {
                "price" => query.Sort(x => x.Ascending(a => a.Price)),
                "new" => query.Sort(x => x.Descending(a => a.Created)),
                _ => query.Sort(x => x.Ascending(a => a.Status))
            };

            /*query = searchParam.FilterBy switch
            {
                "renew" => query.Match(x => x.Created < DateTime.UtcNow),
                _ => query.Match(x => x.Created > DateTime.UtcNow)
            };*/

            if(!string.IsNullOrEmpty(searchParam.Seller)) 
            {
                query.Match(x => x.Seller == searchParam.Seller);
            }

            if (!string.IsNullOrEmpty(searchParam.Buyer)) 
            {
                query.Match(x => x.Buyer == searchParam.Buyer);
            }

            query.PageNumber(searchParam.PageNumber);
            query.PageSize(searchParam.PageSize);

            var result = await query.ExecuteAsync();

            return Ok (new
            {
                result = result.Results,
                pageCount = result.PageCount,
                totalCount = result.TotalCount
            });
        }
    }
}
