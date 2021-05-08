using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoanComplianceApi.LoanCompliance;

namespace LoanComplianceApi.LoanCompliance.ComplianceChecks
{
    public class VirginiaFeeValidation : FeeValidatorBase
    {
        private const decimal MaxPercentage = .07m;

        public override State State { get; } = State.VA;

        public override ComplianceCheck Validate(Loan loan)
        {
            decimal fees = CalculateFees(loan);
            ComplianceCheck.Passed = base.CheckFeeCompliance(loan.LoanAmount, fees, MaxPercentage);

            return ComplianceCheck;
        }

        private static decimal CalculateFees(Loan loan)
        {
            Fee floodCertificationFee = loan.Fees.SingleOrDefault(fee => fee.FeeType == FeeType.FloodCertification) ?? new Fee { Amount = 0 };
            Fee processingFee = loan.Fees.SingleOrDefault(fee => fee.FeeType == FeeType.Processing) ?? new Fee { Amount = 0 };
            Fee settlementFee = loan.Fees.SingleOrDefault(fee => fee.FeeType == FeeType.Settlement) ?? new Fee { Amount = 0 };

            decimal fees = floodCertificationFee.Amount + processingFee.Amount + settlementFee.Amount;
            return fees;
        }
    }
}
