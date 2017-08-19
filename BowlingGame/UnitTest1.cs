using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BowlingGame
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GivenTwentyZerosThen0Returned()
        {
            var game = new BowlingGame();
            RollMany(game, 20, 0);

            Assert.AreEqual(0, game.Score());
        }

        [TestMethod]
        public void GivenTwentyOnesThen20Returned()
        {
            var game = new BowlingGame();
            RollMany(game, 20, 1);

            Assert.AreEqual(20, game.Score());
        }

        [TestMethod]
        public void GivenOneSpareThenSparePlusNextRoll()
        {
            var game = new BowlingGame();
            game.Roll(4);
            game.Roll(6);
            game.Roll(2);
            RollMany(game, 17, 0);

            Assert.AreEqual(14, game.Score());
        }

        [TestMethod]
        public void GivenSpareIsOnSecondFrameSpareBonusApplied()
        {
            var game = new BowlingGame();
            game.Roll(0);
            game.Roll(1);
            game.Roll(5);
            game.Roll(5);
            game.Roll(2);
            RollMany(game, 16, 0);

            Assert.AreEqual(15, game.Score());
        }

        [TestMethod]
        public void GivenSeparateFramesSpareBonusNotApplied()
        {
            var game = new BowlingGame();
            game.Roll(0);
            game.Roll(5);
            game.Roll(5);
            game.Roll(2);
            RollMany(game, 16, 0);

            Assert.AreEqual(12, game.Score());
        }

        [TestMethod]
        public void GivenOneStrikeThenStrikePlusNextTwoRolls()
        {
            var game = new BowlingGame();
            game.Roll(10);
            game.Roll(1);
            game.Roll(2);
            RollMany(game, 16, 0);

            Assert.AreEqual(16, game.Score());
        }

        [TestMethod]
        public void PerfectGame()
        {
            var game = new BowlingGame();
            RollMany(game, 12, 10);

            Assert.AreEqual(300, game.Score());
        }

        private static void RollMany(BowlingGame game, int times, int pins)
        {
            for(var i = 0; i < times; i++)
            {
                game.Roll(pins);
            }
        }
    }
    
    public class BowlingGame
    {
        private List<int> _pinsSoFar = new List<int>();            

        public void Roll(int pins)
        {
            _pinsSoFar.Add(pins);            
        }

        public int Score()
        {
            var score = 0;
            var roll = 0;

            for (int i = 0; i < 10; i++)
            {
                if (IsSpare(roll))
                {
                    score += SpareBonus(roll);
                    roll = roll + 2;
                }
                else if (_pinsSoFar[roll] == 10)
                {
                    score += StrikeBonus(roll);
                    roll = roll + 1;
                }
                else
                {
                    score += _pinsSoFar[roll] + _pinsSoFar[roll + 1];
                    roll = roll + 2;
                }
            }

            return score;
        }

        private int StrikeBonus(int roll)
        {
            return 10 + _pinsSoFar[roll + 1] + _pinsSoFar[roll + 2];
        }

        private int SpareBonus(int roll)
        {
            return 10 + _pinsSoFar[roll + 2];
        }

        private bool IsSpare(int roll)
        {
            return _pinsSoFar[roll] + _pinsSoFar[roll + 1] == 10;
        }
    }
}
