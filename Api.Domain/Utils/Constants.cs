using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Domain.Utils
{
    public static class Constants
    {
        private static readonly string _workDir = Environment.CurrentDirectory;
        private static readonly string _projectDir = Directory.GetParent(_workDir).Parent.Parent.FullName;
        public static readonly string _scriptPath = Path.Combine(_projectDir, "PythonScript");
    }
}
