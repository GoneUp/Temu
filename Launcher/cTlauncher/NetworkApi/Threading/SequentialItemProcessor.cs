// Type: Hik.Threading.SequentialItemProcessor`1
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hik.Threading
{
    /// <summary>
    /// This class is used to process items sequentially in a multithreaded manner.
    /// 
    /// </summary>
    /// <typeparam name="TItem">Type of item to process</typeparam>
    public class SequentialItemProcessor<TItem>
    {
        /// <summary>
        /// An object to synchronize threads.
        /// 
        /// </summary>
        private readonly object _syncObj = new object();
        /// <summary>
        /// The method delegate that is called to actually process items.
        /// 
        /// </summary>
        private readonly Action<TItem> _processMethod;
        /// <summary>
        /// Item queue. Used to process items sequentially.
        /// 
        /// </summary>
        private readonly Queue<TItem> _queue;
        /// <summary>
        /// A reference to the current Task that is processing an item in
        ///             ProcessItem method.
        /// 
        /// </summary>
        private Task _currentProcessTask;
        /// <summary>
        /// Indicates state of the item processing.
        /// 
        /// </summary>
        private bool _isProcessing;
        /// <summary>
        /// A boolean value to control running of SequentialItemProcessor.
        /// 
        /// </summary>
        private bool _isRunning;

        /// <summary>
        /// Creates a new SequentialItemProcessor object.
        /// 
        /// </summary>
        /// <param name="processMethod">The method delegate that is called to actually process items</param>
        public SequentialItemProcessor(Action<TItem> processMethod)
        {
            this._processMethod = processMethod;
            this._queue = new Queue<TItem>();
        }

        /// <summary>
        /// Adds an item to queue to process the item.
        /// 
        /// </summary>
        /// <param name="item">Item to add to the queue</param>
        public void EnqueueMessage(TItem item)
        {
            lock (this._syncObj)
            {
                if (!this._isRunning)
                    return;
                this._queue.Enqueue(item);
                if (this._isProcessing)
                    return;
                this._currentProcessTask = Task.Factory.StartNew(new Action(this.ProcessItem));
            }
        }

        /// <summary>
        /// Starts processing of items.
        /// 
        /// </summary>
        public void Start()
        {
            this._isRunning = true;
        }

        /// <summary>
        /// Stops processing of items and waits stopping of current item.
        /// 
        /// </summary>
        public void Stop()
        {
            this._isRunning = false;
            lock (this._syncObj)
                this._queue.Clear();
            if (!this._isProcessing)
                return;
            try
            {
                this._currentProcessTask.Wait();
            }
            catch
            {
            }
        }

        /// <summary>
        /// This method runs on a new seperated Task (thread) to process
        ///             items on the queue.
        /// 
        /// </summary>
        private void ProcessItem()
        {
            TItem obj;
            lock (this._syncObj)
            {
                if (!this._isRunning || this._isProcessing || this._queue.Count <= 0)
                    return;
                this._isProcessing = true;
                obj = this._queue.Dequeue();
            }
            this._processMethod(obj);
            lock (this._syncObj)
            {
                this._isProcessing = false;
                if (!this._isRunning || this._queue.Count <= 0)
                    return;
                this._currentProcessTask = Task.Factory.StartNew(new Action(this.ProcessItem));
            }
        }
    }
}
