using System;
using System.Collections.Generic;
using System.Text;

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

        public FileAgent GetNefsFileSys()
        {
            return nec.GetFileAgent();
        }

        public NetworkAgent GetNetworkAgent()
        {
            return nec.GetNetworkAgent();
        }
        #endregion

    }

    public class NightEdgeCore
    {
        private static NightEdgeCore core;
        private FileAgent fileSys;
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
            if (fileSys == null)
            {
                fileSys = FileAgent.GetFileAgent();
            }
            return fileSys;
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
