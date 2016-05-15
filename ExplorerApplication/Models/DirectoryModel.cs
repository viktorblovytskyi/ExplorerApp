using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace ExplorerApplication.Models
{
    /// <summary>
    /// This model describes the properties of the directory.
    /// </summary>
    public class DirectoryModel
    {
        public string Name;
        public List<FileModel> Files;  
    }
}