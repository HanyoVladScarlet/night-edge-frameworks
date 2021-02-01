using System;
using System.Collections.Generic;
using System.Text;
using NightEdgeFramework.Console;

namespace NightEdgeFramework.Core
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

    }

    public class NightEdgeCore
    {
        private static NightEdgeCore core;
        private FileAgent fileAgent;
        private NetworkAgent networkAgent;

        private NightEdgeCore()
        {

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
