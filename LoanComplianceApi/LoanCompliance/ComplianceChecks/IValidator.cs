using LoanComplianceApi.LoanCompliance;

namespace LoanComplianceApi.LoanCompliance.ComplianceChecks
{
    public interface IValidator
    {
        State State { get; }
        ComplianceCheck Validate(Loan loan);
    }
}