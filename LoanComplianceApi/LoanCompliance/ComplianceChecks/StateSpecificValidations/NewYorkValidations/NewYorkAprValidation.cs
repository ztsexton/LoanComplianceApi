using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanComplianceApi.LoanCompliance.ComplianceChecks
{
    public class NewYorkAprValidation : AprValidatorBase
    {
        private const decimal PrimaryOccupancyApr = .06m;
        private const decimal SecondaryOccupancyApr = .08m;

        public override State State { get; } = State.NY;

        public override ComplianceCheck Validate(Loan loan)
        {
            if (loan.LoanType == LoanType.Conventional)
            {
                decimal rate = loan.PrimaryOccupancy ? PrimaryOccupancyApr : SecondaryOccupancyApr;
                ComplianceCheck.Passed = base.ValidateApr(loan.Apr, rate);
            }
            else
            {
                ComplianceCheck.Passed = true;
            }

            return ComplianceCheck;
        }
    }
}
