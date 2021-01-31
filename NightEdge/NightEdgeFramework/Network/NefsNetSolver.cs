using System;
using System.Collections.Generic;
using System.Text;

namespace NightEdgeFramework.Network
{
    public class NefsNetSolver
    {
        private static NefsNetSolver solver;

        private NefsNetSolver()
        {

        }

        public static NefsNetSolver GetNefsNetSolver()
        {
            if(solver == null)
            {
                solver = new NefsNetSolver();
            }
            return solver;
        }
    }
}
