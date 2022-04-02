using EntityStates;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScoutMod.SkillStates.Scout
{
	public class BatterUp : BaseSkillState
	{
		public static float damageCoefficient = Modules.StaticValues.sandmanDamageCoefficient;
		public static float procCoefficient = 1.5f;
		public static float baseDuration = 0.25f;

		public float duration;
		public float fireTime;
		public bool hasFired;

		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = baseDuration / this.attackSpeedStat;
			this.fireTime = 0.2f * this.duration;
		}

		public override void FixedUpdate()
		{
			base.FixedUpdate();

		}
	}
}
