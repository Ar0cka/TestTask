using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using DefaultNamespace;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class DragItem : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private BoxCollider2D itemCollider;
    [SerializeField] private ScrollCheck scrollCheck;
    
    private Vector3 _offset;
    
    private void Start()
    {
        if (rb2D == null) rb2D = GetComponent<Rigidbody2D>();
        if (itemCollider == null) itemCollider = GetComponent<BoxCollider2D>();
        if (scrollCheck == null) scrollCheck = FindObjectOfType<ScrollCheck>();
    }

    private void OnMouseDown() //Поднятие объекта
    {
        _offset = transform.position - GetMousePosition(); //Вычисление вектора смещения
        rb2D.gravityScale = 0f;
        rb2D.velocity = Vector2.zero;
        itemCollider.enabled = false;
    }

   
    
    private void OnMouseDrag() //Перемещение объекта
    {
        Vector3 targetPosition = GetMousePosition() + _offset; //Вычисляем позицию мыши(тача)
        rb2D.MovePosition(targetPosition);
        scrollCheck.TakeAndDropItem(true);//Включаем скролл сцены вместе с взятием
    }

    private void OnMouseUp() //Отпускание объект
    {
        scrollCheck.TakeAndDropItem(false); //Выключаем скрол
        itemCollider.enabled = true;
        rb2D.gravityScale = 1f;
    }

    private Vector3 GetMousePosition() //Взятие позиции мышки(тача) с камеры
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //Позиция мышки
        mousePosition.z = 0;
        return mousePosition;
    }
    
    private void OnDestroy() //Уничтожаем DOTween анимации
    {
        DOTween.KillAll();
    }
}
