using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SnetTestProgram
{
    class Logger
    {
        public static void WriteLog(string str)
        {
            string currentDirectoryPath = Environment.CurrentDirectory.ToString();
            string directoryPath = Path.Combine(currentDirectoryPath, "interruptLog");

            string filePath = directoryPath + @"\MotionDoneTime_" + DateTime.Today.ToString("yyyyMMdd") + ".log";

            DirectoryInfo di = new DirectoryInfo(directoryPath);
            FileInfo fi = new FileInfo(filePath);

            try
            {
                if (!di.Exists)
                    Directory.CreateDirectory(directoryPath);

                string msg = string.Format("{0}: {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), str);

                if (!fi.Exists)
                {
                    using (StreamWriter sw = new StreamWriter(filePath))
                    {
                        sw.WriteLine(msg);
                        sw.Close();
                    }
                }
                else
                {
                    using(StreamWriter sw = File.AppendText(filePath))
                    {
                        sw.WriteLine(msg);
                        sw.Close();
                    }
                }
            }
            catch 
            { 
            }
        }

    }
}
