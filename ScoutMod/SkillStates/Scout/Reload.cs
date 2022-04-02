using EntityStates;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ScoutMod.SkillStates.Scout
{
	internal class Reload : BaseSkillState
	{
		public static string soundString = "";
		private Animator animator;

		static float reloadTime = 0.5f;
		static float reloadTimeFirst = 0.7f;

		float currentReloadTime;
		bool unbrokenReload;

		public override void OnEnter()
		{
			base.OnEnter();

			this.animator = base.GetModelAnimator();

			currentReloadTime = 0;
			unbrokenReload = false;
		}

		public override void FixedUpdate()
		{
			base.FixedUpdate();

			GenericSkill skillSlot = skillLocator.GetSkill(SkillSlot.Primary);

			if (skillSlot.stock == skillSlot.skillDef.GetMaxStock(activatorSkillSlot) && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}			

			float targetReloadTime;

			if (unbrokenReload)
				targetReloadTime = reloadTime / base.attackSpeedStat;
			else
				targetReloadTime = reloadTimeFirst / (base.attackSpeedStat / 4);


			currentReloadTime += Time.deltaTime;

			if (currentReloadTime >= targetReloadTime)
			{
				skillSlot.AddOneStock();
				unbrokenReload = true;
				currentReloadTime = 0;
			}
		}

		public override void OnExit()
		{
			base.OnExit();
			currentReloadTime = 0;
			unbrokenReload = false;
		}

		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}
	}
}
