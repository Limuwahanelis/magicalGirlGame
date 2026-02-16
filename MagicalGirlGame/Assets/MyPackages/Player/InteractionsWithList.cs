using System.Collections.Generic;
using UnityEngine;

public class InteractionsWithList : MonoBehaviour
{
    public List<IInteractableWithList> Interactables => _interactables;
    private List<IInteractableWithList> _interactables= new List<IInteractableWithList>();
    private IInteractableWithList _selctedInteractable;
    public void Interact()
    {
        if (_selctedInteractable != null)
        {
            _selctedInteractable.Interact();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Interact();
        }
        if(Input.GetKeyDown(KeyCode.W))
        {
            ChangeInteractable(1);
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            ChangeInteractable(-1);
        }
    }
    public void ChangeInteractable(int indexIncrease)
    {
        if (_interactables.Count <= 1) return;
        int newIndex = 0;
        int currIndex = _interactables.IndexOf(_selctedInteractable);
        
        if (currIndex + indexIncrease >= _interactables.Count)
        {
            newIndex = 0;
        }
        else if(currIndex + indexIncrease<0)
        {
            newIndex = _interactables.Count - 1;
            if (newIndex == currIndex) return;
        }
        else
        {
            newIndex = currIndex + indexIncrease;
        }
        if (newIndex == currIndex) return;
        _selctedInteractable.DeselectAsInteractable();
        _selctedInteractable = _interactables[newIndex];
        _selctedInteractable.SetAsInteractable();
    }
    public void AddItemToInteract(IInteractableWithList interactable)
    {
        _interactables.Add(interactable);
        if(_selctedInteractable==null)
        {
            interactable.SetAsInteractable();
            _selctedInteractable = interactable;
        }
        
    }
    public void RemoveInteractable(IInteractableWithList interactable)
    {
        if(_interactables.Contains(interactable))
        {
            _interactables.Remove(interactable);
            if(_selctedInteractable==interactable)
            {
                interactable.DeselectAsInteractable();
                IInteractableWithList newInteract = GetNextInteractable();
                if (newInteract == null) return;
                else
                {
                    newInteract.SetAsInteractable();
                    _selctedInteractable = newInteract;
                }
            }
        }
    }
    public IInteractableWithList GetNextInteractable()
    {
        if(_interactables.Count>0)
        {
            return _interactables[0];
        }
        else return null;
    }
}
