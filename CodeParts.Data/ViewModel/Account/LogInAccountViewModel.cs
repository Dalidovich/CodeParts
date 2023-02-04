using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeParts.Data.ViewModel.Account
{
    public class LogInAccountViewModel
    {
        [Required(ErrorMessage = "input login")]
        public string login { get; set; }

        [Required(ErrorMessage = "input password")]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
