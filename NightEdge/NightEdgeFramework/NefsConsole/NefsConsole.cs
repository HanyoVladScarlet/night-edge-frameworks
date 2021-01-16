/* ====================================================
 * NefsConsole: 控制台中间件
 * Encoding: utf-8
 * Author: Vladmir_Lyapunov
 * 
 * ------------ 分割线 ---------------
 * Intro: 
 * 这个中间件为处理用户的输入以及消息分发提供接口。
 * 同时还定义了框架内的消息传递规范。
 * 
 * Reference:
 * https://hanyovladscarlet.github.io
 * 
 ===================================================== */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NightEdgeFramework.Core;

namespace NightEdgeFramework.NefsConsole
{
    public delegate void ConsoleMessageEventHandler(object sender, EventArgs args);

    public class TextToComEventArgs : EventArgs
    {
        public string ComStr { get; set; }
    }

    public class TextToNetEventArgs : EventArgs
    {
        // 当这个值为 -1 时，表示用户正在大厅
        public int Id { get; set; }
        // 用户的数字 id ，用于区分玩家
        public int UserId { get; set; }

        // 用户发送的消息内容
        public string Text { get; set; }
    }

    public class TextToGuiEventArgs : EventArgs
    {
        public string Text { get; set; }
        public ConsoleMessageFlag Flag { get; set; }
    }

    public enum ConsoleMessageFlag
    {
        AlliedText,
        BroadcastCallBack,
        TeamchatCallBack,
        CommandCallBack,
        SystemMessage
    }

    /// <summary>
    /// 这个类是一个中间件
    /// </summary>
    public class NefsConsole
    {
        public event ConsoleMessageEventHandler TextToNetEvent;  // 激活向网络发送内容事件
        public event ConsoleMessageEventHandler TextToGuiEvent;  // 激活向用户界面发送内容事件
        public event ConsoleMessageEventHandler TextToComEvent;  // 激活发送命令事件

        private Dictionary<string, string> commandDictionary;
        
        public NefsConsole()
        {
            //this.TextToComEvent += CommandInputCallback;  // 命令回调，在解析并执行命令后将
        }

        #region Initialize

        private void Initialize()
        {
            // temp
            InitializeCommandDictionary();
        }

        private void InitializeCommandDictionary()
        {
            this.commandDictionary = new Dictionary<string, string>();
            this.commandDictionary.Add("h", "SayHelloWorld!");
        }

        #endregion

        //private void CommandInputCallback(object sender, EventArgs _args)
        //{
        //    TextToComEventArgs args = (TextToComEventArgs)_args;

        //    foreach (var item in args.ComStr)
        //    {
        //        string commandDetail = commandDictionary[item];
        //        // 将命令对应的描述信息传给函数的第一个参数：
        //        // 比如
        //        ShowOnGui(commandDetail, ConsoleMessageFlag.CommandCallBack);
        //    }
        //}

        #region PublicMethods

        public void PushUserInput(string input)
        {
            bool isToEveryone = false;
            int index = 0;
            string[] text = SolveUserInput(input);
            TextToNetEventArgs args = new TextToNetEventArgs();
            string textBackToGui;

            args.Id = UserAgent.GetLocalPlayerId();
            args.UserId = UserAgent.GetLocalUserId();
            args.Text = string.Concat(text," ");

            // 激活向网络发送消息的事件
            TextToNetEvent.Invoke(this, args);

            foreach (var item in text)
            {
                if (item.StartsWith('/'))
                {
                    if (item == "/all")
                        isToEveryone = true;
                    text[index] = null;
                }
                    
                index++;
            }

            textBackToGui = string.Concat(text);

            // 将发送的消息在 GUI 回显
            if (isToEveryone)
            {
                ShowOnGui(textBackToGui, ConsoleMessageFlag.BroadcastCallBack);
            }

            else
            {
                ShowOnGui(textBackToGui, ConsoleMessageFlag.TeamchatCallBack);
            }
            
        }

        #endregion


        // 使用这个方法向 GUI 发送消息。
        // 激活向用户界面发送内容的事件，参数包括要显示的消息,以及消息类型。
        // 可以通过消息类型，在GUI相关的代码设置一个过滤器，如果是命令的回调信息，则不显示。
        private void ShowOnGui(string msg,ConsoleMessageFlag flag)
        {
            TextToGuiEventArgs e = new TextToGuiEventArgs();
            e.Text = msg;
            e.Flag = flag;

            TextToGuiEvent.Invoke(this, e);
        }



        #region IConsoleSolver

        // 将字符串切分，返回去除不应显示内容后的字符串
        private string[] SolveUserInput(string input)
        {
            int index = 0;
            string[] strs = input.Split(' ');
            
            foreach (var item in strs)
            {
                if (item.StartsWith('-'))
                {
                    // 激活发送命令事件，参数是string类型的item
                    string com = string.Concat(item.Split('-'));
                    TextToComEvent.Invoke(this, new TextToComEventArgs { ComStr = com });
                    strs[index] = null;
                }

                else if (item.StartsWith('/'))
                {
                    // 如果是特定的输入模式，比如 /all 等时，将隐藏这个字符串并选择对应的发送方式
                }

                index++;
            }

            return strs;
        }

        #endregion

        #region ICommandDispatcher

        

        #endregion

        /// <summary>
        /// 将储存在队列中最后一条状态信息提交给用户界面
        /// </summary>
        /// <returns></returns>
        public string GetLastStatus()
        {
            return "";
        }

        /// <summary>
        /// 切换用户所在的频道，或选定聊天对象
        /// </summary>
        public void SetChannel()
        {

        }

        /// <summary>
        /// 用于CommandManager等模块获得用户输入的命令
        /// </summary>
        /// <returns></returns>
        public string GetCommand()
        {
            return "";
        }
    }

   

    public struct ConsoleMessage
    {
        public int id;
        public string name;
        public string content;
        public DateTime arrivalTime;
    }

    /// <summary>
    /// 用于接收并储存其他模块传来的消息
    /// </summary>
    interface IConsoleReciever
    {
        public ArrayList CommandStatusList { get; set; }

        #region PublicMethods

        #region CommandManager

        // 用于从暂存的CommandStatus中获得命令状态
        public ConsoleMessage GetCommandStatus();

        // 将其他模块传来的命令完成状态信息等传回
        public void PushCommandStatus(ConsoleMessage message);

        #endregion


        #region UserInterface

        #endregion

        #endregion

    }

    /// <summary>
    /// 用于暂存用户输入的消息
    /// </summary>
    interface IConsoleSender
    {

    }

    /// <summary>
    /// 用于解析用户输入内容，并实现需要传递的消息的分发，为其它模块提供获得消息的方法
    /// </summary>
    interface IConsoleSolver
    {

    }

    /// <summary>
    /// 用于用户选择交流对象，或是频道
    /// </summary>
    interface IConsoleChannel
    {

    }

    /// <summary>
    /// 用于用户界面访问后台暂存的数据
    /// </summary>
    interface IConsolePresenter
    {

    }

    /// <summary>
    /// 用于命令模块访问，并获取用户输入的命令
    /// </summary>
    interface ICommandHandler
    {

    }
}
