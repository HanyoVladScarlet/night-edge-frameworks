/********************************************************************************************
*                                                                                           *
*                                Project Name : Mutopia                                     *
*                                                                                           *
*                                  Programmer : Hatuki                                      *
*                                                                                           *
*                                  Start Date : 2024-05-08                                  *
*                                                                                           *
*                                 Last Update : 2024-05-09                                  *
*                                                                                           *
*-------------------------------------------------------------------------------------------*
* Properties:                                                                               *
*   CommandArgs -- Assign command arguments 
*   CreateTime -- The time this command is assigned.
*   HasBegun -- [get] Whether this command has begun.                                       *
*   HasFinished -- Whether this command is done executing.                                  *
*   Id -- Guid of the entity this command is bound to.
*   IsDeadLoop -- If a command is a dead loop, controller cannot push new command.          *
* Methods:                                                                                  *    
*   Cancel -- Use when quit the command, reverse the Execute method.                        *
*   CompareTo -- Sort with CreateTime.
*   Clone -- Clone the command with its params.
*   Clone<T> -- Generic implement.
*   Destroy -- Explicitly dipose the object.
*   Execute -- Begin the command execution.
*   Tick -- Call every tick by command controller.
*-------------------------------------------------------------------------------------------*/


using System;
using UnityEngine;


namespace NightEdgeFrameworks.Utils.Abstract
{
    public abstract class NefxCommandBase : IComparable<NefxCommandBase>
    {
        protected bool _hasBegun = false;
        protected bool _hasCanceled = false;
        protected Guid _guid;


        private bool _isEnabled = false;
        private bool _isRemoved = false;
        private NefxCommandArgsBase _commandArgs;
        private event Action CancelEvent;
        private event Action ExecuteEvent;


        /// <summary>
        /// The time when this command is created.
        /// </summary>
        public float CreateTime { get; protected set; }
        /// <summary>
        /// Whether this command has begun.
        /// </summary>
        public bool HasBegun => _hasBegun;
        /// <summary>
        /// Whether this command has been canceled.
        /// </summary>
        public bool HasCanceled => _hasCanceled;
        /// <summary>
        /// Whether this command is done executing.
        /// </summary>
        public bool HasFinished => _hasBegun && GetFinishFlag();
        /// <summary>
        /// Whether this command is end point.
        /// </summary>
        public bool IsDeadLoop { get; private set; }
        /// <summary>
        /// Dequeue the command when it is ready to quit.
        /// </summary>
        public bool CanDequeue => HasCanceled || HasFinished || IsRemoved;
        /// <summary>
        /// Dequeue directly if removed.
        /// </summary>
        public bool IsRemoved => _isRemoved;
        /// <summary>
        /// The guid of entity it is bound to.
        /// </summary>
        public Guid Guid => _guid;


        public NefxCommandBase()
        {
            CreateTime = Time.time;
            CancelEvent += OnCommandCanceled;
            ExecuteEvent += OnCommandExecute;
        }

        /// <summary>
        /// Implement this method to execute the command.
        /// </summary>
        protected abstract void OnCommandExecute();
        /// <summary>
        /// Implement this method to cancel the command.
        /// </summary>
        protected abstract void OnCommandCanceled();
        /// <summary>
        /// Implement this method for command initialization.
        /// </summary>
        protected virtual void OnCommandInitialize() { }

        /// <summary>
        /// Clone this command.
        /// </summary>
        public T Clone<T>() where T : NefxCommandBase
        {
            try
            {
                return (T) MemberwiseClone();
            }
            catch
            {
                return default(T);
            }
        }
        /// <summary>
        /// Clone this command.
        /// </summary>
        /// <returns></returns>
        public NefxCommandBase Clone() => Clone<NefxCommandBase>();
        /// <summary>
        /// The earlier created time is, the higher priority the command has.
        /// Higher priority with smaller number.
        /// </summary>
        public int CompareTo(NefxCommandBase other)
        {
            if (other == null) return 1;
            return CreateTime.CompareTo(other.CreateTime);
        }
        /// <summary>
        /// Reverse the Execute method.
        /// </summary>
        public void Cancel()
        {
            if (!_isEnabled)
            {
                Debug.LogError("Call *Initialize* method before cancel.");
            }
            CancelEvent?.Invoke();
            _hasCanceled = true;
        }
        /// <summary>
        /// Execute the command.
        /// </summary>
        public void Execute()
        {
            if (!_isEnabled)
            {
                Debug.LogError("Call *Initialize* method before execute.");
            }
            _hasBegun = true;
            ExecuteEvent?.Invoke();
        }
        /// <summary>
        /// Override this method to decide finish flag.
        /// </summary>
        public virtual void Initialize(Guid guid, NefxCommandArgsBase args)
        {
            _guid = guid;
            _commandArgs = args;
            _isEnabled = true;
            OnCommandInitialize();
        }
        /// <summary>
        /// Override this method to initialize the command, especially assign guid. 
        /// </summary>
        /// <returns></returns>
        protected abstract bool GetFinishFlag();
        /// <summary>
        /// 
        /// </summary>
        public void Remove() => _isRemoved = true;
        /// <summary>
        /// Call every tick in CommandController.
        /// </summary>
        public virtual void Tick()
        {

        }
        /// <summary>
        /// Use this method to get command arguments.
        /// </summary>
        public T GetArgs<T>() where T : NefxCommandArgsBase
        {
            try{ return (T)_commandArgs; }
            catch
            {
                Debug.LogError($"Wrong arguments passed in.");
                return default; 
            }
        }
        /// <summary>
        /// Use this method to get command arguments.
        /// </summary>
        public NefxCommandArgsBase GetArgs() => _commandArgs;
    }

    public class NefxCommandAttribute : Attribute
    {
        public string Name { get; private set; }
        public NefxCommandAttribute(string Name)
        {
            this.Name = Name;
        }
    }

    public class NefxCommandArgsBase
    {
        public NefxCommandArgsBase Clone() => (NefxCommandArgsBase)MemberwiseClone();
    }
}