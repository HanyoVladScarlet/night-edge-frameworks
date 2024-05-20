/********************************************************************************************
*                                                                                           *
*                                Project Name : Mutopia                                     *
*                                                                                           *
*                                  Programmer : Hatuki                                      *
*                                                                                           *
*                                  Start Date : 2024-05-10                                  *
*                                                                                           *
*                                 Last Update : 2024-05-10                                  *
*                                                                                           *
*-------------------------------------------------------------------------------------------*
*-------------------------------------------------------------------------------------------*/


using NightEdgeFrameworks.Core;

namespace NightEdgeFrameworks.Controllers
{
    /// <summary>
    /// Store some status irrelevant methods here.
    /// Dependence inject decoupling.
    /// </summary>
    public static class NefxControllerCollection
    {

        public static NefxCombatController nefxCombatController
        {
            get
            {
                if (_nefxCombatController == null)
                    _nefxCombatController = Simulation.GetModel<NefxCombatController>();
                return _nefxCombatController;
            }
        }
        private static NefxCombatController _nefxCombatController;

        public static NefxCommandController nefxCommandController
        {
            get
            {
                if (_nefxCommandController == null)
                    _nefxCommandController = Simulation.GetModel<NefxCommandController>();
                return _nefxCommandController;
            }
        }
        private static NefxCommandController _nefxCommandController;

        public static NefxDataController nefxDataController
        {
            get
            {
                if (_dataController == null)
                    _dataController = Simulation.GetModel<NefxDataController>();
                return _dataController;
            }
        }
        private static NefxDataController _dataController;

        public static NefxEntityController nefxEntityController
        {
            get
            {
                if (_entityRegister == null)
                    _entityRegister = Simulation.GetModel<NefxEntityController>();
                return _entityRegister;
            }
        }
        private static NefxEntityController _entityRegister;

        public static NefxPlayerController nefxPlayerController
        {
            get
            {
                if (_playerController == null)
                    _playerController = Simulation.GetModel<NefxPlayerController>();
                return _playerController;
            }
        }
        private static NefxPlayerController _playerController;

        public static NefxHudController nefxHudController
        {
            get
            {
                if (_nefxHudController == null)
                    _nefxHudController = Simulation.GetModel<NefxHudController>();
                return _nefxHudController;
            }
        }
        private static NefxHudController _nefxHudController;
    }
}
