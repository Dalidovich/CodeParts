using CodeParts.Data.enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeParts.Data.TableModel
{
    public class AccountDb
    {
        [Key]
        [NotNull]
        public string login { get; set; }

        [NotNull]
        public string password { get; set; }

        [MaybeNull]
        public string nickname { get; set; }

        [MaybeNull]
        public string email { get; set; }
        public Role role { get; set; }
    }
}
