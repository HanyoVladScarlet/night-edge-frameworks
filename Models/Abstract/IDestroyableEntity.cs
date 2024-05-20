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
using System;

namespace NightEdgeFrameworks.Models.Abstract
{
    public interface IDestroyableEntity : INefxDataBase
    {
        public bool IsAlive { get; set; }
        public float CurrentHitPoint { get; set; }
        public float MaxHitPoint { get; set; }
        public float HitPointRegainRate { get; set; }
        public bool IsKillable { get; set; }
        public void TakeDamage(Guid source, float damage);
        public void KillSelf(Guid entity, bool playAnim);
    }

    //public class DataHolderDestroyable : DataHolderBase<IDestroyableEntity> { }
}