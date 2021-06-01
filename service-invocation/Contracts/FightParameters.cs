using System;

namespace Contracts
{
    public class FightParameters
    {
        public string BotName { get; set; }
        public int Place { get; set; }

        public override string ToString()
        {
            return $"{BotName} with place: {Place}";
        }
    }
}
