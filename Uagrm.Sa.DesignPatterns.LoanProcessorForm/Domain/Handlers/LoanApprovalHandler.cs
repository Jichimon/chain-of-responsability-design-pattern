using Uagrm.Sa.DesignPatterns.LoanProcessor.Domain.Interfaces;
using Uagrm.Sa.DesignPatterns.LoanProcessor.Domain.Models;

namespace Uagrm.Sa.DesignPatterns.LoanProcessor.Domain.Handlers;

public abstract class LoanApprovalHandler(ILogger logger, int maxLoanAmount, int minCreditScore)
{
    protected LoanApprovalHandler? _nextHandler;
    protected readonly ILogger _logger = logger;
    protected readonly int _maxLoanAmount = maxLoanAmount;
    protected readonly int _minCreditScore = minCreditScore;

    public LoanApprovalHandler SetNext(LoanApprovalHandler handler)
    {
        _nextHandler = handler;
        return handler;
    }

    public virtual void ProcessLoanApplication(LoanApplication application)
    {
        _logger.Log($"Evaluando en nivel: {GetHandlerName()}");

        if (application.Amount <= _maxLoanAmount && application.CreditScore >= _minCreditScore)
        {
            application.IsApproved = true;
            _logger.Log($"Préstamo aprobado por {GetHandlerName()}");
        }
        else if (_nextHandler != null)
        {
            _logger.Log($"Monto ${application.Amount} excede el límite de ${_maxLoanAmount} o score crediticio menor a {_minCreditScore}, elevando a siguiente nivel...");
            _nextHandler.ProcessLoanApplication(application);
        }
        else
        {
            application.IsApproved = false;
            application.RejectionReason = "Monto excede el máximo permitido por el banco o score crediticio insuficiente";
            _logger.Log($"Préstamo rechazado: {application.RejectionReason}");
        }
    }

    protected abstract string GetHandlerName();
}
