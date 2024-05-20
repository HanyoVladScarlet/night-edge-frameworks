/********************************************************************************************
*                                                                                           *
*                                Project Name : Mutopia                                     *
*                                                                                           *
*                                  Programmer : Hatuki                                      *
*                                                                                           *
*                                  Start Date : 2024-05-09                                  *
*                                                                                           *
*                                 Last Update : 2024-05-14                                 *
*                                                                                           *
*-------------------------------------------------------------------------------------------*
*-------------------------------------------------------------------------------------------*/


using NightEdgeFrameworks.Core;
using NightEdgeFrameworks.Models.Abstract;
using NightEdgeFrameworks.Models.Rts;
using NightEdgeFrameworks.Utils;
using NightEdgeFrameworks.Utils.Abstract;
using UnityEngine;

namespace NightEdgeFrameworks.Controllers
{
    public class NefxInputController : MonoBehaviour
    {
        private static NefxInputController instance;
        public static NefxInputController inputCtl { get { instance ??= new NefxInputController(); return instance; } }    


        private CursorModel _cursor;
        private NefxCommandController _commandController;
        private NefxHudController _hudController;
        private NefxEntityController _entityController;
        private NefxPlayerController _playerController;

        private bool _isAttackMode = false;

        private void Awake()
        {
            _cursor = Simulation.GetModel<CursorModel>();
            _commandController = NefxControllerCollection.nefxCommandController;
            _hudController = NefxControllerCollection.nefxHudController;
            _entityController = NefxControllerCollection.nefxEntityController;
            _playerController = NefxControllerCollection.nefxPlayerController;
        }
        private void Update()
        {
            OnAttackInput();
            OnMoveInput();
            OnSleepInput();
        }

        private void OnSleepInput() { if (Input.GetKeyDown(KeyCode.S)) _commandController.RegisterCommandToActive<SleepCommand>(default); }
        private void OnAttackInput()
        {
            if (Input.GetKeyUp(KeyCode.A))
            {
                var ray = _cursor.GetRay();
                var isHit = Physics.Raycast(ray, maxDistance: 500f, hitInfo: out var hit, layerMask: LayerMask.GetMask("Ground"));
                if (isHit && hit.collider != null)
                {
                    var enemy = NefxRuleCollection.GetNearestEnemy(hit.point);
                    if (enemy != null)
                    {
                        //Debug.Log((enemy as NefxEntityBase).name);
                        var args = new AttackCommandArgs(enemy);
                        _commandController.RegisterCommandToActive<AttackCommand>(args);
                    }
                }
            }
        }
        private void OnMoveInput()
        {
            if (Input.GetMouseButtonDown(1))
            {
                var ray = _cursor.GetRay();
                var isHit = Physics.Raycast(ray, maxDistance: 500f, hitInfo: out var hitEnemy);
                //if (isHit && hitEnemy.collider != null)
                if (isHit)
                {
                    var hasNeb = hitEnemy.collider.TryGetComponent<NefxEntityBase>(out var entity);
                    if (hasNeb && !_playerController.IsAllied(_playerController.MyPlayerSlot, entity.Ownership))
                    {
                        Debug.Log("attack!");
                        var attackArgs = new AttackCommandArgs(entity as IDestroyableEntity);
                        _commandController.RegisterCommandToActive<AttackCommand>(attackArgs);
                        return;
                    }
                }
                isHit = Physics.Raycast(ray, maxDistance: 500f, hitInfo: out var hitGround, layerMask: LayerMask.GetMask("Ground"));
                if (isHit)
                {
                    //var tPos = hit.point;
                    //var entities = _entityController.GetActives();
                    //var destinations = new List<Vector3>();
                    //var sumVector = Vector3.zero;
                    //for (int i = 0; i < entities.Count(); i++)
                    //{
                    //    var maxVector = tPos;
                    //    foreach (var v in destinations)
                    //    {
                    //        var diff = v - tPos;
                    //        maxVector = diff.magnitude < (maxVector - tPos).magnitude ? maxVector : v;

                    //    }
                    //    sumVector = new Vector3(
                    //        - destinations.Average(x => x.x),
                    //        - destinations.Average(x => x.y),
                    //        - destinations.Average(x => x.z)
                    //    );
                    //    var referenceVector = sumVector.normalized * (maxVector - tPos).magnitude * 1.2f;
                    //    var lessVector = maxVector = tPos;

                    //    foreach (var v in destinations)
                    //    {
                    //        var diff = v - referenceVector;
                    //        if (diff.magnitude > (maxVector - referenceVector).magnitude)
                    //        {
                    //            lessVector = maxVector;
                    //            maxVector = v;
                    //        }
                    //    }
                    //}

                    //Debug.Log($"Hit pos: {hit.point}");

                    var args = new MoveCommandArgs(targetPos: hitGround.point);
                    _commandController.RegisterCommandToActive<MoveCommand>(args);
                }
                else
                {
                    Debug.Log("Not hit!");
                }



                //var hits = Physics.RaycastAll(ray);
                //Debug.Log(hits.Length);
                //foreach (var hit in hits)
                //{
                //    if (hit.collider == null) continue;
                //    if (hit.collider.gameObject.layer == LayerMask.GetMask("Ground"))
                //    {
                //        var moveArgs = new MoveCommandArgs(targetPos: hit.point);
                //        _commandController.RegisterCommandToActive<MoveCommand>(moveArgs);

                //        break;
                //    }
                //    var entity = hit.collider.GetComponent<NefxEntityBase>();
                //    if (entity == null || entity.Ownership == _playerController.MyPlayerId) continue;
                //    var attackArgs = new AttackCommandArgs(entity as IDestroyableEntity);
                //    _commandController.RegisterCommandToActive<AttackCommand>(attackArgs);


                //    //if (h.collider == null) continue;
                //    //if (h.collider.gameObject.layer == LayerMask.GetMask("Ground"))
                //    //{
                //    //    var args = new MoveCommandArgs(targetPos: hit.point);
                //    //    _commandController.RegisterCommandToActive<MoveCommand>(args);
                //    //    break;
                //    //}
                //    //var entity = h.collider.GetComponent<EntityStateBase>();

                //}

            }
        }

    }

}

