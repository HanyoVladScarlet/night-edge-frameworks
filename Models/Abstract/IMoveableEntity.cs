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
using UnityEngine;

namespace NightEdgeFrameworks.Models.Abstract
{
    public interface IMoveableEntity : INefxDataBase
    {
        public bool MoveWithoutLookAt { get; }
        public Vector3 Velocity { get; }
        public float AngularSpeed { get; }
        public Vector3 movingDirection { get; }
        public Vector3 DestinationPosition { get; }
        public bool CanMove { get; }
        public bool IsMoving { get; }
        public bool IsArrived { get; }
        public void TurnToTargetDelta(Vector3 direction, bool instant = false);
        public void SetDestination(Vector3 distPos);
        public void BeginMove();
        public void EndMove();
    }
}