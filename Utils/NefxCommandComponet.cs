using NightEdgeFrameworks.Commands;
using NightEdgeFrameworks.Utils.Abstract;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace NightEdgeFrameworks.Utils
{

    /// <summary>
    /// Bound to prefab.
    /// </summary>
    public class NefxCommandComponent : MonoBehaviour
    {
        private LinkedList<NefxCommandBase> _commandList;

        public string CurrentCommand => string.Join('/', _commandList.Select(x => x.GetType()));

        private void Awake()
        {
            _commandList = new LinkedList<NefxCommandBase>();
        }

        private void FixedUpdate()
        {
            UpdateCommandQueue();
            _commandList.FirstOrDefault()?.Tick();
        }

        private void UpdateCommandQueue()
        {
            while (_commandList.First != null)
            {
                var first = _commandList.First.Value;
                if (first.CanDequeue || first.GetType() == typeof(NefxCommandDefault)
                    && _commandList.Count > 1)
                {
                    Remove(first);
                    continue;
                }
                if (first.HasBegun) first.Tick();
                else first.Execute();
                break;
            }
            // When command list is empty, use default.
            if (_commandList.First == null)
            {
                var ncd = new NefxCommandDefault();
                ncd.Initialize(GetComponent<NefxEntityBase>().Guid, null);
                _commandList.AddFirst(ncd);
            }
        }

        /// <summary>
        /// Use when new order given not using way point mode.
        /// </summary>
        public void Clear()
        {
            while (_commandList.Count > 1) _commandList.RemoveLast();
            _commandList.First?.Value.Cancel();
            if (_commandList.First != null) _commandList.RemoveFirst();
        }
        /// <summary>
        /// Remove a node from list in way point mode.
        /// </summary>
        public void Remove(NefxCommandBase command)
        {
            if (command == null) return;
            if (command == _commandList.First.Value) command.Cancel();
            _commandList.Remove(command);
        }
        /// <summary>
        /// Push a command in way point mode.
        /// </summary>
        public void Push(NefxCommandBase command)
        {
            _commandList.AddLast(command);
        }
        /// <summary>
        /// Break from default order.
        /// </summary>
        public void CancelCurrent() => _commandList.First.Value.Cancel();
    }
}
