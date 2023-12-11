using System;

namespace Codebase.Infrastructure.Services.Inputs
{
  public interface IInputService : IService, IDisposable
  {
    bool IsRightAttackButtonDown();
    bool IsLeftAttackButtonDown();
    bool IsJumpButton();
    bool IsJumpButtonUp();

    event Action AttackButtonPressed;
  }
}