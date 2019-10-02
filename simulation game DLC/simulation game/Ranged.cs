using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation_game
{
    class Ranged:Unit
    {
        public Ranged(int x, int y, string team, int health = 10, int speed = 2, int attack = 3, int range = 2, string symbol = " {  ") : base(x, y, health, speed, attack, range, team, symbol)
        {
            name = "Archer";
        }

        public override string ToString()
        {
            if (Health <= 0)
            {
                return "Ranged: DEAD";
            }
            else
            {
                return "Ranged unit: "  +name + " \n Health: " + Health + "\n Range: " + attackRange + "\n Speed: " + speed + "\n Team: " + team;
            }
        }

        public override string SaveData()
        {
            return $"R,{Xvalue},{Yvalue},{Team},{Health},{speed},{MaxHealth}";
        }
    }
}
