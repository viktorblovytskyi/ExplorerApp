using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExplorerApplication.Models
{
    public class FileModel
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public long Size { get; set; }
        public string Type { get; set; }        
    }
}