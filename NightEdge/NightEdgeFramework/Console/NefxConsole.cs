using System;
using System.Collections.Generic;
using System.Text;
using NightEdgeFramework.Core;

namespace NightEdgeFramework.Console
{
    public class NefxConsole
    {
        private static NefxConsole nefsConsole;

        public event ConsoleMessageEventHandler TextToNetEvent;  // 激活向网络发送内容事件
        public event ConsoleMessageEventHandler TextToGuiEvent;  // 激活向用户界面发送内容事件
        public event ConsoleMessageEventHandler TextToComEvent;  // 激活发送命令事件

        public static NefxConsole GetNefxConsole()
        {
            if (nefsConsole == null)
                nefsConsole = new NefxConsole();
            return nefsConsole;
        }

        public void PushComMessage(string text)
        {
            TextToGuiEventArgs textToGuiEventArgs = new TextToGuiEventArgs
            {
                Text = SolveComMsg(text),
                PlayerId = 0,
                UserId = 0,
                Flag = ConsoleMessageFlag.SystemMessage
            };

            TextToGuiEvent.Invoke(this, textToGuiEventArgs);
        }

        public void PushGuiMessage(string text)
        {
            TextToGuiEventArgs textToGuiEventArgs = new TextToGuiEventArgs();
            TextToNetEventArgs textToNetEventArgs = new TextToNetEventArgs();

            textToGuiEventArgs.Text = SolveGuiMsg(text);
            textToGuiEventArgs.Flag = ConsoleMessageFlag.SelfText;
            textToNetEventArgs.Text = text;
            textToNetEventArgs.PlayerId = UserAgent.GetLocalPlayerId();
            textToNetEventArgs.UserId = UserAgent.GetLocalUserId();

            if (textToGuiEventArgs != null)
                TextToGuiEvent.Invoke(this, textToGuiEventArgs);
            if (textToNetEventArgs != null)
                TextToNetEvent.Invoke(this, textToNetEventArgs);
            
        }

        public void PushNetMessage(TextToNetEventArgs args)
        {
            TextToGuiEventArgs textToGuiEventArgs = SolveNetMsg(args);
            TextToGuiEvent.Invoke(this, textToGuiEventArgs);
        }

        private void Initialize()
        {

        }

        private NefxConsole()
        {
            Initialize();
        }

        private string SolveComMsg(string text)
        {
            const string CONSOLE = "(Console:) ";

            return CONSOLE + text;
        }

        private string SolveGuiMsg(string text)
        {
            string[] strs = text.Split(' ');
            int index = 0;
            TextToComEventArgs textToComEventArgs = new TextToComEventArgs();

            foreach (var item in strs)
            {
                // 如果被 { } 包裹则将被包裹的内容提取，然后作为参数激活TextToComEvent事件
                if (item.StartsWith('{') && item.EndsWith('}'))
                {
                    char[] chs = new char[item.ToCharArray().Length-2];
                    for (int i = 0; i < item.ToCharArray().Length-2; i++)
                    {
                        chs[i] = item[i + 1];
                    }
                    textToComEventArgs.ComStr = new string(chs);
                    TextToComEvent.Invoke(this, textToComEventArgs);
                }

                else if (item.StartsWith('$'))
                {
                    // 查字典将内容转义，如果字典中有这个转义符代表的条目，则替换之
                }

                else if (item.StartsWith('/'))
                {
                    // 聊天窗口命令,如果字典中有这个命令，那么将这个值设置为null
                    
                }

                index++;
            }

            // 将传入的text解析，返回结果
            return string.Concat(strs);
        }

        private TextToGuiEventArgs SolveNetMsg(TextToNetEventArgs args)
        {
            string[] strs = args.Text.Split(' ');
            int index = 0;
            ConsoleMessageFlag flag = ConsoleMessageFlag.SystemMessage;
            TextToGuiEventArgs textToGuiEventArgs = new TextToGuiEventArgs();

            foreach (var item in strs)
            {
                // 如果被 { } 包裹则将被包裹的内容提取，然后作为参数激活TextToComEvent事件
                if (item.StartsWith('{') && item.EndsWith('}'))
                {
                    strs[index] = null;
                }

                else if (item.StartsWith('$'))
                {
                    // 查字典将内容转义，如果字典中有这个转义符代表的条目，则替换之
                }

                else if (item.StartsWith('/'))
                {
                    // 聊天窗口命令,如果字典中有这个命令，那么将这个值设置为null
                    // 根据字典返回结果将聊天对象标注，这里暂时按公屏来处理
                    flag = ConsoleMessageFlag.BroadcastCallBack;
                }

                index++;
            }

            textToGuiEventArgs.Text = string.Concat(strs);
            textToGuiEventArgs.PlayerId = args.PlayerId;
            textToGuiEventArgs.UserId = args.UserId;
            textToGuiEventArgs.Flag = flag;

            return textToGuiEventArgs;
        }
    }

    public delegate void ConsoleMessageEventHandler(object sender, EventArgs args);

    public class TextToComEventArgs : EventArgs
    {
        public string ComStr { get; set; }
    }

    public class TextToNetEventArgs : EventArgs
    {
        // 当这个值为 -1 时，表示用户正在大厅
        public int PlayerId { get; set; }
        // 用户的数字 id ，用于区分玩家
        public int UserId { get; set; }

        // 用户发送的消息内容
        public string Text { get; set; }
    }

    public class TextToGuiEventArgs : TextToNetEventArgs
    {
        public ConsoleMessageFlag Flag { get; set; }
    }

    public enum ConsoleMessageFlag
    {
        SelfText,
        AlliedText,
        BroadcastCallBack,
        TeamchatCallBack,
        CommandCallBack,
        SystemMessage
    }

}
