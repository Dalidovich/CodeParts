using CodeParts.Data.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeParts.Data.Response.Base
{
    public interface IBaseResponse<T>
    {
        T Data { get; set; }
        StatusCode StatusCode { get; set; }
        public string Description { get; set; }
    }
}
