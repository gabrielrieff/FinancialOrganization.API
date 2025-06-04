using FinancialOrganization.API.Domain.Enums;

namespace FinancialOrganization.API.Communication.Request;

public class SearchListRequest
{
    public int Page { get; set;}
    public int PerPage { get; set;}
    public string Search { get; set;}
    public string Sort { get; set;}
    public SearchOrder Dir { get; set; }

    public SearchListRequest(int page = 1, 
        int perPage = 5, 
        string search = "", 
        string sort = "", 
        SearchOrder dir = SearchOrder.Asc)
    {
        Page = page;
        PerPage = perPage;
        Search = search;
        Sort = sort;
        Dir = dir;
    }
}
