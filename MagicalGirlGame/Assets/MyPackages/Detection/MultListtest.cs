using UnityEngine;

public class MultListtest : MonoBehaviour,IInteractableWithList
{
    [SerializeField] GameObject _ob;

    public void DeselectAsInteractable()
    {
        _ob.SetActive(false);
    }

    public void Interact()
    {
        gameObject.SetActive(false);
    }

    public void SetAsInteractable()
    {
        _ob.SetActive(true);
    }
}
