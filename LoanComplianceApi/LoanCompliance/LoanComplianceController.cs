using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoanComplianceApi.LoanCompliance.ComplianceEngine;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LoanComplianceApi.LoanCompliance
{
    [ApiController]
    public class LoanComplianceController
    {
        private readonly ILogger<LoanComplianceController> _logger;
        private readonly IComplianceEngine _complianceEngine;

        public LoanComplianceController(ILogger<LoanComplianceController> logger, IComplianceEngine complianceEngine)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _complianceEngine = complianceEngine ?? throw new ArgumentNullException(nameof(complianceEngine));
        }

        [HttpPost("compliancecheck")]
        public LoanComplianceResult CheckCompliance(Loan loan)
        {
            var result = _complianceEngine.Validate(loan);

            return result;

        }
    }
}
