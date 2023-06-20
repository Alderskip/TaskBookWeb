using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskbookServer.Models
{
    public class PaginationFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int groupid { get; set; }
        public string SortСolumn { get; set; }
        public string SortBy { get; set; }
        public string stringtofind { get; set; }
        public string typeOfStringtofind { get; set; }
        public PaginationFilter()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
            this.SortСolumn = "Id";
            this.SortBy = "ASC";
        }
        public PaginationFilter(int pageNumber, int pageSize, string SortСolumn = "Id", string SortBy = "ASC")
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize > 10 ? 10 : pageSize;
            this.SortСolumn = SortСolumn;
            this.SortBy = SortBy;
        }
    }
}
