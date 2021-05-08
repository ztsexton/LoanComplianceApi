using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanComplianceApi.LoanCompliance.ComplianceChecks
{
    public class FloridaFeeValidation : FeeValidatorBase
    {
        public override State State { get; } = State.VA;
        private readonly List<FeeType> _feeTypes = new List<FeeType> { FeeType.FloodCertification, FeeType.Application, FeeType.TitleSearch };

        public override ComplianceCheck Validate(Loan loan)
        {
            decimal fees = base.CalculateFees(_feeTypes, loan);
            decimal maxPercentage = FindMaxPercentage(loan.LoanAmount);
            ComplianceCheck.Passed = base.CheckFeeCompliance(loan.LoanAmount, fees, maxPercentage);

            return ComplianceCheck;
        }

        private decimal FindMaxPercentage(decimal loanAmount)
        {
            if (loanAmount <= 20000m)
            {
                return .06m;
            }
            else if (loanAmount <= 75000m)
            {
                return .08m;
            }
            else if (loanAmount <= 150000m)
            {
                return .09m;
            }
            else
            {
                return .10m;
            }
        }
    }
}
