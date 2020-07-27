using System;
using System.Collections.Generic;
using System.Text;

namespace Aloha.Types
{
    public class AppOptions
    {
        public string Name { get; set; }
        public string Service { get; set; }
        public string Instance { get; set; }
        public string Version { get; set; }
        public bool DisplayBanner { get; set; } = true;
        public bool DisplayVersion { get; set; } = true;
    }
}
