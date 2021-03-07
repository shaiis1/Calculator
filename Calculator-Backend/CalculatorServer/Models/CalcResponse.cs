using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculatorServer.Models
{
    public class CalcResponse
    {
        public string Result { set; get; }

        public List<string> AllResults { set; get; }
    }
}
