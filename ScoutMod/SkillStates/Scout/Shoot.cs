using EntityStates;
using RoR2;
using UnityEngine;
using ScoutMod.Modules;

namespace ScoutMod.SkillStates
{
    public class Shoot : BaseSkillState
    {
        public static float damageCoefficient = Modules.StaticValues.scattergunDamageCoefficient;
        public static float procCoefficient = 0.75f;
        public static float baseDuration = 0.625f;
        public static float force = 30f;
        public static float recoil = 3f;
        public static float range = 256f;
        public static GameObject tracerEffectPrefab = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/Tracers/TracerGoldGat");

        private float duration;
        private float fireTime;
        private bool hasFired;
        private string muzzleString;

        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = Shoot.baseDuration / this.attackSpeedStat;
            this.fireTime = 0.2f * this.duration;
            base.characterBody.SetAimTimer(2f);
            this.muzzleString = "Muzzle";

            base.PlayAnimation("LeftArm, Override", "ShootGun", "ShootGun.playbackRate", 1.8f);
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        private void Fire()
        {
            if (!this.hasFired)
            {
                this.hasFired = true;

                base.characterBody.AddSpreadBloom(1.5f);
                EffectManager.SimpleMuzzleFlash(EntityStates.Commando.CommandoWeapon.FirePistol2.muzzleEffectPrefab, base.gameObject, this.muzzleString, false);
                // Util.PlaySound("HenryShootPistol", base.gameObject);

                if (base.isAuthority)
                {
                    Ray aimRay = base.GetAimRay();
                    base.AddRecoil(-1f * Shoot.recoil, -2f * Shoot.recoil, -0.5f * Shoot.recoil, 0.5f * Shoot.recoil);

                    bool isCrit = base.RollCrit();
                    int spread;

					for (int i = 0; i < 10; i++)
					{
                        if (i == 1)
                            spread = 0;
                        else
                            spread = 5;

                        BulletAttack bulletAttack = new BulletAttack
                        {
                            bulletCount = 1,
                            aimVector = aimRay.direction,
                            origin = aimRay.origin,
                            damage = Shoot.damageCoefficient * this.damageStat,
                            damageColorIndex = DamageColorIndex.Default,
                            damageType = DamageType.Generic,
                            falloffModel = BulletAttack.FalloffModel.None,
                            maxDistance = Shoot.range,
                            force = Shoot.force,
                            hitMask = LayerIndex.CommonMasks.bullet,
                            minSpread = 0,
                            maxSpread = spread,
                            isCrit = isCrit,
                            owner = base.gameObject,
                            muzzleName = muzzleString,
                            smartCollision = false,
                            procChainMask = default(ProcChainMask),
                            procCoefficient = procCoefficient,
                            radius = 0.75f,
                            sniper = false,
                            stopperMask = LayerIndex.CommonMasks.bullet,
                            weapon = null,
                            tracerEffectPrefab = Shoot.tracerEffectPrefab,
                            spreadPitchScale = 1f,
                            spreadYawScale = 1f,
                            queryTriggerInteraction = QueryTriggerInteraction.UseGlobal,
                            hitEffectPrefab = EntityStates.Commando.CommandoWeapon.FirePistol2.hitEffectPrefab,
                        };

                        bulletAttack.modifyOutgoingDamageCallback = (BulletAttack attack, ref BulletAttack.BulletHit hitInfo, DamageInfo damageInfo) =>
                        {
                            int maxFalloffDist = 30;

                            float distance = Mathf.Clamp(hitInfo.distance, 0, maxFalloffDist) / maxFalloffDist;
                            float modifier;

                            if (!base.HasBuff(Modules.Buffs.miniCritBuff))
							{
                                if (distance < 0.5)
                                {
                                    modifier = GetBezierValueAtTime(distance, new Vector2(0, 1.75f), new Vector2(maxFalloffDist / 2, 1.75f), new Vector2(maxFalloffDist / 2, 0.25f), new Vector2(maxFalloffDist, 0.25f));
                                }
                                else if (distance > 0.5)
                                {
                                    modifier = GetBezierValueAtTime(distance, new Vector2(0, 1.5f), new Vector2(maxFalloffDist / 2, 1.5f), new Vector2(maxFalloffDist / 2, 0.5f), new Vector2(maxFalloffDist, 0.5f));
                                }
                                else
                                {
                                    modifier = 1;
                                }
                            }
							else
							{
                                modifier = 1.75f;
							}

                            modifier = Mathf.Clamp(modifier, 0.5f, 1.75f);

                            attack.damage *= modifier;
                            damageInfo.damage *= modifier;
                        };

                        bulletAttack.Fire();
                    }

				}
            }
        }

        float GetBezierValueAtTime(float t, Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            float uuu = uu * u;
            float ttt = tt * t;

            Vector2 p = uuu * p0;
            p += 3 * uu * t * p1;
            p += 3 * u * tt * p2;
            p += ttt * p3;

            return p.y;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (base.fixedAge >= this.fireTime)
            {
                this.Fire();
            }

            if (base.fixedAge >= this.duration && base.isAuthority)
            {
                this.outer.SetNextStateToMain();
                return;
            }
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }
    }
}