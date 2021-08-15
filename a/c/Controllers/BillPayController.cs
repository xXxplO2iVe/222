using System.Collections.Generic;
using WebAPI.Models.DataManager;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Models.DataController
{
    [ApiController, Route("api/[controller]")]
    public class BillPayController : ControllerBase
    {
        private readonly BillPayManager _repo;
        public BillPayController(BillPayManager repo)
        {
            _repo = repo;
        }

        // Return BillPay for specified ID.
        [HttpGet("{id}")]
        public BillPay Get(int id)
        {
            return _repo.Get(id);
        }

        // Return all BillPay.
        [HttpGet]
        public IEnumerable<BillPay> GetAll()
        {
            return _repo.GetAll();
        }

        // Change BillPay status for specified ID.
        [HttpPut("{id}")]
        public void BlockUnblock(int id)
        {
           _repo.BlockUnblock(id);
        }
    }
}