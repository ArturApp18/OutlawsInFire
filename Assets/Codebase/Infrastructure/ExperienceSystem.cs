using System;
using UnityEngine;

namespace Codebase.Infrastructure
{
	public class ExperienceSystem : MonoBehaviour
	{
		public event Action<int> OnLevelUp;

		public int CurrentLevel { get; private set; }
		public int CurrentExperience { get; private set; }

		public int ExperienceRequiredForNextLevel => CalculateExperienceRequired(CurrentLevel + 1);

		private void Start()
		{
			CurrentLevel = 1;
			CurrentExperience = 0;
		}

		public void GainExperience(int amount)
		{
			CurrentExperience += amount;

			if(CurrentExperience >= ExperienceRequiredForNextLevel)
			{
				LevelUp();
			}
		}

		private void LevelUp()
		{
			CurrentLevel++;
			CurrentExperience -= ExperienceRequiredForNextLevel;
			OnLevelUp?.Invoke(CurrentLevel);
		}

		private int CalculateExperienceRequired(int level)
		{
			return level * 100;
		}
	}
}