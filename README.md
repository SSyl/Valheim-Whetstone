# Whetstone - Repair Kits & Sharpening Stones v1.0.0
## by SSyl
This mod adds a repair kit for every "tier" of tools and weapons in the game (not armor). Currently, it works by repairing your currently equipped weapon. If your weapon or tool is completely broken, there's an Advanced Repair Kit that allows you to repair all items in your inventory by a small amount which then lets you equip your item again to repair it.

I came to the point in the game where I was carrying multiple pickaxes on me when I went on longer mining trips or having to keep destroying and rebuilding portals just to teleport myself back to base, repair, and come back over and over again. I figured that repair kits could help with this issue.

One thing though, I didn't want to completely remove the need to go back to base to repair so I didn't want repair kits to be extremely easy to make. I also didn't want them to be too much of a hassle to make, because then it's easier to just build multiple pickaxes which is what I was avoiding in the first place.

I'm still fine-tuning the recipes and they'll change as I update the mod, however the recipes are entirely customizable using the ***recipes.json*** file that you can find in your ***Valheim\BepInEx\Plugins\Whetstone folder***.

By default, the tiered repair kits repair 25% durability and the Advanced Repair Kit repairs 5% durability to *all* broken weapons and tools in your bag. These percentages can be customized in the config file.

## Added recipes

**Bronze Repair Kit (x5):** 1 Hard antler, 5 Flint, 3 Core Wood. 
***Can repair up to Bronze-Tier weapons and tools***

**Iron Repair Kit (x5):** 1 Iron, 3 Withered bone, 2 Ancient Bark 
***Can repair up to Iron tier and below***

**Silver Repair Kit (x5):** 1 Silver, 1 Crystal, 5 Obsidian 
***Can repair up to Silver tier and below***

**Black Metal Repair Kit (x5):** 2 Black Metal, 2 Crsytal, 2 Serpent Scale, 2 Surtling Core 
***Can repair up to Black Metal tier and below***

**Advanced Repair Kit:** 1 Bronze, 2 Hard Antler, 3 Fine Wood, 2 Surtling Core
***Repairs all broken weapons and tools in your inventory, allowing you to equip it and repair it with a standard repair kit***

## Frequently Asked Questions
**How do I repair my items?** You repair items by first equipping them and then using one of the standard repair kits.
**My item is broken! Why isn't the repair kit working?** To repair a fully broken item, you have to use the *Advanced Repair Kit*. This kit repairs all broken tools and weapons in your inventory by a tiny amount. After you use that, you can equip your broken item and use a standard repair kit.
**Why can't I repair my shield or armor?** The repair kits are only meant for weapons and tools. Armor has a lot more durability, so I felt it wasn't needed. I might add the option in the future.
**I think the recipes are too hard/easy/not to my liking** You can edit the recipes in the recipes.json file. Please note, future updates or reinstalling the mod will replace this file. So make sure you make a backup of the file with your changes so you can restore them in the future.
**The repair kits repair too little/too much** You can change how much repair kits repair in the config file. It's percentage based and uses a decimal, so if you wanted to say, make it repair 35% of an item's total durability, you'd put 0.35 in the config file.

## Extra Information
Eventually I'd like to add a "repair" tab to your personal crafting window (when you press tab) which will allow you to individually repair items using repair kits. However, since I'm only a hobbiest coder I couldn't figure out how to do that. That's why it only repairs your currently equipped weapon/tool rather than manually selecting what to repair through a GUI. That said, I did extensive testing and it works well. If you do find any bugs, please let me know.

## Installation

### Vortex Installation:

1. Download and enable JotunnLib (it's a separate download from this mod).
2. Download and enable this mod and you're good to go!

### Manual Installation:

1. Install BepInEx.
2. Download JotunnLib and place the JotunnLib.dll inside the Valheim\BepInEx\plugins folder.
3. Download Whetstone and place the Whetstone folder (move the entire folder, not just the files) inside the plugins folder. If you did it correctly, there should be a folder inside of Valheim\BepInEx\plugins called Whetstone and inside that folder should three files. One file called Whetstone.dll, one file called whetstone.assets, and a recipes.json file.

### Requirements:
* [JotunnLib](https://www.nexusmods.com/valheim/mods/507)
* [BepInEx](https://valheim.thunderstore.io/package/denikson/BepInExPack_Valheim/)

### Sources
* Source: https://github.com/SSyl/Whetstone
* NexusMods: https://www.nexusmods.com/valheim/mods/