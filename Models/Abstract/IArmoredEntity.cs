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
    public interface IArmoredEntity : INefxDataBase
    {
        public float ArmorPoint { get; }
        public ArmorTypeFlag ArmorType { get; }
        public void TakeDamageArmor(Guid source, IArmoredEntity target, float damage, DamageTypeFlag type);
        public void DestroyArmor(Guid source, IArmoredEntity target, bool playAnim);
    }

    public enum ArmorTypeFlag { }
}