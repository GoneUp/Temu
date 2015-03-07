// Type: Hik.Threading.Timer
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using System;
using System.Threading;

namespace Tera.NetworkApi.Threading
{
    /// <summary>
    /// This class is a timer that performs some tasks periodically.
    /// 
    /// </summary>
    public class Timer
    {
        /// <summary>
        /// This timer is used to perfom the task at spesified intervals.
        /// 
        /// </summary>
        private readonly System.Threading.Timer _taskTimer;
        /// <summary>
        /// Indicates that whether timer is running or stopped.
        /// 
        /// </summary>
        private volatile bool _running;
        /// <summary>
        /// Indicates that whether performing the task or _taskTimer is in sleep mode.
        ///             This field is used to wait executing tasks when stopping Timer.
        /// 
        /// </summary>
        private volatile bool _performingTasks;

        /// <summary>
        /// Task period of timer (as milliseconds).
        /// 
        /// </summary>
        public int Period { get; set; }

        /// <summary>
        /// Indicates whether timer raises Elapsed event on Start method of Timer for once.
        ///             Default: False.
        /// 
        /// </summary>
        public bool RunOnStart { get; set; }

        /// <summary>
        /// This event is raised periodically according to Period of Timer.
        /// 
        /// </summary>
        public event EventHandler Elapsed;

        /// <summary>
        /// Creates a new Timer.
        /// 
        /// </summary>
        /// <param name="period">Task period of timer (as milliseconds)</param>
        public Timer(int period)
            : this(period, false)
        {
        }

        /// <summary>
        /// Creates a new Timer.
        /// 
        /// </summary>
        /// <param name="period">Task period of timer (as milliseconds)</param><param name="runOnStart">Indicates whether timer raises Elapsed event on Start method of Timer for once</param>
        public Timer(int period, bool runOnStart)
        {
            this.Period = period;
            this.RunOnStart = runOnStart;
            this._taskTimer = new System.Threading.Timer(new TimerCallback(this.TimerCallBack), (object)null, -1, -1);
        }

        /// <summary>
        /// Starts the timer.
        /// 
        /// </summary>
        public void Start()
        {
            this._running = true;
            this._taskTimer.Change(this.RunOnStart ? 0 : this.Period, -1);
        }

        /// <summary>
        /// Stops the timer.
        /// 
        /// </summary>
        public void Stop()
        {
            lock (this._taskTimer)
            {
                this._running = false;
                this._taskTimer.Change(-1, -1);
            }
        }

        /// <summary>
        /// Waits the service to stop.
        /// 
        /// </summary>
        public void WaitToStop()
        {
            lock (this._taskTimer)
            {
                while (this._performingTasks)
                    Monitor.Wait((object)this._taskTimer);
            }
        }

        /// <summary>
        /// This method is called by _taskTimer.
        /// 
        /// </summary>
        /// <param name="state">Not used argument</param>
        private void TimerCallBack(object state)
        {
            lock (this._taskTimer)
            {
                if (!this._running || this._performingTasks)
                    return;
                this._taskTimer.Change(-1, -1);
                this._performingTasks = true;
            }
            try
            {
                if (this.Elapsed == null)
                    return;
                this.Elapsed((object)this, new EventArgs());
            }
            catch
            {
            }
            finally
            {
                lock (this._taskTimer)
                {
                    this._performingTasks = false;
                    if (this._running)
                        this._taskTimer.Change(this.Period, -1);
                    Monitor.Pulse((object)this._taskTimer);
                }
            }
        }
    }
}
