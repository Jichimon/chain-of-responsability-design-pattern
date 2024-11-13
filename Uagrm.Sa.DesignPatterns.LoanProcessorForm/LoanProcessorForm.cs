using Uagrm.Sa.DesignPatterns.LoanProcessor.Domain.Models;
using Uagrm.Sa.DesignPatterns.LoanProcessor.Loggers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Uagrm.Sa.DesignPatterns.LoanProcessorForm;

public partial class LoanProcessorForm : Form
{
    private readonly Bank _bank;
    private RichTextBox logTextBox;
    private TextBox nameTextBox;
    private TextBox ageTextBox;
    private TextBox incomeTextBox;
    private TextBox loanAmountTextBox;
    private TextBox creditScoreTextBox;
    private Button processButton;
    private Button clearButton;

#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
    public LoanProcessorForm()
#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
    {
        InitializeComponents();
        _bank = new Bank(new ConsoleLogger(logTextBox!));
    }


    private void InitializeComponents()
    {
        this.Text = "Loan Application Processor";
        this.Size = new Size(800, 600);

        var nameLabel = new Label
        {
            Text = "Nombre:",
            Location = new Point(20, 20),
            Size = new Size(150, 20)
        };

        nameTextBox = new TextBox
        {
            Location = new Point(180, 20),
            Size = new Size(200, 20)
        };

        var ageLabel = new Label
        {
            Text = "Edad:",
            Location = new Point(20, 50),
            Size = new Size(150, 20)
        };

        ageTextBox = new TextBox
        {
            Location = new Point(180, 50),
            Size = new Size(200, 20)
        };

        var incomeLabel = new Label
        {
            Text = "Ingreso_Anual:",
            Location = new Point(20, 80),
            Size = new Size(150, 20)
        };

        incomeTextBox = new TextBox
        {
            Location = new Point(180, 80),
            Size = new Size(200, 20)
        };

        var loanAmountLabel = new Label
        {
            Text = "Monto_Préstamo:",
            Location = new Point(20, 110),
            Size = new Size(150, 20)
        };

        loanAmountTextBox = new TextBox
        {
            Location = new Point(180, 110),
            Size = new Size(200, 20)
        };

        var creditScoreLabel = new Label
        {
            Text = "Score_Crediticio:",
            Location = new Point(20, 140),
            Size = new Size(150, 20)
        };

        creditScoreTextBox = new TextBox
        {
            Location = new Point(180, 140),
            Size = new Size(200, 20)
        };

        processButton = new Button
        {
            Text = "Procesar Solicitud",
            Location = new Point(130, 180),
            Size = new Size(120, 30)
        };
#pragma warning disable CS8622 // La nulabilidad de los tipos de referencia del tipo de parámetro no coincide con el delegado de destino (posiblemente debido a los atributos de nulabilidad).
        processButton.Click += ProcessButton_Click;
#pragma warning restore CS8622 // La nulabilidad de los tipos de referencia del tipo de parámetro no coincide con el delegado de destino (posiblemente debido a los atributos de nulabilidad).

        clearButton = new Button
        {
            Text = "Limpiar",
            Location = new Point(260, 180),
            Size = new Size(70, 30)
        };
#pragma warning disable CS8622 // La nulabilidad de los tipos de referencia del tipo de parámetro no coincide con el delegado de destino (posiblemente debido a los atributos de nulabilidad).
        clearButton.Click += ClearButton_Click;
#pragma warning restore CS8622 // La nulabilidad de los tipos de referencia del tipo de parámetro no coincide con el delegado de destino (posiblemente debido a los atributos de nulabilidad).

        logTextBox = new RichTextBox
        {
            Location = new Point(20, 230),
            Size = new Size(740, 300),
            ReadOnly = true
        };

        this.Controls.AddRange([
                nameLabel, nameTextBox,
                ageLabel, ageTextBox,
                incomeLabel, incomeTextBox,
                loanAmountLabel, loanAmountTextBox,
                creditScoreLabel, creditScoreTextBox,
                processButton, clearButton,
                logTextBox
            ]);
    }


    private void ProcessButton_Click(object sender, EventArgs e)
    {
        if (!ValidateInputs())
        {
            MessageBox.Show("Por favor complete todos los campos con valores válidos.", "Error de Validación");
            return;
        }

        var loanApplication = new LoanApplication
        {
            ApplicantName = nameTextBox.Text,
            Age = int.Parse(ageTextBox.Text),
            AnnualIncome = decimal.Parse(incomeTextBox.Text),
            Amount = (int)decimal.Parse(loanAmountTextBox.Text),
            CreditScore = int.Parse(creditScoreTextBox.Text)
        };

        _bank.ProcessLoan(loanApplication);

        if (loanApplication.IsApproved)
        {
            MessageBox.Show($"¡Préstamo aprobado!\nMonto: ${loanApplication.Amount:N2}",
                "Préstamo Aprobado",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        else
        {
            MessageBox.Show($"Préstamo rechazado\nRazón: {loanApplication.RejectionReason}",
                "Préstamo Rechazado",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }

    private void ClearButton_Click(object sender, EventArgs e)
    {
        nameTextBox.Clear();
        ageTextBox.Clear();
        incomeTextBox.Clear();
        loanAmountTextBox.Clear();
        creditScoreTextBox.Clear();
        logTextBox.Clear();
    }

    private bool ValidateInputs()
    {
        if (string.IsNullOrWhiteSpace(nameTextBox.Text)) 
            return false;

        if (!int.TryParse(ageTextBox.Text, out int age) || age < 18 || age > 100) 
            return false;
        
        if (!decimal.TryParse(incomeTextBox.Text, out decimal income) || income <= 0) 
            return false;
        
        if (!decimal.TryParse(loanAmountTextBox.Text, out decimal amount) || amount <= 0) 
            return false;
        
        if (!int.TryParse(creditScoreTextBox.Text, out int score) || score < 300 || score > 850) 
            return false;

        return true;
    }
}
