using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// To use this class assging camera for raycasts andsprite for selection. Then add CameraInputHandler2D and assign there a camera to assign values in helperClass. Then add CameraInputHandler2D and assging 
// RaycastFromMouse2D there for getting correct drag start and end.
public class RaycastFromMouse2D : MonoBehaviour
{
    [SerializeField] Camera _cam;
    [SerializeField] float _OverlapCircleRadius;
    [SerializeField] Transform _selectionSprite;
    private bool _isDragging=false;
    private Vector2 _dragStartPos;
    private Vector2 _selectionEndPos;
    private Vector2 _selectionStartPos;
    private List<Mouse2DRaycastSelectable> _selection = new List<Mouse2DRaycastSelectable>();
    private Collider2D[] _selectedColliders;
    // Start is called before the first frame update
    void Start()
    {
        if (_cam == null) _cam = Camera.main;
    }
    private void Update()
    {
        if(_isDragging)
        {
            _selection.Clear();
            _selectionEndPos = HelperClass.MousPosWorld; //_cam.ScreenToWorldPoint(HelperClass.MousePosScreen);

            _selectionSprite.localScale = _selectionEndPos - _selectionStartPos;

            _selectedColliders =Physics2D.OverlapAreaAll(_dragStartPos, HelperClass.MousPosWorld);//_cam.ScreenToWorldPoint(HelperClass.MousePosScreen));



            foreach (var collider in _selectedColliders)
            {
                Mouse2DRaycastSelectable selectable = collider.gameObject.GetComponent<Mouse2DRaycastSelectable>();
                if (selectable != null)
                {
                    _selection.Add(selectable);
                    selectable.SelectItem();
                }
                
            }
        }
    }
    public Collider2D OverlapPoint(out Vector3 point, LayerMask mask)
    {
        Ray ray = _cam.ScreenPointToRay(HelperClass.MousePosScreen);
        point = ray.origin;

        return Physics2D.OverlapPoint(ray.origin, mask);
    }
    public Collider2D[] OverlapCircleAll(out Vector3 point,LayerMask mask)
    {
        Ray ray = _cam.ScreenPointToRay(HelperClass.MousePosScreen);
        point = ray.origin;

        return Physics2D.OverlapCircleAll(ray.origin, _OverlapCircleRadius, mask);
    }
    public void StartDrag()
    {
        _isDragging = true;
        _dragStartPos = _cam.ScreenToWorldPoint(HelperClass.MousePosScreen);
        _selectionStartPos = _dragStartPos;
        _selectionSprite.position = _selectionStartPos;
        _selectionSprite.gameObject.SetActive(true);
        Logger.Log(_dragStartPos);
    }
    public void EndDrag()
    {
        _isDragging = false;
        _selectionSprite.gameObject.SetActive(false);
        foreach (Mouse2DRaycastSelectable selectable in _selection)
        {
            Logger.Log(selectable);
        }
    }
    public void DeslectItems()
    {
        foreach (Mouse2DRaycastSelectable selectable in _selection)
        {
            selectable.DeselectItem();
        }
        _selection.Clear();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _OverlapCircleRadius);
    }
}
