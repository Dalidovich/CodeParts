using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CodeParts.Data.TableModel
{
    public class CodeDb
    {
        [Key]
        [NotNull]
        public long? id { get; set; }

        [NotNull]
        public string userLogin { get; set; }

        [NotNull]
        public string content { get; set; }

        [MaybeNull]
        public string tags { get; set; }

        [MaybeNull]
        public string title { get; set; }
    }
}
