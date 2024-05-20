/********************************************************************************************
*                                                                                           *
*                                Project Name : Mutopia                                     *
*                                                                                           *
*                                  Programmer : Hatuki                                      *
*                                                                                           *
*                                  Start Date : 2024-05-09                                  *
*                                                                                           *
*                                 Last Update : 2024-05-09                                  *
*                                                                                           *
*-------------------------------------------------------------------------------------------*
*-------------------------------------------------------------------------------------------*/


using System.Reflection;
using NightEdgeFrameworks.Controllers;
using NightEdgeFrameworks.Utils.Abstract;
using UnityEngine;


namespace NightEdgeFrameworks.Utils.Abstract
{
    public abstract class NefxEntityBase : MonoBehaviour
    {
        [SerializeField]
        private int _ownership = -1;
        private NefxEntityController _entityController = NefxControllerCollection.nefxEntityController;

        public NefxEntityBase Parent { get;private set; }
        public int Ownership => _ownership;
        public System.Guid Guid { get; set; } = System.Guid.NewGuid();
        private void OnEnable() => _entityController.Register(this);
        private void OnDisable() => _entityController.Unregister(this.Guid);
        private void Awake() => Initialize();
        private void FixedUpdate() => Tick();
        public void SetParent(NefxEntityBase parent) => Parent = parent;
        public void SetOwnerShip(int ownership) => _ownership = ownership;
        public abstract void Tick();
        public abstract void Initialize();
    }
}

namespace NightEdgeFrameworks.Core.Extensions
{
    public class EntityAliasAttribute : System.Attribute
    {
        public string Alias { get; private set; }
        public EntityAliasAttribute(string alias) => Alias = alias;
    }

    public static class NefxEntityExtension
    {
        public static string GetAlias(this NefxEntityBase entity)
        {
            try { return entity.GetType().GetCustomAttribute<EntityAliasAttribute>().Alias; }
            catch { return default; }
        }
    }
}


