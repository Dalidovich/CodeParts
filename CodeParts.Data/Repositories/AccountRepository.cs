using CodeParts.Data.Interfaces;
using CodeParts.Data.TableModel;

namespace CodeParts.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _db;

        public AccountRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<bool> createAsync(AccountDb entity)
        {
            await _db.Account.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> deleteAsync(AccountDb entity)
        {
            _db.Account.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public IQueryable<AccountDb> GetAll()
        {
            return  _db.Account;
        }
    }
}
