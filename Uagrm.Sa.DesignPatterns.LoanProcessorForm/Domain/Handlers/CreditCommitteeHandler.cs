using Uagrm.Sa.DesignPatterns.LoanProcessor.Domain.Interfaces;

namespace Uagrm.Sa.DesignPatterns.LoanProcessor.Domain.Handlers;

public class CreditCommitteeHandler : LoanApprovalHandler
{
    public CreditCommitteeHandler(ILogger logger)
        : base(logger, maxLoanAmount: int.MaxValue, minCreditScore: 800) { }

    protected override string GetHandlerName() => "Comité de Crédito";
}
