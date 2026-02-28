using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] SpawnableItem _spawnableItem;
    [SerializeField] Renderer _renderer;
    [SerializeField] float _yinYangMult;
    [SerializeField] Collider2D _missileColider;
    [SerializeField] float _speed;
    [SerializeField] Rigidbody2D _rb;
    private DamageInfo _damageInfo;
    private DamageInfo _yinYangDamageInfo;
    public void SetUp(ElementInfo elementInfo,DamageInfo.DamageType damageType)
    {
        _damageInfo = new DamageInfo(elementInfo.Damage, transform.position, damageType, new Collider2D[] { _missileColider });
        _yinYangDamageInfo = new DamageInfo(elementInfo.Damage, transform.position, elementInfo.YinYangDamageType, new Collider2D[] { _missileColider });
    }
    // Update is called once per frame
    void Update()
    {
        if(!_renderer.isVisible)
        {
            _spawnableItem.ReturnToPool();
        }
        
    }
    private void FixedUpdate()
    {
        if (TimeControl.IsTimeStopped) return;
        _rb.MovePosition(_rb.position + (Vector2)transform.up * _speed*Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamagable damagable = collision.GetComponent<IDamagable>();
        if (damagable != null)
        {
            _yinYangDamageInfo.dmgPosition=transform.position;
            _damageInfo.dmgPosition = transform.position;
            damagable.TakeDamage(_yinYangDamageInfo);
            damagable.TakeDamage(_damageInfo);
        }
        _spawnableItem.ReturnToPool();
    }
}
