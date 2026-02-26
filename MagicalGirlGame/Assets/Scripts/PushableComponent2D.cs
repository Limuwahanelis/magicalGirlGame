using UnityEngine;

public class PushableComponent2D : MonoBehaviour,IPushable
{
    [SerializeField] Rigidbody2D _rb2D;
    public void Push(PushInfo pushInfo)
    {
        _rb2D.AddForce((transform.position - pushInfo.pushPosition).normalized * pushInfo.pushForce,ForceMode2D.Impulse);
    }
}
