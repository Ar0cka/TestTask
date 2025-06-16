using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class ComboUI : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [FormerlySerializedAs("onDestroyObj")] [SerializeField] private Transform onDestroyParent;

        private List<GameObject> _spawnedItems = new List<GameObject>();

        public void CreateNewItem(ChipData chip)
        {
            chip.gameObject.SetActive(false);

            var obj = Instantiate(prefab, transform);
            Image image = obj.GetComponent<Image>();

            image.sprite = chip.GetSprite();

            _spawnedItems.Add(obj);
        }

        public void ClearUI()
        {
            foreach (var item in _spawnedItems)
            {
                Destroy(item.gameObject);
            }
            
            _spawnedItems.Clear();
        }
    }
}