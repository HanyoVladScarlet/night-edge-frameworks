using System;
using System.Collections.Generic;
using System.Text;

namespace NightEdgeFramework
{

    /// <summary>
    /// This interface is used for factory buildings such as barracks and war factories.
    /// </summary>
    interface IWarFactory
    {
        public List<UnitBase> ProductQueue { get; set; }


        //public async void FinishQueue()
        //{
        //    // await 
        //} 
    }
}
