using CodeParts.Data.enums;
using CodeParts.Data.Interfaces;
using CodeParts.Data.Response.Base;
using CodeParts.Data.TableModel;
using CodeParts.Data.ViewModel.Account;
using CodeParts.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace CodeParts.Service.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ICodeRepository _codeRepository;
        private readonly ILogger<AccountService> _logger;

        public AccountService(IAccountRepository accountRepository, ICodeRepository codeRepository, ILogger<AccountService> logger)
        {
            _accountRepository = accountRepository;
            _codeRepository = codeRepository;
            _logger = logger;
        }

        public async Task<IBaseResponse<ClaimsIdentity>> Authentication(LogInAccountViewModel viewModel)
        {
            try
            {
                var account = await _accountRepository.GetAll().FirstOrDefaultAsync(x => x.login == viewModel.login);
                if (account == null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "account not found"

                    };
                }
                if (viewModel.password != account.password)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "invalid password"

                    };
                }
                var ressult = Autheticate(account);
                return new BaseResponse<ClaimsIdentity>()
                {
                    Data=ressult,
                    StatusCode=StatusCode.AccountRead
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Authentication] : {ex.Message}");
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<bool>> Delete(string login)
        {
            try
            {
                var account = await _accountRepository.GetAll().FirstOrDefaultAsync(x => x.login == login);
                var code = await _codeRepository.GetAll().Where(x => x.accountLogin == login).ToArrayAsync();
                if (account == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "account not found"
                    };
                }
                
                return new BaseResponse<bool>()
                {
                    Data = await _accountRepository.deleteAsync(account)&&
                        await _codeRepository.chengeOwner(login,"admin"),
                    StatusCode = StatusCode.AccountDelete
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Delete] : {ex.Message}");
                return new BaseResponse<bool>()
                {
                    Description =ex.Message,
                    StatusCode = StatusCode.InternalServerError,
                };
            }
        }
        public async Task<IBaseResponse<bool>> DeleteAllDataAccount(string login)
        {
            try
            {
                var account = await _accountRepository.GetAll().FirstOrDefaultAsync(x => x.login == login);
                var code=await _codeRepository.GetAll().Where(x => x.accountLogin == login).ToArrayAsync();
                if (account == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "account not found"
                    };
                }

                return new BaseResponse<bool>()
                {
                    Data = await _accountRepository.deleteAsync(account)&&
                        await _codeRepository.deleteRangeAsync(code),
                    StatusCode = StatusCode.AccountDelete
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Delete] : {ex.Message}");
                return new BaseResponse<bool>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError,
                };
            }
        }
        public async Task<IBaseResponse<IndexAccountViewModel>> GetOneWithCode(string login)
        {
            try
            {
                var account = await _accountRepository.GetAll().FirstOrDefaultAsync(x => x.login == login);
                var code = await _codeRepository.GetAll().Where(x => x.accountLogin == login).ToArrayAsync();
                var indexAccount = new IndexAccountViewModel()
                {
                    account=account,
                    code=code
                };
                if (account == null)
                {
                    return new BaseResponse<IndexAccountViewModel>()
                    {
                        Description = "account not found"
                    };
                }
                return new BaseResponse<IndexAccountViewModel>()
                {
                    Data = indexAccount,
                    StatusCode = StatusCode.AccountRead
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[GetOne] : {ex.Message}");
                return new BaseResponse<IndexAccountViewModel>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError,
                };
            }
        }
        public async Task<IBaseResponse<AccountViewModel>> GetOne(string login)
        {
            try
            {
                var account = await _accountRepository.GetAll().FirstOrDefaultAsync(x => x.login == login);
                var indexAccount = new AccountViewModel()
                {
                    login = account.login,
                    password = account.password,
                    nickname = account.nickname,
                    email = account.email
                };
                if (account == null)
                {
                    return new BaseResponse<AccountViewModel>()
                    {
                        Description = "account not found"
                    };
                }
                return new BaseResponse<AccountViewModel>()
                {
                    Data = indexAccount,
                    StatusCode = StatusCode.AccountRead
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[GetOne] : {ex.Message}");
                return new BaseResponse<AccountViewModel>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError,
                };
            }
        }
        public async Task<IBaseResponse<ClaimsIdentity>> Registration(RegisterAccountViewModel viewModel)
        {
            try
            {
                var account = await _accountRepository.GetAll().FirstOrDefaultAsync(x => x.login == viewModel.login);
                if (account != null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description="user with that login alredy exist"
                    };
                }
                account = new AccountDb()
                {
                    login = viewModel.login,
                    password = viewModel.password,
                    role = Role.standart
                };
                await _accountRepository.createAsync(account);
                var result = Autheticate(account);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    Description = "Account added",
                    StatusCode= StatusCode.AccountCreate
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Registration] : {ex.Message}");
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<AccountViewModel>> Update(AccountViewModel viewModel, string login)
        {
            throw new NotImplementedException();
        }

        private ClaimsIdentity Autheticate(AccountDb account)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType,account.login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType,account.role.ToString())
            };
            return new ClaimsIdentity(claims,"ApplicationCookie",ClaimsIdentity.DefaultNameClaimType,ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}
