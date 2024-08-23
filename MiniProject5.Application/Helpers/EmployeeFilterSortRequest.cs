using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject5.Application.Helpers
{
    public class EmployeeFilterSortRequest
    {
        public string Fname { get; set; }
        public string Deptname { get; set; }
        public string Position { get; set; }
        public int Level { get; set; }
        public string Employeetype { get; set; }
        public string SortBy { get; set; }
        public bool SortDescending { get; set; } = false;
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
