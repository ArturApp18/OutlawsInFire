using System;
using CodeBase.Infrastructure.Services.Audio;
using Codebase.Infrastructure.Services.Randomizer;

namespace Codebase.Logic
{
  public interface IHealth
  {
    event Action HealthChanged;
    float Current { get; set; }
    float Max { get; set; }
    void TakeDamage(float damage);
    void Construct(IAudioService audioService);
  }

}