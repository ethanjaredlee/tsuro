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
        Server server;

        [TestInitialize]
        public void Initialize()
        {
            server = new Server();
        }

        // go to line 223 to skip constructor tests
        Tile testTile1 = new Tile(1, new List<int>(8) {
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
            for (int i = 0; i < Constants.colors.Count; i++) {
                MPlayer player = new MPlayer();
                Player p = new Player(player, Constants.colors[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Color not allowed")]
        public void TestBadColorConstructor()
        {
            MPlayer mPlayer = new MPlayer();
            Player player = new Player(mPlayer, "turquoise");
        }

        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException),
        //    "Invalid initialization of the position of player was overlooked")]
        //public void TestPlayerPosition_1()
        //{
        //    // wrong x -lower than -1
        //    Player player_wrongx = new Player(new List<int> { -3, 0, 3 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        //}

        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException),
        //    "Invalid initialization of the position of player was overlooked")]
        //public void TestPlayerPosition_2()
        //{
        //    // wrong x -lower than -1
        //    Player player_wrongx = new Player(new List<int> { -1, -1, 3 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        //}
        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException),
        //    "Invalid initialization of the position of player was overlooked")]
        //public void TestPlayerPosition_3()
        //{
        //    // wrong x -lower than -1
        //    Player player_wrongx = new Player(new List<int> { -1, 6, 3 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        //}
        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException),
        //    "Invalid initialization of the position of player was overlooked")]
        //public void TestPlayerPosition_4()
        //{
        //    // wrong x -lower than -1
        //    Player player_wrongx = new Player(new List<int> { -1, 3, 4 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        //}

        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException),
        //    "Invalid initialization of the position of player was overlooked")]
        //public void TestPlayerPosition_5()
        //{
        //    // wrong x -lower than -1
        //    Player player_wrongx = new Player(new List<int> { -1, -1, 4 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        //}
        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException),
        //    "Invalid initialization of the position of player was overlooked")]
        //public void TestPlayerPosition_6()
        //{
        //    // wrong x -lower than -1
        //    Player player_wrongx = new Player(new List<int> { 6, -1, 4 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        //}
        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException),
        //    "Invalid initialization of the position of player was overlooked")]
        //public void TestPlayerPosition_7()
        //{
            
        //    Player player_wrongx = new Player(new List<int> { 3, -4, 6 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        //}
        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException),
        //    "Invalid initialization of the position of player was overlooked")]
        //public void TestPlayerPosition_8()
        //{
        //    Player player_wrongx = new Player(new List<int> { 3, 4, 6 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        //}
        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException),
        //    "Invalid initialization of the position of player was overlooked")]
        //public void TestPlayerPosition_9()
        //{
        //    Player player_wrongx = new Player(new List<int> { 3, -1, 6 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        //}

        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException),
        //    "Invalid initialization of the position of player was overlooked")]
        //public void TestPlayerPosition_10()
        //{
        //    Player player_wrongx = new Player(new List<int> { 6, -1, 6 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        //}
        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException),
        //    "Invalid initialization of the position of player was overlooked")]
        //public void TestPlayerPosition_11()
        //{
        //    Player player_wrongx = new Player(new List<int> { 6, 6, 6 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        //}
        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException),
        //    "Invalid initialization of the position of player was overlooked")]
        //public void TestPlayerPosition_12()
        //{
        //    Player player_wrongx = new Player(new List<int> { 6, 3, 4 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        //}
        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException),
        //    "Invalid initialization of the position of player was overlooked")]
        //public void TestPlayerPosition_13()
        //{
        //    Player player_wrongx = new Player(new List<int> { -1, 6, 0 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        //}
        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException),
        //    "Invalid initialization of the position of player was overlooked")]
        //public void TestPlayerPosition_14()
        //{
        //    Player player_wrongx = new Player(new List<int> { 6, 6, 0 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        //}
        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException),
        //    "Invalid initialization of the position of player was overlooked")]
        //public void TestPlayerPosition_15()
        //{
        //    Player player_wrongx = new Player(new List<int> { 3, 5, 0 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        //}
        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException),
        //    "Invalid initialization of the position of player was overlooked")]
        //public void TestPlayerPosition_16()
        //{
        //    Player player_wrongx = new Player(new List<int> { 3, 7, 0 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        //}
        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException),
        //    "Invalid initialization of the position of player was overlooked")]
        //public void TestPlayerPosition_17()
        //{
        //    Player player_wrongx = new Player(new List<int> { 3, 6, 4 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        //}


        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException),
        //    "Invalid initialization of the position of player was overlooked")]
        //public void TestPlayerPositionPort()
        //{
        //    // wrong port, over 7
        //    Player player_wrongx = new Player(new List<int> { 5, 6, 8 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        //}
        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException),
        //    "Invalid initialization of the position of player was overlooked")]
        //public void TestPlayerPositionPort_1()
        //{
        //    // wrong port, under 0
        //    Player player_wrongx = new Player(new List<int> { 5, 6, -1 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        //}
        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException),
        //    "Invalid initialization of the position of player was overlooked")]
        //public void TestPlayerPositionPort_2()
        //{
        //    // wrong port, under 0
        //    Player player_wrongx = new Player(new List<int> { 5, 6, 2 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        //}
        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException),
        //    "Invalid initialization of the position of player was overlooked")]
        //public void TestPlayerPositionPort_3()
        //{
        //    // wrong port, under 0
        //    Player player_wrongx = new Player(new List<int> { 5, 6, 4 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        //}
        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException),
        //    "Invalid initialization of the position of player was overlooked")]
        //public void TestPlayerPositionPort_4()
        //{
        //    // wrong port, under 0
        //    Player player_wrongx = new Player(new List<int> { 5, 6, 6 }, new List<Tile> { testTile1, testTile2, testTile3 }, 78, "blue");
        //}


        [TestMethod]
        public void TestAddTiletoHand()
        {
            // takes tile and adds the player to the Hand
            MPlayer machine = new MPlayer();
            Player p1 = new Player(machine, "green");
            Assert.AreEqual(0, p1.Hand.Count);
            p1.AddTiletoHand(testTile1);
            Assert.AreEqual(1, p1.Hand.Count);
            Assert.AreEqual(testTile1.id, (p1.Hand.Find(each => each.id == 1)).id);
        }

        //[TestMethod]
        //public void TestInitPlayerPosition()
        //{
        //    MPlayer machine = new MPlayer();
        //    Player p1 = new Player(machine, 1);
        //    p1.InitPlayerPosition(0, -1, 4);
        //    Assert.AreEqual(0, p1.position.x);
        //    Assert.AreEqual(-1, p1.position.y);
        //    Assert.AreEqual(4, p1.position.port);
        //}

        [TestMethod]
        public void TestAddTileFromHand()
        {
            // should fail
            MPlayer1 mPlayer = new MPlayer1("mark");
            Player player = new Player(mPlayer, "blue");

            player.AddTiletoHand(new Tile(1, new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 }));

            Assert.AreEqual(player.Hand.Count, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Player hand is already full!")]
        public void TestAddTileToFullHand()
        {
            // should fail
            MPlayer1 mPlayer = new MPlayer1("mark");
            Player player = new Player(mPlayer, "blue");

            player.AddTiletoHand(new Tile(1, new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 }));
            player.AddTiletoHand(new Tile(2, new List<int> { 2, 1, 0, 3, 4, 5, 6, 7 }));
            player.AddTiletoHand(new Tile(3, new List<int> { 4, 1, 2, 3, 0, 5, 6, 7 }));
            player.AddTiletoHand(new Tile(4, new List<int> { 3, 1, 2, 4, 0, 5, 6, 7 }));
        }

        [TestMethod]
        public void TestRemoveTilefromHand()
        {
            // should fail
            MPlayer1 mPlayer = new MPlayer1("mark");
            Player player = new Player(mPlayer, "blue");

            player.AddTiletoHand(new Tile(1, new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 }));
            player.AddTiletoHand(new Tile(2, new List<int> { 2, 1, 0, 3, 4, 5, 6, 7 }));
            player.AddTiletoHand(new Tile(3, new List<int> { 4, 1, 2, 3, 0, 5, 6, 7 }));

            Assert.AreEqual(3, player.Hand.Count);

            player.RemoveTilefromHand(testTile1);
            Assert.AreEqual(2, player.Hand.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Player hand is already empty!")]
        public void TestRemoveTilefromEmptyHand()
        {
            // should fail
            MPlayer1 mPlayer = new MPlayer1("mark");
            Player player = new Player(mPlayer, "blue");

            player.RemoveTilefromHand(testTile1);
        }

        [TestMethod]
        public void TestTileinHand()
        {
            MPlayer1 mPlayer = new MPlayer1("mark");
            Player player = new Player(mPlayer, "blue");

            player.AddTiletoHand(new Tile(1, new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 }));

            Tile testTile4 = new Tile(4, new List<int>(8) {
                0, 1, 2, 3, 4, 5, 6, 7,
            });

            Assert.IsTrue(player.TileinHand(testTile1));
            Assert.IsFalse(player.TileinHand(testTile4));
        }

        [TestMethod]
        public void TestMPlayer1PlayTurnNoLegalHand()
        {
            MPlayer1 mPlayer = new MPlayer1("mark");
            Player player = new Player(mPlayer, "blue");
            Board board = new Board(6);
            player.iplayer.Initialize(player.Color, new List<string> {"red", "hotpink"});
            Position p = player.iplayer.PlacePawn(board); // this position should be (0, -1, 5)
            board.AddPlayerToken("blue", p);
            Tile t = player.iplayer.PlayTurn(board, new List<Tile> { Constants.tiles[0] }, 35); // both these tiles should kill player
            board.PlaceTile(t, 0, 0);
            board.MovePlayer("blue");
            Assert.IsTrue(board.IsDead("blue"));
        }

        [TestMethod]
        public void TestMPlayer1PlayTurnOneLegalHand()
        {
            MPlayer1 mPlayer = new MPlayer1("mark");
            Player player = new Player(mPlayer, "blue");
            Board board = new Board(6);
            player.iplayer.Initialize(player.Color, new List<string> { "red", "hotpink" });
            Position p = player.iplayer.PlacePawn(board); // this position should be (0, -1, 5)
            board.AddPlayerToken("blue", p);
            Tile t = player.iplayer.PlayTurn(board, new List<Tile> { Constants.tiles[0], Constants.tiles[1] }, 35); // both these tiles should kill player
            board.PlaceTile(t, 0, 0);
            board.MovePlayer("blue");
            Assert.AreEqual(2, t.id);
            Assert.IsFalse(board.IsDead("blue"));
            // will randomly select an orientation that will leave the player at port 5 or 2
            Assert.IsTrue(board.tokenPositions["blue"].port == 5 || board.tokenPositions["blue"].port == 2);
        }

        [TestMethod]
        public void TestMPlayer1PlayTurnMultiLegalHand()
        {
            MPlayer1 mPlayer = new MPlayer1("mark");
            List<string> other_colors = new List<string>(Constants.colors);
            other_colors.Remove("blue");
            mPlayer.Initialize("blue", other_colors);
            server.AddPlayer(mPlayer, "blue");
            Player player = new Player(mPlayer, "blue");
            server.board.AddPlayerToken("blue", new Position(0, -1, 5));

            player.AddTiletoHand(new Tile(1, new List<int> { 0, 3, 2, 1, 4, 5, 6, 7 }));

            Tile testTile4 = new Tile(4, new List<int>(8) {
                0, 5, 1, 2, 3, 6, 4, 7
            });
            player.AddTiletoHand(testTile4);

            server.gameState = Server.State.safe;
            Tile tobePlayed = player.iplayer.PlayTurn(server.board, player.Hand, 33);

            Assert.AreEqual(1, server.alive.Count);
            if(tobePlayed.id == 1){
                while (player.iplayer.PlayTurn(server.board, player.Hand, 33).CompareByPath(testTile1)){}
            } else {
                while (player.iplayer.PlayTurn(server.board, player.Hand, 33).CompareByPath(testTile4)){}
            }
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    Assert.IsNull(server.board.tiles[i][j]);
                }
            }
        }

        [TestMethod]
        public void TestMPlayer2PlayTurnNoLegalHand()
        {
            MPlayer2 mPlayer = new MPlayer2("mark");
            List<string> other_colors = new List<string>(Constants.colors);
            other_colors.Remove("blue");
            mPlayer.Initialize("blue", other_colors);
            server.AddPlayer(mPlayer, "blue");
            Player player = new Player(mPlayer, "blue");
            server.board.AddPlayerToken("blue", new Position(0, -1, 5));

            // symmetricity of 1
            player.AddTiletoHand(testTile1);

            // symmetricity of 4
            Tile testTile4 = new Tile(4, new List<int>(8) {
                0, 5, 1, 3, 2, 6, 4, 7
            });
            player.AddTiletoHand(testTile4);

            // right of tile placement
            Tile testTile5 = new Tile(5, new List<int>(8) {
                6, 0, 7, 1, 2, 3, 4, 5
            });
            // below of tile placement
            Tile testTile6 = new Tile(6, new List<int>(8) {
                0, 7, 1, 6, 2, 3, 4, 5
            });

            server.board.PlaceTile(testTile5, 1, 0);
            server.board.PlaceTile(testTile6, 0, 1);

            server.gameState = Server.State.safe;
            Tile tobePlayed = player.iplayer.PlayTurn(server.board, player.Hand, 33);

            Assert.IsTrue(testTile1.CompareByPath(tobePlayed));
        }

        [TestMethod]
        public void TestMPlayer2PlayTurnOneLegalHand()
        {
            MPlayer2 mPlayer = new MPlayer2("mark");
            List<string> other_colors = new List<string>(Constants.colors);
            other_colors.Remove("blue");
            mPlayer.Initialize("blue", other_colors);
            server.AddPlayer(mPlayer, "blue");
            Player player = new Player(mPlayer, "blue");
            server.board.AddPlayerToken("blue", new Position(0, -1, 5));

            // symmetricity of 1
            player.AddTiletoHand(testTile1);

            // symmetricity of 4
            Tile testTile4 = new Tile(4, new List<int>(8) {
                0, 5, 1, 3, 2, 6, 4, 7
            });
            player.AddTiletoHand(testTile4);

            server.gameState = Server.State.safe;
            Tile tobePlayed = player.iplayer.PlayTurn(server.board, player.Hand, 33);
            Assert.AreEqual(1, server.alive.Count);
         
            Assert.IsTrue(testTile4.CompareByPath(tobePlayed));

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    Assert.IsNull(server.board.tiles[i][j]);
                }
            }
        }

        [TestMethod]
        public void TestMPlayer2PlayTurnMultiLegalHand()
        {
            MPlayer2 mPlayer = new MPlayer2("mark");
            List<string> other_colors = new List<string>(Constants.colors);
            other_colors.Remove("blue");
            mPlayer.Initialize("blue", other_colors);
            server.AddPlayer(mPlayer, "blue");
            Player player = new Player(mPlayer, "blue");
            server.board.AddPlayerToken("blue", new Position(0, -1, 5));

            // symmetricity of 1, illegal
            player.AddTiletoHand(testTile1);

            // symmetricity of 4, legal
            Tile testTile4 = new Tile(4, new List<int>(8) {
                0, 5, 1, 3, 2, 6, 4, 7
            });
            player.AddTiletoHand(testTile4);
            // symmetricity of 1, legal
            Tile testTile5 = new Tile(5, new List<int>(8) {
                0, 5, 1, 4, 2, 7, 6, 3
            });
            player.AddTiletoHand(testTile5);

            server.gameState = Server.State.safe;
            Tile tobePlayed = player.iplayer.PlayTurn(server.board, player.Hand, 32);

            Assert.IsTrue(testTile5.CompareByPath(tobePlayed));

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    Assert.IsNull(server.board.tiles[i][j]);
                }
            }
        }

        [TestMethod]
        public void TestMPlayer2PlayTurnMultiLegalHandFirst()
        {
            MPlayer2 mPlayer = new MPlayer2("mark");
            List<string> other_colors = new List<string>(Constants.colors);
            other_colors.Remove("blue");
            mPlayer.Initialize("blue", other_colors);
            server.AddPlayer(mPlayer, "blue");
            Player player = new Player(mPlayer, "blue");
            server.board.AddPlayerToken("blue", new Position(0, -1, 5));

            // symmetricity of 1, illegal
            player.AddTiletoHand(testTile1);

            // symmetricity of 4, legal
            Tile testTile4 = new Tile(4, new List<int>(8) {
                0, 5, 1, 3, 2, 6, 4, 7
            });
            // symmetricity of 4, legal
            Tile testTile5 = new Tile(5, new List<int>(8) {
                0, 2, 1, 4, 3, 7, 5, 6
            });
            player.AddTiletoHand(testTile4);
            player.AddTiletoHand(testTile5);

            server.gameState = Server.State.safe;
            Tile tobePlayed = player.iplayer.PlayTurn(server.board, player.Hand, 32);

            // same symmetricity, both 4 and 5 legal. But 4 was added to  hand first so it should be 4
            Assert.IsTrue(testTile4.CompareByPath(tobePlayed));

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    Assert.IsNull(server.board.tiles[i][j]);
                }
            }
        }

        [TestMethod]
        public void TestMPlayer2PlayTurnMultiLegalHandFirstFlipped()
        {
            MPlayer2 mPlayer = new MPlayer2("mark");
            List<string> other_colors = new List<string>(Constants.colors);
            other_colors.Remove("blue");
            mPlayer.Initialize("blue", other_colors);
            server.AddPlayer(mPlayer, "blue");
            Player player = new Player(mPlayer, "blue");
            server.board.AddPlayerToken("blue", new Position(0, -1, 5));

            // symmetricity of 1, illegal
            player.AddTiletoHand(testTile1);

            // symmetricity of 4, legal
            Tile testTile4 = new Tile(4, new List<int>(8) {
                0, 5, 1, 3, 2, 6, 4, 7
            });
            // symmetricity of 4, legal
            Tile testTile5 = new Tile(5, new List<int>(8) {
                0, 2, 1, 4, 3, 7, 5, 6
            });
            player.AddTiletoHand(testTile5);
            player.AddTiletoHand(testTile4);

            server.gameState = Server.State.safe;
            Tile tobePlayed = player.iplayer.PlayTurn(server.board, player.Hand, 32);

            // same symmetricity, both 4 and 5 legal. But 4 was added to  hand first so it should be 4
            Assert.IsTrue(testTile5.CompareByPath(tobePlayed));

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    Assert.IsNull(server.board.tiles[i][j]);
                }
            }
        }

        [TestMethod]
        public void TestMPlayer3PlayTurnNoLegalHand()
        {
            MPlayer3 mPlayer = new MPlayer3("mark");
            List<string> other_colors = new List<string>(Constants.colors);
            other_colors.Remove("blue");
            mPlayer.Initialize("blue", other_colors);
            server.AddPlayer(mPlayer, "blue");
            Player player = new Player(mPlayer, "blue");
            server.board.AddPlayerToken("blue", new Position(0, -1, 5));

            // symmetricity of 1
            player.AddTiletoHand(testTile1);

            // symmetricity of 4
            Tile testTile4 = new Tile(4, new List<int>(8) {
                0, 5, 1, 3, 2, 6, 4, 7
            });
            player.AddTiletoHand(testTile4);

            // right of tile placement
            Tile testTile5 = new Tile(5, new List<int>(8) {
                6, 0, 7, 1, 2, 3, 4, 5
            });
            // below of tile placement
            Tile testTile6 = new Tile(6, new List<int>(8) {
                0, 7, 1, 6, 2, 3, 4, 5
            });

            server.board.PlaceTile(testTile5, 1, 0);
            server.board.PlaceTile(testTile6, 0, 1);

            server.gameState = Server.State.safe;
            Tile tobePlayed = player.iplayer.PlayTurn(server.board, player.Hand, 33);

            Assert.IsTrue(testTile4.CompareByPath(tobePlayed));
        }

        [TestMethod]
        public void TestMPlayer3PlayTurnOneLegalHand()
        {
            MPlayer3 mPlayer = new MPlayer3("mark");
            List<string> other_colors = new List<string>(Constants.colors);
            other_colors.Remove("blue");
            mPlayer.Initialize("blue", other_colors);
            server.AddPlayer(mPlayer, "blue");
            Player player = new Player(mPlayer, "blue");
            server.board.AddPlayerToken("blue", new Position(0, -1, 5));

            // symmetricity of 1
            player.AddTiletoHand(testTile1);

            // symmetricity of 4
            Tile testTile4 = new Tile(4, new List<int>(8) {
                0, 5, 1, 3, 2, 6, 4, 7
            });
            player.AddTiletoHand(testTile4);

            server.gameState = Server.State.safe;
            Tile tobePlayed = player.iplayer.PlayTurn(server.board, player.Hand, 33);
            Assert.AreEqual(1, server.alive.Count);

            Assert.IsTrue(testTile4.CompareByPath(tobePlayed));

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    Assert.IsNull(server.board.tiles[i][j]);
                }
            }
        }

        [TestMethod]
        public void TestMPlayer3PlayTurnMultiLegalHand()
        {
            MPlayer3 mPlayer = new MPlayer3("mark");
            List<string> other_colors = new List<string>(Constants.colors);
            other_colors.Remove("blue");
            mPlayer.Initialize("blue", other_colors);
            server.AddPlayer(mPlayer, "blue");
            Player player = new Player(mPlayer, "blue");
            server.board.AddPlayerToken("blue", new Position(0, -1, 5));

            // symmetricity of 1, illegal
            player.AddTiletoHand(testTile1);

            // symmetricity of 4
            Tile testTile4 = new Tile(4, new List<int>(8) {
                0, 5, 1, 3, 2, 6, 4, 7
            });
            player.AddTiletoHand(testTile4);
            // symmetricity of 1, legal
            Tile testTile5 = new Tile(5, new List<int>(8) {
                0, 5, 1, 4, 2, 7, 6, 3
            });
            player.AddTiletoHand(testTile5);



            server.gameState = Server.State.safe;
            Tile tobePlayed = player.iplayer.PlayTurn(server.board, player.Hand, 33);
            Assert.AreEqual(1, server.alive.Count);

            Assert.IsTrue(testTile4.CompareByPath(tobePlayed));
        }

        [TestMethod]
        public void TestMPlayer3PlayTurnMultiLegalHandFirst()
        {
            MPlayer3 mPlayer = new MPlayer3("mark");
            List<string> other_colors = new List<string>(Constants.colors);
            other_colors.Remove("blue");
            mPlayer.Initialize("blue", other_colors);
            server.AddPlayer(mPlayer, "blue");
            Player player = new Player(mPlayer, "blue");
            server.board.AddPlayerToken("blue", new Position(0, -1, 5));

            // symmetricity of 1, illegal
            player.AddTiletoHand(testTile1);

            // symmetricity of 4
            Tile testTile4 = new Tile(4, new List<int>(8) {
                0, 5, 1, 3, 2, 6, 4, 7
            });
            player.AddTiletoHand(testTile4);
            // symmetricity of 4, legal
            Tile testTile5 = new Tile(5, new List<int>(8) {
                0, 2, 1, 4, 3, 7, 5, 6
            });
            player.AddTiletoHand(testTile5);
            server.gameState = Server.State.safe;
            Tile tobePlayed = player.iplayer.PlayTurn(server.board, player.Hand, 33);

            Assert.IsTrue(testTile4.CompareByPath(tobePlayed));
        }

        [TestMethod]
        public void TestMPlayer3PlayTurnMultiLegalHandFirstFlipped()
        {
            MPlayer3 mPlayer = new MPlayer3("mark");
            List<string> other_colors = new List<string>(Constants.colors);
            other_colors.Remove("blue");
            mPlayer.Initialize("blue", other_colors);
            server.AddPlayer(mPlayer, "blue");
            Player player = new Player(mPlayer, "blue");
            server.board.AddPlayerToken("blue", new Position(0, -1, 5));

            // symmetricity of 1, illegal
            player.AddTiletoHand(testTile1);

            // symmetricity of 4
            Tile testTile4 = new Tile(4, new List<int>(8) {
                0, 5, 1, 3, 2, 6, 4, 7
            });
            // symmetricity of 4, legal
            Tile testTile5 = new Tile(5, new List<int>(8) {
                0, 2, 1, 4, 3, 7, 5, 6
            });
            player.AddTiletoHand(testTile5);
            player.AddTiletoHand(testTile4);

            server.gameState = Server.State.safe;
            Tile tobePlayed = player.iplayer.PlayTurn(server.board, player.Hand, 33);

            Assert.IsTrue(testTile5.CompareByPath(tobePlayed));
        }

        [TestMethod]
        public void TestReplacePlayer()
        {
            MPlayer1 mPlayer1 = new MPlayer1("john");
            MPlayer2 replacement = new MPlayer2("adam");

            Player player = new Player(mPlayer1, "blue");
            player.ReplaceIPlayer(replacement);
            Assert.AreEqual("adam", player.iplayer.GetName());
        }


    }
}
