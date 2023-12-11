using System;
using UnityEngine;

namespace Codebase.Infrastructure.Services.Inputs
{
	public class InputService : IInputService
	{
		private const string LeftAttack = "Fire1";
		private const string RightAttack = "Fire2";
		private const string Jump = "Jump";

		public event Action AttackButtonPressed;

		public bool IsRightAttackButtonDown() =>
			Input.GetButtonDown(RightAttack);

		public bool IsLeftAttackButtonDown() =>
			Input.GetButtonDown(LeftAttack);

		public bool IsJumpButton() =>
			Input.GetButton(Jump);

		public bool IsJumpButtonUp() =>
			Input.GetButtonUp(Jump);

		public void Dispose()
		{
			
		}
	}
}