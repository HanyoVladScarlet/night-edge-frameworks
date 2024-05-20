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


using UnityEngine;
using NightEdgeFrameworks.Core;
using UnityEngine.AI;
using System;
using UnityEngine.VFX;
using NightEdgeFrameworks.Models.Abstract;
using NightEdgeFrameworks.Utils.Abstract;
using NightEdgeFrameworks.Controllers;
using NightEdgeFrameworks.Utils;
using System.Collections.Generic;


[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(EntityStateMachine))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(NefxCommandComponent))]
public class BasicPawn : NefxEntityBase, IDestroyableEntity, IArmedEntity, ISelectableEntity, IMoveableEntity
{
    [SerializeField] private EntityTypeFlag _entityTypeFlag;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private VisualEffect _fireEffect;
    private AudioSource _audiosSource;
    private NefxDataController _dataController;
    private NavMeshAgent _agent;


    public override void Tick()
    {

    }
    public override void Initialize()
    {
        var ds = Simulation.GetModel<NefxEntityFactory>();
        ds.InitializeBasicPawnData(this, _entityTypeFlag);
        _audiosSource = GetComponent<AudioSource>();
        _dataController = NefxControllerCollection.nefxDataController;
        _agent = GetComponent<NavMeshAgent>();
        //_agent.updateRotation = false;
    }



    public float CurrentHitPoint { get; set; }
    public float HitPointRegainRate { get; set; }
    public bool IsAlive { get; set; } = true;
    public bool IsKillable { get; set; } = true;
    public float MaxHitPoint { get; set; }
    public void KillSelf(Guid entity, bool playAnim)
    {
        IsAlive = false;
        gameObject.SetActive(false);
    }
    public void TakeDamage(Guid source, float damage)
    {
        CurrentHitPoint -= damage;
        //if (damage > 5f)
        //    Debug.Log($"{this.Guid} received {damage} damage from " +
        //        $"{source}, remaining hp : {CurrentHitPoint} " +
        //        $"of {MaxHitPoint}");
    }





    public float AlertAngle { get; set; }
    public float AttackRange => NefxRuleFormulas.GetFiringRange(WeaponActive);
    public bool IsFiring => _isAttacking;
    public bool ReadyToFire => true;
    public bool NeedReload => false;
    public bool IsAiming => _isAiming;
    // For scanning enemies;
    private bool _inScanCooldown => Time.time - _lastScanTime < scanInterval;
    private float _lastScanTime;
    private readonly float scanInterval = 0.1f;
    // Attempt to attack.
    private float _lastAttack = 0f;
    public float AttackInterval { get; set; }
    private bool _readyToAim => Time.time - _lastAttack > AttackInterval;
    private float _beginAimTime = 0f;
    private readonly float _beforeAttackTime = 0.2f;
    private bool _readyToAttack => Time.time - _beginAimTime > _beforeAttackTime;
    public bool TargetInAngle => Vector3.Angle(TargetVector, transform.forward) < 15
        && Vector3.Angle(TargetVector, transform.forward) > -15;
    public IDestroyableEntity Target { get; private set; }
    private bool _isAttacking = false;
    private bool _isAiming = false;
    public IWeaponryEntity WeaponActive { get; private set; }
    public IList<IWeaponryEntity> WeaponList { get; private set; } = new List<IWeaponryEntity>();
    public float AttackDuration { get; set; }
    public void ArrangeAttack()
    {
        if (!TargetInAngle) TurnToTargetDelta((Target as NefxEntityBase).transform.position - transform.position);
        if (_readyToAim && !_isAiming)
        {
            _isAiming = true;
            _beginAimTime = Time.time;
        }
        if (_readyToAttack && _isAiming)
        {
            _isAttacking = true;
            _isAiming = false;
            if (Target != null && Target.IsAlive)
            {
                AttackOnTarget();
                _lastAttack = Time.time;
                _isAttacking = false;
            }
        }
    }
    public void AttackOnTarget()
    {
        //NefxControllerCollection.nefxCombatController.Attack(this, Target);
        //if (_audiosSource != null && _audioClip != null) _audiosSource.PlayOneShot(_audioClip);
        //if (_fireEffect != null) _fireEffect.Play();
        WeaponActive.BeginFire();
    }
    public void Sound() { if (_audiosSource != null && _audioClip != null) _audiosSource.PlayOneShot(_audioClip); }
    public void VFX() { if (_fireEffect != null) _fireEffect.Play(); }

    public void Reload() => throw new NotImplementedException();
    public void ScanEnemy()
    {
        if (_inScanCooldown) return;
        Target = _dataController.PickEnemyTarget(this, AttackRange);
        _lastScanTime = Time.time;
    }
    public void SetTarget(IDestroyableEntity target) => Target = target;
    public void SetWeaponActive(IWeaponryEntity weapon) { if(WeaponList.Contains(weapon)) WeaponActive = weapon; }
    public void AddWeapon(IWeaponryEntity weapon) { if (!WeaponList.Contains(weapon)) WeaponList.Add(weapon); }
    public void RemoveWeapon(IWeaponryEntity weapon) { if (WeaponList.Contains(weapon)) WeaponList.Remove(weapon); }





    private Vector3 TargetVector => Target == null ? transform.forward
        : (Target as NefxEntityBase).transform.position - transform.position;
    public Vector3 Velocity => _agent.velocity;

    private readonly float ANGLE_THRESHOLD = 15f;
    public Vector3 movingDirection => Velocity.normalized;
    public Vector3 DestinationPosition { get; private set; }
    public bool CanMove => MoveWithoutLookAt || Vector3.Angle(movingDirection, transform.forward) < ANGLE_THRESHOLD;
    public bool MoveWithoutLookAt => false;
    public bool IsMoving { get; private set; }
    public bool IsArrived => (_agent.destination - transform.position).magnitude < 0.01f;
    public float AngularSpeed { get; set; } = 300f;


    public void TurnToTargetDelta(Vector3 direction, bool instant = false)
    {
        var t = instant ? 1 : AngularSpeed * Time.fixedDeltaTime / 360;
        t = Mathf.Clamp01(t);
        transform.rotation = Quaternion.LookRotation(direction);
    }

    public void SetDestination(Vector3 distPos)
    {
        _agent.SetDestination(distPos);
        DestinationPosition = distPos;
    }

    public void BeginMove()
    {
        IsMoving = true;
        _agent.isStopped = false;
        _agent.updatePosition = true;
    }

    public void Moving()
    {

    }

    public void EndMove()
    {
        IsMoving = false;
        _agent.isStopped = true;
    }
}

public enum DamageTypeFlag
{
    Physical = 1,
    Magical = 2,

}

namespace NightEdgeFrameworks.Models.Abstract
{

}

