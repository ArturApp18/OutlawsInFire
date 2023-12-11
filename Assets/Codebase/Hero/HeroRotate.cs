using Codebase.Infrastructure.Services.Inputs;
using UnityEngine;

namespace Codebase.Hero
{
	public class HeroRotate : MonoBehaviour
	{
		private bool _isFacingRight = true;
		
		private IInputService _inputService;

		public void Construct(IInputService inputService) =>
			_inputService = inputService;

		private void Update()
		{
			if (_inputService.IsLeftAttackButtonDown() && _isFacingRight)
			{
				transform.Rotate(0,180,0);
				_isFacingRight = !_isFacingRight;
			}
			else if (_inputService.IsRightAttackButtonDown() && !_isFacingRight)
			{
				transform.Rotate(0, 180,0 );
				_isFacingRight = !_isFacingRight;
			}
		}
	}
}