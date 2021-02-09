using System;
using System.Collections.Generic;
using System.Text;

namespace NightEdgeFrameworks.Network
{
    public class NefxNetSolver
    {
        private static NefxNetSolver solver;

        private NefxNetSolver()
        {

        }

        public static NefxNetSolver GetNefsNetSolver()
        {
            if(solver == null)
            {
                solver = new NefxNetSolver();
            }
            return solver;
        }
    }
}
