using UnityEngine;

namespace DefaultNamespace.Interface
{
    public interface IPutChip
    {
        GameObject PutChip(Vector3 pos, int layer, GameObject chipPrefab);
    }
}