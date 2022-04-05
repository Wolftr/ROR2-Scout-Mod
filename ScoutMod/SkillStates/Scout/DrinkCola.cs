using EntityStates;
using R2API.Networking;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace ScoutMod.SkillStates.Scout
{
	internal class DrinkCola : BaseSkillState
	{
		public static int duration = 8;

		private Animator animator;

		public override void OnEnter()
		{
			base.OnEnter();

			this.animator = base.GetModelAnimator();
		}

		public override void FixedUpdate()
		{
			base.FixedUpdate();

			if (base.isAuthority && base.fixedAge >= 0.5f)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		public override void OnExit()
		{
			base.OnExit();
			base.characterBody.ApplyBuff(Modules.Buffs.miniCritBuff.buffIndex, 1, DrinkCola.duration);
		}
	}
}
