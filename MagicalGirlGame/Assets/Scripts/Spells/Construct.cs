using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

public class Construct : MonoBehaviour
{
    private bool _isPlaced = false;
    public bool CanBePlaced
    {
        get
        {
            if (_collidersInside.Count > 0)
            {
                if (_collidersInside.All(x => HelperClass.IsInLayerMask(x.gameObject, _groundLayer))) return true;
            }
            return false;
            
        }
    }
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] Collider2D _col;
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Color _wrongColor;
    [SerializeField] LayerMask _groundLayer;
    [SerializeField] SpawnableItem _spawnableItem;
    private Color _originalColor;
    private List<Collider2D> _collidersInside = new List<Collider2D>();
    private void Start()
    {
        _originalColor = _spriteRenderer.color;
    }
    private void Update()
    {
        if (CanBePlaced)
        {
            _spriteRenderer.color = _originalColor;
        }
        else _spriteRenderer.color = _wrongColor;
    }
    public void Move(Vector2 direction)
    {
        _rb.MovePosition(_rb.position + direction);
    }
    public void ChageTriggerToCol()
    {
        _isPlaced = true;
        _col.isTrigger = false;
    }
    public void Rotate()
    {
        if (transform.eulerAngles.z >= 90) transform.up = Vector3.up;
        else transform.up = Vector3.left;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isPlaced) return;
        if (!_collidersInside.Contains(collision))
        {
            _collidersInside.Add(collision);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_collidersInside.Contains(collision))
        {
            _collidersInside.Remove(collision);
        }
    }
    public void Deactivate()
    {
        _spawnableItem.ReturnToPool();
    }
}
