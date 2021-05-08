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
            var loan = CreateTestLoan(750000, .05m, State.NY, LoanType.Conventional);

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

        [Fact]
        public void ValidVirginiaConventionalLoanReturnsValidResult()
        {
            var sut = new ComplianceEngine(_validators);
            var loan = CreateTestLoan(750000, .05m, State.VA, LoanType.Conventional);

            var expected = new LoanComplianceResult
            {
                IsCompliant = true,
                ComplianceChecks = new List<ComplianceCheck>
                {
                    new ComplianceCheck { ComplianceType = ValidationType.AprValidation, Passed = true },

                    new ComplianceCheck { ComplianceType = ValidationType.FeeValidation, Passed = true }
                }
            };

            var actual = sut.Validate(loan);

            Assert.Equal(actual.GetHashCode(), expected.GetHashCode());
        }

        [Fact]
        public void ValidVirginiaFhaLoanReturnsValidResult()
        {
            var sut = new ComplianceEngine(_validators);
            var loan = CreateTestLoan(750000, .05m, State.VA, LoanType.Fha);

            var expected = new LoanComplianceResult
            {
                IsCompliant = true,
                ComplianceChecks = new List<ComplianceCheck>
                {
                    new ComplianceCheck { ComplianceType = ValidationType.AprValidation, Passed = true },

                    new ComplianceCheck { ComplianceType = ValidationType.FeeValidation, Passed = true }
                }
            };

            var actual = sut.Validate(loan);

            Assert.Equal(actual.GetHashCode(), expected.GetHashCode());
        }

        [Fact]
        public void ValidVirginiaVALoanReturnsValidResult()
        {
            var sut = new ComplianceEngine(_validators);
            var loan = CreateTestLoan(750000, .05m, State.VA, LoanType.VA);

            var expected = new LoanComplianceResult
            {
                IsCompliant = true,
                ComplianceChecks = new List<ComplianceCheck>
                {
                    new ComplianceCheck { ComplianceType = ValidationType.AprValidation, Passed = true },

                    new ComplianceCheck { ComplianceType = ValidationType.FeeValidation, Passed = true }
                }
            };

            var actual = sut.Validate(loan);

            Assert.Equal(actual.GetHashCode(), expected.GetHashCode());
        }

        [Fact]
        public void ExcessiveFeesVirginiaVALoanReturnsValidResult()
        {
            var sut = new ComplianceEngine(_validators);
            var loan = CreateTestLoan(750000, .05m, State.VA, LoanType.VA);
            var fee = new Fee { FeeType = FeeType.FloodCertification, Amount = 100000 };
            loan.Fees.Add(fee);

            var expected = new LoanComplianceResult
            {
                IsCompliant = false,
                ComplianceChecks = new List<ComplianceCheck>
                {
                    new ComplianceCheck { ComplianceType = ValidationType.AprValidation, Passed = true },

                    new ComplianceCheck { ComplianceType = ValidationType.FeeValidation, Passed = false }
                }
            };

            var actual = sut.Validate(loan);

            Assert.Equal(actual.GetHashCode(), expected.GetHashCode());
        }

        private Loan CreateTestLoan(decimal amount, decimal apr, State state, LoanType loanType)
        {
            return new Loan
            {
                Apr = apr,
                LoanAmount = amount,
                Fees = new List<Fee>
                {
                    new Fee { Amount = 50, FeeType = FeeType.Application }
                },
                LoanType = loanType,
                PrimaryOccupancy = true,
                State = state
            };
        }
    }
}
