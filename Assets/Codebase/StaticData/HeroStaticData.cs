using UnityEngine;

namespace Codebase.StaticData
{
	[CreateAssetMenu(fileName = "HeroData", menuName = "StaticData/Hero")]
	public class HeroStaticData : ScriptableObject
	{
		[Header("HP")]
		[Range(1, 50)]
		public int Hp;

		[Header("Attack")]
		[Range(1, 10)]
		public float Damage;
		[Range(0.5f, 2)]
		public float Cleavage;	
		
		public GameObject Prefab;
	}

}