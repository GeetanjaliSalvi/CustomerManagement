﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerManagementSystemAPI.Models
{
    public class Customer
    {
        public int CustomersId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
    }
}