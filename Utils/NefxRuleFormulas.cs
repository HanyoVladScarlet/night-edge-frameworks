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
using System.Linq;
using UnityEngine;

namespace NightEdgeFrameworks.Utils
{
    public static class NefxRuleFormulas
    {
        private static readonly float ATK_RATE_FACTOR = - 0.01f;
        public static float GetFiringInterval(float firingRate) => Mathf.Exp(ATK_RATE_FACTOR * firingRate);
        public static float GetFiringRange(IWeaponryEntity entity) => (entity.FiringRange + entity.Accessories.Sum(x => x.FiringRange)) * (entity.Warhead?.PowderRate ?? 1f);
        public static float GetWarheadDamage(IWeaponryEntity weapon, IWarhead warhead) => weapon.FiringDamage + (warhead?.Damage ?? 0f);
    }
}