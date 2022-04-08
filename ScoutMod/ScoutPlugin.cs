using BepInEx;
using ScoutMod.Modules.Survivors;
using R2API.Utils;
using RoR2;
using System.Collections.Generic;
using System.Security;
using System.Security.Permissions;
using UnityEngine;
using System;

[module: UnverifiableCode]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]

namespace ScoutMod
{
    [BepInDependency("com.bepis.r2api", BepInDependency.DependencyFlags.HardDependency)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]
    [BepInPlugin(MODUID, MODNAME, MODVERSION)]
    [R2APISubmoduleDependency(new string[]
    {
        "PrefabAPI",
        "LanguageAPI",
        "SoundAPI",
        "LoadoutAPI",
    })]

    public class ScoutPlugin : BaseUnityPlugin
    {
        public const string MODUID = "com.vulf.ScoutMod";
        public const string MODNAME = "ScoutMod";
        public const string MODVERSION = "0.1.0";

        public const string DEVELOPER_PREFIX = "VULF";

        public static ScoutPlugin instance;

        public static Color scoutColor = new Color((float)184 / 255, (float)56 / 255, (float)59 / 255);

        private void Awake()
        {
            instance = this;

            // load assets and read config
            Modules.Assets.Initialize();
            Modules.Config.ReadConfig();
            Modules.States.RegisterStates(); // register states for networking
            Modules.Buffs.RegisterBuffs(); // add and register custom buffs/debuffs
            Modules.Projectiles.RegisterProjectiles(); // add and register custom projectiles
            Modules.Tokens.AddTokens(); // register name tokens
            Modules.ItemDisplays.PopulateDisplays(); // collect item display prefabs for use in our display rules

            // survivor initialization
            new Scout().Initialize();

            // now make a content pack and add it- this part will change with the next update
            new Modules.ContentPacks().Initialize();

            Hook();
        }
        
        private void Hook()
        {
            // run hooks here, disabling one is as simple as commenting out the line
            On.RoR2.CharacterBody.RecalculateStats += CharacterBody_RecalculateStats;
            On.RoR2.TeleporterInteraction.Start += Teleporter_StartEvent;
        }

		private void CharacterBody_RecalculateStats(On.RoR2.CharacterBody.orig_RecalculateStats orig, CharacterBody self)
        {
            orig(self);

            if (self)
            {
                if (self.HasBuff(Modules.Buffs.miniCritBuff))
                {
                    self.damage *= 1.35f;
                }
            }
        }

		private void Teleporter_StartEvent(On.RoR2.TeleporterInteraction.orig_Start orig, TeleporterInteraction self)
		{
            orig(self);

            if (self.holdoutZoneController.holdoutZoneShape == HoldoutZoneController.HoldoutZoneShape.Count)
                return;

            // Bind the function for adding the charge rate
            self.holdoutZoneController.calcChargeRate += RecalculateChargeRate;
		}

        public void RecalculateChargeRate(ref float rate)
		{
            bool hasScoutTeamMember = false;

            IReadOnlyCollection<TeamComponent> teamComponents = TeamComponent.GetTeamMembers(TeamIndex.Player);

			foreach (TeamComponent component in teamComponents)
                if (component.body.bodyIndex == BodyCatalog.FindBodyIndex("ScoutBody"))
                    hasScoutTeamMember = true;

            if (hasScoutTeamMember)
                rate *= 2;
		}
    }
}