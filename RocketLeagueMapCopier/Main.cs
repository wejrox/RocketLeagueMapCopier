using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RocketLeagueMapCopier
{
    public partial class Main : Form
    {

        public string[] results = new string[] { "Success", "Complete" };

        public Main()
        {
            InitializeComponent();
        }

        // Edit this however you like, just loading default stuff
        private void Main_Load(object sender, EventArgs e)
        {
            txtModsDir.Text = Properties.Settings.Default.modsDir;
            txtMapDir.Text = Properties.Settings.Default.mapDir;
            txtMapName.Text = Properties.Settings.Default.mapName;
            txtPackage1.Text = Properties.Settings.Default.packageName1;
            txtPackage2.Text = Properties.Settings.Default.packageName2;
            txtPackage3.Text = Properties.Settings.Default.packageName3;
            txtPackage4.Text = Properties.Settings.Default.packageName4;
            txtPackage5.Text = Properties.Settings.Default.packageName5;
        }

        // Edit this however you like, just storing default stuff
        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.mapDir = txtMapDir.Text;
            Properties.Settings.Default.mapName = txtMapName.Text;
            Properties.Settings.Default.modsDir = txtModsDir.Text;
            Properties.Settings.Default.packageName1 = txtPackage1.Text;
            Properties.Settings.Default.packageName2 = txtPackage2.Text;
            Properties.Settings.Default.packageName3 = txtPackage3.Text;
            Properties.Settings.Default.packageName4 = txtPackage4.Text;
            Properties.Settings.Default.packageName5 = txtPackage5.Text;
            Properties.Settings.Default.Save();
        }

        private void btnApplyMap_Click(object sender, EventArgs e)
        {
            // Grab the package names (I wish i knew how to make form elements into an array..)
            List<string> packages = new List<string>();
            if (chkPackage1.Checked)
                packages.Add(txtPackage1.Text);
            if (chkPackage2.Checked)
                packages.Add(txtPackage2.Text);
            if (chkPackage3.Checked)
                packages.Add(txtPackage3.Text);
            if (chkPackage4.Checked)
                packages.Add(txtPackage4.Text);
            if (chkPackage5.Checked)
                packages.Add(txtPackage5.Text);

            // Copy straight over the existing map
            string mapOrigFile = Path.Combine(txtMapDir.Text, txtMapName.Text + ".udk");
            string mapDestFile = Path.Combine(txtModsDir.Text, txtMapName.Text + ".upk");
            try {
                File.Copy(mapOrigFile, mapDestFile, true);
                Console.WriteLine("CopiedMap");
            } catch (FileNotFoundException) {
                MessageBox.Show("Map '" + txtMapName.Text + ".udk' Doesn't Exist");
                lblCopyResult.BackColor = Color.Red;
                lblCopyResult.Text = "Failed";
                return;
            }
            catch (Exception) {
                MessageBox.Show("Something's gone wrong. \nCheck your Directories and try again.\n Post on reddit if it persists");
                lblCopyResult.BackColor = Color.Red;
                lblCopyResult.Text = "Failed";
                return;
            }

            // Copy over the packages
            foreach(string s in packages)
            {
                string packOrigFile = Path.Combine(txtMapDir.Text, s);
                string packDestFile = Path.Combine(txtModsDir.Text, s);

                try {
                    File.Copy(packOrigFile, packDestFile, true);
                } catch (FileNotFoundException) {
                    lblCopyResult.BackColor = Color.Yellow;
                    lblCopyResult.Text = "Partial Success";
                    MessageBox.Show("'" + s + "' Doesn't exist.\n" + 
                                    "Make sure you included '.upk' in your file name.\n" + 
                                    "Package not copied.");
                }
                catch (ArgumentException) {
                    lblCopyResult.BackColor = Color.Yellow;
                    lblCopyResult.Text = "Partial Success";
                    MessageBox.Show("Copy requested on package without a name given.\nPackage not copied.");
                }
                catch (IOException) {
                    lblCopyResult.BackColor = Color.Yellow;
                    lblCopyResult.Text = "Partial Success";
                    MessageBox.Show("Copy requested on package without a name given.\nPackage not copied.");
                }
            }

            // Make sure there weren't complications
            if (lblCopyResult.BackColor != Color.Yellow && lblCopyResult.BackColor != Color.Red)
            {
                lblCopyResult.BackColor = Color.LightGreen;

                // Notify of completion (multiple values so they know it's worked when they re-copy it again.)
                if (lblCopyResult.Text == results[0])
                    lblCopyResult.Text = results[1];
                else
                    lblCopyResult.Text = results[0];
            }
        }

        private void lblCopyResult_Click(object sender, EventArgs e)
        {

        }
    }
}
