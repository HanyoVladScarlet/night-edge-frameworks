/********************************************************************************************
*                                                                                           *
*                                Project Name : Mutopia                                     *
*                                                                                           *
*                                  Programmer : Hatuki                                      *
*                                                                                           *
*                                  Start Date : 2024-05-14                                  *
*                                                                                           *
*                                 Last Update : 2024-05-14                                  *
*                                                                                           *
*-------------------------------------------------------------------------------------------*
*-------------------------------------------------------------------------------------------*/


using NightEdgeFrameworks.Models.Abstract;
using NightEdgeFrameworks.Utils.Abstract;
using NightEdgeFrameworks.Controllers;


namespace NightEdgeFrameworks.Models
{
    public class SimpleWeaponry : WeaponEntityBase
    {
        private IDestroyableEntity Target => (Parent as IArmedEntity).Target;
        private NefxDataController _dataCtl = NefxControllerCollection.nefxDataController;

        public new bool IsAmmoDepleted => false;
        public override void CastWarhead()
        {
            base.CastWarhead();
            _dataCtl.DoDamage(
                Parent.Guid,
                Target,
                FiringDamage,
                DamageTypeFlag.Physical
            );
            (Parent as BasicPawn).Sound();
            (Parent as BasicPawn).VFX();
        }
        public override void Initialize() { }
    }
}

