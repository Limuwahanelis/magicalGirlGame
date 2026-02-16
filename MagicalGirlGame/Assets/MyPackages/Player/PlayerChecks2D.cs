using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChecks2D : MonoBehaviour
{
    [SerializeField] bool _debug;
    public RaycastHit2D GroundHit => _groundHit;
    public bool IsOnGround => _isOnGround;
    [SerializeField] Transform _mainBody;
    [Header("Layers")]
    [SerializeField] LayerMask _ground;
    [Header("Rays")]
    [SerializeField] Transform _groundRaycastOrigin;
    [SerializeField] float _groundRaycastLength;
    [Header("Boxes")]
    [SerializeField] Transform _groundBoxOverlapOrigin;
    [SerializeField] Vector2 _groundBoxSize;

    private bool _isOnGround;
    private RaycastHit2D _groundHit;
    private Collider2D[] _colliders = new Collider2D[2];
    #region Editor debug
#if UNITY_EDITOR
    private bool _hasGroundRayHit;
#endif
    #endregion
    private void Start()
    {
        if (_groundRaycastOrigin) _groundHit = Physics2D.Raycast(_groundRaycastOrigin.position, -_mainBody.up, _groundRaycastLength, _ground);
        if(_groundBoxOverlapOrigin) _isOnGround = Physics2D.OverlapBoxNonAlloc(_groundBoxOverlapOrigin.position, _groundBoxSize, 0, _colliders, _ground) > 0;
        else _isOnGround=true;
        #region Editor debug
#if UNITY_EDITOR
        _hasGroundRayHit = _groundHit;
#endif
        #endregion
    }
    private void Update()
    {
        if (_groundRaycastOrigin) _groundHit = Physics2D.Raycast(_groundRaycastOrigin.position, -_mainBody.up, _groundRaycastLength, _ground);
        if (_groundBoxOverlapOrigin) _isOnGround = Physics2D.OverlapBoxNonAlloc(_groundBoxOverlapOrigin.position, _groundBoxSize, 0, _colliders, _ground) > 0;
        else _isOnGround = true;
        #region Editor debug
#if UNITY_EDITOR
        _hasGroundRayHit = _groundHit;
#endif
        #endregion
    }
    private void OnDrawGizmos()
    {
        if (!_debug) return;
        Gizmos.color = Color.cyan;
        if (_groundRaycastOrigin) Gizmos.DrawRay(_groundRaycastOrigin.position, -_mainBody.up * _groundRaycastLength);
        if (_groundBoxOverlapOrigin) Gizmos.DrawWireCube(_groundBoxOverlapOrigin.position, new Vector3(_groundBoxSize.x, _groundBoxSize.y));
    }
}
