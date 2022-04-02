using R2API;
using System;

namespace ScoutMod.Modules
{
    internal static class Tokens
    {
        internal static void AddTokens()
        {
            #region Scout
            string prefix = ScoutPlugin.DEVELOPER_PREFIX + "_SCOUT_BODY_";

            string desc = "Scout is a high mobility survivor that faces foes head on.<color=#CCD3E0>" + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > The Scattergun is a high damage shotgun most effective at close range." + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > The Sandman fires a fast arcing projectile that stuns foes allowing Scout to close the distance safely." + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > Roll is a quick way to stay out of danger by providing invincibility." + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > The Crit-a-Cola grants Scout Mini-Crits on command that provide a 1.35 damage multiplier and removes damage falloff from his weapons." + Environment.NewLine + Environment.NewLine;

            string outro = "..and so he left, ready for his next fight.";
            string outroFailure = "..and so he vanished, a virgin.";

            LanguageAPI.Add(prefix + "NAME", "Scout");
            LanguageAPI.Add(prefix + "DESCRIPTION", desc);
            LanguageAPI.Add(prefix + "SUBTITLE", "The Force-a-Nature");
            LanguageAPI.Add(prefix + "LORE", "sample lore");
            LanguageAPI.Add(prefix + "OUTRO_FLAVOR", outro);
            LanguageAPI.Add(prefix + "OUTRO_FAILURE", outroFailure);

            #region Skins
            LanguageAPI.Add(prefix + "DEFAULT_SKIN_NAME", "Boston Boy");
            LanguageAPI.Add(prefix + "MASTERY_SKIN_NAME", "Alternate");
            #endregion

            #region Passive
            LanguageAPI.Add(prefix + "PASSIVE_NAME", "Hustler");
            LanguageAPI.Add(prefix + "PASSIVE_DESCRIPTION", "Scout's fast nature allows him to always sprint, even while attacking, as well as the ability to double jump and capture areas twice as fast.");
            #endregion

            #region Primary
            LanguageAPI.Add(prefix + "SCATTERGUN_NAME", "Scattergun");
            LanguageAPI.Add(prefix + "SCATTERGUN_DESCRIPTION", Helpers.agilePrefix + $"Swing forward for <style=cIsDamage>{100f * StaticValues.scattergunDamageCoefficient}% damage</style>.");
            #endregion

            #region Secondary
            LanguageAPI.Add(prefix + "BAT_NAME", "The Sandman");
            LanguageAPI.Add(prefix + "BAT_DESCRIPTION", Helpers.agilePrefix + $"Launch a stunning projectile for <style=cIsDamage>{100f * StaticValues.sandmanDamageCoefficient}% damage</style>.");
            #endregion

            #region Utility
            LanguageAPI.Add(prefix + "UTILITY_ROLL_NAME", "Roll");
            LanguageAPI.Add(prefix + "UTILITY_ROLL_DESCRIPTION", "Roll a short distance, gaining <style=cIsUtility>300 armor</style>. <style=cIsUtility>You cannot be hit during the roll.</style>");
            #endregion

            #region Special
            LanguageAPI.Add(prefix + "SPECIAL_BOMB_NAME", "Bomb");
            LanguageAPI.Add(prefix + "SPECIAL_BOMB_DESCRIPTION", $"");
            #endregion

            #region Achievements
            LanguageAPI.Add(prefix + "MASTERYUNLOCKABLE_ACHIEVEMENT_NAME", "Scout: Mastery");
            LanguageAPI.Add(prefix + "MASTERYUNLOCKABLE_ACHIEVEMENT_DESC", "As Scout, beat the game or obliterate on Monsoon.");
            LanguageAPI.Add(prefix + "MASTERYUNLOCKABLE_UNLOCKABLE_NAME", "Scout: Mastery");
            #endregion
            #endregion
        }
    }
}