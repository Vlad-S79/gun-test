using System;
using UnityEngine;

namespace Game.Input
{
    public class KeyCodeAction
    {
        public KeyCodeAction(KeyCode keyCode, Action action)
        {
            KeyCode = keyCode;
            Action = action;
        }
        
        public KeyCode KeyCode;
        public Action Action;
    }
}