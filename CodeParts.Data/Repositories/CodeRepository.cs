using CodeParts.Data.Interfaces;
using CodeParts.Data.TableModel;
using Microsoft.EntityFrameworkCore;

namespace CodeParts.Data.Repositories
{
    public class CodeRepository : ICodeRepository
    {

        private readonly AppDbContext _db;

        public CodeRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task<bool> createAsync(CodeDb entity)
        {
            await _db.Content.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }
        public IQueryable<CodeDb> GetAll()
        {
            return _db.Content;
        }
        public async Task<bool> deleteAsync(CodeDb entity)
        {
            _db.Content.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> deleteRangeAsync(params CodeDb[] entities)
        {
            _db.Content.RemoveRange(entities);
            await _db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> chengeOwner(string oldOwner, string newOwner)
        {
            await _db.Content.Where(x=>x.accountLogin==oldOwner).ForEachAsync(x=>x.accountLogin= newOwner);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
