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
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using NightEdgeFrameworks.Utils.Abstract;

namespace NightEdgeFrameworks.Controllers
{
    /// <summary>
    /// Deal with basic logic dealing with entity data.
    /// </summary>
    public class NefxDataController
    {
        private NefxEntityController _entityCtl;
        private NefxPlayerController _playerCtl;


        /// <summary>
        /// Parameters : source, target. Subscribe to get entity killed event.
        /// </summary>
        public event Action<Guid, IDestroyableEntity> OnEntityKilled;
        /// <summary>
        /// Parameters: source, target, value, type.
        /// Subscribe to get taking damage event.
        /// e.g. display damage number when damaged.
        /// </summary>
        public event Action<Guid, IDestroyableEntity, float, DamageTypeFlag> OnTakingDamage;


        public void DoDamage(Guid source, IDestroyableEntity target, float damage, DamageTypeFlag type)
        {
            if (target == null) return;
            target.TakeDamage(source, damage);
            if (target.CurrentHitPoint <= 0 && target.IsKillable) KillEntity(source, target, true);
            OnTakingDamage?.Invoke(source, target, damage, type);
        }
        public IEnumerable<IDestroyableEntity> GetEnemiesInRange(IArmedEntity self, float radius) =>
           _entityCtl.GetAllEntities()
               .Where(x => x.GetType().GetInterfaces().Contains(typeof(IDestroyableEntity))
               && ((IDestroyableEntity)x).IsAlive
               && IsEnemy((self as NefxEntityBase).Guid, x.Guid)
               && Vector3.Distance(x.transform.position, (self as NefxEntityBase).transform.position) < radius)
               .Select(x => x as IDestroyableEntity);
        /// <summary>
        /// Whether two entities are enemies.
        /// </summary>
        public IEnumerable<Guid> GetInRange(Guid self, float radius) =>
            GetInRange(_entityCtl.GetEntity(self), radius)
            .Select(x => x.Guid);
        public IEnumerable<NefxEntityBase> GetInRange(NefxEntityBase self, float radius) =>
            _entityCtl.GetAllEntities()
                .Where(x => x != self
                && Vector3.Distance(x.transform.position, self.transform.position) < radius);
        public IDestroyableEntity GetNearestEnemy(NefxEntityBase target) => GetNearestEnemy(target.transform.position);
        public IDestroyableEntity GetNearestEnemy(Vector3 pos) => _entityCtl.GetAllEntities()
            .Where(x => (x.transform.position - pos).magnitude < 30f && IsHostile(x.Ownership, _playerCtl.MyPlayerSlot))
            .OrderBy(x => (x.transform.position - pos).magnitude)
            .FirstOrDefault() as IDestroyableEntity;
        public bool IsEnemy(Guid guidX, Guid guidY) => IsEnemy(_entityCtl.GetEntity(guidX), _entityCtl.GetEntity(guidY));
        /// <summary>
        /// Whether two entities are enemies.
        /// </summary>
        public bool IsEnemy(NefxEntityBase entX, NefxEntityBase entY) => _playerCtl.IsHostile(entX.Ownership, entY.Ownership);
        public bool IsHostile(int playerX, int playerY) => _playerCtl.IsHostile(playerX, playerY);
        public bool IsHostile(NefxEntityBase entX, NefxEntityBase entY) => IsHostile(entX.Ownership, entY.Ownership);
        public bool IsHostile(Guid entX, Guid entY) => IsHostile(_entityCtl.GetEntity(entX), _entityCtl.GetEntity(entY));
        /// <summary>
        /// Better call this by other controller than directly in a certain entity.
        /// </summary>
        public void KillEntity(Guid source, IDestroyableEntity target, bool useAnim = true)
        {
            target.KillSelf(source, useAnim);
            OnEntityKilled?.Invoke(source, target);
        }
        public NefxDataController()
        {
            _entityCtl = NefxControllerCollection.nefxEntityController;
            _playerCtl = NefxControllerCollection.nefxPlayerController;
        }
        public IDestroyableEntity PickEnemyTarget(IArmedEntity self, float radius)
        {
            var enemies = GetEnemiesInRange(self, radius);
            var res = enemies.FirstOrDefault();

            foreach (var enemy in enemies.Skip(1))
            {
                if (enemy.CurrentHitPoint < res.CurrentHitPoint)
                    res = enemy;
            }
            return res;
        }
        internal void Tick()
        {

        }
    }
}




