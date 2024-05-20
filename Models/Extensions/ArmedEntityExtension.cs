/********************************************************************************************
*                                                                                           *
*                                Project Name : Mutopia                                     *
*                                                                                           *
*                                  Programmer : Hatuki                                      *
*                                                                                           *
*                                  Start Date : 2024-05-09                                  *
*                                                                                           *
*                                 Last Update : 2024-05-10                                  *
*                                                                                           *
*-------------------------------------------------------------------------------------------*
*-------------------------------------------------------------------------------------------*/


using NightEdgeFrameworks.Models.Abstract;
using NightEdgeFrameworks.Utils;

namespace NightEdgeFrameworks.Models.Extensions
{
    public static class ArmedEntityExtension
    {
        public static bool TargetInRange(this IArmedEntity self) => TargetInRange(self, self.Target);
        public static bool TargetInRange(this IArmedEntity self, IDestroyableEntity target) => NefxRuleCollection.IsTargetValid(self, target);
    }
}