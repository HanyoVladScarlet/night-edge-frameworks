using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using NightEdgeFramework.Command;

namespace NightEdgeFramework.Core
{
    public class CommandAgent
    {
        private static CommandAgent ca;
        private NefxComStash ncs;
        private GlobalClockTick gct;

        #region Initialize

        private void Initialize()
        {
            ncs = NefxComStash.GetComStash();
            gct = GlobalClockTick.GetGlobalClockTick();
            gct.Tick64 += OnTick64;
        }

        private void OnTick64(object sender)
        {
            throw new NotImplementedException();
        }

        private CommandAgent()
        {
            Initialize();
        }

        #endregion

        #region PublicMethods

        static public CommandAgent GetCommandAgent()
        {
            if (ca == null)
                ca = new CommandAgent();
            return ca;
        }

        public void PushCommand(string com)
        {
            if (ncs.ComDict.ContainsKey(com))
                ncs.ComList.Add(com);
        }

        #endregion

        #region PrivateMethods

        private void ExecuteCommands()
        {
            foreach (var item in ncs.ComList)
            {
                // 使用反射，按照字符串索引函数并执行
            }
            ncs.ComList.Clear();
        }

        #endregion


    }

}
