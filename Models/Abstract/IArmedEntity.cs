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


using NightEdgeFrameworks.Utils.Abstract;
using System.Collections.Generic;

namespace NightEdgeFrameworks.Models.Abstract
{
    public interface IArmedEntity : INefxDataBase
    {
        //public float AttackValue { get; }
        //public float AttackRate { get; }

        /// <summary>
        /// Threats out of alert angle have less risk of being spotted.
        /// </summary>
        public float AlertAngle { get; set; }
        public float AttackDuration { get; set; }
        public float AttackInterval { get; set; }
        public float AttackRange { get; }
        public bool IsFiring { get; }
        public bool NeedReload { get; }
        public bool ReadyToFire { get; }
        public IDestroyableEntity Target { get; }
        public IWeaponryEntity WeaponActive { get; }
        public IList<IWeaponryEntity> WeaponList { get; }
        /// <summary>
        /// Ready aim fire, if there is a target.
        /// </summary>
        public void ArrangeAttack();
        /// <summary>
        /// Trigger the weaponry.
        /// </summary>
        public void AttackOnTarget();
        public void Reload();
        public void ScanEnemy();
        public void SetTarget(IDestroyableEntity target);
        public void SetWeaponActive(IWeaponryEntity weapon);
        public void AddWeapon(IWeaponryEntity weapon);
        public void RemoveWeapon(IWeaponryEntity weapon);
    }
}