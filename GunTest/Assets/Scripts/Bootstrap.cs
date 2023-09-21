using Core;
using Core.UI;
using Game;
using Game.Camera;
using Game.Data;
using Game.Input;
using Game.Player;
using UnityEngine;
using LevelComponent = Game.Level.LevelComponent;

public class Bootstrap : MonoBehaviour
{
      [SerializeField] private Storage _storage;
      
      private void Start()
      {
            var container = new DiContainer();
            container.AddComponent(_storage);
            
            container.AddComponent(new BehaviourComponent(this));
            
            container.AddComponent(new UiComponent());
            container.AddComponent(new CameraComponent());

            container.AddComponent(new InputComponent());
            container.AddComponent(new GameCameraComponent());
            container.AddComponent(new LevelComponent());
            container.AddComponent(new PlayerComponent());
            
            container.AddComponent(new InitComponent());
      }
}