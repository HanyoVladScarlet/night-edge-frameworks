/********************************************************************************************
*                                                                                           *
*                                Project Name : Mutopia                                     *
*                                                                                           *
*                                  Programmer : Hatuki                                      *
*                                                                                           *
*                                  Start Date : 2024-05-10                                  *
*                                                                                           *
*                                 Last Update : 2024-05-10                                  *
*                                                                                           *
*-------------------------------------------------------------------------------------------*
*-------------------------------------------------------------------------------------------*/


using System;
using NightEdgeFrameworks.Models.Abstract;
using NightEdgeFrameworks.Utils;
using NightEdgeFrameworks.Utils.Abstract;

namespace NightEdgeFrameworks.Controllers
{
    /// <summary>
    /// 关于战斗行为的控制器.
    /// </summary>
    public class NefxCombatController
    {
        private NefxDataController _dataController;
        public NefxCombatController()
        {
            _dataController = NefxControllerCollection.nefxDataController;
        }

        /// <summary>
        /// Temporary implement
        /// </summary>
        [Obsolete("Temporary Implementation.")]
        public void Attack(IArmedEntity source, IDestroyableEntity target)
        {
            //Debug.Log($"Attack on target {(target as NefxEntityBase).name} from {(source as NefxEntityBase).name}");
            _dataController.DoDamage(
                (source as NefxEntityBase).Guid,
                target, NefxRuleFormulas.GetWarheadDamage(source.WeaponActive, source.WeaponActive.Warhead),
                DamageTypeFlag.Physical
            );
        }

    }
}


