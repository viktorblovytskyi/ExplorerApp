using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExplorerApplication.Models
{
    public class DirectoryModel
    {
        public string Name { get; private set; }
        public string ParentDirectory { get; private set; }
        public List<FileModel> Files;

        public DirectoryModel(string dirName)
        {
            this.Name = dirName;
            this.Files.Add(new FileModel() { Name="test", Date="22.02.2015", Size = 1213  });
        }
    }
}