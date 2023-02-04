using CodeParts.Data.enums;
using CodeParts.Data.Interfaces;
using CodeParts.Data.Response.Base;
using CodeParts.Data.TableModel;
using CodeParts.Data.ViewModel.Code;
using CodeParts.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CodeParts.Service.Implementations
{
    public class CodeService : ICodeService
    {
        private readonly ICodeRepository _codeRepository;
        private readonly ILogger<CodeService> _logger;

        public CodeService(ICodeRepository codeRepository, ILogger<CodeService> logger)
        {
            _codeRepository = codeRepository;
            _logger = logger;
        }

        public async Task<IBaseResponse<CodeViewModel>> Create(CodeViewModel viewModel)
        {
            try
            {
                var code = new CodeDb()
                {
                    id = null,
                    userLogin= viewModel.userLogin,
                    content= viewModel.content,
                    tags= viewModel.tags,
                    title= viewModel.title
                };
                await _codeRepository.createAsync(code);
                return new BaseResponse<CodeViewModel>()
                {
                    Data = viewModel,
                    StatusCode = StatusCode.CodeCreate,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Create] : {ex.Message}");
                return new BaseResponse<CodeViewModel>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError,
                };
            }
        }

        public async Task<IBaseResponse<bool>> Delete(long id)
        {
            try
            {
                var code = await _codeRepository.GetAll().FirstOrDefaultAsync(x => x.id == id);
                if (code == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "code not found"
                    };
                }

                return new BaseResponse<bool>()
                {
                    Data = await _codeRepository.deleteAsync(code),
                    StatusCode = StatusCode.CodeDelete
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

        public async Task<IBaseResponse<IEnumerable<CodeDb>>> GetContent()
        {
            try
            {
                var contents = await _codeRepository.GetAll().ToListAsync();
                if (contents == null)
                {
                    return new BaseResponse<IEnumerable<CodeDb>>()
                    {
                        Description = "code not found"
                    };
                }
                return new BaseResponse<IEnumerable<CodeDb>>()
                {
                    Data = contents,
                    StatusCode = StatusCode.CodeRead
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[GetContent] : {ex.Message}");
                return new BaseResponse<IEnumerable<CodeDb>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError,
                };
            }
        }
        public async Task<IBaseResponse<CodeDb>> GetOne(long id)
        {
            try
            {
                var code = await _codeRepository.GetAll().FirstOrDefaultAsync(x => x.id == id);
                if (code == null)
                {
                    return new BaseResponse<CodeDb>()
                    {
                        Description = "code not found"
                    };
                }
                return new BaseResponse<CodeDb>()
                {
                    Data = code,
                    StatusCode = StatusCode.CodeRead
                };
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"[GetOne] : {ex.Message}");
                return new BaseResponse<CodeDb>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError,
                };
            }
        }

        public Task<IBaseResponse<CodeViewModel>> Update(CodeViewModel viewModel, long id)
        {
            throw new NotImplementedException();
        }
    }
}
