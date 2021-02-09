using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NightEdgeFrameworks;
using NightEdgeFrameworks.MathLib;

namespace NightEdgeFrameworks.Clock
{
    public class Thinker:IDisposable        
    {
        public static ThinkerPool ThinkerPool { get { return ThinkerPool.GetThinkerPool(); } }

        public bool IsFired { get { return this.is_fired; } }
        private bool is_fired = false;
        private int current_count;
        private int max_count;
        private Action callBackMethod;

        #region Ctor
        private Thinker(Action action, int time)
        {
            NefxFracInt time_fra = new NefxFracInt(time, 1000);
            time_fra.NF_AmplifyFracint(10);
            this.current_count = 0;
            this.max_count = (int)(time * 128);
            this.callBackMethod = action;
            ThinkerPool.AddThinker(this);
        }

        // 需修改！
        private Thinker(Action action, int time, int period, int rounds)
        {
            this.current_count = 0;
            this.max_count = (int)(time * 128);
            this.callBackMethod = action;
        }

        public static Thinker Think(Action callBackMethod, int time)
        {
            var th = new Thinker(callBackMethod, time);
            return th;
        }

        public static Thinker Think(Action callBackMethod, int time, int period, int rounds)
        {
            var th = new Thinker(callBackMethod, time, period, rounds);
            return th;
        }

        #endregion

        internal void Tick()
        {
            current_count += TimeFlowRate.ThinkRate.nf_numerator;
        }
        
        internal void Fire()
        {
            if (this.current_count >= this.max_count)
            {
                this.callBackMethod.Invoke();
            }
        }

        public void Dispose()
        {
            
        }

    }
   
    [Initias]
    public class ThinkerPool
    {
        private static ThinkerPool thinkerPool;
        private List<Thinker> thinkerList;

        private ThinkerPool()
        {

        }

        internal ThinkerPool GetThinkerPool()
        {
            // 初始化ThinkerPool
            if (thinkerPool == null)
            {
                thinkerPool = new ThinkerPool();
                GlobalClockTick.GetGlobalClockTick().Tick128 += OnTick128;
            }

            return thinkerPool;
        }

        private void Initialize()
        {
            GetThinkerPool();
        }

        public static Thinker StartNew(Action callBackMethod,int delay)
        {
            var th = Thinker.Think(callBackMethod, delay);
            thinkerPool.AddThinker(th);
            return th;
        }

        // 将一个Thinker添加到thinkerList当中去
        internal void AddThinker(Thinker th)
        {
            if(!thinkerList.Contains(th))
                this.thinkerList.Add(th);
        }

        // GlobalClockTick.Tick128事件回调函数：
        // 1. 检查失效的Thinker
        // 2. 将Thinker的count++
        // 3. 检查并Fire满足要求的Thinker
        private void OnTick128(object sender)
        {
            this.RemoveThinker();
            this.TickThinker();
            this.FireThinker();
        }

        private void RemoveThinker()
        {
            List<Thinker> invalidThinkers = new List<Thinker>();
            foreach (var th in this.thinkerList)
            {
                if (th.IsFired)
                {
                    invalidThinkers.Add(th);    // 使用IsFired来推断方法是否失效
                }
            }
            foreach (var th in invalidThinkers)
            {
                thinkerList.Remove(th);
            }
        }

        private void TickThinker()
        {
            foreach (var th in thinkerList)
            {
                th.Tick();
            }
        }

        private void FireThinker()
        {
            foreach (var th in thinkerList)
            {
                th.Fire();
            }
        }

    }
}
