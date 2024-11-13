namespace Uagrm.Sa.DesignPatterns.LoanProcessor.Domain.Models;

public class LoanApplication
{
    public required string ApplicantName { get; set; }
    public int Age { get; set; }
    public decimal AnnualIncome { get; set; }
    public int Amount { get; set; }
    public int CreditScore { get; set; }
    public bool IsApproved { get; set; }
    public string? RejectionReason { get; set; }
}
