using LoanComplianceApi.LoanCompliance;

namespace LoanComplianceApi.LoanCompliance.Validations
{
    public interface IValidator
    {
        State State { get; }
        ComplianceCheck Validate(Loan loan);
    }
}