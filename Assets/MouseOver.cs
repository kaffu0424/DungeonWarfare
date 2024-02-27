using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


enum UIType
{
    NONE,
    LEVEL,
    TRAP
}
public class MouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private UIType ui;
    public void OnPointerEnter(PointerEventData eventData)
    {
        switch(ui)
        {
            case UIType.NONE:
                break;
            case UIType.LEVEL:
                UIManager.Instance.MouserOverLevel(true);
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
        }
    }
}
