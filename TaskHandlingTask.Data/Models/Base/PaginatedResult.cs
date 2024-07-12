using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsHandling.Domain.Models.Base
{
    public class PaginatedResult<T>
    {
        public PaginatedResult(IEnumerable<T> data, int count, int page, int pageSize)
        {
            Data = data;
            TotalCount = count;
            CurrentPage = page;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Succeeded = true;
        }

        public PaginatedResult(bool succeeded, IEnumerable<T> data = null, IEnumerable<string> messages = null, int count = 0, int page = 1, int pageSize = 10)
        {
            Data = data;
            CurrentPage = page;
            Succeeded = succeeded;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
            Messages = messages?.ToList() ?? new List<string>();
        }

        public static PaginatedResult<T> Success(IEnumerable<T> data, int count, int page, int pageSize)
        {
            return new PaginatedResult<T>(true, data, null, count, page, pageSize);
        }

        public static PaginatedResult<T> Failure(IEnumerable<string> messages)
        {
            return new PaginatedResult<T>(false, null, messages);
        }


        public IEnumerable<T> Data { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
        public List<string> Messages { get; set; }
        public bool Succeeded { get; set; }
    }

}
