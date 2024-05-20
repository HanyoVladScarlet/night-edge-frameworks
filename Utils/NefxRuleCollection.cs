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
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using NightEdgeFrameworks.Utils.Abstract;
using NightEdgeFrameworks.Controllers;

namespace NightEdgeFrameworks.Utils
{
    /// <summary>
    /// Use this set of methods to swiftly deal with game logic 
    /// </summary>
    public static class NefxRuleCollection
    {
        private static int playerId => _playerCtl.MyPlayerSlot;
        private static NefxPlayerController _playerCtl = NefxControllerCollection.nefxPlayerController;
        private static NefxEntityController _entityCtl = NefxControllerCollection.nefxEntityController;
        private static NefxDataController _dataCtl = NefxControllerCollection.nefxDataController;


        public static bool AnyEnemy() => _entityCtl.GetAllEntities().Any(x => IsHostile(x.Ownership, playerId));
        public static bool AnyMyPawn() => _entityCtl.GetAllEntities().Any(x => x.Ownership ==  playerId);
        public static int CountEnemy() => _entityCtl.GetAllEntities().Count(x => IsHostile(x.Ownership, playerId));
        public static int CountMyPawn() => _entityCtl.GetAllEntities().Count(x => x.Ownership == playerId);
        public static float GetDistance(NefxEntityBase ent1, NefxEntityBase ent2) =>  
            GetPointToVector(ent1, ent2).magnitude;
        public static Vector3 GetPointToVector(NefxEntityBase ent1, NefxEntityBase ent2) => 
            ent1.transform.position - ent2.transform.position;
        public static IDestroyableEntity GetNearestEnemy(Vector3 pos) => _dataCtl.GetNearestEnemy(pos);
        public static IDestroyableEntity GetNearestEnemy(NefxEntityBase entity) => _dataCtl.GetNearestEnemy(entity);
        public static IEnumerable<IDestroyableEntity> GetTargetsInRange(IArmedEntity entity) => 
            _dataCtl.GetEnemiesInRange(entity, entity.AttackRange);
        public static bool IsAlive(IDestroyableEntity entity) => entity != null && entity.IsAlive;
        public static bool IsAllied(int playerX, int playerY) => _playerCtl.IsAllied(playerX, playerY);
        public static bool IsAllied(NefxEntityBase entX, NefxEntityBase entY) => IsAllied(entX.Ownership, entY.Ownership);
        public static bool IsAllied(Guid entX, Guid entY) => IsAllied(_entityCtl.GetEntity(entX), _entityCtl.GetEntity(entY));
        public static bool IsHostile(int playerX, int playerY) => _playerCtl.IsHostile(playerX, playerY);
        public static bool IsHostile(NefxEntityBase entX, NefxEntityBase entY) => IsHostile(entX.Ownership, entY.Ownership);
        public static bool IsHostile(Guid entX, Guid entY) => IsHostile(_entityCtl.GetEntity(entX), _entityCtl.GetEntity(entY));
        /// <summary>
        /// Return whether current target NOT_NULL and ALIVE and IN_RANGE.
        /// </summary>
        public static bool IsTargetValid(IArmedEntity source) => IsTargetValid(source, source.Target);
        /// <summary>
        /// Return whether current target NOT_NULL and ALIVE and IN_RANGE.
        /// </summary>
        public static bool IsTargetValid(IArmedEntity source, IDestroyableEntity target) => TargetInRange(source, target) && IsAlive(target);
        /// <summary>
        /// Return whether current target is in firing range.
        /// </summary>
        public static bool TargetInRange(IArmedEntity source) => TargetInRange(source, source.Target);
        /// <summary>
        /// Return whether current target is in firing range.
        /// </summary>
        public static bool TargetInRange(IArmedEntity source, IDestroyableEntity target) => target == null ? false : GetDistance(source as NefxEntityBase, target as NefxEntityBase) < source.AttackRange;
    }
}