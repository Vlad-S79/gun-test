using Core;
using Core.UI;
using Game.Camera;
using Game.Ui;

namespace Game
{
    public class InitComponent : IComponent
    {
        public void Init(DiContainer container)
        {
            var uiComponent = container.GetComponent<UiComponent>();

            var mainWindow = uiComponent.GetWindow<MainWindow>();
            mainWindow.Open();
        }
    }
}