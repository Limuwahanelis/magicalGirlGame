using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectSelectableOnEnable : MonoBehaviour
{
    [SerializeField] Selectable _firstSelect;
    private SelectSelectableOnEnable _previousSel=null;
    //private static List<SelectSelectableOnEnable> _prevSelectables = new List<SelectSelectableOnEnable>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(_firstSelect.gameObject);
    }
    public void SetSelectable(Selectable _selectable)
    {
        _firstSelect = _selectable;
    }
    public void SelectSelectable()
    {
        EventSystem.current.SetSelectedGameObject(_firstSelect.gameObject);
    }
    private void ReturnToPrevious()
    {
        if(_previousSel!=null)
        {
            EventSystem.current.SetSelectedGameObject(_previousSel.gameObject);
            _previousSel = null;
        }
    }
}
