using System;
using System.Collections.Generic;
using System.Text;
using EntityStates;
using JetBrains.Annotations;
using RoR2;
using RoR2.Skills;
using UnityEngine;

namespace ScoutMod.Content.SkillDefs
{
	internal class ScattergunSkillDef : SkillDef
	{
		public EntityStates.SerializableEntityStateType reloadState = new EntityStates.SerializableEntityStateType(typeof(SkillStates.Scout.Reload));

		public InterruptPriority reloadInterruptPriority = InterruptPriority.Skill;

		public override SkillDef.BaseSkillInstanceData OnAssigned([NotNull] GenericSkill skillSlot)
		{
			return new ScattergunSkillDef.InstanceData();
		}

		public override void OnUnassigned([NotNull] GenericSkill skillSlot)
		{
			base.OnUnassigned(skillSlot);
		}

		public override void OnFixedUpdate([NotNull] GenericSkill skillSlot)
		{
			base.OnFixedUpdate(skillSlot);
			ScattergunSkillDef.InstanceData instanceData = (ScattergunSkillDef.InstanceData)skillSlot.skillInstanceData;
			instanceData.currentStock = skillSlot.stock;

			if (instanceData.currentStock < this.GetMaxStock(skillSlot))
			{
				if (skillSlot.stateMachine && !(skillSlot.stateMachine.state.GetType() == this.reloadState.stateType) && !skillSlot.stateMachine.HasPendingState() && skillSlot.stateMachine.CanInterruptState(this.reloadInterruptPriority))
				{
					skillSlot.stateMachine.SetNextState(EntityStateCatalog.InstantiateState(this.reloadState));
					return;
				}
			}
		}

		protected class InstanceData : SkillDef.BaseSkillInstanceData
		{
			public int currentStock;
		}
	}
}
