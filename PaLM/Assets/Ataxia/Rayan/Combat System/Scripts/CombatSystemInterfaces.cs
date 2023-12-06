using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat_System
{
	public interface IBasicCombatAction
	{
		public void PerformBasicCombatAction(BaseCharacter target, Action OnActionComplete);

	}


}