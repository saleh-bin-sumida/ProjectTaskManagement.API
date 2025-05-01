namespace ProjectTaskManagement.API.Helper;

public class PagedResult<T>
{
    public int TotalRecords { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public IEnumerable<T> Data { get; set; }

    public PagedResult(int totalRecords, int pageNumber, int pageSize, IEnumerable<T> data)
    {
        TotalRecords = totalRecords;
        PageNumber = pageNumber;
        PageSize = pageSize;
        Data = data;
    }

    public static PagedResult<T> Create(IEnumerable<T> data, int totalRecords, int pageNumber, int pageSize)
    {
        return new PagedResult<T>(totalRecords, pageNumber, pageSize, data);
    }
}
