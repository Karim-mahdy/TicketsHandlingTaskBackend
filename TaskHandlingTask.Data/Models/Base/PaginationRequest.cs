namespace TicketsHandling.Domain.Models.Base
{
    public class PaginationRequest
    {
        public int PageSize { get; set; } = 5;
        public int PageNumber { get; set; } = 1;

    }

}
