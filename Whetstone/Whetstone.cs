using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using JotunnLib.Managers;
using JotunnLib.Entities;
using Whetstone.Prefabs;
using BepInEx.Configuration;

namespace Whetstone
{
    [BepInPlugin("SSyl.Whetstone", "Whetstone", "1.1.0")]
    [BepInDependency("com.bepinex.plugins.jotunnlib")]
    public class Whetstone : BaseUnityPlugin
    {
        public static ManualLogSource logger;

        public static ConfigEntry<bool> modEnabled;
        public static ConfigEntry<float> repairAmount;
        public static ConfigEntry<float> repairAmountAdvancedKit;
        public static ConfigEntry<bool> customJsonRecipes;
        public static ConfigEntry<int> nexusID;

        private void Awake()
        {
            logger = Logger;

            modEnabled = Config.Bind("General", "Enabled", true, "Settings this to false disables all features of this mod.");
            repairAmount = Config.Bind("General", "RepairAmountPercentage", 0.25f, "This is the percentage (in decimal) of an item's durability that gets repared when using a Bronze/Iron/Silver/BlackMetal repair kit. Example: If you want 15% put 0.15");
            repairAmountAdvancedKit = Config.Bind("General", "AdvancedKitRepairPercentage", 0.05f, "This is the percentage (in decimal) of ALL broken item's durability repaired when using the Advanced Repair Kit. Example: If you want 15% put 0.15");
            customJsonRecipes = Config.Bind("General", "CustomJsonRecipes", false, "Setting this to true will make the mod use custom recipes that you've defined in a seperate custom_recipes.json file. You must create this file yourself, otherwise this setting won't do anything.");
            nexusID = Config.Bind("General", "NexusID", 651, "Nexus mod ID for update checker. Do not change this value.");

            if (modEnabled.Value)
                return;

                Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), "SSyl.Whetstone");
                ObjectManager.Instance.ObjectRegister += registerObjects;
                PrefabManager.Instance.PrefabRegister += registerPrefabs;
        }
        private void registerPrefabs(object sender, EventArgs e)
        {
            PrefabManager.Instance.RegisterPrefab(new RepairKitBronze());
            PrefabManager.Instance.RegisterPrefab(new RepairKitIron());
            PrefabManager.Instance.RegisterPrefab(new RepairKitSilver());
            PrefabManager.Instance.RegisterPrefab(new RepairKitBlackMetal());
            PrefabManager.Instance.RegisterPrefab(new RepairKitAdvanced());
        }

        private void registerObjects(object sender, EventArgs e)
        {
            string[] itemsArray = 
            {   "RepairKitBronze",
                "RepairKitIron",
                "RepairKitSilver",
                "RepairKitBlackMetal",
                "RepairKitAdvanced"
            };

            foreach (string item in itemsArray)
            {
                ObjectManager.Instance.RegisterItem(item);
            }

            string recipesFileName = "recipes.json";
            if(customJsonRecipes.Value == true && File.Exists(Path.Combine(Paths.PluginPath, "Whetstone", "custom_recipes.json")))
            {
                recipesFileName = "custom_recipes.json";
            }

            string jsonString = File.ReadAllText(Path.Combine(Paths.PluginPath, "Whetstone", recipesFileName));
            RecipeList recipeList = LitJson.JsonMapper.ToObject<RecipeList>(jsonString);

            if (recipeList != null)
            {
                foreach (var recipe in recipeList.recipes)
                {
                    RecipeConfig recipeConfig = new RecipeConfig
                    {
                        Name = recipe.name,
                        Item = recipe.item,
                        Amount = recipe.amount,
                        CraftingStation = recipe.craftingStation,
                        MinStationLevel = recipe.minStationLevel
                    };
                    List<PieceRequirementConfig> list = new List<PieceRequirementConfig>();
                    foreach (var recipeReq in recipe.requirements)
                    {
                        list.Add(
                        new PieceRequirementConfig()
                        {
                            Item = recipeReq.item,
                            Amount = recipeReq.amount
                        });
                    }
                    recipeConfig.Requirements =  list.ToArray();

                    if (itemsArray.Contains(recipeConfig.Item))
                    {
                        ObjectManager.Instance.RegisterRecipe(recipeConfig);
                    }
                }
            }
        }
    }
}
