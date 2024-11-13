using Uagrm.Sa.DesignPatterns.LoanProcessor.Domain.Interfaces;

namespace Uagrm.Sa.DesignPatterns.LoanProcessor.Domain.Handlers;

public class LoanOfficerHandler : LoanApprovalHandler
{
    public LoanOfficerHandler(ILogger logger)
        : base(logger, maxLoanAmount: 25000, minCreditScore: 650) { }

    protected override string GetHandlerName() => "Oficial de Préstamos";
}
