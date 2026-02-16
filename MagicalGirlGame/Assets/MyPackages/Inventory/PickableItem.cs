using MyBox;
using UnityEngine;

public class PickableItem : MonoBehaviour,IPickable
{
    public bool IsStackable => _stackable;

    public PickableItemSO ItemSO { get => _itemSO; }
    public int MaxNumberInStack { get => _maxNumberInStack; }

    [SerializeField] bool _stackable;
    [SerializeField] PickableItemSO _itemSO;
    [SerializeField, ConditionalField("_stackable")] int _maxNumberInStack;
    public void Interact(GameObject gameObject)
    {
        PickUp(gameObject.GetComponent<Inventory>());
    }

    public void PickUp(Inventory inventory)
    {
        inventory.AddItemToInventory(this);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
