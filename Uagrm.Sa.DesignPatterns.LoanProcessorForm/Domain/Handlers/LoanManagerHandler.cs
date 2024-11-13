using Uagrm.Sa.DesignPatterns.LoanProcessor.Domain.Interfaces;

namespace Uagrm.Sa.DesignPatterns.LoanProcessor.Domain.Handlers;

public class LoanManagerHandler : LoanApprovalHandler
{
    public LoanManagerHandler(ILogger logger)
        : base(logger, maxLoanAmount: 150000, minCreditScore: 750) { }

    protected override string GetHandlerName() => "Gerente";
}
