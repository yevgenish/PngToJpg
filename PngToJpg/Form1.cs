using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PngToJpg
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            try
            {
                string folder = txtFolder.Text;

                string errorMessage = null;
                bool isPathValid = ValidatePath(folder, out errorMessage);

                if (!isPathValid)
                {
                    MessageBox.Show(errorMessage);
                    return;
                }

                int filesConverted = ToJPG(folder);

                MessageBox.Show(String.Format("Done. Files converted: {0}.", filesConverted));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public int ToJPG(string folder)
        {
            int filesConverted = 0;
            foreach (string file in Directory.GetFiles(folder))
            {
                string extension = Path.GetExtension(file);
                if (extension.ToLower() == ".png")
                {
                    string name = Path.GetFileNameWithoutExtension(file);
                    string path = Path.GetDirectoryName(file);
                    using(Image png = Image.FromFile(file))
                    {
                        string newFilePath = Path.Combine(path, name + ".jpg");
                        png.Save(newFilePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                        filesConverted++;
                    }
                }
            }

            return filesConverted;
        }

        private bool ValidatePath(string folder, out string errorMessage)
        {
            errorMessage = null;

            try
            {
                if (String.IsNullOrWhiteSpace(folder))
                {
                    errorMessage = "Empty folder.";
                    return false;
                }

                string folderPath = Path.GetFullPath(folder);

                if (folderPath.Equals(folder))
                {
                    return true;
                }
                else
                {
                    errorMessage = "Folder is invalid.";
                    return false;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.ToString();
            }

            return false;
        }

     
    }
}
