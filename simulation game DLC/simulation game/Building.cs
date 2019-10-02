using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation_game
{
    abstract class Building
    {
        protected int xPos, yPos, health, maxHealth;
        protected string team, symbol;

        public abstract void Kill();

        public abstract string ToString();
        
        protected Building(int x, int y, int health, string team, string symbol)
        {
            xPos = x;
            yPos = y;
            this.health = health;
            maxHealth = health;
            this.team = team;
            this.symbol = symbol;
        }

        public abstract int XPos { get; set; }
        public abstract int YPos { get; set; }
        public abstract string Team { get; }
        public abstract string Symbol { get; }
        public abstract int ResourcePoolRemaining { get; }//resource building only
        
        public abstract int Speed { get; }

        public abstract string SaveData();
        public abstract Unit DoBuildingFunction();
    }
}
