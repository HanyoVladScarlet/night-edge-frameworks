using System;
using System.Collections.Generic;
using System.Text;

namespace NightEdgeFramework
{
    class ExampleStatus : IGlobalClockListener
    {
        public void OnDoubleTick()
        {
            throw new NotImplementedException();
        }

        public void OnTick()
        {
            throw new NotImplementedException();
        }
    }

    class Ex : ExampleStatus
    {
         public new void OnDoubleTick()
         {

         }
    }
}
