using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation_game
{
    class ResourceBuilding : Building
    {
        private string resourceType;
        private int resourcesGenerated, resourcesPerRound, resourcePoolRemaining;
        public ResourceBuilding(int x, int y, string team, int health = 20, string symbol = "R", int ResourcePerRound = 1, int resourcePoolRemaining = 10) : base(x, y, health, team, symbol)
        {
            this.resourcesPerRound = ResourcePerRound;
        }

        public override void Kill()
        {
            symbol = "X";

        }

        public override string ToString()
        {
            if (Health <= 0)
            {
                return "Building: Destroyed";
            }
            else
            {
                return "Building: Resource generation \n Health: " + health + "\n Team: " + team + "\n Resources avalible: " + resourcesGenerated;
            }

        }

        public void GenerateResource()
        {
            resourcesGenerated += resourcesPerRound;//add what was generated
        }

        public override Unit DoBuildingFunction()
        {
            if (resourcePoolRemaining > 0)
            {
                GenerateResource();
                resourcePoolRemaining -= resourcesPerRound;//generate recource then subtract what was generated from how much it can produce in total
            }

            return null;
        }

        public override string SaveData()
        {
            return $"RE,{XPos},{YPos},{Team},{Health},{resourcesPerRound},{resourcePoolRemaining}";
        }

        //   properties




        public override int XPos
        {
            get { return base.xPos; }
            set { base.xPos = value; }
        }

        public override int YPos
        {
            get { return base.yPos; }
            set { base.yPos = value; }
        }

        public int Health
        {
            get { return base.health; }
        }

        public override string Team
        {
            get { return base.team; }
        }

        public override string Symbol
        {
            get { return base.symbol; }
        }

        public override int ResourcePoolRemaining
        {
            get { return resourcePoolRemaining; }
        }

        

        public override int Speed
        {
            get { return 1; }
        }
    }
}
