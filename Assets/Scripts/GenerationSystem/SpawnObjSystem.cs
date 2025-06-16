using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.GenerationSystem
{
    public class SpawnObjSystem : MonoBehaviour
    {
        [Header("Prefabs reference")]
        [SerializeField] private List<GameObject> prefab;
        [SerializeField] private Transform parent;
        
        private List<GameObject> _spawnedPrefabs = new List<GameObject>();

        public int SpawnPrefab(Vector2Int currentSize, int offsetRows, int rows)
        {
            _spawnedPrefabs.Clear();
            
            if (prefab.Count == 0)
                return 0;

            int chipCount = 0;

            int chipIndex = 0;

            for (int layer = 0; layer < rows; layer++)
            {
                int layerOffset = layer * offsetRows;
                int layerWidth = currentSize.x - layerOffset;
                int layerHeight = currentSize.y - layerOffset;

                if (layerWidth <= 0 || layerHeight <= 0)
                    continue;

                int chipCountInLayer = layerWidth * layerHeight;
                chipCount += chipCountInLayer;

                List<GameObject> clonePrefab = new List<GameObject>(prefab);
                List<GameObject> spawnedThisLayer = new List<GameObject>();
                
                while (chipCountInLayer > 0 && clonePrefab.Count > 0)
                {
                    if (chipIndex >= clonePrefab.Count) chipIndex = 0;

                    var chipPrefab = clonePrefab[chipIndex];
                    var chipData = chipPrefab.GetComponent<ChipData>();

                    if (chipData == null)
                    {
                        Debug.LogWarning("ChipData is missing on prefab");
                        clonePrefab.RemoveAt(chipIndex);
                        continue;
                    }

                    if (chipData.MaxCombo > chipCountInLayer)
                    {
                        clonePrefab.RemoveAt(chipIndex);
                        continue;
                    }

                    if ((chipCountInLayer - chipData.MaxCombo) % 2 != 0)
                    {
                        chipIndex++;
                        if (chipIndex >= clonePrefab.Count) chipIndex = 0;
                        continue;
                    }

                    for (int i = 0; i < chipData.MaxCombo; i++)
                    {
                        var obj = Instantiate(chipPrefab, parent);
                        spawnedThisLayer.Add(obj);
                    }

                    chipCountInLayer -= chipData.MaxCombo;

                    chipIndex++;
                }

                Shuffle(ref spawnedThisLayer);

                WriteAllOnList(spawnedThisLayer);
            }
            
            return chipCount;
        }

        public bool GetFirstItem(out GameObject chipPrefab)
        {
            if (_spawnedPrefabs.Count == 0)
            {
                chipPrefab = null;
                return false;
            }
                
            
            chipPrefab = _spawnedPrefabs[0];
            _spawnedPrefabs.RemoveAt(0);
            return true;
        }
        
        private void WriteAllOnList(List<GameObject> list)
        {
            foreach (var item in list)
            {
                _spawnedPrefabs.Add(item);
            }
        }

        private void Shuffle(ref List<GameObject> list)
        {
            if (list.Count <= 0) return;

            for (int j = 0; j < list.Count; j++)
            {
                int rand = Random.Range(0, list.Count);
                (list[j], list[rand]) = (list[rand], list[j]);
            }
        }
    }
}