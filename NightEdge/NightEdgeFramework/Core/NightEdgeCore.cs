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

        public NightEdgeCore GetNightEdgeCore()
        {
            return nyx.nec;
        }

        public NefsFileSys GetNefsFileSys()
        {
            return nec.GetNefsFileSys();
        }
    }

    public class NightEdgeCore
    {
        private static NightEdgeCore core;
        private NefsFileSys fileSys; 

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

        public NefsFileSys GetNefsFileSys()
        {
            if (fileSys == null)
            {
                fileSys = NefsFileSys.GetNefsFileSys();
            }
            return fileSys;
        }
    }
}
