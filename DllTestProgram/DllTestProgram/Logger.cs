using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DllTestProgram
{
    class Logger
    {
        public static void WriteLog(string str)
        {
            string currentDirectoryPath = Environment.CurrentDirectory.ToString();
            string directoryPath = Path.Combine(currentDirectoryPath, "timeLog");

            string filePath = directoryPath + @"\CallTime_" + DateTime.Today.ToString("yyyyMMdd") + ".log";

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
                    using (StreamWriter sw = File.AppendText(filePath))
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

        public static void WriteLogList(List<int> dataList)
        {
            string currentDirectoryPath = Environment.CurrentDirectory.ToString();
            string directoryPath = Path.Combine(currentDirectoryPath, "timeLog");

            string filePath = directoryPath + @"\CallTime_" + DateTime.Today.ToString("yyyyMMdd") + ".log";

            DirectoryInfo di = new DirectoryInfo(directoryPath);
            FileInfo fi = new FileInfo(filePath);

            try
            {
                List<string> msg = new List<string>();

                if (!di.Exists)
                    Directory.CreateDirectory(directoryPath);

                for (int i = 0; i < dataList.Count(); i++)
                {
                    int num = i % 10;
                    if (i == 0)
                        msg.Add(string.Format("{0}: {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), dataList[0].ToString()) + "nsec, ");
                    else if (i == dataList.Count() - 1)
                        msg.Add(dataList[i] + "nsec" + '\n');
                    else if (num == 0)
                        msg.Add(dataList[i] + "nsec," + '\n');
                    else
                        msg.Add(dataList[i] + "nsec, ");

                    if (!fi.Exists)
                    {
                        using (StreamWriter sw = new StreamWriter(filePath))
                        {
                            sw.Write(msg[i]);
                            sw.Close();
                        }
                    }
                    else
                    {
                        using (StreamWriter sw = File.AppendText(filePath))
                        {
                            sw.Write(msg[i]);
                            sw.Close();
                        }
                    }
                }
            }
            catch
            {
                Debug.WriteLine("error");
            }
        }
    }
}
