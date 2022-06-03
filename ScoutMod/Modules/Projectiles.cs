using R2API;
using RoR2;
using RoR2.Projectile;
using UnityEngine;
using UnityEngine.Networking;

namespace ScoutMod.Modules
{
    internal static class Projectiles
    {
        internal static GameObject ballPrefab;
        internal static GameObject milkPrefab;

        internal static void RegisterProjectiles()
        {
            CreateBall();
            CreateMilk();

            AddProjectile(ballPrefab);
            AddProjectile(milkPrefab);
        }

        internal static void AddProjectile(GameObject projectileToAdd)
        {
            Modules.Content.AddProjectilePrefab(projectileToAdd);
        }

        private static void CreateBall()
        {
            ballPrefab = CloneProjectilePrefab("CommandoGrenadeProjectile", "SandmanProjectile");

            ProjectileImpactExplosion ballImpactEffect = ballPrefab.GetComponent<ProjectileImpactExplosion>();
            InitializeImpactExplosion(ballImpactEffect);

            ballImpactEffect.blastRadius = 16f;
            ballImpactEffect.destroyOnEnemy = true;
            ballImpactEffect.lifetime = 12f;
            // ballImpactEffect.impactEffect = Modules.Assets.bombExplosionEffect;
            //bombImpactExplosion.lifetimeExpiredSound = Modules.Assets.CreateNetworkSoundEventDef("HenryBombExplosion");
            ballImpactEffect.timerAfterImpact = true;
            ballImpactEffect.lifetimeAfterImpact = 0.1f;

            ProjectileController ballController = ballPrefab.GetComponent<ProjectileController>();
            if (Modules.Assets.mainAssetBundle.LoadAsset<GameObject>("ballGhost") != null) ballController.ghostPrefab = CreateGhostPrefab("ballGhost");
            //bombController.startSound = "";
        }

        private static void CreateMilk()
        {
            milkPrefab = CloneProjectilePrefab("CommandoGrenadeProjectile", "MadMilkProjectile");

            ProjectileImpactExplosion milkImpactEffect = milkPrefab.GetComponent<ProjectileImpactExplosion>();
            InitializeImpactExplosion(milkImpactEffect);
            milkImpactEffect.blastRadius = 16f;
            milkImpactEffect.destroyOnEnemy = true;
            milkImpactEffect.lifetime = 12f;
            milkImpactEffect.timerAfterImpact = true;
            milkImpactEffect.lifetimeAfterImpact = 0.1f;

            ProjectileController milkController = milkPrefab.GetComponent<ProjectileController>();
            if (Modules.Assets.mainAssetBundle.LoadAsset<GameObject>("milkGhost") != null) milkController.ghostPrefab = CreateGhostPrefab("milkGhost");

            ProjectileInflictTimedBuff inflictDebuff = milkPrefab.AddComponent<ProjectileInflictTimedBuff>();
            inflictDebuff.buffDef = Modules.Buffs.madMilkDebuff;
            inflictDebuff.duration = 4f;
        }

        private static void InitializeImpactExplosion(ProjectileImpactExplosion projectileImpactExplosion)
        {
            projectileImpactExplosion.blastDamageCoefficient = 0.5f;
            projectileImpactExplosion.blastProcCoefficient = 0.5f;
            projectileImpactExplosion.blastRadius = 0.5f;
            projectileImpactExplosion.bonusBlastForce = Vector3.zero;
            projectileImpactExplosion.childrenCount = 0;
            projectileImpactExplosion.childrenDamageCoefficient = 0f;
            projectileImpactExplosion.childrenProjectilePrefab = null;
            projectileImpactExplosion.destroyOnEnemy = false;
            projectileImpactExplosion.destroyOnWorld = false;
            projectileImpactExplosion.falloffModel = RoR2.BlastAttack.FalloffModel.None;
            projectileImpactExplosion.fireChildren = false;
            projectileImpactExplosion.impactEffect = null;
            projectileImpactExplosion.lifetime = 0f;
            projectileImpactExplosion.lifetimeAfterImpact = 0f;
            projectileImpactExplosion.lifetimeRandomOffset = 0f;
            projectileImpactExplosion.offsetForLifetimeExpiredSound = 0f;
            projectileImpactExplosion.timerAfterImpact = false;
            projectileImpactExplosion.GetComponent<ProjectileDamage>().damageType = DamageType.Stun1s;
        }

        private static GameObject CreateGhostPrefab(string ghostName)
        {
            GameObject ghostPrefab = Modules.Assets.mainAssetBundle.LoadAsset<GameObject>(ghostName);
            if (!ghostPrefab.GetComponent<NetworkIdentity>()) ghostPrefab.AddComponent<NetworkIdentity>();
            if (!ghostPrefab.GetComponent<ProjectileGhostController>()) ghostPrefab.AddComponent<ProjectileGhostController>();

            Modules.Assets.ConvertAllRenderersToHopooShader(ghostPrefab);

            return ghostPrefab;
        }

        private static GameObject CloneProjectilePrefab(string prefabName, string newPrefabName)
        {
            GameObject newPrefab = PrefabAPI.InstantiateClone(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/Projectiles/" + prefabName), newPrefabName);
            return newPrefab;
        }
    }
}