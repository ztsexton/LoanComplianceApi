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

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + IsCompliant.GetHashCode();
            if (ComplianceChecks != null || ComplianceChecks.Count != 0)
            {
                var complianceChecks = ComplianceChecks.OrderBy(x => x.ComplianceType);
                foreach (var complianceCheck in complianceChecks)
                {
                    hash = hash * 23 + complianceCheck.ComplianceType.GetHashCode();
                    hash = hash * 23 + complianceCheck.Passed.GetHashCode();
                }
            }
           

            return hash;
        }
    }
}
