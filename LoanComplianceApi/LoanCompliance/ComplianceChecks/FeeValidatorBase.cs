using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoanComplianceApi.LoanCompliance;

namespace LoanComplianceApi.LoanCompliance.ComplianceChecks
{
    public class FeeValidatorBase : ValidatorBase
    {
        protected virtual ComplianceCheck ComplianceCheck { get; set; } = new ComplianceCheck { ComplianceType = ValidationType.FeeValidation };

        public ValidationType ValidationType { get; } = ValidationType.FeeValidation;

        public virtual bool CheckFeeCompliance(decimal loanAmount, decimal fees, decimal maxPercentage)
        {
            if (fees / loanAmount <= maxPercentage)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual decimal CalculateFees(List<FeeType> feeTypes, Loan loan)
        {
            decimal fees = 0m;
            foreach (var feeType in feeTypes)
            {
                var fee = loan.Fees.SingleOrDefault(fee => fee.FeeType == feeType) ?? new Fee { Amount = 0m };
                fees += fee.Amount;
            }

            return fees;
        }

    }
}
