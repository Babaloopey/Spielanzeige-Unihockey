using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Umsetzung_III.Commands
{

    public class LogoCommand : CommandBase
    {
        private readonly SpielanzeigeViewModel _viewModel;
        public LogoCommand(SpielanzeigeViewModel viewModel)
        {
            _viewModel = viewModel;

        }
        public override void Execute(object? parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.png;*.bmp|All Files|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedImagePath = openFileDialog.FileName;
                string targetFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logo");

                this._viewModel.LogoSource = "";

                CreateNewFolderIfNotExists(targetFolder);

                string uniqueFileName = System.IO.Path.Combine(targetFolder, System.IO.Path.GetFileName(selectedImagePath));

                CopyFileToFolderIfNotExists(selectedImagePath, uniqueFileName);

                string textFilePath = "logoPath.txt";
                System.IO.File.WriteAllText(textFilePath, uniqueFileName);

                this._viewModel.LogoSource = uniqueFileName;
            }
        }

        private static void CopyFileToFolderIfNotExists(string selectedImagePath, string uniqueFileName)
        {
            if (System.IO.File.Exists(uniqueFileName))
            {
                MessageBox.Show("This filename already exists. Please rename the file before storing it in the 'logo' folder.", "File Exists", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                System.IO.File.Copy(selectedImagePath, uniqueFileName);
            }
        }

        private static void CreateNewFolderIfNotExists(string targetFolder)
        {
            if (!System.IO.Directory.Exists(targetFolder))
            {
                System.IO.Directory.CreateDirectory(targetFolder);
            }
        }
    }
}
