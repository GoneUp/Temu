using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Logger;


namespace Configuration
{
    public class Task
    {
        #region Loader
        protected delegate int Loader();
        protected static List<Loader> Loaders = new List<Loader> { };

        public static void LoadAll()
        {
            Parallel.For(0, Loaders.Count, i => LoadTask(Loaders[i]));
        }
        private static void LoadTask(Loader loader)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            int readed = loader.Invoke();
            stopwatch.Stop();

            Logger.WriteLine(LogState.Info, "Init: {0} {1} values in {2}s"
                , loader.Method.Name
                , readed
                , (stopwatch.ElapsedMilliseconds / 1000.0).ToString("0.00"));
        }
        #endregion Loader

        public static void Init()
        {
            Logger.WriteLine(LogState.Info, "TaskLoader System v0.1...connected!");
            //Init Data
            LoadAll(); //Load Config Files
        }

    }
}
