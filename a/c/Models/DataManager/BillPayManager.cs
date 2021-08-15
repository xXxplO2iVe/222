using System.Linq;
using System.Collections.Generic;
using WebAPI.Data;
using WebAPI.Models.Repository;


namespace WebAPI.Models.DataManager
{
    public class BillPayManager : IDataRepository<BillPay, int>
    {
        private readonly MCBAContext _context;
        public BillPayManager(MCBAContext context)
        {
            _context = context;
        }

        // Add new BillPay to database.
        public int Add(BillPay billPay)
        {
            _context.BillPays.Add(billPay);
            _context.SaveChanges();

            return billPay.BillPayID;
        }

        // Delete BillPay from database.
        public int Delete(int id)
        {
            _context.BillPays.Remove(_context.BillPays.Find(id));
            _context.SaveChanges();

            return id;
        }

        // Get BillPay for specified ID from database.
        public BillPay Get(int id)
        {
            return _context.BillPays.Find(id);
        }

        // Get all BillPay from database. Return as a list.
        public IEnumerable<BillPay> GetAll()
        {
            return _context.BillPays.ToList();
        }

        // Update BillPay information for specified ID.
        public int Update(int id, BillPay item)
        {
            throw new System.NotImplementedException();
        }

        // Change BillPay status. Block or unblock BillPay. 
        public void BlockUnblock(int id)
        {
            var billPay = Get(id);

            if (billPay.Blocked == true)
            {
                billPay.Blocked = false;
            }
            else
            {
                billPay.Blocked = true;
            }

             _context.SaveChanges();
        }
    }
}
