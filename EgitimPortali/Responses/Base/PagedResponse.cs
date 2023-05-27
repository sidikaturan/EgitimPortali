namespace EgitimPortali.Responses.Base
{
    public class PagedResponse<T>
    {
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public int PageSize { get; set; }
        public Boolean HasNext { get; set; }
        public Boolean HasPrevious { get; set; }
        public List<T> Data { get; set; }

        public PagedResponse(List<T> data,int pageNumber, int pageSize,int totalRecords)
        {
            if(pageSize<1) pageSize=1;
            PageNumber=pageNumber;
            PageSize=pageSize;
            Data = data;
            TotalRecords = totalRecords;
            TotalPages = (TotalRecords / PageSize)+1;
            HasNext = PageNumber < TotalPages ? true : false;
            HasPrevious=PageNumber>1? true : false;
        }
    }
}
