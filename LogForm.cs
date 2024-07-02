using System;
using System.Drawing;
using System.Windows.Forms;

public class LogForm : Form
{
    private TextBox textBox;

    public LogForm()
    {
        this.Text = "Log Window";
        this.ClientSize = new Size(400, 300);
        this.textBox = new TextBox()
        {
            Multiline = true,
            Dock = DockStyle.Fill,
            ReadOnly = true,
            ScrollBars = ScrollBars.Vertical
        };
        this.Controls.Add(this.textBox);
    }

    public void LogMessage(string message)
    {
        textBox.AppendText(message + Environment.NewLine);
    }
}