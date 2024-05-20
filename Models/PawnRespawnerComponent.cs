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


using NightEdgeFrameworks.Utils.Abstract;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class PawnRespawnerComponent : MonoBehaviour
{
    [SerializeField] private BasicPawn _pawn;
    [SerializeField] private List<Transform> _transforms;
    [SerializeField] private EntityTypeFlag _type;
    [SerializeField] private int _ownership;
    [SerializeField] private float _interval;
    [SerializeField] Func<bool> _respawnCondition;

    private float _lastRespawn = float.MinValue;
    private bool _executeOnce = false;

    private bool CanRespawn => _respawnCondition?.Invoke() ?? false
        //&& _executeOnce ^ _interval < 0
        //&& Time.time - _lastRespawn > _interval
        && !_executeOnce;


    [SerializeField] private Transform _moveTo;
    [SerializeField] private NefxEntityBase _attackOn;
    [SerializeField] private Transform _GuardOn;


    private void Awake()
    {
        //var icon = AssetDatabase.LoadAssetAtPath<Texture2D>("");
        //EditorGUIUtility.SetIconForObject(gameObject, icon);
    }

    private void Update()
    {
        if (CanRespawn) { _executeOnce = true; Respawn(); _lastRespawn = Time.time;  }
    }

    private void Respawn()
    {
        Debug.Log("Respawn!");
        foreach (var t in  _transforms) { 
            var instance = Instantiate(_pawn.gameObject, position: transform.position, rotation:t.rotation);
            var factory = new NefxEntityFactory();
            factory.InitializeBasicPawnData(instance.GetComponent<BasicPawn>(), _type);
        }
    }
}