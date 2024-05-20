/********************************************************************************************
*                                                                                           *
*                                Project Name : Mutopia                                     *
*                                                                                           *
*                                  Programmer : Hatuki                                      *
*                                                                                           *
*                                  Start Date : 2024-05-09                                  *
*                                                                                           *
*                                 Last Update : 2024-05-11                                  *
*                                                                                           *
*-------------------------------------------------------------------------------------------*
*-------------------------------------------------------------------------------------------*/


using NightEdgeFrameworks.Utils.Abstract;

namespace NightEdgeFrameworks.Models.Abstract
{
    public interface IWarhead
    {
        public float Damage { get; }
        public NefxEntityBase Entity { get; }
        public float PowderRate { get; }
        public void Travel();
        public void Explode();
    }

}

