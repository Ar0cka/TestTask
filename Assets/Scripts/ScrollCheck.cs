using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class ScrollCheck : MonoBehaviour
    {
        public bool IsScroll { get; private set; }
        
        //Проверка, нажата ли мышка на поле сцены, для того чтобы начать скол камеры
        
        private void OnMouseDrag()
        {
            IsScroll = true;
        }

        private void OnMouseUp()
        {
            IsScroll = false;
        }

        public void TakeAndDropItem(bool state)
        {
            IsScroll = state;
        }
    }
}