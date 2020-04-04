﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LudoGameEngine
{
    public class GameBoard
    {
        public IList<BoardCoordinates> CoordinateOuterPosition = new List<BoardCoordinates>();
        public IList<GamePlayer> GamePlayers = new List<GamePlayer>();

        private int gamePlayerAmnt = 0;
        private string winner { get; set; } = "";
        private int playerTurn;

        public IGameSession gs;

        public Dice dice = new Dice();

        public enum ColorOrder
        {
            Red,    //0
            Blue,   //1
            Green,  //2
            Yellow  //3
        }


        public GameBoard(IGameSession gameSession)
        {
            this.gs = gameSession;
        }

        //this loop runs the game
        public void GameLoop()
        {            
            InitializeGame();
            GamePlayers = SetPlayOrder(playerTurn, GamePlayers);
            while (winner != "")
            {
                //all the gameplay here
                


            }

        }

        public void InitializeGame()
        {
            //players
            gamePlayerAmnt = gs.GetPlayerAmount();
            IList<Tuple<int, string, string>> sessionData = gs.GetSessionData();

            for(int i = 1; i <= gamePlayerAmnt; i++)
            {
                GamePlayers.Add(new GamePlayer(sessionData[i].Item1,sessionData[i].Item2, sessionData[i].Item3));
                GamePlayers[i].GlobalStartPos = SetColorStartPositon(GamePlayers[i].Color);
            }
            
            playerTurn = DecidePlayerStart(GamePlayers.Count);
        }

        private int DecidePlayerStart(int pAmount)
        {
            
            IDictionary<int, int> playersThrow = new Dictionary<int, int>();
            for(int i = 1; i <= pAmount; i++)
            {
                int dValue = dice.RollDice();
                playersThrow.Add(i, dValue);
            }

            var playerStart = playersThrow.OrderByDescending(x => x.Value).First();
            return playerStart.Key;
        }

        //work in progress
        public IList<GamePlayer> SetPlayOrder(int id, IList<GamePlayer> gp)
        {  
            var currentColor = gp.Where(c => c.GamePlayerID == id).Select(c => c.Color).FirstOrDefault();
            
            var newOrder = new List<GamePlayer>();
            
            var gPID = gp.Where(g => g.Color == currentColor).Select(g => g).FirstOrDefault();
            newOrder.Add(gPID);
            gp.Remove(gPID);

            var nextColor = "";

            var values = Enum.GetNames(typeof(ColorOrder));

            int index = Array.IndexOf(values, currentColor);

            for(int i = 0; i < gp.Count; i++)
            {
                while (currentColor != gp[i].Color)
                {
                    index = Array.IndexOf(values, currentColor);
                    if (index != values.Length)
                    {
                        index++;
                        if (index == values.Length)
                            index = 0;

                        nextColor = values[index];                        
                    }

                    currentColor = nextColor;
                }

                gPID = gp.Where(g => g.Color == currentColor).Select(g => g).FirstOrDefault();
                newOrder.Add(gPID);
            }

            return newOrder;
        }

        private void MovePiece()
        {

        }

        public int SetColorStartPositon(string color)
        {
            int startPos = 0;

            if (color == "Red")
                startPos = 1;  //ev. 0 om man räknar från index 0
            else if (color == "Blue")
                startPos = 11;  //ev. 10 om man räknar från index 0
            else if (color == "Green")
                startPos = 21;  //ev. 20 om man räknar från index 0
            else if (color == "Yellow")
                startPos = 31;  //ev. 30 om man räknar från index 0

            return startPos;
        }
    }

    public class BoardCoordinates
    {
        public bool IsOccupied { get; set; }
        public int BoardPosition { get; set; }
        public int OccupiedPlayerID { get; set; }
    }
}
