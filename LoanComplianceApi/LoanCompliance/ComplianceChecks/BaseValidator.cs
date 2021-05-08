using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoanComplianceApi.LoanCompliance;

namespace LoanComplianceApi.LoanCompliance.ComplianceChecks
{
    public abstract class BaseValidator : IValidator
    {
        public virtual State State => throw new NotImplementedException();

        public virtual ComplianceCheck Validate(Loan loan)
        {
            throw new NotImplementedException();
        }
    }
}
