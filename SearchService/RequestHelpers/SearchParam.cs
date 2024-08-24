namespace SearchService.RequestHelpers
{
    public class SearchParam
    {
        public string SearchTerms { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 4;
        public string Seller {  get; set; }
        public string Buyer { get; set; }
        public string OrderBy { get; set; }
        public string FilterBy { get; set; }
    }
}
