﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject5.Application.Dtos
{
    public class EmployeeDetailDto
    {
        public int? EmpNo { get; set; }
        public string EmployeeName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public string SupervisorName { get; set; }
        public string EmployeeType { get; set; }
    }
}
