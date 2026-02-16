using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

[DefaultExecutionOrder(-5)]
public class CameraInputHandler2D : MonoBehaviour
{
    [SerializeField] PlayerCamera2D _cam;
    [SerializeField,Tooltip("Camera to set mouse world pos in HelperClass")] Camera _camera;
    [SerializeField] float _edgeSize;
    [SerializeField] float _camMoveSpeed;
    [SerializeField,Tooltip("Should camera be moved by other means then just follosing a point")] bool _moveCamera;
    [SerializeField,ConditionalField("_moveCamera")]private bool _moveCameraByMouse = false;
    [SerializeField,Header("Raycasts")] bool _mouseRaycasts;
    [SerializeField, ConditionalField("_mouseRaycasts")] RaycastFromMouse2D _raycast;
    [SerializeField, ConditionalField("_mouseRaycasts")] InputActionReference _LMBAction;
    private Vector3 _keyboardCamMove;
    private bool _moveCameraByKeyboard = false;
    private void Start()
    {
        if (_mouseRaycasts)
        {
            _LMBAction.action.performed += LMB;
            _LMBAction.action.canceled += LMB;
            _LMBAction.action.started += LMB;
        }
    }
    // Update is called once per frameMov
    void Update()
    {
        if (_camMoveSpeed <= 0) return;
        if (_moveCameraByMouse) return;
        Vector3 pos = _cam.PositionToFollow;
        if (HelperClass.MousePosScreen.x > Screen.width - _edgeSize) pos.x += _camMoveSpeed * Time.deltaTime;
        if (HelperClass.MousePosScreen.x < 0 + _edgeSize) pos.x -= _camMoveSpeed * Time.deltaTime;
        if (HelperClass.MousePosScreen.y > Screen.height-_edgeSize) pos.y += _camMoveSpeed * Time.deltaTime;
        if (HelperClass.MousePosScreen.y < 0+_edgeSize) pos.y -= _camMoveSpeed * Time.deltaTime;
        if(_moveCameraByKeyboard)
        {
          pos += _keyboardCamMove * _camMoveSpeed * Time.deltaTime;
        }
        _cam.SetPositionToFollow(pos);
    }
    void OnMoveCamera(InputValue value)
    {
        if(value.Get<Vector2>()!=Vector2.zero) _moveCameraByKeyboard=true;
        else _moveCameraByMouse = false;
        Vector2 pos = _cam.PositionToFollow;
        //Logger.Log(value.Get<Vector2>());
        _keyboardCamMove = value.Get<Vector2>();
    }
    void OnMoveCameraByMouse(InputValue value)
    {
        if (!_moveCamera) return;
        if (value.Get<float>()>=1) _moveCameraByMouse = true;
        else _moveCameraByMouse = false;
    }
    void OnMouseDelta(InputValue value)
    {
        if (!_moveCamera) return;
        Vector2 pos = _cam.PositionToFollow;
        Vector2 delta= value.Get<Vector2>();
        if (_moveCameraByMouse)
        {
            pos -= delta* _camMoveSpeed * Time.deltaTime;
            _cam.SetPositionToFollowRaw(pos);
        }
    }
    void OnMousePos(InputValue value)
    {
        HelperClass.SetMousePos(value.Get<Vector2>());
        if(_camera)
        {
            HelperClass.SetMousePosWorld(_camera);
        }
    }
    public void LMB(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                if (context.interaction is HoldInteraction)
                {
                    _raycast.StartDrag();
                    Logger.Log("Hold");
                }
                else
                {
                    Logger.Log("Press");
                }
                break;

            case InputActionPhase.Canceled:
                _raycast.EndDrag();
                Logger.Log("Cancel");
                break;
        }
    }
    private void OnDestroy()
    {
        if (_mouseRaycasts)
        {
            _LMBAction.action.performed -= LMB;
            _LMBAction.action.canceled -= LMB;
            _LMBAction.action.started -= LMB;
        }
    }
}
