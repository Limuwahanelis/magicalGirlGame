using UnityEngine;

public class Minimap : MonoBehaviour
{
    [SerializeField] float _minZoom;
    [SerializeField] float _maxZoom;
    [SerializeField] Camera _minimapCamera;
    private float _zoom;
    private void Start()
    {
        if (_minimapCamera.orthographic)
        {
            _zoom=_minimapCamera.orthographicSize ;
        }
        else
        {
            _zoom = _minimapCamera.transform.localPosition.y;
        }
    }
    public void SetZoom(float level)
    {
        _zoom = level;
        _zoom = Mathf.Clamp(_zoom, _minZoom, _maxZoom);
        UpdateCamera();
    }
    public void IncreaseZoom(float value)
    {
        _zoom += value;
        _zoom = Mathf.Clamp(_zoom, _minZoom, _maxZoom);
        UpdateCamera();
    }
    public void DecreaseZoom(float value)
    {
        _zoom -= value;
        _zoom = Mathf.Clamp(_zoom, _minZoom, _maxZoom);
        UpdateCamera();
    }
    private void UpdateCamera()
    {
        if (_minimapCamera.orthographic)
        {
            _minimapCamera.orthographicSize = _zoom;
        }
        else
        {
            Vector3 pos = _minimapCamera.transform.localPosition;
            pos.y = _zoom;
            _minimapCamera.transform.localPosition = pos;
        }
    }
}
