using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AppClient.Models
{
    public class Press
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        public string category { get; set; }
    }
}