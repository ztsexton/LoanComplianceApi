using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanComplianceApi.LoanCompliance.ComplianceChecks
{
    public class NewYorkAprValidation : AprValidatorBase
    {
        private const decimal PrimaryOccupancyApr = 6.00m;
        private const decimal SecondaryOccupancyApr = 8.00m;

        public override State State { get; } = State.NY;

        public override ComplianceCheck Validate(Loan loan)
        {
            if (loan.LoanType == LoanType.Conventional)
            {
                decimal rate = loan.PrimaryOccupancy ? PrimaryOccupancyApr : SecondaryOccupancyApr;
                ComplianceCheck.Passed = loan.Apr < rate ? true : false;
            }
            else
            {
                ComplianceCheck.Passed = true;
            }

            return ComplianceCheck;
        }
    }
}
