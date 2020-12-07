using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleTraining.Utilties
{
    public class FileIO
    {
        public static void RunPowerShellScript(string scriptPath) 
        {
            var process = Process.Start(@"C:\windows\system32\windowspowershell\v1.0\powershell.exe", scriptPath);
            process.WaitForExit();
        }
    }
}
