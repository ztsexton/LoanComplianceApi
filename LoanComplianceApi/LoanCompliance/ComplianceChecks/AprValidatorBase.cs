using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoanComplianceApi.LoanCompliance;

namespace LoanComplianceApi.LoanCompliance.ComplianceChecks
{
    public class AprValidatorBase : ValidatorBase
    {

        protected virtual ComplianceCheck ComplianceCheck { get; set; } = new ComplianceCheck { ComplianceType = ValidationType.AprValidation };
        public ValidationType ValidationType { get; } = ValidationType.AprValidation;

        public virtual bool ValidateApr(decimal loanApr, decimal maxApr)
        {
            return loanApr <= maxApr;
        }
    }
}
