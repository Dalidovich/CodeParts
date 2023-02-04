using CodeParts.Data.TableModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeParts.Data.Interfaces
{
    public interface ICodeRepository:IBaseRepository<CodeDb>
    {
        public Task<bool> deleteRangeAsync(params CodeDb[] entities);
        public Task<bool> chengeOwner(string oldOwner,string newOwner);
    }
}
