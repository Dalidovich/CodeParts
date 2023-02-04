using CodeParts.Data.TableModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeParts.Data.ViewModel.Account
{
    public class IndexAccountViewModel
    {
        public AccountDb account { get; set; }
        public CodeDb[] code { get; set; }
    }
}
