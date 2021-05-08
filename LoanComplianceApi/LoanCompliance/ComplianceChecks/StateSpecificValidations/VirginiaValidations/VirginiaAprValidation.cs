using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoanComplianceApi.LoanCompliance;

namespace LoanComplianceApi.LoanCompliance.ComplianceChecks
{
    public class VirginiaAprValidation : AprValidatorBase
    {
        private const decimal PrimaryOccupancyApr = 5.00m;
        private const decimal SecondaryOccupancyApr = 8.00m;

        public override State State { get; } = State.VA;

        public override ComplianceCheck Validate(Loan loan)
        {
            decimal rate = loan.PrimaryOccupancy ? PrimaryOccupancyApr : SecondaryOccupancyApr;
            ComplianceCheck.Passed = loan.Apr < rate ? true : false;

            return ComplianceCheck;
        }
    }
}
