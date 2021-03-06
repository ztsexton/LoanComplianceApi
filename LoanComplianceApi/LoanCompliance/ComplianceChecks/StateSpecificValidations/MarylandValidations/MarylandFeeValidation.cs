using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanComplianceApi.LoanCompliance.ComplianceChecks
{
    public class MarylandFeeValidation : FeeValidatorBase
    {
        public override State State { get; } = State.MD;

        private readonly List<FeeType> _feeTypes = new List<FeeType> { FeeType.Application, FeeType.CreditReport };

        public override ComplianceCheck Validate(Loan loan)
        {
            decimal fees = base.CalculateFees(_feeTypes, loan);
            decimal maxPercentage = GetMaxPercentage(loan.LoanAmount);

            ComplianceCheck.Passed = base.CheckFeeCompliance(loan.LoanAmount, fees, maxPercentage);

            return ComplianceCheck;
        }

        private decimal GetMaxPercentage(decimal loanAmount)
        {
            if (loanAmount <= 200000.00m)
            {
                return .04m;
            }
            else
            {
                return .06m;
            }
        }
    }
}
