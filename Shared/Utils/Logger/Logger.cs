using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Utils.Logger
{
    #region EnumLogState
    public enum LogState : byte
    {
        Default = 0,
        Exception = 1,
        Error = 2,
        Info = 3,
        Warn = 4,
        Debug = 5,
        PacketDump = 6,
        None = 7,
    }    
    #endregion EnumLogState

    public class Logger
    {
        #region properties
        private static readonly Mutex Locker = new Mutex();
        public static TextWriter Writer { get; set; }
        public static uint LogLevel;
        public static string LogFile = string.Format(@"logs\{0}.log", DateTime.Now.ToString("d_M_yyyy HH_mm_ss"));
        #endregion properties

        #region Init
        public void Init(uint pLogLevel, string pLogFile)
        {      
            //set logger vars
            this.SetLogLevel(pLogLevel);
            this.SetLogFile(pLogFile);
        }
        #endregion Init

        #region Functions
        public static void WriteLine(LogState pLogLevel, string pFormat, params object[] pArgs)
        {
            //  if (LogLogLevel > (byte)pLogLevel) return;
            string header = "[" + DateTime.Now.ToShortTimeString() + "] (" + pLogLevel + ") ";
            string buffer = string.Format(pFormat, pArgs);

            Locker.WaitOne();
            try
            {
                Console.ForegroundColor = GetColor(pLogLevel);

                if ( ((LogState)Enum.Parse(typeof(LogState), pLogLevel.ToString())).ToString() != "None" )
                { 
                    Console.Write(header); 
                }
                    
                Console.ForegroundColor = ConsoleColor.Gray;  
                Console.WriteLine(buffer);
                if (String.IsNullOrEmpty(LogFile))
                {
                    return;
                }

                StreamWriter sw = File.AppendText(LogFile);
                try
                {
                    sw.WriteLine(header + buffer);
                }
                finally
                {
                    sw.Close();
                }
            }
            finally
            {
                Locker.ReleaseMutex();
            }
        }
        public void SetLogFile(string filename)
        {
            string path = filename.Replace(Path.GetFileName(filename), "");
            if (Directory.Exists(path))
            {
                if (!Directory.Exists(path + "backup"))
                {
                    Directory.CreateDirectory(path + "backup");
                }
                string[] filesToBackup = Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly);

                foreach (string file in filesToBackup)
                {
                    FileInfo mFile = new FileInfo(file);

                    if (new FileInfo(path + "backup\\" + mFile.Name).Exists == false)
                    {
                        mFile.MoveTo(path + "backup\\" + mFile.Name);
                    }
                }
            }
            else
            {
                Directory.CreateDirectory(filename.Replace(Path.GetFileName(filename), ""));
            }

            LogFile = filename;
        }
        public void SetLogLevel(uint level)
        {
            LogLevel = level;
        } 
        public static ConsoleColor GetColor(LogState pLevel)
        {
            switch (pLevel)
            {
                case LogState.None:
                    return ConsoleColor.DarkYellow;
                case LogState.Info:
                    return ConsoleColor.Green;
                case LogState.Warn:
                    return ConsoleColor.Yellow;
                case LogState.Debug:
                    return ConsoleColor.Magenta;
                case LogState.Error:
                    return ConsoleColor.DarkRed;
                case LogState.Exception:
                    return ConsoleColor.Red;
                default:
                    return ConsoleColor.White;
            }
        }
        #endregion Functions
    }
}
