using System;
using System.Collections.Generic;
using System.Text;

namespace EasyWeb.Core.Logger.Configurations
{
    public class LoggerConfiguration
    {
        public string FileName { get; set; }
        public string LogPath { get; set; }
        public int MaxArchiveFiles { get; set; }
    }
}
