using LoanComplianceApi.LoanCompliance;

namespace LoanComplianceApi.LoanCompliance.ComplianceEngine
{
    public interface IComplianceEngine
    {
        LoanComplianceResult Validate(Loan loan);
    }
}