using UnityEngine;

public interface IPickable:IInteractable
{
    void PickUp(Inventory inventory);
}