using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation_game
{
    class Melee : Unit
    {
        public Melee(int x, int y, string team, int health = 20, int speed = 1, int attack = 4, int range = 1, string symbol = " !  ") : base(x, y, health, speed, attack, range, team, symbol)
        {
            name = "Knight";
        }



        public override string ToString()
        {
            if (Health <= 0)
            {
                return "Melee: DEAD";
            }
            else
            {
                return "Melee unit: " + name + " \n Health: " + Health + "\n Range: " + attackRange + "\n Speed: " + speed + "\n Team: " + team;
            }

        }

        public override string SaveData()
        {
            return $"M,{Xvalue},{Yvalue},{Team},{Health},{speed},{MaxHealth}";
        }
    }
}
