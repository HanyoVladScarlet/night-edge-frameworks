using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NightEdgeFrameworks.Models.Rts;

namespace NightEdgeFrameworks.Controllers
{
    public class CursorController : MonoBehaviour
    {
        private CursorModel _cursor;

        private void Awake()
        {
            _cursor = NightEdgeFrameworks.Core.Simulation.GetModel<CursorModel>();
        }

        private void Update()
        {
            CursorPositionUpdate();
            //CursorPositionOnClicked();
        }

        /// <summary>
        /// 根据鼠标轴输入计算光标对应的位移值
        /// </summary>
        /// <param name="isClamped"></param>
        private void CursorPositionUpdate()
        {
            var dx = Input.GetAxis("Mouse X") * _cursor.CursorSensitivity;
            var dy = Input.GetAxis("Mouse Y") * _cursor.CursorSensitivity;
            
            _cursor.UpdatePosDelta(dx, dy);
        }

        private void CursorPositionOnClicked()
        {
            if(Input.GetMouseButtonDown(0))
            {
                Debug.Log($"Cursor position on: {_cursor.CursorPosition2d}.\n" +
                    $"Cursor bound status: {_cursor.CursorOnBound}");
            }        
        }
    }
}