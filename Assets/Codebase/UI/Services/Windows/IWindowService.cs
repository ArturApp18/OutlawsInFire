using Codebase.Infrastructure.Services;

namespace Codebase.UI.Services.Windows
{
  public interface IWindowService : IService
  {
    void Open(WindowId windowId);
  }
}