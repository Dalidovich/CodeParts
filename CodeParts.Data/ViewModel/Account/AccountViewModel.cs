﻿using CodeParts.Data.enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeParts.Data.ViewModel.Account
{
    public class AccountViewModel
    {
        public string login { get; set; }
        public string password { get; set; }
        public string nickname { get; set; }
        public string email { get; set; }
        public Role role { get; set; }
    }
}
