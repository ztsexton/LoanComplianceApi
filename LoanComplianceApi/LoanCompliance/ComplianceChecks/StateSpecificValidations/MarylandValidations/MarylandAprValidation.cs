using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanComplianceApi.LoanCompliance.ComplianceChecks
{
    public class MarylandAprValidation : AprValidatorBase
    {
        private const decimal Rate = .04m;

        public override State State { get; } = State.MD;

        public override ComplianceCheck Validate(Loan loan)
        {
            ComplianceCheck.Passed = base.ValidateApr(loan.Apr, Rate);
            return ComplianceCheck;
        }
    }
}
