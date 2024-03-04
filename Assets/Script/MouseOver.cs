using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


enum UIType
{
    NONE,
    LEVEL,
    SHOP
}

enum ObjectType
{
    NONE,
    TRAP,
    ENEMY
}
public class MouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private UIType ui;
    [SerializeField] private ObjectType obj;
    [SerializeField] private int trap_id;
    [SerializeField] private RectTransform rectTransform;

    [SerializeField] private Trap trap;
    private TrapData trapData;
    [SerializeField] private Enemy enemy;
    private EnemyData enemyData;

    private void Start()
    {
        switch(obj)
        {
            case ObjectType.NONE:
                break;
            case ObjectType.TRAP:
                trapData = trap.GetData();
                break;
            case ObjectType.ENEMY:
                enemyData = enemy.GetData();
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        switch(ui)
        {
            case UIType.NONE:
                break;
            case UIType.LEVEL:
                UIManager.Instance.MouserOverLevel(true);
                break;
            case UIType.SHOP:
                UIManager.Instance.MouseOverTrapSlot(true, trap_id, rectTransform);
                break;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        switch (ui)
        {
            case UIType.NONE:
                break;
            case UIType.LEVEL:
                UIManager.Instance.MouserOverLevel(false);
                break;
            case UIType.SHOP:
                UIManager.Instance.MouseOverTrapSlot(false, trap_id, rectTransform);
                break;
        }
    }


    private void OnMouseEnter()
    {
        switch(obj)
        {
            case ObjectType.NONE:
                break;
            case ObjectType.TRAP:
                if (trapData == null)
                    break;
                UIManager.Instance.UpdateTrapInfomation(trapData);
                UIManager.Instance.InformationUI(true);
                break;
            case ObjectType.ENEMY:
                UIManager.Instance.UpdateEnemyInfomation(enemyData);
                UIManager.Instance.InformationUI(true);
                break;
        }
    }

    private void OnMouseExit()
    {
        switch (obj)
        {
            case ObjectType.NONE:
                break;
            case ObjectType.TRAP:
                UIManager.Instance.InformationUI(false);
                break;
            case ObjectType.ENEMY:
                UIManager.Instance.InformationUI(false);
                break;
        }
    }
}
