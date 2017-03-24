using System;
using System.Collections.Generic;
using System.Text;

namespace FunSharp.Core.Games.Randomized
{
    public class Dice : Randomizer
    {
        /// <summary>
        /// A static Randomizer which shares a seed
        /// </summary>
        public static new Dice Instance { get; } = new Dice();

        public Dice() : base() { }
        public Dice(int seed) : base(seed) { }

        public int RollBetween(int min, int max) { return Random.Next(min, max); }
        public int Roll(int sidesOnDice) { return Random.Next(1, sidesOnDice + 1); }
        public int Roll(int numberOfDice, int sidesOnDice)
        {
            int totalRoll = 0;
            for (int i = 0; i < numberOfDice; i++)
                totalRoll += Roll(sidesOnDice);
            return totalRoll;
        }

        public List<int> RollMultiple(int numberOfDice, int sidesOnDice)
        {
            List<int> rolls = new List<int>();
            for (int i = 0; i < numberOfDice; i++)
            {
                rolls.Add(Roll(sidesOnDice));
            }

            return rolls;
        }
        public List<int> RollMultiple(string dieString)
        {
            string[] dieSplit = dieString.Split('d');

            int numberOfDice;
            int sidesOnDice;

            if (!int.TryParse(dieSplit[0], out numberOfDice))
                return new List<int>();
            if (!int.TryParse(dieSplit[1], out sidesOnDice))
                return new List<int>();

            return RollMultiple(numberOfDice, sidesOnDice);
        }
    }
}
