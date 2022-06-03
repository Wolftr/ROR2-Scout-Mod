using RoR2;
using System.Collections.Generic;
using UnityEngine;

namespace ScoutMod.Modules
{
    public static class Buffs
    {
        internal static BuffDef miniCritBuff;
        internal static BuffDef madMilkDebuff;

        internal static void RegisterBuffs()
        {
            miniCritBuff = AddNewBuff("Scout Mini-Crit Buff", RoR2.LegacyResourcesAPI.Load<Sprite>("Textures/BuffIcons/texBuffGenericShield"), ScoutPlugin.scoutColor, false, false);
            madMilkDebuff = AddNewBuff("Milked", Modules.Assets.mainAssetBundle.LoadAsset<Sprite>("texMadMilkDebuff"), ScoutPlugin.scoutColor, false, false);
            
        }

        // simple helper method
        internal static BuffDef AddNewBuff(string buffName, Sprite buffIcon, Color buffColor, bool canStack, bool isDebuff)
        {
            BuffDef buffDef = ScriptableObject.CreateInstance<BuffDef>();
            buffDef.name = buffName;
            buffDef.buffColor = buffColor;
            buffDef.canStack = canStack;
            buffDef.isDebuff = isDebuff;
            buffDef.eliteDef = null;
            buffDef.iconSprite = buffIcon;

            Modules.Content.AddBuffDef(buffDef);

            return buffDef;
        }
    }
}