using System;
using System.Collections.Generic;

namespace Whetstone
{
    [Serializable]
    public class RecipeRequirements
    {
        public string item = "";
        public int amount = 1;
    }

    [Serializable]
    public class Recipes
    {
        public string name = "";
        public string item = "";
        public int amount = 1;
        public string craftingStation = "";
        public int minStationLevel = 1;
        public bool enabled = true;
        public string repairStation = "";
        public List<RecipeRequirements> requirements = new List<RecipeRequirements>();
    }

    [Serializable]
    public class RecipeList
    {
        public List<Recipes> recipes = new List<Recipes>();
    }

}
