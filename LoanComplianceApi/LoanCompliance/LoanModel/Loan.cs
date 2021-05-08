using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanComplianceApi.LoanCompliance
{
    public class Loan
    {
        public State State { get; set; }
        public decimal LoanAmount { get; set; }
        public decimal Apr { get; set; }
        public List<Fee> Fees { get; set; }
        public LoanType LoanType { get; set; }
        public bool PrimaryOccupancy { get; set; }
    }
}
