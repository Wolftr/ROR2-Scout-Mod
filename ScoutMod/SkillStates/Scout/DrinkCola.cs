using EntityStates;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace ScoutMod.SkillStates.Scout
{
	internal class DrinkCola : BaseSkillState
	{
		public static float duration = 8;

		private Animator animator;

		bool buffApplied = false;

		public override void OnEnter()
		{
			base.OnEnter();

			this.animator = base.GetModelAnimator();
		}

		public override void FixedUpdate()
		{
			base.FixedUpdate();

			if (base.isAuthority && base.fixedAge >= 0.5f && !buffApplied)
			{
				if (NetworkServer.active)
				{
					base.characterBody.AddTimedBuff(Modules.Buffs.miniCritBuff, DrinkCola.duration);
					buffApplied = true;
				}

				this.outer.SetNextStateToMain();
				return;
			}
		}
	}
}
