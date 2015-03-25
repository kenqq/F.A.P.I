using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace F.A.P.I
{
    class prompt
    {
        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            prompt.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            prompt.MaximizeBox = false;
            prompt.MinimizeBox = false;
            prompt.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;

            prompt.Width = 500;
            prompt.Height = 550;
            prompt.Text = caption;
            prompt.StartPosition = FormStartPosition.CenterScreen;
            Label textLabel = new Label() { Left = 50, Top = 20, Text = text, Width = 400 };
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400,Height=200 };
            textBox.Multiline = true;

            // Set the Multiline property to true.
            textBox.Multiline = true;
            // Add vertical scroll bars to the TextBox control.
            textBox.ScrollBars = ScrollBars.Vertical;
            // Allow the RETURN key to be entered in the TextBox control.
            textBox.AcceptsReturn = true;
            // Allow the TAB key to be entered in the TextBox control.
            textBox.AcceptsTab = true;
            // Set WordWrap to true to allow text to wrap to the next line.
            textBox.WordWrap = true;

            Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = textBox.Top + 70 + textBox.Height };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;
            prompt.ShowDialog();
            return textBox.Text;
        }
    }
}
