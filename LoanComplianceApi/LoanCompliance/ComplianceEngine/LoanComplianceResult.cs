using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanComplianceApi.LoanCompliance
{
    public class LoanComplianceResult
    {
        public bool IsCompliant { get; set; }
        public List<ComplianceCheck> ComplianceChecks { get; set; }
    }
}
