using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Apriori.Model
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Support { get; set; }
    }
}
