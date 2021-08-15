﻿using System.Collections.Generic;
using WebAPI.Models.DataManager;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Models.DataController
{
    [ApiController, Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerManager _repo;
        public CustomerController(CustomerManager repo)
        {
            _repo = repo;
        }

        // Return all customers. 
        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            return _repo.GetAll();
        }

        // Return customer for specified ID. 
        [HttpGet("{id}")]
        public Customer Get(int id)
        {
            return _repo.Get(id);
        }

        // Update customer information. 
        [HttpPut]
        public void Put([FromBody] Customer customer)
        {
            _repo.Update(customer.CustomerID, customer);
        }

        // Change customer status for specified ID. 
        [HttpPut("{id}")]
        public void Lock(int id)
        {
            _repo.LockUnlock(id);
        }

        // Delete customer for specified ID. 
        [HttpDelete("{id}")]
        public long Delete(int id)
        {
            return _repo.Delete(id);
        }
    }
}
