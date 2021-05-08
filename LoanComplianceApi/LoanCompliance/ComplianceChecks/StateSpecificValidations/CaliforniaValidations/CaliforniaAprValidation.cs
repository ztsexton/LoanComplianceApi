using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanComplianceApi.LoanCompliance.ComplianceChecks
{
    public class CaliforniaAprValidation : AprValidatorBase
    {
        private const decimal VALoanRate = .03m;
        private const decimal PrimaryOccupancyRate = .05m;
        private const decimal SecondaryOccupancyRate = .04m;

        public override State State { get; } = State.CA;

        public override ComplianceCheck Validate(Loan loan)
        {
            var rate = FindRate(loan);

            ComplianceCheck.Passed = base.ValidateApr(loan.Apr, rate);
            return ComplianceCheck;
        }

        private decimal FindRate(Loan loan)
        {
            if (loan.LoanType == LoanType.VA)
            {
                return VALoanRate;
            }

            return loan.PrimaryOccupancy ? PrimaryOccupancyRate : SecondaryOccupancyRate;
        }
    }
}
