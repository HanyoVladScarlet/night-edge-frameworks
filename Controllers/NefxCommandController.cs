/********************************************************************************************
*                                                                                           *
*                                Project Name : Mutopia                                     *
*                                                                                           *
*                                  Programmer : Hatuki                                      *
*                                                                                           *
*                                  Start Date : 2024-05-09                                  *
*                                                                                           *
*                                 Last Update : 2024-05-09                                  *
*                                                                                           *
*-------------------------------------------------------------------------------------------*
*-------------------------------------------------------------------------------------------*/


using NightEdgeFrameworks.Core;
using NightEdgeFrameworks.Utils;
using NightEdgeFrameworks.Utils.Abstract;
namespace NightEdgeFrameworks.Controllers
{
    public class NefxCommandController
    {
        private NefxEntityController _register;

        public NefxCommandController()
        {
            _register = Simulation.GetModel<NefxEntityController>();
        }


        /// <summary>
        /// Attach command to command list.
        /// </summary>
        public void RegisterCommandToActive<T>(NefxCommandArgsBase args) where T : NefxCommandBase, new()
        {
            foreach (var entity in _register.GetActives())
            {
                // TODO: This instantiating approach has risk in ctor of each command.
                if (entity == null || entity.GetComponent<NefxCommandComponent>() == null) continue;
                var argsClone = args?.Clone() ?? default;
                var command = new T();
                command.Initialize(entity.Guid, argsClone);
                entity.GetComponent<NefxCommandComponent>().Clear();
                entity.GetComponent<NefxCommandComponent>().Push(command);
                //entity.GetComponent<NefxCommandComponent>().CancelCurrent();
            }
        }

        public void RegisterCommand<T>(NefxEntityBase entity, NefxCommandArgsBase args) where T : NefxCommandBase, new()
        {
            if (entity == null || entity.GetComponent<NefxCommandComponent>() == null) return;
            var argsClone = args?.Clone() ?? default;
            var command = new T();
            command.Initialize(entity.Guid, argsClone);
            entity.GetComponent<NefxCommandComponent>().Clear();
            entity.GetComponent<NefxCommandComponent>().Push(command);
            //entity.GetComponent<NefxCommandComponent>().CancelCurrent();
        }
    }
}




