using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.IO;

namespace OSKDConfig
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            colorComboBox.Items.Add("Blue");
            colorComboBox.Items.Add("Green");
            colorComboBox.Items.Add("Orange");
            colorComboBox.Items.Add("Pink");
            colorComboBox.Items.Add("Purple");
            colorComboBox.Items.Add("Red");
            colorComboBox.Items.Add("Teal");
            colorComboBox.Items.Add("Yellow");          

            DirectoryInfo di = new DirectoryInfo(@".\Configuration\Layouts");
            foreach (FileInfo fi in di.GetFiles("*.xml"))
            {
                layoutComboBox.Items.Add(fi.Name.Replace(".xml",string.Empty));
            }

            AppSettingsSection config = ConfigurationManager.OpenExeConfiguration(@".\OSKD.exe").AppSettings;

            colorComboBox.SelectedItem = config.Settings["Color"].Value;
            layoutComboBox.SelectedItem = config.Settings["KeyboardLayout"].Value;
            scrollSecuChkBox.Checked = bool.Parse(config.Settings["ScrollLock"].Value);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(@".\OSKD.exe");
            AppSettingsSection appSettings = config.AppSettings;

            appSettings.Settings["Color"].Value = colorComboBox.SelectedItem.ToString();
            appSettings.Settings["KeyboardLayout"].Value = layoutComboBox.SelectedItem.ToString();
            appSettings.Settings["ScrollLock"].Value = scrollSecuChkBox.Checked.ToString();

            config.Save();

            this.Close();
        }
    }
}
