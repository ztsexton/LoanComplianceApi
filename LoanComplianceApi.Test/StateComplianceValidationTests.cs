using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanComplianceApi.LoanCompliance;
using LoanComplianceApi.LoanCompliance.ComplianceChecks;
using LoanComplianceApi.LoanCompliance.ComplianceEngine;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace LoanComplianceApi.Test
{
    public class StateComplianceValidationTests
    {
        private readonly IEnumerable<IValidator> _validators;
        public StateComplianceValidationTests()
        {
            var services = new ServiceCollection();
            services.AddScoped<IValidator, VirginiaFeeValidation>();
            services.AddScoped<IValidator, VirginiaAprValidation>();
            services.AddScoped<IValidator, NewYorkAprValidation>();

            var serviceProvider = services.BuildServiceProvider();

            _validators = serviceProvider.GetServices<IValidator>();
        }

        [Fact]
        public void ValidNewYorkLoanReturnsValidResult()
        {
            var sut = new ComplianceEngine(_validators);
            var loan = new Loan
            {
                Apr = .05m,
                LoanAmount = 100000m,
                Fees = new List<Fee>
                { 
                    new Fee { Amount = 50, FeeType = FeeType.Application }
                },
                LoanType = LoanType.Conventional,
                PrimaryOccupancy = true,
                State = State.NY
            };

            var expected = new LoanComplianceResult
            {
                IsCompliant = true,
                ComplianceChecks = new List<ComplianceCheck>
                {
                    new ComplianceCheck { ComplianceType = ValidationType.AprValidation, Passed = true }
                }
            };

            var actual = sut.Validate(loan);

            Assert.Equal(actual.GetHashCode(), expected.GetHashCode());
        }
    }
}
