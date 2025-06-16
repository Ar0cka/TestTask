using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ChipData : MonoBehaviour
{
    [SerializeField] private SpriteRenderer rendererMain;
    [SerializeField] private SpriteRenderer rendererSub;

    [SerializeField] private Transform imageTransform;
    
    [field:SerializeField] public int MaxCombo { get; private set;}

    [field:SerializeField] public string ItemName { get; private set; }

    private Vector2 _savedPosition;
    
    private ChipData _chipChild;
    public bool ChipClosed { get; private set;}

    public void Initialize(Vector2 spawnPoint, int order)
    {
        _savedPosition = spawnPoint;
        InitializeSprite(order);
    }
    
    private void InitializeSprite(int order)
    {
        imageTransform.position = new Vector3(imageTransform.position.x, imageTransform.position.y, imageTransform.position.z + -order);
        
        rendererMain.sortingOrder = order;
        rendererSub.sortingOrder = order + 1;
    }

    public void ReturnItemOnBoard()
    {
        gameObject.SetActive(true);
        gameObject.transform.position = _savedPosition;
    }
    
    public Sprite GetSprite()
    {
        return rendererSub.sprite;
    }
    
}
