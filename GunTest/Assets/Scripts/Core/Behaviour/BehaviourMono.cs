using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class BehaviourMono : MonoBehaviour
    {
        
        public List<IUpdateBehaviour> updateBehaviours;
        public List<IFixedUpdateBehaviour> fixedUpdateBehaviours;

        public BehaviourMono()
        {
            updateBehaviours = new List<IUpdateBehaviour>();
            fixedUpdateBehaviours = new List<IFixedUpdateBehaviour>();
        }

        private void Update()
        {
            foreach (var updateBehaviour in updateBehaviours)
            {
                updateBehaviour.Update();
            }
        }

        private void FixedUpdate()
        {
            foreach (var fixedUpdateBehaviour in fixedUpdateBehaviours)
            {
                fixedUpdateBehaviour.FixedUpdate();
            }
        }
    }
}