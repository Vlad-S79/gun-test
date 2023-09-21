using Core;
using Core.UI;
using Game.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Ui
{
    public class MainWindow : Window
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private Button _infoButton;
        [SerializeField] private TextMeshProUGUI _amountText;

        private PlayerComponent _playerComponent;

        protected override void OnInit(DiContainer container)
        {
            _playerComponent = container.GetComponent<PlayerComponent>();
            var playerReference = container.GetComponent<PlayerReference>();
            
            _slider.onValueChanged.AddListener(OnChangeSlideValue);
            var defValue = (float)playerReference.defaultPower / 100;
            OnChangeSlideValue(defValue);
            _slider.value = defValue;
            
            _infoButton.onClick.AddListener(OnClickInfoButton);
        }

        private void OnClickInfoButton()
        {
            var window = UiComponent.GetWindow<InfoWindow>();
            if (window.IsOpen()) return;
            window.Open();
        }

        private void OnChangeSlideValue(float value)
        {
            var textValue = Mathf.RoundToInt(value * 100);
            _amountText.text = textValue.ToString();
            _playerComponent.SetPlayerPower(value);
        }

        protected override void OnOpen() { }
        protected override void OnClose() { }
    }
}