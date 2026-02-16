using UnityEngine;

public class Interactions : MonoBehaviour
{
    private IInteractable _interactable;
    public void Interact()
    {
        if(_interactable != null)
        {
            _interactable.Interact(gameObject);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Interact();
        }
    }

    public void SetItemToInteract(IInteractable interactable)
    {
        _interactable = interactable;
    }
    public void RemoveInteractable(IInteractable interactable)
    {
        if(_interactable == interactable) _interactable=null;
    }
    public void ForceRemoveInteracable()
    {
        _interactable = null;
    }
}
