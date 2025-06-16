using DefaultNamespace.Interface;
using UnityEngine;

namespace DefaultNamespace.GenerationSystem
{
    public class PutChipOnBoard : IPutChip
    {
        public GameObject PutChip(Vector3 pos, int layer, GameObject chipPrefab)
        {
            Vector3 spawnPos = new Vector3(pos.x, pos.y, -layer * 2);

            if (chipPrefab == null) return null;

            if (chipPrefab.TryGetComponent(out ChipData chipData))
            {
                chipData.Initialize(spawnPos, layer * 2);
            }
            
            chipPrefab.transform.position = spawnPos;
            
            return chipPrefab;
        }
    }
}