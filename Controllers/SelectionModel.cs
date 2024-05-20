/********************************************************************************************
*                                                                                           *
*                                Project Name : Mutopia                                     *
*                                                                                           *
*                                  Programmer : Hatuki                                      *
*                                                                                           *
*                                  Start Date : 2024-05-09                                  *
*                                                                                           *
*                                 Last Update : 2024-05-14                                  *
*                                                                                           *
*-------------------------------------------------------------------------------------------*
*-------------------------------------------------------------------------------------------*/


using NightEdgeFrameworks.Core;
using NightEdgeFrameworks.Models.Rts;
using NightEdgeFrameworks.Models.Abstract;

using UnityEngine;
using NightEdgeFrameworks.Utils.Abstract;

namespace NightEdgeFrameworks.Controllers
{
    public class SelectionModel : MonoBehaviour
    {

        private CursorModel _cursor;
        private NefxEntityController _entityRegister;
        private NefxPlayerController _playerController;



        private bool _isDragging => Input.GetMouseButton(0) && true; // TODO: 补充判断此时光标是否在其他 GUI 上
        private Vector2 _dragStart;
        private Vector2 _dragEnd;
        private Rect _selectionRect;
        //private float _lastClick = 0f;
        //private bool _isClicking => Time.time - _lastClick < 0.5f;


        private float _rectWidth = 1f;
        private Color _colorBorder = new Color(0, 0.8f, 0);
        private Color _colorFill = new Color(0, 0.8f, 0, 0.2f);
        private Texture2D _textureBorder;
        private Texture2D _textureFill;

        public bool IsActive => _isDragging;
        public bool IsAdditive => Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);


        public bool BeginSelect => Input.GetMouseButtonDown(0);
        public bool EndSelect => Input.GetMouseButtonUp(0);


        public Vector2 CursorPosition => _cursor.CursorPosition2d;
        //public Vector2 CursorPosition => Input.mousePosition;

        public Rect SelectionRect => _selectionRect;
        public float rectArea => SelectionRect.height * SelectionRect.width;

        public float xMin => _dragStart.x < _dragEnd.x ? _dragStart.x : _dragEnd.x;
        public float xMax => _dragStart.x < _dragEnd.x ? _dragEnd.x : _dragStart.x;
        public float yMin => _dragStart.y < _dragEnd.y ? _dragStart.y : _dragEnd.y;
        public float yMax => _dragStart.y < _dragEnd.y ? _dragEnd.y : _dragStart.y;

        private void Awake()
        {
            _selectionRect = new Rect();
            _cursor = Simulation.GetModel<CursorModel>();
            _entityRegister = Simulation.GetModel<NefxEntityController>();
            _playerController = Simulation.GetModel<NefxPlayerController>();

            _textureBorder = new Texture2D(1, 1);
            _textureFill = new Texture2D(1, 1);
            _textureBorder.SetPixel(0, 0, _colorBorder);
            _textureFill.SetPixel(0, 0, _colorFill);
        }


        private void Update()
        {
            DragSelect();
            ClickSelect();
        }

        private void OnGUI()
        {
            DrawSelectionBox();
        }


        /// <summary>
        /// GUI action.
        /// </summary>
        private void DrawSelectionBox()
        {
            if (BeginSelect) _dragStart = CursorPosition;
            if (!_isDragging) return;

            _dragEnd = CursorPosition;
            _selectionRect.Set(xMin, Screen.height - yMax, xMax - xMin, yMax - yMin);   // Fill.   
            GUI.DrawTexture(_selectionRect, _textureFill);
            _selectionRect.Set(xMin, Screen.height - yMax, _rectWidth, yMax - yMin);    // Left border.
            GUI.DrawTexture(_selectionRect, _textureBorder);
            _selectionRect.Set(xMin, Screen.height - yMin, _rectWidth, yMin - yMax);    // Right border.
            GUI.DrawTexture(_selectionRect, _textureBorder);
            _selectionRect.Set(xMin, Screen.height - yMax, xMax - xMin, _rectWidth);    // Top border.
            GUI.DrawTexture(_selectionRect, _textureBorder);
            _selectionRect.Set(xMin, Screen.height - yMin, xMax - xMin, _rectWidth);    // Bottom border.
            GUI.DrawTexture(_selectionRect, _textureBorder);
        }

        /// <summary>
        /// Click select can effect entities that are not owned by player.
        /// Click select only effect on those are selectable.
        /// </summary>
        private void ClickSelect()
        {
            if (!BeginSelect) return;
            if (!IsAdditive) _entityRegister.ClearActive();

            var ray = Camera.main.ScreenPointToRay(CursorPosition);
            foreach (var hit in Physics.RaycastAll(ray))
            {
                try
                {
                    var ent = hit.collider.gameObject.GetComponent<NefxEntityBase>();
                    var sel = (ISelectableEntity)ent;
                    if (ent.Ownership != _playerController.MyPlayerSlot) { continue; }
                    var id = hit.collider.gameObject.GetComponent<NefxEntityBase>().Guid;   // Find the first entity hit by ray cast.
                    if (_entityRegister.ContainsActive(id) && IsAdditive) _entityRegister.RemoveActive(id);
                    else _entityRegister.AddActive(id);
                }
                catch { continue; }
            }
            //          var ray = Camera.main.ScreenPointToRay(CursorPosition);
            //var hit = Physics.RaycastAll(ray).FirstOrDefault(x => x.collider != null 
            //    && x.collider.gameObject.GetComponent<NefxEntityBase>() != null
            //    && x.collider.gameObject.GetComponent<NefxEntityBase>().Ownership == _playerController.MyPlayerId);
            //if(hit.collider == null) return;
            //var id = hit.collider.gameObject.GetComponent<NefxEntityBase>().Guid;   // Find the first entity hit by ray cast.

            //if (_entityRegister.ContainsActive(id)) _entityRegister.RemoveActive(id);
            //else _entityRegister.AddActive(id);

            //_lastClick = Time.time;
        }


        /// <summary>
        /// Select NefxEntities when command is called
        /// </summary>
        /// <returns></returns>
        private void DragSelect()
        {
            // TODO: Find out a more efficient approach.
            // TODO: Use the intersection point when camera ray cast onto the ground instead.
            if (!EndSelect) return;                             // Effect when releasing mouse.
            if (!IsAdditive && rectArea > 1) _entityRegister.ClearActive();     // Clear the list before if not pressing shift.

            var entities = _entityRegister.GetAllEntities();

            foreach (var entity in entities)
            {
                //if (entity == null)
                //{
                //    Debug.Log("Null!");
                //    continue;
                //}
                //var pos = Camera.main.WorldToScreenPoint(entity.transform.position);
                //var res = pos.x > xMin && pos.y > yMin && pos.x < xMax && pos.y < yMax;
                //res &= entity.Ownership == _playerController.MyPlayerId;
                ////Debug.Log($"{entity.Guid}: {pos}, in box: {res}");
                //if (res) _entityRegister.AddActive(entity.Guid);

                try
                {
                    var sel = (ISelectableEntity)entity;
                    if (sel == null) Debug.Log($"Entity {entity.name} is NULL!");
                    var pos = Camera.main.WorldToScreenPoint(entity.transform.position);
                    var res = pos.x > xMin && pos.y > yMin && pos.x < xMax && pos.y < yMax;
                    res &= entity.Ownership == _playerController.MyPlayerSlot;
                    //Debug.Log($"{entity.Guid}: {pos}, in box: {res}");
                    if (res) _entityRegister.AddActive(entity.Guid);
                }
                catch { continue; }
            }
        }
    }
}