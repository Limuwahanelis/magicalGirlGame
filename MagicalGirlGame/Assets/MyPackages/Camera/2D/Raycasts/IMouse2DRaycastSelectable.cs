using UnityEngine;
using UnityEngine.Events;

public interface IMouse2DRaycastSelectable
{
    public UnityEvent OnitemSelected { get; }
}