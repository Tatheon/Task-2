using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace simulation_game
{
    class GameEngine
    {
        private int rounds = 1;//after all units have had their turn this is incremented
        public Map world = new Map(10, 4);
        public bool gameOver = false;
        string winningFaction;

        public void Run()
        {
            foreach (Building building in world.buildings)
            {
                if (rounds % building.Speed == 0)  // find mod to see if the building can perform its function
                {
                    Unit sample = building.DoBuildingFunction();

                    if (sample != null)
                    {
                        Array.Resize( ref world.players, world.players.Length+1);
                        world.players[world.players.Length-1] = sample; 
                    }
                }
            }
            world.UpdateWorld();

            foreach (Unit unit in world.players)
            {
                if (unit.IsDead) { continue; }

                unit.NearestEnemy(world.players);

                if(unit.ClosestUnit == null)
                {
                    gameOver = true;
                    winningFaction = unit.Team;
                    world.UpdateWorld();
                    return;
                }

                double healthPercent = unit.Health / unit.MaxHealth * 100;
                if (healthPercent <= 25)
                {
                    unit.RandomMove();
                }
                else if (unit.WithinRange())
                {
                    unit.Attack(unit.ClosestUnit);
                }
                else
                {
                    unit.Move();
                }

                world.UpdateWorld();
                
            }
            rounds++;
        }

        public GameEngine()
        {

        }

        public string Rounds
        {
            get { return "Rounds " + rounds; }
        }

        public string WinningTeam
        {
            get { return winningFaction; }
        }
    }
}
