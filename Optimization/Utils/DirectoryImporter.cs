using System;
using Gtk;

namespace Optimizer.Utils
{
    public abstract class DirectoryImporter
    {
        public static string SelectDirectory()
        {
            Application.Init();

            string selectedFolder;

            var folderDialog = new FileChooserDialog("Select a folder", null, FileChooserAction.SelectFolder, "Cancel", ResponseType.Cancel, "Open", ResponseType.Accept);
            if (folderDialog.Run() == (int)ResponseType.Accept)
            {
                selectedFolder = folderDialog.Filename;
                //ProcessSelectedFolder(selectedFolder);
            }

            selectedFolder = folderDialog.Filename;



            folderDialog.Destroy();

            return selectedFolder;
        }
    }

}
