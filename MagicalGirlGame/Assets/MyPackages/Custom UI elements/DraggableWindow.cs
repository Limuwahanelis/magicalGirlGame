using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using UnityEngine.UIElements.InputSystem;

public class DraggableWindow : MonoBehaviour
{
    public UnityAction OnCloseWindow;
    private Vector3 _offset;


    public void OnBeginDrag(BaseEventData data)
    {
        _offset = (Vector2)transform.position - ((PointerEventData)data).position;
    }
    public void OnDrag(BaseEventData data)
    {
        transform.position = (Vector3)((PointerEventData)data).position + _offset;
    }


    public void CloseWindow()
    {
        if (OnCloseWindow != null)
        {
            OnCloseWindow?.Invoke();
        }
        else Destroy(gameObject);
    }

}
