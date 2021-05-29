using System;

namespace Contracts
{
    public class FightParameters
    {
        public string BotName { get; set; }
        public string BotCode { get; set; }

        public override string ToString()
        {
            return $"{BotName} with code: {BotCode}";
        }
    }
}
