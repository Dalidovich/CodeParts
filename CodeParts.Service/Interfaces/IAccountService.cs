using CodeParts.Data.Response.Base;
using CodeParts.Data.TableModel;
using CodeParts.Data.ViewModel.Account;
using CodeParts.Data.ViewModel.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CodeParts.Service.Interfaces
{
    public interface IAccountService
    {
        Task<IBaseResponse<AccountViewModel>> GetOne(string login);
        Task<IBaseResponse<IndexAccountViewModel>> GetOneWithCode(string login);
        Task<IBaseResponse<ClaimsIdentity>> Registration(RegisterAccountViewModel viewModel); 
        Task<IBaseResponse<AccountViewModel>> Update(AccountViewModel viewModel, string login);
        Task<IBaseResponse<bool>> Delete(string login);
        Task<IBaseResponse<ClaimsIdentity>> Authentication(LogInAccountViewModel viewModel);
        Task<IBaseResponse<bool>> DeleteAllDataAccount(string login);
    }
}
