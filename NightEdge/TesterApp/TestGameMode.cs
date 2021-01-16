using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Threading;
using NightEdgeFramework;
using NightEdgeFramework._3rd;

namespace TesterApp.TestGameMode
{
    public class GameDemo
    {
        private static GameDemo game;

        private Clock clock;

        private List<string> RendererTexts;

        event Action<EventArgs> UpdateRendererEvent;
        event Action<EventArgs> UpdateKeyPressedCount;

        private string keyCode;
        private string lastKeyCode;

        private int keyPressedCounter;

        // Use this delegate to log information.
        private GameDemo()
        {
            Initialize();
        }

        #region GameStateChanges

        

        #endregion

        #region GameInitialization

        // Initialize the game
        private void Initialize()
        {
            this.keyPressedCounter = 0;
            this.RendererTexts = new List<string>();
            this.RendererTexts.Add("Welcome!");
            this.RendererTexts.Add(this.keyCode);
            clock = new Clock(64, false, 0);
            UpdateRendererEvent += UpdateRenderer;
            Thread inputThread = new Thread(GetKeyBoardInput);
            inputThread.Start();
            //this.clock.Tick += () => { UpdateRenderer(new EventArgs()); };

            this.Main();

        }

        private void Main()
        {
            while (true) ;
        }

        public static GameDemo GetGameDemo()
        {
            if (game == null)
                game = new GameDemo();
            return game;
        }

        #endregion

        #region Renderer

        public void UpdateRenderer(EventArgs e)
        {
            Console.Clear();

            foreach (var item in RendererTexts)
            {
                Console.WriteLine(item);
            }
        }

        #endregion

        #region KeyboardInput

        private void GetKeyBoardInput()
        {
            this.clock.Tick += () => {
                this.lastKeyCode = this.keyCode;
                this.keyCode = Console.ReadKey(true).Key.ToString();
                if (this.keyCode == this.lastKeyCode)
                {
                    UpdateKeyPressedCount.Invoke(new EventArgs());
                }
                this.RendererTexts[1] = this.keyCode;
                UpdateRendererEvent.Invoke(new EventArgs());
            };       

        }

        private bool GetKey(string key)
        {
            return key == this.keyCode;
        }

        private string GetKeyImpulse()
        {
            if (this.keyCode == null)
                return "No key pressed.";
            return this.keyCode;
        }

        #endregion

    }




}
