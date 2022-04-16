using System;
using System.Collections.Generic;
using System.Text;

namespace MiniTD
{
    class Player
    {
        public int Gold { get; private set; }
        public Player()
        {
            this.Gold = 0;
        }

        public void AddBounty(Enemy enemy)
        {
            Gold += enemy.Bounty;
        }

        public bool SpendGold(int amount)
        {
            if (Gold - amount >= 0)
            {
                Gold -= amount;
                return true;
            }
            return false;
        }
    }
}
