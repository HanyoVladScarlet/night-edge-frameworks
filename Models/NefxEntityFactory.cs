/********************************************************************************************
*                                                                                           *
*                                Project Name : Mutopia                                     *
*                                                                                           *
*                                  Programmer : Hatuki                                      *
*                                                                                           *
*                                  Start Date : 2024-05-09                                  *
*                                                                                           *
*                                 Last Update : 2024-05-11                                  *
*                                                                                           *
*-------------------------------------------------------------------------------------------*
*-------------------------------------------------------------------------------------------*/


using NightEdgeFrameworks.Models;
using NightEdgeFrameworks.Models.Abstract;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Create nefx entities.
/// </summary>
public class NefxEntityFactory
{
    private Dictionary<EntityTypeFlag, float[]> floatSheets = new Dictionary<EntityTypeFlag, float[]>
    {
        [EntityTypeFlag.EliteRabbit] = new float[]
        {
            500f,           // Current Hit Point
            500f,           // Max Hit Point
            3f,             // HP Regain Rate.
            0.5f,             // Attack duration.
            1.2f,             // Attack Interval
            20f,            // Firing damage.
            200f,          // Firing rate.
            10f,            // Firing range.
            135,            // Alert angle
        },
        [EntityTypeFlag.GangThug] = new float[]
        {
            300,            // Current Hit Point
            300f,           // Max Hit Point
            3f,             // HP Regain Rate.
            0.5f,           // Attack duration.
            1.2f,             // Attack Interval
            40f,            // Firing damage.
            100f,           // Firing rate.
            10f,            // Firing range.
            135,            // Alert angle
        }
    };

    /// <summary>
    /// Used to initialize entity directly in hierarchy.
    /// Check
    /// </summary>
    public void InitializeBasicPawnData(BasicPawn target, EntityTypeFlag type)
    {
        (target as IDestroyableEntity).CurrentHitPoint = floatSheets[type][0];
        (target as IDestroyableEntity).MaxHitPoint = floatSheets[type][1];
        (target as IDestroyableEntity).HitPointRegainRate = floatSheets[type][2];
        (target as IArmedEntity).AttackDuration = floatSheets[type][3];
        (target as IArmedEntity).AttackInterval = floatSheets[type][4];

        var go = new GameObject();

        var weapon = new GameObject().AddComponent<SimpleWeaponry>();
        InitializePlayerWeaponData(weapon, type);
        weapon.SetParent(target);
        (target as IArmedEntity).AddWeapon(weapon);
        (target as IArmedEntity).SetWeaponActive(weapon);
    }
    public void InitializePlayerWeaponData(SimpleWeaponry weapon, EntityTypeFlag type)
    {
        weapon.FiringDamage = floatSheets[type][5];
        weapon.FiringRate = floatSheets[type][6];     // About 4 shot per sec.
        weapon.FiringRange = floatSheets[type][7];
    }
}

/// <summary>
/// Load data file for each entity.
/// Note: NefxDataController deals with data that already loaded and attached, rather than load data.
/// </summary>
public class NefxEntityLoader
{

}


public enum EntityTypeFlag
{
    EliteRabbit = 0,
    GangThug = 1,
}