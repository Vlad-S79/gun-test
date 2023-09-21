using Core;
using Core.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Ui
{
    public class InfoWindow : Window
    {
        [SerializeField] private Button _button;
        
        protected override void OnInit(DiContainer container)
        {
            _button.onClick.AddListener(Close);
        }

        protected override void OnOpen() { }
        protected override void OnClose() { }
    }
}