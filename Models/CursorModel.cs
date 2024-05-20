using System;
using UnityEngine;


namespace NightEdgeFrameworks.Models.Rts
{
    [Serializable]
    public class CursorModel
    {
        public float BoundWidth = 5f;

        public Vector2 CursorPosition2d => EnableCustomCursor ? new Vector2(_posX, _posY) : Input.mousePosition;

        public bool EnableCustomCursor = false;

        public float posX => EnableCustomCursor ? _posX : Input.mousePosition.x;
        public float posY => EnableCustomCursor ? _posY : Input.mousePosition.y;


        private float _posX;
        private float _posY;


        [SerializeField]
        private bool _isPositionLocked = false;

        public float CursorRayMaxDistance = 1000f;
        public readonly int CursorSensitivityBase = 10;
        public float CursorSensitivityBonus = 1f;

        /// <summary>
        /// Cursor won't leave screen when true.
        /// </summary>
        public bool IsBoundClamped { get; set; } = true;
        /// <summary>
        /// Cursor position will be locked up if true.
        /// </summary>
        public bool IsPositionLocked { get => _isPositionLocked; set => _isPositionLocked = value; }

        public float CursorSensitivity => CursorSensitivityBase * CursorSensitivityBonus;
        public CursorBoundingStatus CursorOnBound => GetCursorBoundFlag();


        public Ray GetRay() => Camera.main.ScreenPointToRay(CursorPosition2d);
        

        /// <summary>
        /// 通过增量来更新位置
        /// </summary>
        /// <param name="deltaX"></param>
        /// <param name="deltaY"></param>
        public void UpdatePosDelta(float deltaX, float deltaY)
        {
            _posX += deltaX;
            _posY += deltaY;
            if (!IsBoundClamped) return;

            _posX = _posX < 0 ? 0 : _posX;
            _posX = Screen.width < _posX ? Screen.width : _posX;
            _posY = _posY < 0 ? 0 : _posY;
            _posY = Screen.height < _posY ? Screen.height : _posY;
        }


        /// <summary>
        /// Decide whether the cursor bounds the margin.
        /// </summary>
        /// <returns></returns>
        private CursorBoundingStatus GetCursorBoundFlag()
        {
            var screenWidth = Screen.width;
            var screenHeight = Screen.height;
            var res = CursorBoundingStatus.None;

            if (CursorPosition2d.x < BoundWidth) res |= CursorBoundingStatus.Left;
            if (CursorPosition2d.x > screenWidth - BoundWidth * 2) res |= CursorBoundingStatus.Right;
            if (CursorPosition2d.y < BoundWidth) res |= CursorBoundingStatus.Down;
            if (CursorPosition2d.y > screenHeight - BoundWidth * 2) res |= CursorBoundingStatus.Up;

            return res;
        }
    }

    /// <summary>
    /// A flag to mark whether cursor is on bound
    /// </summary>
    public enum CursorBoundingStatus
    {
        None = 0,
        Up = 1,
        Down = 2,
        Left = 4,
        Right = 8,
    }
}


