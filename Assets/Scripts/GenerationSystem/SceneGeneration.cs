using System.Collections.Generic;
using DefaultNamespace.GenerationSystem;
using DefaultNamespace.Interface;
using UnityEngine;
using Random = UnityEngine.Random;

public class SceneGeneration : MonoBehaviour
{
    [Header("Components")] 
    [SerializeField] private SpawnObjSystem spawnObjSystem;
    
    [Header("BoardSettings")]
    [SerializeField] private List<Vector2Int> sizeList;
    [SerializeField] private Vector2 prefabSize;
    [SerializeField] private int maxRowsCount;
    [SerializeField] private int offsetRows;
    
    [Header("Debug")] 
    [SerializeField] private bool debug;

    private IPutChip _putChip;
    
    private List<GameObject> _chipList = new();
    
    private bool _isWin;
    public int CurrentChipCount { get; private set; }

    private Vector2Int _currentSize;
    private int _rows;

    private void Awake()
    {
        _putChip = new PutChipOnBoard();
        GenerateLevel();
    }

    private void Update()
    {
        if (CurrentChipCount <= 0 && !_isWin)
        {
            _isWin = true;
            GenerateLevel();
        }
    }

    public void GenerateLevel()
    {
        Clear();
        
        _currentSize = sizeList[Random.Range(0, sizeList.Count)];
        _rows = Random.Range(1, maxRowsCount);
        
        CurrentChipCount = spawnObjSystem.SpawnPrefab(_currentSize, offsetRows, _rows);
        
        _isWin = false;

        Spawn();
    }

    private void Spawn()
    {
        for (int layer = 0; layer < _rows; layer++)
        {
            int layerOffset = layer * offsetRows;
            int layerWidth = _currentSize.x - layerOffset;
            int layerHeight = _currentSize.y - layerOffset;

            if (layerHeight <= 0 || layerWidth <= 0) continue;

            Vector2 layerSize = new Vector2(layerWidth * prefabSize.x, layerHeight * prefabSize.y);
            Vector2 startPosition = (Vector2)transform.position - layerSize / 2;

            for (int x = 0; x < layerWidth; x++)
            {
                for (int y = 0; y < layerHeight; y++)
                {
                    Vector3 spawnPos = startPosition + new Vector2(
                        x * prefabSize.x + prefabSize.x / 2,
                        y * prefabSize.y + prefabSize.y / 2
                    );

                    PutChip(spawnPos, layer);
                }
            }
        }
    }

    private void PutChip(Vector3 pos, int layer)
    {
        if (_putChip == null) _putChip = new PutChipOnBoard();
        
        if (spawnObjSystem.GetFirstItem(out var chipPrefab))
        {
#if UNITY_EDITOR
            if (layer == 0 && debug)
                chipPrefab.SetActive(false);
#endif
            
            var prefab =  _putChip.PutChip(pos, layer, chipPrefab);
            
            _chipList.Add(prefab);
        }
    }
    
    public void DeleteChips(int countCollected, List<ChipData> chips)
    {
        if (CurrentChipCount <= 0) return;

        CurrentChipCount -= countCollected;

        foreach (var chip in chips)
        {
            _chipList.Remove(chip.gameObject);
        }
    }

    public void Clear()
    {
        foreach (var item in _chipList)
        {
            if (item != null)
            {
                Destroy(item);
            }
        }

        _chipList.Clear();
    }


#if UNITY_EDITOR

    public void AddVectorsOnList()
    {
        sizeList.Clear();

        for (int x = 2; x < 20; x++)
        {
            if (x % 2 != 0) continue;

            for (int y = 1; y < 6; y++)
            {
                sizeList.Add(new Vector2Int(x, y));
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (debug && _currentSize != Vector2.zero)
        {
            Gizmos.DrawWireCube(transform.position, new Vector3(_currentSize.x, _currentSize.y));
        }
    }
    
    public void ClearTest()
    {
        foreach (var item in _chipList)
        {
            if (item != null)
            {
                DestroyImmediate(item);
            }
        }

        _chipList.Clear();
    }

#endif
}