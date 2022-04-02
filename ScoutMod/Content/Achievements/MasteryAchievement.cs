using RoR2;
using System;
using UnityEngine;

namespace ScoutMod.Modules.Achievements
{
    internal class MasteryAchievement : BaseMasteryUnlockable
    {
        public override string AchievementTokenPrefix => ScoutPlugin.DEVELOPER_PREFIX + "_SCOUT_BODY_MASTERY";
        public override string AchievementSpriteName => "texMasteryAchievement";
        public override string PrerequisiteUnlockableIdentifier => ScoutPlugin.DEVELOPER_PREFIX + "_SCOUT_BODY_UNLOCKABLE_REWARD_ID";

        public override string RequiredCharacterBody => "ScoutBody";

        public override float RequiredDifficultyCoefficient => 3;
    }
}