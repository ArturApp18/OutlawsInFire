using System;

namespace Codebase.Data
{
	[Serializable]
	public class Stats
	{
		public float Damage;
		public float AttackRadius;
		public float CurrentHP;
		public float MaxHP;

		public void ResetHp()
		{
			CurrentHP = MaxHP;
		}
	}
}