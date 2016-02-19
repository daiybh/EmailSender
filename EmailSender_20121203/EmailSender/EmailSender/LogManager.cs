using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.IO;

namespace EmailSender
{
    public class LogManager
    {
        public static void Log(string log)
        {
            try
            {
                string logfile = ConfigurationSettings.AppSettings["logfile"];
                StreamWriter writer = new StreamWriter(logfile, true);
                writer.WriteLine(log);
                writer.Close();
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
