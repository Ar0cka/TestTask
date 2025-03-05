using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace
{
    public class ItemHitTheTrigger : MonoBehaviour
    {
        
        [SerializeField] private Vector3 scaleItems;
        [SerializeField] private Vector3 defaultScale;

        [SerializeField] private float duration = 1f;


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Item"))
            {
                RectTransform item = other.GetComponent<RectTransform>();

                item.transform.DOScale(scaleItems, duration); //Запуск анимации уменьшение объекта
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Item"))
            {
                RectTransform item = other.GetComponent<RectTransform>();

                item.transform.DOScale(defaultScale, duration); //Запуск анимации увеличение объекта
            }
        }
    }
}