using System.IO;
using BepInEx;
using JotunnLib.Entities;
using UnityEngine;

namespace Whetstone.Prefabs
{
    public class RepairKitSilver : PrefabConfig
    {
        //Using Obsidian as the base just for the model
        public RepairKitSilver() : base("RepairKitSilver", "Obsidian")
        {

        }

        public override void Register()
        {
            // Configure item drop
            ItemDrop item = Prefab.GetComponent<ItemDrop>();
            item.m_itemData.m_shared.m_itemType = ItemDrop.ItemData.ItemType.Material;
            item.m_itemData.m_shared.m_name = "Silver Repair Kit";
            item.m_itemData.m_shared.m_description = "Repair kit with sharpening stone that can repair up to Silver quality tools.\n\nRestores " + Whetstone.repairAmount.Value.ToString("P0").Replace(" ", "") + " durability to your equipped tool or weapon.";
            item.m_itemData.m_dropPrefab = Prefab;
            item.m_itemData.m_shared.m_weight = 2f;
            item.m_itemData.m_shared.m_maxStackSize = 10;
            item.m_itemData.m_shared.m_variants = 1;
            item.m_itemData.m_shared.m_teleportable = true;

            var LoadedAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Paths.PluginPath, "Whetstone", "whetstone.assets"));

            if (LoadedAssetBundle == null)
            {
                Whetstone.logger.LogWarning("Failed to load AssetBundle!");
                return;
            }

            Texture2D icon = LoadedAssetBundle.LoadAsset<Texture2D>("repairkitsilver-sprite.png");
            if (icon != null)
            {
                Sprite sprite = Sprite.Create(icon, new Rect(0f, 0f, icon.width, icon.height), Vector2.zero);
                //m_icons[0] is the actual sprite itself.
                item.m_itemData.m_shared.m_icons[0] = sprite;
            }

            LoadedAssetBundle?.Unload(false);
        }
    }
}