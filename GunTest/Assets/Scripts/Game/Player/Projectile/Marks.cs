using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Game.Player
{
    // todo: I don't understand what this means renderer texture 
    // todo: The second option was to paint on the wall texture
    
    public class Marks
    {
        private GameObject _ref;

        private Queue<GameObject> _markList;
        private int _maxMarkInScene = 50;
        private int _counter;

        public void Init(DiContainer container)
        {
            _markList = new Queue<GameObject>();
            
            var playerReference = container.GetComponent<PlayerReference>();
            _ref = playerReference.mark;
        }

        private GameObject GetMarkGameObject()
        {
            if (_counter >= _maxMarkInScene)
            {
                var markReuse = _markList.Dequeue();
                _markList.Enqueue(markReuse);
                return markReuse;
            }
            
            var mark = Object.Instantiate(_ref);
            mark.name = "mark_" + _counter;
            _counter++;
            
            _markList.Enqueue(mark);
            return mark;
        }

        public void SetMark(RaycastHit raycastHit)
        {
            var mark = GetMarkGameObject();
            mark.gameObject.SetActive(true);

            var transform = mark.transform;
            transform.position = raycastHit.point + raycastHit.normal * 0.001f;
            transform.forward = -raycastHit.normal;

            transform.localScale = Vector3.one * .2f;
        }
        
    }
}