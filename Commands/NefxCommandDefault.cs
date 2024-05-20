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


namespace NightEdgeFrameworks.Commands
{
    [Utils.Abstract.NefxCommand("default_command")]
    public class NefxCommandDefault : Utils.Abstract.NefxCommandBase
    {
        protected override bool GetFinishFlag() => false;

        protected override void OnCommandCanceled() { }

        protected override void OnCommandExecute() { }
    }
}



