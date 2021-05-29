using System;

namespace Contracts
{
    public class NextMoveRequest
    {
        public int MoveNumber { get; set; }

        public NextMoveRequest(int moveNumber)
        {
            MoveNumber = moveNumber;
        }

        public NextMoveRequest()
        {
            
        }
    }
}
