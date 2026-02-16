using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ResizableWindow : MonoBehaviour
{
    public enum ResizeType
    {
        NONE,LEFT,RIGHT,DOWN,UP
    }
    public enum HoverType
    {
        NONE, VERT, HOR, ALL
    }
    [SerializeField] Vector2 _minWindowSize = new Vector2(250,200);
    [SerializeField] UICursorManager _curMan;
    private Vector2 _pointerResizeStartposition;
    RectTransform _rectTransform;
    ResizeType _vertResize=ResizeType.NONE;
    ResizeType _horResize = ResizeType.NONE;
    HoverType _hoverType = HoverType.NONE;
    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }
    private void Update()
    {
        if (_vertResize == ResizeType.NONE && _horResize==ResizeType.NONE) return;
        Resize();
    }
    #region Hover
    public void OnBeginHorHover(BaseEventData data)
    {
        
        _hoverType = HoverType.HOR;
        if (_vertResize != ResizeType.NONE || _horResize != ResizeType.NONE) return;
        _curMan.SetHorizontalResizeCursor();
    }
    public void OnBeginVertHover(BaseEventData data)
    {
        _hoverType = HoverType.VERT;
        if (_vertResize != ResizeType.NONE || _horResize != ResizeType.NONE) return;
        _curMan.SetVerticalResizeCursor();
    }
    public void OnBeginAllHover(BaseEventData data)
    {
        _hoverType = HoverType.ALL;
        if (_vertResize != ResizeType.NONE || _horResize != ResizeType.NONE) return;
        _curMan.SetAllResizeCursor();
    }
    public void OnEndHover(BaseEventData data)
    {
        _hoverType = HoverType.NONE;
        if (_vertResize != ResizeType.NONE || _horResize != ResizeType.NONE) return;
        _curMan.SetDefaultCursor();

    }
    #endregion
    #region Resize
    public void OnBeginResizeHorRight(BaseEventData data)
    {
        _horResize = ResizeType.RIGHT;
    }
    public void OnBeginResizeHorLeft(BaseEventData data)
    {
        _horResize = ResizeType.LEFT;
    }
    public void OnBeginResizeVertUp(BaseEventData data)
    {
        _vertResize = ResizeType.UP;
    }
    public void OnBeginResizeVertDown(BaseEventData data)
    {
        _vertResize = ResizeType.DOWN;
    }
    public void OnBeginResizeDownLeft(BaseEventData data)
    {
        _vertResize = ResizeType.DOWN;
        _horResize = ResizeType.LEFT;
    }
    public void OnBeginResizeDownRight(BaseEventData data)
    {
        _vertResize = ResizeType.DOWN;
        _horResize = ResizeType.RIGHT;
    }
    public void OnBeginResizeUpLeft(BaseEventData data)
    {
        _vertResize = ResizeType.UP;
        _horResize = ResizeType.LEFT;
    }
    public void OnBeginResizeUpRight(BaseEventData data)
    {
        _vertResize = ResizeType.UP;
        _horResize = ResizeType.RIGHT;
    }
    public void OnStopResizeHor(BaseEventData data)
    {
        
        _horResize = ResizeType.NONE;
        switch (_hoverType)
        {
            case HoverType.HOR: _curMan.SetHorizontalResizeCursor(); break;
            case HoverType.VERT: _curMan.SetVerticalResizeCursor(); break;
            case HoverType.ALL: _curMan.SetAllResizeCursor(); break;
            case HoverType.NONE: _curMan.SetDefaultCursor(); break;
        }
    }
    public void OnStopResizeVert(BaseEventData data)
    {
        _vertResize = ResizeType.NONE;
        switch (_hoverType)
        {
            case HoverType.HOR: _curMan.SetHorizontalResizeCursor(); break;
            case HoverType.VERT: _curMan.SetVerticalResizeCursor(); break;
            case HoverType.ALL: _curMan.SetAllResizeCursor(); break;
            case HoverType.NONE: _curMan.SetDefaultCursor(); break;
        }
    }
    public void Resize()
    {

        //Logger.Log("res");
        Vector2 delta = _rectTransform.sizeDelta;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, HelperClass.MousePosScreen, null, out Vector2 pointerPosInRect);
        Vector2 pos = _rectTransform.position;
        if (delta.x >= _minWindowSize.x)
        {
            if (_horResize == ResizeType.RIGHT)
            {
                float diffx = pointerPosInRect.x - _rectTransform.rect.max.x;
                delta.x += diffx;
                pos.x += diffx / 2;
            }
            else if (_horResize == ResizeType.LEFT)
            {
                float diffx = _rectTransform.rect.min.x - pointerPosInRect.x;
                delta.x += diffx;
                pos.x -= diffx / 2;
            }
        }
        if (_vertResize == ResizeType.UP)
        {
            float diffy = pointerPosInRect.y - _rectTransform.rect.max.y;
            delta.y += diffy;
            pos.y += diffy / 2;
        }
        else if (_vertResize == ResizeType.DOWN)
        {
            float diffy = _rectTransform.rect.min.y - pointerPosInRect.y;
            delta.y += diffy;
            pos.y -= diffy / 2;
        }
        // Logger.Log(diff);
        //// delta.x += diff.x;
        _rectTransform.sizeDelta = delta;
        _rectTransform.position = pos;
        if (_rectTransform.sizeDelta.x < _minWindowSize.x)
        {
            float diffx = _minWindowSize.x - _rectTransform.sizeDelta.x;

            //Logger.Log("dif : " + diffx);
            //Logger.Log("dif d: " + diffx);
            delta.x = _minWindowSize.x;
            if(_horResize == ResizeType.RIGHT) pos.x += diffx / 2;
            else if (_horResize == ResizeType.LEFT) pos.x -= diffx / 2;

            _rectTransform.sizeDelta = delta;
            _rectTransform.position = pos;
        }
        if(_rectTransform.sizeDelta.y < _minWindowSize.y)
        {
            float diffy = _minWindowSize.y - _rectTransform.sizeDelta.y;
            delta.y = _minWindowSize.y;
            pos = _rectTransform.position;
            if (_vertResize == ResizeType.UP) pos.y += diffy / 2;
            else if (_vertResize == ResizeType.DOWN) pos.y -= diffy / 2;

            _rectTransform.sizeDelta = delta;
            _rectTransform.position = pos;
        }
    }
    #endregion
}
