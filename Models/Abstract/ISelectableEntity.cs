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



using NightEdgeFrameworks.Controllers;
using NightEdgeFrameworks.Utils.Abstract;


namespace NightEdgeFrameworks.Models.Abstract
{
    public interface ISelectableEntity : INefxDataBase { }

    public static class SelectableEntityExtension
    {
        private static NefxEntityController entityController = NefxControllerCollection.nefxEntityController;
        public static bool IsActive(this ISelectableEntity target) => entityController.ContainsActive((target as NefxEntityBase).Guid);
    }
}