/********************************************************************************************
*                                                                                           *
*                                Project Name : Mutopia                                     *
*                                                                                           *
*                                  Programmer : Hatuki                                      *
*                                                                                           *
*                                  Start Date : 2024-05-14                                 *
*                                                                                           *
*                                 Last Update : 2024-05-14                                  *
*                                                                                           *
*-------------------------------------------------------------------------------------------*
*-------------------------------------------------------------------------------------------*/

namespace NightEdgeFrameworks.Models.Abstract
{
    public interface IWeaponryAccessory
    {
        public float FiringRange { get; set; }
        public float FiringRate { get; set; }
        public float MaxAmmo { get; set; }
        public float ReloadingTime { get; set; }
    }
}