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
                    Unit sample = building.DoBuildingFunction();//if the returned unit in sample is null then the building was a recource building, 

                    if (sample != null)
                    {//else add the new unit to the player array                        Array.Resize( ref world.players, world.players.Length+1);
                        world.players[world.players.Length-1] = sample; 
                    }
                }
            }
            world.UpdateWorld();

            foreach (Unit unit in world.players)
            {
                if (unit.IsDead) { continue; }//if the unit is dead, dont waist your bread.....

                unit.NearestEnemy(world.players);//find a unit

                if (unit.ClosestUnit == null)
                {
                    gameOver = true;
                    winningFaction = unit.Team;
                    world.UpdateWorld();
                    return;
                }

                double healthPercent = unit.Health / unit.MaxHealth * 100;
                if (healthPercent <= 25)//if the unit is low on health it should run away
                {
                    unit.RandomMove();
                }
                else if (unit.WithinRange())
                {
                    unit.Attack(unit.ClosestUnit);
                }
                else
                {
                    unit.Move();//if they cant reach their enemy then they must move closer to their enemy.
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
