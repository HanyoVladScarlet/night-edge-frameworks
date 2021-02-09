using System;
using System.Collections.Generic;
using System.Text;

namespace NightEdgeFrameworks.Command
{
    class NefxComQueue
    {
        private static NefxComQueue ncs;
        /// <summary>
        /// 这个字典用作命令和相关函数名的映射，通过读取一个json文件或是dll文件获得
        /// </summary>
        public Dictionary<string,string> ComDict { get; set; }
        public List<string> ComList { get; set; }

        #region Initialize

        private NefxComQueue()
        {

        }

        #endregion

        public static NefxComQueue GetComStash()
        {
            if (ncs == null)
                ncs = new NefxComQueue();
            return ncs;
        }
    }
}
