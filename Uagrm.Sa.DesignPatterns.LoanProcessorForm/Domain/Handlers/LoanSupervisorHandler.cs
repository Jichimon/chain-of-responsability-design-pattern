using Uagrm.Sa.DesignPatterns.LoanProcessor.Domain.Interfaces;

namespace Uagrm.Sa.DesignPatterns.LoanProcessor.Domain.Handlers;

public class LoanSupervisorHandler : LoanApprovalHandler
{
    public LoanSupervisorHandler(ILogger logger)
        : base(logger, maxLoanAmount: 75000, minCreditScore: 700) { }

    protected override string GetHandlerName() => "Supervisor";
}
