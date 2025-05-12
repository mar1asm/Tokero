using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tokero.TestData.Models
{
    public class CoinPairDetails
    {
        public decimal Price { get; set; }
        public List<decimal[]> Asks { get; set; }
        public List<decimal[]> Bids { get; set; }
    }

}
