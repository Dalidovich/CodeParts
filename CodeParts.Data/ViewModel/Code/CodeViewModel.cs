using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeParts.Data.ViewModel.Code
{
    public class CodeViewModel
    {
        [MaybeNull]
        public string userLogin;

        [Required]
        [NotNull]
        public string content { get; set; }

        [MaybeNull]
        public string tags;

        [Required]
        [NotNull]
        public string title { get; set; }
    }

}
