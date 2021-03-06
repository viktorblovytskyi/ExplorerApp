﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExplorerApplication.Models
{
    /// <summary>
    /// This model describes the properties of the file.
    /// </summary>
    public class FileModel
    {
        public string Name;
        public DateTime Date;
        public long Size;
        public string Type;
        public string FullName;      
    }
}