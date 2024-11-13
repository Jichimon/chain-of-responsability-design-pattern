using Uagrm.Sa.DesignPatterns.LoanProcessor.Domain.Interfaces;
using Uagrm.Sa.DesignPatterns.LoanProcessor.Extensions;

namespace Uagrm.Sa.DesignPatterns.LoanProcessor.Loggers;

public class ConsoleLogger(RichTextBox logTextBox) : ILogger
{
    private readonly RichTextBox _logTextBox = logTextBox;

    public void Log(string message)
    {
        _logTextBox.InvokeIfRequired(() =>
        {
            _logTextBox.AppendText($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}{Environment.NewLine}");
            _logTextBox.ScrollToCaret();
        });
    }
}
