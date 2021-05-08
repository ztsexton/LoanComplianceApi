using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoanComplianceApi.LoanCompliance.ComplianceChecks;

namespace LoanComplianceApi.LoanCompliance.ComplianceEngine
{
    public class ComplianceEngine : IComplianceEngine
    {
        private readonly IEnumerable<IValidator> _validators;

        public ComplianceEngine(IEnumerable<IValidator> validators)
        {
            _validators = validators;
        }

        public LoanComplianceResult Validate(Loan loan)
        {
            var complianceChecks = new List<ComplianceCheck>();

            foreach (var validator in _validators.Where(x => x.State == loan.State))
            {
                complianceChecks.Add(validator.Validate(loan));
            }

            var loanComplianceResult = CreateLoanComplianceResult(complianceChecks);

            return loanComplianceResult;
        }

        private static LoanComplianceResult CreateLoanComplianceResult(List<ComplianceCheck> complianceChecks)
        {
            LoanComplianceResult loanComplianceResult = new LoanComplianceResult
            {
                ComplianceChecks = complianceChecks,
                IsCompliant = false
            };

            if (loanComplianceResult.ComplianceChecks.Count == 0)
            {
                loanComplianceResult.IsCompliant = true;
            }

            if (loanComplianceResult.ComplianceChecks.All(x => x.Passed))
            {
                loanComplianceResult.IsCompliant = true;
            }

            return loanComplianceResult;
        }
    }
}
