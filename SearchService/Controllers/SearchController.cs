using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;
using SearchService.Dto;

namespace SearchService.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Product>>> SearchProducts(string searchParam, int pageNum = 1, int pageSize = 4)
        {
            var query = DB.PagedSearch<Product>();

            query.Sort(x => x.Ascending(a => a.Brand));

            // add searchParam
            if (!string.IsNullOrEmpty(searchParam) )
            {
                query.Match(Search.Full, searchParam).SortByTextScore();
            }

            query.PageNumber(pageNum);
            query.PageSize(pageSize);

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
