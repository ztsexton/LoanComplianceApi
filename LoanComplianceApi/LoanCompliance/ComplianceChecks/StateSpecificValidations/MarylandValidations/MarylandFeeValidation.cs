using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanComplianceApi.LoanCompliance.ComplianceChecks
{
    public class MarylandFeeValidation : FeeValidatorBase
    {
        public override State State { get; } = State.MD;

        public override ComplianceCheck Validate(Loan loan)
        {
            decimal fees = CalculateFees(loan);
            decimal maxPercentage = GetMaxPercentage(loan.LoanAmount);

            ComplianceCheck.Passed = base.CheckFeeCompliance(loan.LoanAmount, fees, maxPercentage);

            return ComplianceCheck;
        }

        private static decimal CalculateFees(Loan loan)
        {
            Fee applicationFee = loan.Fees.SingleOrDefault(fee => fee.FeeType == FeeType.Application) ?? new Fee { Amount = 0 };
            Fee creditReportFee = loan.Fees.SingleOrDefault(fee => fee.FeeType == FeeType.CreditReport) ?? new Fee { Amount = 0 };

            decimal fees = applicationFee.Amount + creditReportFee.Amount;
            return fees;
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
