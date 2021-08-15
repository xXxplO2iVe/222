using System.Linq;
using System.Collections.Generic;
using WebAPI.Data;
using WebAPI.Models.Repository;


namespace WebAPI.Models.DataManager
{
    public class AccountManager : IDataRepository<Account, int>
    {
        private readonly MCBAContext _context;
        public AccountManager(MCBAContext context)
        {
            _context = context;
        }

        // Add new account to database.
        public int Add(Account account)
        {
            _context.Accounts.Add(account);
            _context.SaveChanges();

            return account.AccountNumber;
        }

        // Delete account from database.
        public int Delete(int id)
        {
            _context.Accounts.Remove(_context.Accounts.Find(id));
            _context.SaveChanges();

            return id;
        }

        // Get account for specified ID from database.
        public Account Get(int id)
        {
            return _context.Accounts.Find(id);
        }

        // Get all accounts from database. Return as a list.
        public IEnumerable<Account> GetAll()
        {
            return _context.Accounts.ToList();
        }

        // Update account information for specified ID.
        public int Update(int id, Account account)
        {
            _context.Update(account);
            _context.SaveChanges();

            return id;
        }
    }
}
