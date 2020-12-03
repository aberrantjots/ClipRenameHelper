using System;
using System.IO;
using System.Windows.Forms;

namespace ClipHelper
{
    public partial class MainForm : Form
    {
        private string _ogExtension = string.Empty;
        private string _ogPath = string.Empty;

        public MainForm()
        {
            InitializeComponent();
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            var result = browseOpenFileDialog.ShowDialog(this);
            if (result != DialogResult.OK) return;
            fileNameTextBox.Text = browseOpenFileDialog.FileName;
            resultTextBox.Text = browseOpenFileDialog.SafeFileName;
            _ogExtension = browseOpenFileDialog.DefaultExt;
            _ogPath = browseOpenFileDialog.FileName.Replace(browseOpenFileDialog.SafeFileName, string.Empty);
        }


        private void radioButton1_Click(object sender, EventArgs e)
        {
            radioButton2.Checked = radioButton3.Checked = radioButton4.Checked = radioButton5.Checked = !radioButton1.Checked;
            UpdateResult();
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            radioButton1.Checked = radioButton3.Checked = radioButton4.Checked = radioButton5.Checked = !radioButton2.Checked;
            UpdateResult();
        }

        private void radioButton3_Click(object sender, EventArgs e)
        {
            radioButton1.Checked = radioButton2.Checked = radioButton4.Checked = radioButton5.Checked = !radioButton3.Checked;
            UpdateResult();
        }

        private void radioButton4_Click(object sender, EventArgs e)
        {
            radioButton1.Checked = radioButton2.Checked = radioButton3.Checked = radioButton5.Checked = !radioButton4.Checked;
            UpdateResult();
        }

        private void radioButton5_Click(object sender, EventArgs e)
        {
            radioButton1.Checked = radioButton2.Checked = radioButton3.Checked = radioButton4.Checked = !radioButton5.Checked;
            UpdateResult();
        }
        private void counter_ValueChanged(object sender, EventArgs e)
        {
            UpdateResult();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            UpdateResult();
        }

        private void UpdateResult()
        {
            foreach (var control in Controls)
            {
                if (control is RadioButton radioButton && radioButton.Checked)
                {
                    resultTextBox.Text = $"{testCaseTextBox.Text}_Day{numericUpDown1.Value}_{radioButton.Text}_{counter.Value}.{_ogExtension}";
                }
            }
        }

        private void helpButton_Click(object sender, EventArgs e)
        {
            var fileCheck = File.Exists(fileNameTextBox.Text);
            if (!fileCheck)
            {
                MessageBox.Show("Error: Bad filename?");
                return;
            }

            var destination = $"{_ogPath}{resultTextBox.Text}";
            var destinationCheck = File.Exists(destination);
            if (destinationCheck)
            {
                MessageBox.Show("Error: that file already exists. Update your selections.");
            }
            // Rename the file
            try
            {
                File.Move(fileNameTextBox.Text, destination);
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Error: {exception.Message}");
            }

        }
    }
}
