using System;
using UnityEngine;

namespace Codebase.Logic
{
	public class TalentClickObserver : MonoBehaviour
	{
		public event Action MouseDown;

		public void OnPointerDown() =>
			MouseDown?.Invoke();
	}
}