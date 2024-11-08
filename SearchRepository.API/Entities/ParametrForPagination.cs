namespace SearchRepository.API.Entities;

public class ParametrsForPaginations
{
    const int maxPageSize = 50;

    public int PageNumber {get; set;}

    private int _pageSize = 5;
    public int PageSize{ 
        get{
        return _pageSize;

        }
        set {
        _pageSize = (value > maxPageSize )? maxPageSize : value;
        }}

}