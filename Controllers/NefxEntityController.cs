/********************************************************************************************
*                                                                                           *
*                                Project Name : Mutopia                                     *
*                                                                                           *
*                                  Programmer : Hatuki                                      *
*                                                                                           *
*                                  Start Date : 2024-05-09                                  *
*                                                                                           *
*                                 Last Update : 2024-05-15                                *
*                                                                                           *
*-------------------------------------------------------------------------------------------*
*-------------------------------------------------------------------------------------------*/



using NightEdgeFrameworks.Utils.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;


namespace NightEdgeFrameworks.Controllers
{
    public enum DataHolderFlag
    {
        Destroyable = 0,
    }


    public class NefxEntityController
    {
        private static NefxEntityController instance;
        public static NefxEntityController entityCtl
        {
            get
            {
                if (instance == null) instance = new NefxEntityController();
                return instance;
            }
        }
        /***********************************************************************************************
        *                                                                                              *
        * Used to deal with entity register, all NefxEntityBase shall be registered once instantiated. *
        *                                                                                              *
        *                                                                                              *
        *                                                                                              *
        *----------------------------------------------------------------------------------------------*
        *----------------------------------------------------------------------------------------------*/
        private IDictionary<Guid, NefxEntityBase> _entityTable = new Dictionary<Guid, NefxEntityBase>();
        private IList<Guid> _activeEntities = new List<Guid>();
        public NefxEntityBase GetEntity(Guid id) => _entityTable.ContainsKey(id) ? _entityTable[id] : default;
        public IEnumerable<NefxEntityBase> GetAllEntities() => _entityTable.Values;


        /// <summary>
        /// Call when new NefxEntity is created.
        /// </summary>
        internal bool Register(NefxEntityBase entity)
        {
            try
            {
                while (_entityTable.ContainsKey(entity.Guid))
                {
                    entity.Guid = Guid.NewGuid();
                }
                _entityTable.Add(entity.Guid, entity);

                //RegisterEntityData(entity);


                //Debug.Log($"Guid: {entity.Guid} registered.");

                return true;
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                return false;
            }
        }

        /// <summary>
        /// Call when one NefxEntity has been destroyed
        /// </summary>
        internal bool Unregister(Guid entityGuid)
        {
            try
            {
                var entity = _entityTable[entityGuid];
                if (entity != null)
                {
                    _entityTable.Remove(entityGuid);
                }
                //UnregisterEntityData(entity);
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                return false;
            }
        }


        /************************************************************************************************
        *                                                                                               *
        * Used to deal with active entity, NefxEntityBase shall be active once selected and ready to be *
        * commanded.                                                                                    *
        *                                                                                               *
        *                                                                                               *
        *-----------------------------------------------------------------------------------------------*
        *-----------------------------------------------------------------------------------------------*/


        /// <summary>
        /// Add an entity to active list.
        /// </summary>
        /// <param name="id"></param>
        public void AddActive(Guid id)
        {
            if (!_activeEntities.Contains(id)) _activeEntities.Add(id);
        }
        /// <summary>
        /// Check whether an entity is active.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool ContainsActive(Guid id) => _activeEntities.Contains(id);
        /// <summary>
        /// 清除被选中对象的列表.
        /// </summary>
        public void ClearActive()
        {
            foreach (var entity in _activeEntities)
            {
                var ent = GetEntity(entity);
            }
            _activeEntities.Clear();
        }
        public IEnumerable<NefxEntityBase> GetActives() => _activeEntities
            .Select(x => GetEntity(x)).Where(x => x != null).ToList();
        /// <summary>
        /// 从激活的对象列表中去除一个.
        /// </summary>
        /// <param name="id"></param>
        public void RemoveActive(Guid id)
        {
            if (_activeEntities.Contains(id)) _activeEntities.Remove(id);
        }

        #region DataHolder
        ///// <summary>
        ///// Data sheet map.
        ///// </summary>
        //public readonly IList<IDataHolder> HolderList = new IDataHolder[] {
        //    new DataHolderDestroyable(),
        //};
        ///// <summary>
        ///// Unregister data sheet for each entity.
        ///// </summary>
        //private void UnregisterEntityData(NefxEntityBase entity)
        //{
        //    foreach (var hud in HolderList)
        //    {
        //        hud.TryRegister(entity);
        //    }
        //}

        ///// <summary>
        ///// Register data sheet for each entity.
        ///// </summary>
        //private void RegisterEntityData(NefxEntityBase entity)
        //{
        //    foreach (var hud in HolderList)
        //    {
        //        hud.TryRegister(entity);
        //    }
        //}

        #endregion
    }

    public interface IDataHolder
    {
        public IEnumerable<Guid> EntityIds { get; }
        public bool TryRegister(NefxEntityBase entity);
        public bool TryUnregister(NefxEntityBase entity);
    }
    /// <summary>
    /// Deal with entity guid for game logic data.
    /// </summary>
    public class DataHolderBase<T> : IDataHolder where T : INefxDataBase
    {
        private IList<System.Guid> _guids = new List<System.Guid>();
        public IEnumerable<Guid> EntityIds => _guids;

        public bool TryRegister(NefxEntityBase entity)
        {
            var canRegister = entity.GetType().GetInterfaces().Any(x => x == typeof(T));
            if (canRegister && !_guids.Contains(entity.Guid)) _guids.Add(entity.Guid);
            return canRegister;
        }

        public bool TryUnregister(NefxEntityBase entity)
        {
            var canUnregister = _guids.Contains(entity.Guid);
            if (canUnregister) _guids.Remove(entity.Guid);
            return canUnregister;
        }
    }
}

