using UnityEngine;
using UnityEngine.Events;

public class Mouse2DRaycastSelectable : MonoBehaviour
{
    public UnityEvent<Mouse2DRaycastSelectable> OnItemSelected;
    public UnityEvent<Mouse2DRaycastSelectable> OnItemDeselected;

    public void SelectItem()
    {
        OnItemSelected?.Invoke(this);
    }
    public void DeselectItem()
    {
        OnItemDeselected?.Invoke(this);
    }
}
