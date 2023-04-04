using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CodeParts.Data.TableModel
{
    public class CodeDb
    {
        public long? id { get; set; }

        public string accountLogin { get; set; }

        public string content { get; set; }

        public string tags { get; set; }
        public string title { get; set; }

        public AccountDb? Account { get; set; }
    }
}
