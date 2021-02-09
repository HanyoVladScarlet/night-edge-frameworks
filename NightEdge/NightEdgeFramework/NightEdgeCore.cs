using System;
using System.Collections.Generic;
using System.Text;
using NightEdgeFrameworks.Clock;
using NightEdgeFrameworks.Debug;
using NightEdgeFrameworks.Network;
using NightEdgeFrameworks.FileLib;

namespace NightEdgeFrameworks
{
    public class Nyx
    {
        private static Nyx nyx;
        private NightEdgeCore nec;

        #region Initialize

        private Nyx()
        {
            Initialize();
        }

        private void Initialize()
        {
            nec = NightEdgeCore.GetNightEdgeCore();
        }

        public static Nyx GetNyx()
        {
            if (nyx == null)
            {
                nyx = new Nyx();
            }
            return nyx;
        }

        #endregion


        #region GetMethods
        public NightEdgeCore GetNightEdgeCore()
        {
            return nyx.nec;
        }

        public FileAgent GetFileAgent()
        {
            return nec.GetFileAgent();
        }

        public NetworkAgent GetNetworkAgent()
        {
            return nec.GetNetworkAgent();
        }

        public NefxConsole GetNefxConsole()
        {
            return NefxConsole.GetNefxConsole();
        }
        #endregion

        public void Think(Action action, int time)
        {
            Thinker.Think(action, time);
        }
    }

    public class NightEdgeCore
    {
        private static NightEdgeCore core;
        private FileAgent fileAgent;
        private NetworkAgent networkAgent;
        private GlobalClockTick tick;

        private NightEdgeCore()
        {
            tick = GlobalClockTick.GetGlobalClockTick();
        }

        public static NightEdgeCore GetNightEdgeCore()
        {
            if(core == null)
            {
                core = new NightEdgeCore();
            }
            return core;
        }

        public FileAgent GetFileAgent()
        {
            if (fileAgent == null)
            {
                fileAgent = FileAgent.GetFileAgent();
            }
            return fileAgent;
        }

        public NetworkAgent GetNetworkAgent()
        {
            if (networkAgent == null)
            {
                networkAgent = NetworkAgent.GetNetworkAgent();
            }
            return networkAgent;
        }

    }

}
