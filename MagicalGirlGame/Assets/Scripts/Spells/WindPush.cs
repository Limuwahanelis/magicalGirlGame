using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindPush : MonoBehaviour
{
    [SerializeField] float _pushForce;
    [SerializeField] AnimationManager _animMan;
    [SerializeField] SpawnableItem _spawnableItem;
    private float _windElements;
    private List<Collider2D> _collidedObjects=new List<Collider2D>();
    private Coroutine _pushCor;
    private float _animlength;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!_collidedObjects.Contains(collision))
        {
           
            
            if(collision.attachedRigidbody)
            {
                IPushable pushable = collision.attachedRigidbody.GetComponent<IPushable>();
                if(pushable!=null)
                {
                    pushable.Push(new PushInfo(transform.position, _pushForce, Vector2.zero));
                    _collidedObjects.Add(collision);
                }
            }
        }
    }

    public void ResetCollision()
    {
        _collidedObjects.Clear();
    }

    public void StartPushing()
    {
        if (_pushCor != null) StopCoroutine(_pushCor);
        _pushCor= StartCoroutine(Push());

    }
    IEnumerator Push()
    {


        _animlength = _animMan.GetAnimationLength("Wind push");
        yield return new WaitForSeconds(_animlength);
        ResetCollision();
        _spawnableItem.ReturnToPool();
    }
}
