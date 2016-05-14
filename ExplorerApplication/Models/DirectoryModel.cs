using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace ExplorerApplication.Models
{
    public class DirectoryModel
    {
        public string Name { get; set; }
        public List<FileModel> Files = new List<FileModel>();

        public DirectoryModel(string path)
        {
            if (Directory.Exists(path))
            {
                this.Name = path;
                DirectoryInfo dir = new DirectoryInfo(path);
                
                
                DirectoryInfo[] directories = dir.GetDirectories();
                foreach (var directory in directories)
                {
                    FileModel fileModel = new FileModel() { Name = directory.Name, Date = directory.CreationTime, Size = 0, Type = "directory" };
                    this.Files.Add(fileModel);
                }

                FileInfo[] files = dir.GetFiles();
                foreach (var file in files)
                {
                    FileModel fileModel = new FileModel() { Name = file.Name, Date = file.CreationTime, Size = file.Length, Type = file.Extension };
                    this.Files.Add(fileModel);
                }


            }
            //this.Files.Add(new FileModel() { Name="test", Date="22.02.2015", Size = 1213  });
        }
    }
}