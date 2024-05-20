using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace NightEdgeFrameworks.Models
{
    public class SelectionModel
    {
        public HashSet<Guid> SelectedList => _selected;

        // 选择框的对角线端点
        public Vector3 EndPoint => _endPoint;
        public Vector3 StartPoint => _startPoint;
        public Vector3 SelectionRatio{
            get
            {
                var x = (_endPoint.x - _startPoint.x) ;
                var y = (_endPoint.y - _startPoint.y) ;
                x = math.abs(x) > BoxSelectThreshold ? x  : BoxSelectThreshold;
                y = math.abs(y) > BoxSelectThreshold ? y : BoxSelectThreshold;
                return new Vector3(x / Screen.width, y / Screen.height, 0);
            }
        } 
        public float BoxSelectThreshold => 1e-4f;

        public event Action SelectionBeginEvent; 
        public event Action EndPointChangeEvent;
        public event Action SelectionFinishEvent;

        public void SetStartPoint(Vector3 target) 
        {
            _startPoint = target;
            SelectionBeginEvent?.Invoke();
        }
        
        public void SetEndPoint(Vector3 target)
        {
            _endPoint = target;
            EndPointChangeEvent?.Invoke();
        }

        public void SelectFinished() { 
            SelectionFinishEvent?.Invoke();
            Debug.Log($"Start at {_startPoint}, end at {_endPoint}.");
        }

        private Vector3 _endPoint = Vector3.zero;
        private Vector3 _startPoint = Vector3.zero;
        private readonly HashSet<Guid> _selected = new();
    }
}
