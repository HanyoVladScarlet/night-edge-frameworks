/********************************************************************************************
*                                                                                           *
*                                Project Name : Mutopia                                     *
*                                                                                           *
*                                  Programmer : Hatuki                                      *
*                                                                                           *
*                                  Start Date : 2024-05-14                                 *
*                                                                                           *
*                                 Last Update : 2024-05-14                                  *
*                                                                                           *
*-------------------------------------------------------------------------------------------*
*-------------------------------------------------------------------------------------------*/

using NightEdgeFrameworks.Utils.Abstract;
using System.Collections.Generic;

namespace NightEdgeFrameworks.Models.Abstract
{
    public interface IWeaponryEntity
    {
        public int AverageWarheadCount { get; set; }
        public int CurrentAmmo { get; set; }
        public float FiringDamage { get; set; }
        public float FiringRange { get; set; }
        public float FiringRate { get; set; }
        public bool Functional { get; }
        public bool IsFiring { get; }
        public bool IsReloading { get; }
        public bool IsAmmoDepleted { get; }
        public int MaxAmmo { get; set; }
        public bool TimeReloadingRemaining { get; }
        public bool TimeReloading { get; set; }
        public int WarheadCount { get; }
        public IWarhead Warhead { get; }
        public IList<IWeaponryAccessory> Accessories { get; }
        public void BeginFire();
        public void BeginReload();
        public void EndFire();
        public void EndReload();
        public void Implode();
        public void Reload();
        public void Stuck();
    }
}