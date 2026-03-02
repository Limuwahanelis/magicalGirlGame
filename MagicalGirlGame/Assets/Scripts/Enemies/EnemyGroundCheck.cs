using UnityEngine;

public class EnemyGroundCheck : MonoBehaviour
{
    public bool IsOnGround => _isOnGround;
    [SerializeField] Transform _groundCheckTran;
    [SerializeField] float _groundCheckLength;
    [SerializeField] bool _debug;
    [SerializeField] LayerMask _groundMask;
    private bool _isOnGround;

    // Update is called once per frame
    void Update()
    {
        if(Physics2D.Raycast(_groundCheckTran.position,Vector2.down,_groundCheckLength,_groundMask)) _isOnGround = true;
        else _isOnGround = false;
    }
    private void OnDrawGizmos()
    {
        if (!_debug) return;
        if(_groundCheckTran != null) Gizmos.DrawLine(_groundCheckTran.position, _groundCheckTran.position + Vector3.down * _groundCheckLength);

    }

}
