﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoanComplianceApi.LoanCompliance;

namespace LoanComplianceApi.LoanCompliance.Validations
{
    public class AprValidatorBase : BaseValidator
    {

        protected virtual ComplianceCheck ComplianceCheck { get; set; } = new ComplianceCheck { ComplianceType = ValidationType.AprValidation };
        public ValidationType ValidationType { get; } = ValidationType.AprValidation;
    }
}
