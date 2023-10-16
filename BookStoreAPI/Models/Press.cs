using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreAPI.Models
{
    public class Press
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category category { get; set; }
    }
}