using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    public enum PhysicMaterialType
    {
        NONE, NO_FRICTION, NORMAL
    }
    public int FlipSide => _flipSide;
    public bool IsPlayerFalling { get => _rb.linearVelocity.y < 0; }
    public Rigidbody2D PlayerRB => _rb;
    [Header("Common")]
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] PlayerController2D _player;
    [SerializeField] float _normalGravityForce;
    [SerializeField] float _speed;
    [Header("Jump")]
    [SerializeField] Ringhandle _jumpHandle;
    [SerializeField] SlopeDetection _slopeDetection;
    [SerializeField] float _jumpStrength;
    [Header("Push")]
    [SerializeField] Ringhandle _pushHandle;
    [SerializeField] float _normalPushForce;
    [Header("Physics materials")]
    [SerializeField] PhysicsMaterial2D _noFrictionMat;
    [SerializeField] PhysicsMaterial2D _normalMaterial;

    private int _flipSide = 1;
    private GlobalEnums.HorizontalDirections _newPlayerDirection;
    private GlobalEnums.HorizontalDirections _oldPlayerDirection;
    private float _previousDirection;
    public void MoveInAir(Vector2 direction,bool canRotate)
    {
        if (direction.x != 0)
        {
            _oldPlayerDirection = _newPlayerDirection;
            _newPlayerDirection = (GlobalEnums.HorizontalDirections)direction.x;
            _rb.MovePosition(_rb.position + new Vector2(_player.MainBody.transform.right.x * _flipSide * _speed * Time.deltaTime * 0.6f, _rb.position.y));
            _previousDirection = direction.x;
        }
        else
        {
            if (_previousDirection != 0)
            {
                _rb.linearVelocity = new Vector2(0, 0);
                _rb.MovePosition(_rb.position + new Vector2(_player.MainBody.transform.right.x * _flipSide * _speed * Time.deltaTime, 0));
                //StopPlayerOnXAxis();
            }

        }
    }
    public void Move(Vector2 direction,Vector2 groundRayHitPoint)
    {
        if (!_slopeDetection.CanWalkOnSlope()) return;
        if (direction.x != 0)
        {
            _oldPlayerDirection = _newPlayerDirection;
            _newPlayerDirection = (GlobalEnums.HorizontalDirections)direction.x;
            Vector2 moveVector = _rb.position + new Vector2(_player.MainBody.transform.right.x * _flipSide * _speed * Time.deltaTime, 0);
            moveVector.y = ( groundRayHitPoint.y);
            _rb.MovePosition(moveVector);
            if (direction.x > 0)
            {
                _flipSide = 1;
                _player.MainBody.transform.localScale = new Vector3(_flipSide, _player.MainBody.transform.localScale.y, _player.MainBody.transform.localScale.z);
            }
            if (direction.x < 0)
            {
                _flipSide = -1;
                _player.MainBody.transform.localScale = new Vector3(_flipSide, _player.MainBody.transform.localScale.y, _player.MainBody.transform.localScale.z);
            }
            _previousDirection = direction.x;
        }
        else
        {
            if (_previousDirection != 0)
            {
                _rb.linearVelocity = new Vector2(0, 0);
                _rb.MovePosition(_rb.position + new Vector2(_player.MainBody.transform.right.x * _flipSide * _speed * Time.deltaTime, 0));
            }

        }

    }
    public void MoveInAir(Vector2 direction )
    {
        if (direction.x != 0)
        {
            _oldPlayerDirection = _newPlayerDirection;
            _newPlayerDirection = (GlobalEnums.HorizontalDirections)direction.x;
            Vector2 moveVector = _rb.position + new Vector2(_player.MainBody.transform.right.x * _flipSide * _speed * Time.deltaTime, 0);
            _rb.linearVelocity = new Vector2(direction.x * _speed , _rb.linearVelocity.y);
            if (direction.x > 0)
            {
                _flipSide = 1;
                _player.MainBody.transform.localScale = new Vector3(_flipSide, _player.MainBody.transform.localScale.y, _player.MainBody.transform.localScale.z);
            }
            if (direction.x < 0)
            {
                _flipSide = -1;
                _player.MainBody.transform.localScale = new Vector3(_flipSide, _player.MainBody.transform.localScale.y, _player.MainBody.transform.localScale.z);
            }
            _previousDirection = direction.x;
        }
        else
        {
            //if (_previousDirection != 0)
            //{
            //    _rb.linearVelocity = new Vector2(0, 0);
            //    _rb.MovePosition(_rb.position + new Vector2(_player.MainBody.transform.right.x * _flipSide * _speed * Time.deltaTime, 0));
            //}

        }
    }
    public void PushPlayer(PushInfo pushInfo)
    {
        StopPlayer();
        Vector2 pushDirection = _pushHandle.GetVector();

        if (!pushInfo.pushType.HasFlag(DamageInfo.DamageType.TRAPS))
        {
            if (HelperClass.CheckIfObjectIsBehind(transform.position, pushInfo.pushPosition, (GlobalEnums.HorizontalDirections)_flipSide)) pushDirection.x = -pushDirection.x;
        }
        if(pushInfo.pushForce==-1) _rb.AddForce(pushDirection * _normalPushForce, ForceMode2D.Impulse);
        else _rb.AddForce(pushDirection * pushInfo.pushForce, ForceMode2D.Impulse);

    }
    public void SetRBMaterial(PhysicMaterialType type)
    {
        switch (type)
        {
            case PhysicMaterialType.NORMAL: _rb.sharedMaterial = _normalMaterial; break;
            case PhysicMaterialType.NONE: _rb.sharedMaterial = null; break;
            case PhysicMaterialType.NO_FRICTION: _rb.sharedMaterial = _noFrictionMat; break;
        }
    }
    public void SetRB(bool isdynamic)
    {
        if (isdynamic) _rb.bodyType = RigidbodyType2D.Dynamic;
        else _rb.bodyType = RigidbodyType2D.Kinematic;
    }
    public void SetRBVelocity(Vector2 velocity)
    {
        _rb.linearVelocity = velocity;
    }
    public void StopPlayer(bool vertical = true, bool horizontal = true)
    {
        if (horizontal) _rb.linearVelocity = new Vector2(0, _rb.linearVelocity.y);
        if (vertical) _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0);
    }
    public void Jump()
    {
        _rb.linearVelocity = new Vector3(0, 0, 0);
        _rb.AddForce(new Vector2(0, _jumpStrength), ForceMode2D.Impulse);
    }
}
