using UnityEngine;

public class Beam : MonoBehaviour
{
    [SerializeField] float _pushForce;
    [SerializeField] LayerMask _toHitMask;
    PushInfo _pushInfo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _pushInfo = new PushInfo(transform.position, _pushForce);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IPushable pushable = collision.GetComponent<IPushable>();
        if (pushable == null)
        {
            if (collision.attachedRigidbody != null)
            {
                pushable = collision.attachedRigidbody.GetComponent<IPushable>();
                if(pushable!=null)
                {
                    if(HelperClass.IsInLayerMask(collision.gameObject,_toHitMask)) pushable.StartLongPush();
                }
                
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        IPushable pushable = collision.GetComponent<IPushable>();
        if(pushable==null)
        {
            if (collision.attachedRigidbody != null)
            {
                _pushInfo.pushPosition = transform.position;
                pushable = collision.attachedRigidbody.GetComponent<IPushable>();
                if (pushable != null)
                {
                    if (HelperClass.IsInLayerMask(collision.gameObject, _toHitMask)) pushable.LongPush(_pushInfo, ForceMode2D.Force);

                }

            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        IPushable pushable = collision.GetComponent<IPushable>();
        if (pushable == null)
        {
            if (collision.attachedRigidbody != null)
            {
                _pushInfo.pushPosition = transform.position;
                pushable = collision.attachedRigidbody.GetComponent<IPushable>();
                if (pushable != null)
                {
                    if (HelperClass.IsInLayerMask(collision.gameObject, _toHitMask)) pushable.EndLongPush();

                }

            }
        }
    }
}
