using BepInEx;
using HarmonyLib;
using LitJson;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Whetstone
{
    //public void UseItem(Inventory inventory, ItemDrop.ItemData item, bool fromInventoryGui)
    [HarmonyPatch(typeof(Humanoid), "UseItem")]
    public static class Humanoid_Patch
    {

        public static void Postfix(Inventory inventory, ItemDrop.ItemData item)
        {
            //Whetstone.logger.LogInfo("Right-clicked on " + item.m_shared.m_name);
            //Whetstone.logger.LogInfo("Is equipped: " + item.m_equiped);

            //This checks if the item that was right clicked is a Repair Kit and returns an int of the repair level
            int repairKitLevel = RepairKitLevel(item.m_shared.m_name);
            //TODO: make this the prefab name or a variable that can be localized
            if (repairKitLevel != 0 && repairKitLevel != 99)
            {
                Player player = Player.m_localPlayer;

                ItemDrop.ItemData rightEquippedItem = player.GetRightItem();
                ItemDrop.ItemData leftEquippedItem = player.GetLeftItem();

                if (rightEquippedItem != null && rightEquippedItem.m_shared.m_canBeReparied)
                {
                    RepairItem(player, inventory, item, rightEquippedItem, repairKitLevel);
                }
                else if (leftEquippedItem != null && leftEquippedItem.m_shared.m_itemType != ItemDrop.ItemData.ItemType.Shield)
                {
                    RepairItem(player, inventory, item, leftEquippedItem, repairKitLevel);
                }
                else
                {
                    player.Message(MessageHud.MessageType.Center, "Nothing to repair.");
                }
            }
            else if (repairKitLevel == 99)
            {
                int brokenItems = 0;
                List<ItemDrop.ItemData> allInventory = inventory.GetAllItems();
                foreach (ItemDrop.ItemData invItem in allInventory)
                {
                    if ((invItem.IsWeapon() || invItem.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Tool) && invItem.m_durability <= 0f)
                    {
                        //Repair all broken items to config float value (default is 0.05f)
                        invItem.m_durability = invItem.GetMaxDurability(invItem.m_quality) * Whetstone.repairAmountAdvancedKit.Value;
                        brokenItems++;
                    }
                }
                if (brokenItems > 0)
                {
                    Player.m_localPlayer.Message(MessageHud.MessageType.Center, brokenItems + " items repaired.");
                }
                else
                {
                    Player.m_localPlayer.Message(MessageHud.MessageType.Center, "You have no broken items that can be repaired by this kit.");
                }

            }
        }

        private static int RepairKitLevel(string itemName)
        {
            switch (itemName)
            {
                case "Bronze Repair Kit":
                    return 1;
                case "Iron Repair Kit":
                    return 2;
                case "Silver Repair Kit":
                    return 3;
                case "Black Metal Repair Kit":
                    return 4;
                case "Advanced Repair Kit":
                    return 99;
                default:
                    return 0;
            }
        }
        private static void RepairItem(Player player, Inventory inventory, ItemDrop.ItemData repairKitItem, ItemDrop.ItemData equippedItem, int repairKitLevel)
        {
            //Name of the equipped item
            string equippedName = equippedItem.m_shared.m_name;
            //Max durability of the item with its current quality (star) level
            float equippedMaxDurability = equippedItem.GetMaxDurability(equippedItem.m_quality);
            //Minimum crafting station level for repair. I use this to determine repair kit quality needed to repair the item.
            int minRepairLevel = ObjectDB.instance.GetRecipe(equippedItem).m_minStationLevel;

            if (minRepairLevel == 0)
            {
                player.Message(MessageHud.MessageType.Center, "Can't repair item.");
                //Log a warning since it shouldn't get this far without having a repair level.
                Whetstone.logger.LogWarning("Whetstone: Can't repair " + equippedName);
            }
            else if (minRepairLevel > repairKitLevel)
            {
                player.Message(MessageHud.MessageType.Center, Localization.instance.Localize(equippedName) + " needs a higher quality repair kit.");
            }
            else if (equippedItem.m_durability >= equippedMaxDurability)
            {
                player.Message(MessageHud.MessageType.Center, Localization.instance.Localize(equippedName) + " can't be repaired further.");
            }
            else if (equippedItem.m_durability < equippedMaxDurability && minRepairLevel <= repairKitLevel)
            {
                //If repairing would make the item go above the max durability, repair it to the max instead.
                if(equippedItem.m_durability + equippedMaxDurability * Whetstone.repairAmount.Value >= equippedMaxDurability)
                {
                    equippedItem.m_durability = equippedMaxDurability;
                }
                else
                {
                    //Adds % durability to equipped item. % value is taken from config float value (default is 0.25f) 
                    equippedItem.m_durability += equippedItem.GetMaxDurability(equippedItem.m_quality) * Whetstone.repairAmount.Value;
                    player.Message(MessageHud.MessageType.Center, Localization.instance.Localize(equippedName) + " repaired.");
                }
                //Delete the repair kit from player's inventory.
                inventory.RemoveOneItem(repairKitItem);
            }
        }
    }


}