using Uagrm.Sa.DesignPatterns.LoanProcessor.Domain.Handlers;
using Uagrm.Sa.DesignPatterns.LoanProcessor.Domain.Interfaces;

namespace Uagrm.Sa.DesignPatterns.LoanProcessor.Domain.Models;

public class Bank
{
    private readonly LoanApprovalHandler _loanApprovalChain;
    private readonly ILogger _logger;

    public Bank(ILogger logger)
    {
        _logger = logger;

        _loanApprovalChain = new LoanOfficerHandler(logger);
        _loanApprovalChain
            .SetNext(new LoanSupervisorHandler(logger))
            .SetNext(new LoanManagerHandler(logger))
            .SetNext(new CreditCommitteeHandler(logger));
    }

    public void ProcessLoan(LoanApplication application)
    {
        _logger.Log($"Iniciando proceso para {application.ApplicantName}");

        if (application.Age < 18)
        {
            application.IsApproved = false;
            application.RejectionReason = "El solicitante debe ser mayor de edad";
            _logger.Log($"Préstamo rechazado: {application.RejectionReason}");
            return;
        }

        _loanApprovalChain.ProcessLoanApplication(application);
    }
}
