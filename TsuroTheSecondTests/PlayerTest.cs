using System;
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
        public Tile testTile1 = new Tile(1, new List<int>(8) {
            0, 1, 2, 3, 4, 5, 6, 7,
        });


        Tile testTile2 = new Tile(2, new List<int>(8) {
            2, 3, 4, 5, 6, 7, 0, 1,

        });

        Tile testTile3 = new Tile(3, new List<int>(8) {
            6, 7, 0, 1, 2, 3, 4, 5,
        });
        [TestMethod]
        public void TestConstructor()
        {
            // valid constructor
            Player player = new Player(new List<int> { -1, 2, 3 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
            Player player1 = new Player(new List<int> { 6, 3, 6 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
            Player player2 = new Player(new List<int> { 3, -1, 4 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
            Player player3 = new Player(new List<int> { 5, 6, 0 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid initialization of the position of player was overlooked")]
        public void TestPlayerPosition_1()
        {
            // wrong x -lower than -1
            Player player_wrongx = new Player(new List<int> { -3, 0, 3 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid initialization of the position of player was overlooked")]
        public void TestPlayerPosition_2()
        {
            // wrong x -lower than -1
            Player player_wrongx = new Player(new List<int> { -1, -1, 3 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid initialization of the position of player was overlooked")]
        public void TestPlayerPosition_3()
        {
            // wrong x -lower than -1
            Player player_wrongx = new Player(new List<int> { -1, 6, 3 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid initialization of the position of player was overlooked")]
        public void TestPlayerPosition_4()
        {
            // wrong x -lower than -1
            Player player_wrongx = new Player(new List<int> { -1, 3, 4 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid initialization of the position of player was overlooked")]
        public void TestPlayerPosition_5()
        {
            // wrong x -lower than -1
            Player player_wrongx = new Player(new List<int> { -1, -1, 4 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid initialization of the position of player was overlooked")]
        public void TestPlayerPosition_6()
        {
            // wrong x -lower than -1
            Player player_wrongx = new Player(new List<int> { 6, -1, 4 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid initialization of the position of player was overlooked")]
        public void TestPlayerPosition_7()
        {
            
            Player player_wrongx = new Player(new List<int> { 3, -4, 6 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid initialization of the position of player was overlooked")]
        public void TestPlayerPosition_8()
        {
            Player player_wrongx = new Player(new List<int> { 3, 4, 6 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid initialization of the position of player was overlooked")]
        public void TestPlayerPosition_9()
        {
            Player player_wrongx = new Player(new List<int> { 3, -1, 6 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid initialization of the position of player was overlooked")]
        public void TestPlayerPosition_10()
        {
            Player player_wrongx = new Player(new List<int> { 6, -1, 6 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid initialization of the position of player was overlooked")]
        public void TestPlayerPosition_11()
        {
            Player player_wrongx = new Player(new List<int> { 6, 6, 6 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid initialization of the position of player was overlooked")]
        public void TestPlayerPosition_12()
        {
            Player player_wrongx = new Player(new List<int> { 6, 3, 4 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid initialization of the position of player was overlooked")]
        public void TestPlayerPosition_13()
        {
            Player player_wrongx = new Player(new List<int> { -1, 6, 0 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid initialization of the position of player was overlooked")]
        public void TestPlayerPosition_14()
        {
            Player player_wrongx = new Player(new List<int> { 6, 6, 0 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid initialization of the position of player was overlooked")]
        public void TestPlayerPosition_15()
        {
            Player player_wrongx = new Player(new List<int> { 3, 5, 0 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid initialization of the position of player was overlooked")]
        public void TestPlayerPosition_16()
        {
            Player player_wrongx = new Player(new List<int> { 3, 7, 0 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid initialization of the position of player was overlooked")]
        public void TestPlayerPosition_17()
        {
            Player player_wrongx = new Player(new List<int> { 3, 6, 4 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid initialization of the position of player was overlooked")]
        public void TestPlayerPositionPort()
        {
            // wrong port, over 7
            Player player_wrongx = new Player(new List<int> { 5, 6, 8 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid initialization of the position of player was overlooked")]
        public void TestPlayerPositionPort_1()
        {
            // wrong port, under 0
            Player player_wrongx = new Player(new List<int> { 5, 6, -1 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid initialization of the position of player was overlooked")]
        public void TestPlayerPositionPort_2()
        {
            // wrong port, under 0
            Player player_wrongx = new Player(new List<int> { 5, 6, 2 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid initialization of the position of player was overlooked")]
        public void TestPlayerPositionPort_3()
        {
            // wrong port, under 0
            Player player_wrongx = new Player(new List<int> { 5, 6, 4 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid initialization of the position of player was overlooked")]
        public void TestPlayerPositionPort_4()
        {
            // wrong port, under 0
            Player player_wrongx = new Player(new List<int> { 5, 6, 6 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        }

        [TestMethod]
        public void TestIsDead()
        {
            Player player_wrongx = new Player(new List<int> { 5, 6, 1 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
            Assert.IsTrue(player_wrongx.IsDead());
            player_wrongx.position[1] = 3;
            Assert.IsTrue(!player_wrongx.IsDead());
        }

        [TestMethod]
        public void TestUpdatePositionBaseCase()
        {
            // start at 5, 6 and at port 0
            Player player1 = new Player(new List<int> { 5, 6, 0 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
            Board board = new Board(6);
            Tile testTile_1 = new Tile(1, new List<int>(8) {
                0, 4, 1, 5, 2, 6, 3, 7
            });
            // places a tile that gives direct path up 
            board.PlaceTile(testTile_1, 5, 5);
            // move and update position of the player
            player1.UpdatePosition(board);
            // check position
            Assert.IsFalse(player1.IsDead());
            Assert.AreEqual(player1.position[0], 5);
            Assert.AreEqual(player1.position[1], 5);
            Assert.AreEqual(player1.position[2], 1);
        }
        [TestMethod]
        public void TestUpdatePositionInductiveCase()
        {
            // start at 5, 6 and at port 0
            Player player1 = new Player(new List<int> { 5, 6, 0 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
            Board board = new Board(6);
            Tile testTile_1 = new Tile(1, new List<int>(8) {
                0, 4, 1, 5, 2, 6, 3, 7
            });
            Tile testTile_2 = new Tile(2, new List<int>(8) {
                4, 7, 0, 6, 1, 3, 5, 2
            });
            // places a tile that gives direct path up 
            board.PlaceTile(testTile_1, 5, 5);
            board.PlaceTile(testTile_2, 4, 5);
            // move and update position of the player
            player1.UpdatePosition(board);
            // check position
            Assert.IsFalse(player1.IsDead());
            Assert.AreEqual(player1.position[0], 5);
            Assert.AreEqual(player1.position[1], 4);
            Assert.AreEqual(player1.position[2], 7);
        }
        [TestMethod]
        public void TestAddTiletoHand()
        {
            // takes tile and adds the player to the Hand
            Player player1 = new Player(new List<int> { 5, 6, 0 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
            Assert.AreEqual(player1.Hand.Count, 3);
            player1.Hand.RemoveAt(0);
            Assert.AreEqual(2, player1.Hand.Count);
            player1.AddTiletoHand(testTile1);
            Assert.AreEqual(3, player1.Hand.Count);
            Assert.AreEqual(testTile1.id, (player1.Hand.Find(each => each.id == 1)).id);
        }
        [TestMethod]
        public void TestRemoveTilefromHand()
        {
            // should fail
            Player player1 = new Player(new List<int> { 5, 6, 0 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
            Assert.AreEqual(player1.Hand.Count, 3);
            player1.RemoveTilefromHand(testTile1);
            Assert.AreEqual(2, player1.Hand.Count);
        }
        [TestMethod]
        public void TestTileinHand()
        {
            Tile testTile4 = new Tile(4, new List<int>(8) {
                0, 1, 2, 3, 4, 5, 6, 7,
            });
            Player player1 = new Player(new List<int> { 5, 6, 0 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
            Assert.IsTrue(player1.TileinHand(testTile1));
            Assert.IsFalse(player1.TileinHand(testTile4));
        }


    }
}
