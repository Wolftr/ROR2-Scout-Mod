using EntityStates;
using System;
using System.Collections.Generic;
using System.Text;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace ScoutMod.SkillStates.Scout
{
	public class BatterUp : BaseSkillState
    {
        public static float damageCoefficient = Modules.StaticValues.sandmanDamageCoefficient;
        public static float procCoefficient = 1.5f;
        public static float baseDuration = 0.25f;
        public static float throwForce = 150f;

        private float fireTime;
        private bool hasFired;

        public override void OnEnter()
        {
            base.OnEnter();
            this.fireTime = 0.2f;

            base.characterBody.SetAimTimer(2f);
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

                if (base.isAuthority)
                {
                    Ray aimRay = base.GetAimRay();

                    ProjectileManager.instance.FireProjectile(Modules.Projectiles.ballPrefab,
                        aimRay.origin,
                        Util.QuaternionSafeLookRotation(aimRay.direction),
                        base.gameObject,
                        BatterUp.damageCoefficient * this.damageStat,
                        0f,
                        base.RollCrit(),
                        DamageColorIndex.Default,
                        null,
                        BatterUp.throwForce);
                }
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (base.fixedAge >= this.fireTime && !hasFired)
            {
                this.Fire();
            }

            if (base.fixedAge >= baseDuration && base.isAuthority)
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
