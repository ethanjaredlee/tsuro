﻿using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TsuroTheSecond;

namespace TsuroTheSecondTests
{
    [TestClass]
    public class PlayerTest
    {
        // go to line 223 to skip constructor tests
        public Tile testTile1 = new Tile(1, new List<List<int>>(4) {
            new List<int>(2){0, 1},
            new List<int>(2){2, 3},
            new List<int>(2){4, 5},
            new List<int>(2){6, 7},
        });


        Tile testTile2 = new Tile(2, new List<List<int>>(4) {
            new List<int>(2){2, 3},
            new List<int>(2){4, 5},
            new List<int>(2){6, 7},
            new List<int>(2){0, 1},
        });

        Tile testTile3 = new Tile(2, new List<List<int>>(4) {
                new List<int>(2){6, 7},
                new List<int>(2){0, 1},
                new List<int>(2){2, 3},
                new List<int>(2){4, 5},
            });
        [TestMethod]
        public void TestConstructor()
        {
            // valid constructor
            Player player = new Player(new List<int> { -1, 2, 3 }, 1, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
            Player player1 = new Player(new List<int> { 6, 3, 6 }, 3, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
            Player player2 = new Player(new List<int> { 3, -1, 4 }, 2, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
            Player player3 = new Player(new List<int> { 5, 6, 0 }, 0, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid initialization of the position of player was overlooked")]
        public void TestPlayerPosition_1()
        {
            // wrong x -lower than -1
            Player player_wrongx = new Player(new List<int> { -3, 0, 3 }, 1, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid initialization of the position of player was overlooked")]
        public void TestPlayerPosition_2()
        {
            // wrong x -lower than -1
            Player player_wrongx = new Player(new List<int> { -1, -1, 3 }, 1, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid initialization of the position of player was overlooked")]
        public void TestPlayerPosition_3()
        {
            // wrong x -lower than -1
            Player player_wrongx = new Player(new List<int> { -1, 6, 3 }, 1, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid initialization of the position of player was overlooked")]
        public void TestPlayerPosition_4()
        {
            // wrong x -lower than -1
            Player player_wrongx = new Player(new List<int> { -1, 3, 4 }, 2, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid initialization of the position of player was overlooked")]
        public void TestPlayerPosition_5()
        {
            // wrong x -lower than -1
            Player player_wrongx = new Player(new List<int> { -1, -1, 4 }, 2, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid initialization of the position of player was overlooked")]
        public void TestPlayerPosition_6()
        {
            // wrong x -lower than -1
            Player player_wrongx = new Player(new List<int> { 6, -1, 4 }, 2, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid initialization of the position of player was overlooked")]
        public void TestPlayerPosition_7()
        {
            
            Player player_wrongx = new Player(new List<int> { 3, -4, 6 }, 3, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid initialization of the position of player was overlooked")]
        public void TestPlayerPosition_8()
        {
            Player player_wrongx = new Player(new List<int> { 3, 4, 6 }, 3, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid initialization of the position of player was overlooked")]
        public void TestPlayerPosition_9()
        {
            Player player_wrongx = new Player(new List<int> { 3, -1, 6 }, 3, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid initialization of the position of player was overlooked")]
        public void TestPlayerPosition_10()
        {
            Player player_wrongx = new Player(new List<int> { 6, -1, 6 }, 3, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid initialization of the position of player was overlooked")]
        public void TestPlayerPosition_11()
        {
            Player player_wrongx = new Player(new List<int> { 6, 6, 6 }, 3, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid initialization of the position of player was overlooked")]
        public void TestPlayerPosition_12()
        {
            Player player_wrongx = new Player(new List<int> { 6, 3, 4 }, 2, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid initialization of the position of player was overlooked")]
        public void TestPlayerPosition_13()
        {
            Player player_wrongx = new Player(new List<int> { -1, 6, 0 }, 0, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid initialization of the position of player was overlooked")]
        public void TestPlayerPosition_14()
        {
            Player player_wrongx = new Player(new List<int> { 6, 6, 0 }, 0, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid initialization of the position of player was overlooked")]
        public void TestPlayerPosition_15()
        {
            Player player_wrongx = new Player(new List<int> { 3, 5, 0 }, 0, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid initialization of the position of player was overlooked")]
        public void TestPlayerPosition_16()
        {
            Player player_wrongx = new Player(new List<int> { 3, 7, 0 }, 0, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid initialization of the position of player was overlooked")]
        public void TestPlayerPosition_17()
        {
            Player player_wrongx = new Player(new List<int> { 3, 6, 4 }, 2, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid initialization of the position of player was overlooked")]
        public void TestPlayerPositionPort()
        {
            // wrong port, over 7
            Player player_wrongx = new Player(new List<int> { 5, 6, 8 }, 1, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid initialization of the position of player was overlooked")]
        public void TestPlayerPositionPort_1()
        {
            // wrong port, under 0
            Player player_wrongx = new Player(new List<int> { 5, 6, -1 }, 1, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid initialization of the position of player was overlooked")]
        public void TestPlayerPositionPort_2()
        {
            // wrong port, under 0
            Player player_wrongx = new Player(new List<int> { 5, 6, 2 }, 0, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid initialization of the position of player was overlooked")]
        public void TestPlayerPositionPort_3()
        {
            // wrong port, under 0
            Player player_wrongx = new Player(new List<int> { 5, 6, 4 }, 1, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid initialization of the position of player was overlooked")]
        public void TestPlayerPositionPort_4()
        {
            // wrong port, under 0
            Player player_wrongx = new Player(new List<int> { 5, 6, 6 }, 2, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid initialization of the position of player was overlooked")]
        public void TestPlayerPositionPort_5()
        {
            // wrong port, under 0
            Player player_wrongx = new Player(new List<int> { 5, 6, 0 }, 3, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        }







    }
}