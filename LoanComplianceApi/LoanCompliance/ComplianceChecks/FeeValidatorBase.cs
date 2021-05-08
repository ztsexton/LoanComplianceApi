using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoanComplianceApi.LoanCompliance;

namespace LoanComplianceApi.LoanCompliance.Validations
{
    public class FeeValidatorBase : BaseValidator
    {
        protected virtual ComplianceCheck ComplianceCheck { get; set; } = new ComplianceCheck { ComplianceType = ValidationType.FeeValidation };

        public ValidationType ValidationType { get; } = ValidationType.FeeValidation;

    }
}
