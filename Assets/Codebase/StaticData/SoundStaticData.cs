using System.Collections.Generic;
using UnityEngine;

namespace Codebase.StaticData
{
	[CreateAssetMenu(fileName = "SoundData", menuName = "StaticData/Sound")]
	public class SoundStaticData : ScriptableObject
	{
		[Header("BackgroundMusic")]
		public List<AudioClip> MainThemes;

		[Header("GameSFX")]
		[Header("Hero")]
		public List<AudioClip> SwordAttacksSFX;
		public List<AudioClip> MissSwordAttacksSFX;
		public List<AudioClip> HurtSFX;
		public AudioClip HeroDeathSFX;
		[Header("Enemy")]

		public AudioClip EnemyDeathSFX;
	}
}