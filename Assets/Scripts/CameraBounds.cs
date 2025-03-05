using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace DefaultNamespace
{
    public class CameraBounds : MonoBehaviour
    {
        [SerializeField] private Camera cam;
        [SerializeField] private BoxCollider2D boxBound;
        [SerializeField] private ScrollCheck scrollCheck;
        [SerializeField] private float scrollSpeed = 0.01f;
        
        private Vector3 _targetPosition;
        
        
        void Start()
        {
            if (cam == null) cam = GetComponent<Camera>();

            if (cam == null || boxBound == null)
            {
                enabled = false; //Выключение скрипта
            }
        }

        private void LateUpdate()
        {
            if (scrollCheck.IsScroll)
            {
                _targetPosition = cam.ScreenToWorldPoint(Input.mousePosition);
                _targetPosition.z = transform.position.z; //Сохраняем позицию z чтобы камера была первая, среди всех объектов.
                _targetPosition.y = 0; //Убираем движение камеры по y

                transform.position = Vector3.Lerp(transform.position, ClampToBounds(_targetPosition), scrollSpeed); //Добавление интерполяции к движению камеры за мышкой
            }
        }

        Vector3 ClampToBounds(Vector3 position)
        {
            // Область камеры
            float camHeight = cam.orthographicSize;
            float camWidth = camHeight * cam.aspect;

            // Границы триггера
            Vector3 boundsMin = boxBound.bounds.min + new Vector3(camWidth, camHeight, 0); 
            Vector3 boundsMax = boxBound.bounds.max - new Vector3(camWidth, camHeight, 0);

            // Ограничиваем камеры по передвижению
            position.x = Mathf.Clamp(position.x, boundsMin.x, boundsMax.x);
            position.y = Mathf.Clamp(position.y, boundsMin.y, boundsMax.y);

            return position;
        }
        
        
    }
}