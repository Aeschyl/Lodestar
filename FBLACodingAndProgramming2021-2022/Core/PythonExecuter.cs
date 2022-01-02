using IronPython.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBLACodingAndProgramming2021_2022.Core
{
    class PythonExecuter
    {
        public static string _pythonPath { get; set; }
        public static string _errors { get; set; }
        public static void executePython() 
        {
            //Providing Script location...Need to find better way to get path of the script for example when running on different machine
            var script = @"C:\Users\arche\source\repos\FBLACodingAndProgramming2021-2022\FBLACodingAndProgramming2021-2022\Python\src\api.py";

            var psi = new ProcessStartInfo(@"C:\Users\arche\source\repos\FBLACodingAndProgramming2021-2022\FBLACodingAndProgramming2021-2022\Python\src\api.py");
            //Providing python.exe location
            GetPythonPath();
            psi.FileName = _pythonPath + @"/Python39/python.exe";

            

            //Configuring process
            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;
            psi.RedirectStandardError = true;

            psi.Arguments = $"\"{script}\"";

            //Getting errors from script
            var errors = "";

            using (var process = Process.Start(psi))
            {
                errors = process.StandardError.ReadToEnd();
            }

            _errors = errors;


        }

        public static void executePythonEXE() 
        {
            Process.Start(@"C:\Users\arche\source\repos\FBLACodingAndProgramming2021-2022\FBLACodingAndProgramming2021-2022\Python\src\api.exe");
        }


        //Code provided by "Ernest Krom" on https://stackoverflow.com/questions/41920032/automatically-find-the-path-of-the-python-executable
        private static void GetPythonPath() 
        {
            var entries = Environment.GetEnvironmentVariable("path").Split(';');
            string python_location = null;

            foreach (string entry in entries)
            {
                if (entry.ToLower().Contains("python"))
                {
                    var breadcrumbs = entry.Split('\\');
                    foreach (string breadcrumb in breadcrumbs)
                    {
                        if (breadcrumb.ToLower().Contains("python"))
                        {
                            python_location += breadcrumb + '\\';
                            break;
                        }
                        python_location += breadcrumb + '\\';
                    }
                    break;
                }
            }
            _pythonPath = python_location;
        }
    }
}
