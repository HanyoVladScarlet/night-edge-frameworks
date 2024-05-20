/********************************************************************************************
*                                                                                           *
*                                Project Name : Mutopia                                     *
*                                                                                           *
*                                  Programmer : Hatuki                                      *
*                                                                                           *
*                                  Start Date : 2024-05-14                                  *
*                                                                                           *
*                                 Last Update : 2024-05-14                                  *
*                                                                                           *
*-------------------------------------------------------------------------------------------*
*-------------------------------------------------------------------------------------------*/


using UnityEngine;
using System;
using NightEdgeFrameworks.Models.Abstract;
using System.Collections.Generic;


namespace NightEdgeFrameworks.Utils.Abstract
{
    public abstract class WeaponEntityBase : NefxEntityBase, IWeaponryEntity
    {
        private IList<IWeaponryAccessory> _accessories = new List<IWeaponryAccessory>();


        public int AverageWarheadCount { get; set; }
        private bool CooldownReady => Time.time - _lastCast > NefxRuleFormulas.GetFiringInterval(FiringRate);
        public int CurrentAmmo { get; set; }
        public float FiringDamage { get; set; }
        public float FiringRate { get; set; }
        public bool Functional => true;
        public bool IsAmmoDepleted => CurrentAmmo <= 0;
        public bool IsFiring { get; set; }
        public bool IsReloading { get; private set; }
        public int MaxAmmo { get; set; }
        public bool TimeReloading { get; set; }
        public bool TimeReloadingRemaining { get; private set; }
        public IWarhead Warhead { get; set; }
        public int WarheadCount { get; set; }
        public float FiringRange { get; set; }
        public IList<IWeaponryAccessory> Accessories => _accessories;


        private float _lastCast = 0f;
        private float _lastFiring = 0f;
        private bool ShouldEndFire => Time.time - _lastFiring > (Parent as IArmedEntity).AttackDuration;
        public virtual void CastWarhead()
        {
            Debug.Log($"Interval : {NefxRuleFormulas.GetFiringInterval(FiringRate)}");
            if (MaxAmmo > 0) CurrentAmmo--;
            _lastCast = Time.time;
        }

        public override void Tick()
        {
             //CastWarhead();
            if (CooldownReady && IsFiring) CastWarhead();
            if (ShouldEndFire) EndFire();
        }

        public void BeginFire()
        {
            IsFiring = true;
            _lastFiring = Time.time;
        }

        public void EndFire()
        {
            IsFiring = false;
        }

        void IWeaponryEntity.Reload()
        {
            throw new NotImplementedException();
        }

        void IWeaponryEntity.BeginReload()
        {
            throw new NotImplementedException();
        }

        void IWeaponryEntity.EndReload()
        {
            CurrentAmmo = MaxAmmo;
        }

        void IWeaponryEntity.Stuck()
        {
            throw new NotImplementedException();
        }

        void IWeaponryEntity.Implode()
        {
            throw new NotImplementedException();
        }
    }


}


namespace NightEdgeFrameworks.Models
{
}

