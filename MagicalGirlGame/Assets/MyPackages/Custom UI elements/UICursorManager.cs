using UnityEngine;

public class UICursorManager : MonoBehaviour
{
    [SerializeField] Texture2D _verticalResizeCursor;
    [SerializeField] Texture2D _horizontalResizeCursor;
    [SerializeField] Texture2D _allResizeCursor;
    public static bool IsResizing => _isResizing;
    private static bool _isResizing = false;
    public void SetVerticalResizeCursor()
    {
        if (_isResizing) return;
        Cursor.SetCursor(_verticalResizeCursor, new Vector2(_verticalResizeCursor.width / 2, _verticalResizeCursor.height / 2), CursorMode.Auto);
    }
    public void SetHorizontalResizeCursor()
    {
        if (_isResizing) return;
        Cursor.SetCursor(_horizontalResizeCursor, new Vector2(_horizontalResizeCursor.width / 2, _horizontalResizeCursor.height / 2), CursorMode.Auto);
    }
    public void SetAllResizeCursor()
    {
        if (_isResizing) return;
        Cursor.SetCursor(_allResizeCursor, new Vector2(_allResizeCursor.width / 2, _allResizeCursor.height / 2), CursorMode.Auto);
    }
    public void SetDefaultCursor()
    {
        if (_isResizing) return;
        Cursor.SetCursor(null,Vector2.zero,CursorMode.Auto);
    }
    public void SetResising(bool value)
    {
        _isResizing=value;
        if(!_isResizing) SetDefaultCursor();
    }
}
