using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace BookStoreAPI.Controllers
{
    public class PressesController : ODataController
    {
        private readonly MyDataContext _db;
        public PressesController(MyDataContext db)
        {
            _db = db;
        }
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_db.Presses.ToList());
        }
        [EnableQuery]

        public IActionResult Get(int key)
        {
            return Ok(_db.Presses.FirstOrDefault(b => b.Id == key));
        }

    }
}