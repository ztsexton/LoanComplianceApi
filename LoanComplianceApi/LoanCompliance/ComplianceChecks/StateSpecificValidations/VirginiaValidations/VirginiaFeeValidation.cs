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
        private readonly List<FeeType> _feeTypes = new List<FeeType> { FeeType.FloodCertification, FeeType.Processing, FeeType.Settlement };

        public override ComplianceCheck Validate(Loan loan)
        {
            decimal fees = base.CalculateFees(_feeTypes, loan);
            ComplianceCheck.Passed = base.CheckFeeCompliance(loan.LoanAmount, fees, MaxPercentage);

            return ComplianceCheck;
        }
    }
}
