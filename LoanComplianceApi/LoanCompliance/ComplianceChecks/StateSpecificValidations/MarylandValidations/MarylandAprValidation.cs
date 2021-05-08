using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanComplianceApi.LoanCompliance.ComplianceChecks.StateSpecificValidations.MarylandValidations
{
    public class MarylandAprValidation : AprValidatorBase
    {
        private const decimal Rate = 4.00m;

        public override State State { get; } = State.MD;

        public override ComplianceCheck Validate(Loan loan)
        {
            ComplianceCheck.Passed = loan.Apr < Rate ? true : false;
            return ComplianceCheck;
        }
    }
}
