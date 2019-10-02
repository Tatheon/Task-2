using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace simulation_game
{
    class Map
    {
        public string[,] map = new string[mapSize, mapSize];

        public Unit[] players;
        public Building[] buildings;

        
        public Random r = new Random();      //r.Next(1,20);   random number within the array's bounds
        string[] teams = { "A", "B" };

        const int mapSize = 20;
        const string FILENAME = "MapData.txt";
        const char DELIMITER = ',';
        enum UnitSTAT  { Symbol = 0, X = 1, Y = 2, Team = 3, Health = 4, Speed =5, MaxHealth = 6}
        enum FactorySTAT { Symbol = 0, X = 1, Y = 2, Team = 3, Spawnpoint = 4, ProductionSpeed = 5, Health = 6}
        enum ResourceSTAT { Symbol = 0, X = 1, Y = 2, Team = 3, Health = 4, ResourcePerRound = 5, ResourcePool = 6}
        public Map(int numUnits, int numBuildings)
        {
            players = new Unit[numUnits];
            buildings = new Building[numBuildings];
            WorldBuild();
        }

        public void WorldBuild()
        {
            
            for (int i = 0; i < players.Length; i++)//creates the player types and randomizes their positions
            {
                if (r.Next(2) == 0)
                {
                    players[i] = new Ranged(r.Next(1, mapSize), r.Next(0, mapSize), teams[r.Next(2)]);

                }
                else
                {
                    players[i] = new Melee(r.Next(1, mapSize), r.Next(0, mapSize), teams[r.Next(2)]);
                }
            }

            for (int i = 0; i < buildings.Length; i++)//creates the player types and randomizes their positions
            {
                if (r.Next(2) == 0)
                {
                    int yValue = r.Next(1, mapSize);
                    int SpawnVal;
                    if (yValue == 0)
                    {
                        SpawnVal = -1;
                    }
                    else
                    {
                        SpawnVal = 1;
                    }

                    buildings[i] = new FactoryBuilding(r.Next(1, mapSize), yValue, teams[r.Next(2)], SpawnVal);

                }
                else
                {
                    buildings[i] = new ResourceBuilding(r.Next(1, mapSize), r.Next(1, mapSize), teams[r.Next(2)]);
                }
            }

            UpdateWorld();
        }

        public void UpdateWorld()
        {
            for (int y = 0; y < mapSize; y++)
            {
                for (int x = 0; x < mapSize; x++)
                {
                    map[x, y] = "  '  ";
                }
            }
            foreach(Building structure in buildings)
            {
                map[structure.XPos, structure.YPos] = structure.Team + structure.Symbol;
            }

            foreach (Unit playerType in players)
            {
                map[playerType.Xvalue, playerType.Yvalue] =  playerType.Team + playerType.Symbol; //as each unit moves the entire array will need to be displayed so this method will surfise to update the positions of the units on map after they are altered by game engine or something
            }
        }

        public void LoadWorld()
        {
            FileStream infile = new FileStream(FILENAME, FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader reader = new StreamReader(infile);
            string line = reader.ReadLine();
            string[] details;

            buildings = new Building[0];
            players = new Unit[0];

            while (line!=null)
            {

                details = line.Split(DELIMITER);

                switch ( details[ (int)UnitSTAT.Symbol] )
                {
                    case "M":
                        Array.Resize(ref players, players.Length+1);
                        players[players.Length - 1] = new Melee((int.Parse)(details[(int)UnitSTAT.X]), (int.Parse)(details[(int)UnitSTAT.Y]), details[(int)UnitSTAT.Team], (int.Parse)(details[(int)UnitSTAT.Health]), (int.Parse)(details[(int)UnitSTAT.Speed]) );
                        players[players.Length - 1].MaxHealth = (int.Parse)(details[(int)UnitSTAT.MaxHealth]);
                        break;

                    case "R":
                        Array.Resize(ref players, players.Length + 1);
                        players[players.Length - 1] = new Ranged((int.Parse)(details[(int)UnitSTAT.X]), (int.Parse)(details[(int)UnitSTAT.Y]), details[(int)UnitSTAT.Team], (int.Parse)(details[(int)UnitSTAT.Health]), (int.Parse)(details[(int)UnitSTAT.Speed]));
                        players[players.Length - 1].MaxHealth = (int.Parse)(details[(int)UnitSTAT.MaxHealth]);
                        break;

                    case "F":
                        Array.Resize(ref buildings, buildings.Length + 1);
                        buildings[buildings.Length - 1] = new FactoryBuilding((int.Parse)(details[(int)FactorySTAT.X]), (int.Parse)(details[(int)FactorySTAT.Y]), details[(int)FactorySTAT.Team], (int.Parse)(details[(int)FactorySTAT.Spawnpoint]), (int.Parse)(details[(int)FactorySTAT.ProductionSpeed]), details[(int)FactorySTAT.Symbol], (int.Parse)(details[(int)FactorySTAT.Health]));
                        break;

                    case "RE":
                        Array.Resize(ref buildings, buildings.Length + 1);
                        buildings[buildings.Length - 1] = new ResourceBuilding((int.Parse)(details[(int)ResourceSTAT.X]), (int.Parse)(details[(int)ResourceSTAT.Y]), details[(int)ResourceSTAT.Team], (int.Parse)(details[(int)ResourceSTAT.Health]), "R", (int.Parse)(details[(int)ResourceSTAT.ResourcePerRound]), (int.Parse)(details[(int)ResourceSTAT.ResourcePool]));
                        break;
                }
                line = reader.ReadLine();

            }
            UpdateWorld();
            reader.Close();
            infile.Close();
        }

        public void SaveWorld()
        {
            
            FileStream outfile = new FileStream(FILENAME, FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(outfile);
            
            foreach(Building building in buildings)
            {
                writer.WriteLine(building.SaveData());
            }

            foreach (Unit unit in players)
            {
                writer.WriteLine(unit.SaveData());
            }

            writer.Close();
            outfile.Close();
        }
    }
}
