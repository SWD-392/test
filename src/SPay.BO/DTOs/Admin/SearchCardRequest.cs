﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.DTOs.Admin
{
    public class AdminSearchRequest : PagingRequest
    {
        public string Keyword { get; set; }
    }
}