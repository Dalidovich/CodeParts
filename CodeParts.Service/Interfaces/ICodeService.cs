using CodeParts.Data.Response.Base;
using CodeParts.Data.TableModel;
using CodeParts.Data.ViewModel.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeParts.Service.Interfaces
{
    public interface ICodeService
    {
        Task<IBaseResponse<IEnumerable<CodeDb>>> GetContent();
        Task<IBaseResponse<CodeDb>> GetOne(long id);
        Task<IBaseResponse<CodeViewModel>> Create(CodeViewModel viewModel);
        Task<IBaseResponse<CodeViewModel>> Update(CodeViewModel viewModel, long id);
        Task<IBaseResponse<bool>> Delete(long id);
    }
}
