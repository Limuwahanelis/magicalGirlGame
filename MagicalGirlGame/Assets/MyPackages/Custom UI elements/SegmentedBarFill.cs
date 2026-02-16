using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SegmentedBarFill : MonoBehaviour
{
    public ProgressBar progressBar => _progressBar;
    [SerializeField] RectTransform _fillRectTransform;
    [SerializeField] Image _fillImage;
    [SerializeField] ProgressBar _progressBar;
    [SerializeField] Color _flashingColor;
    [SerializeField] Color _normalColor;
    [SerializeField] Color _lackColor;
    [SerializeField] Color _lackFlashColor;
    
    [SerializeField] float _timeTOChangeColor;
    private float _segmentMult;
    private bool _isFlashing=false;
    private int _curSeg = 0;

    private Coroutine _cor;
    private void Start()
    {
    }
    private void Update()
    {

    }
    public void ApplyColors(Color normalColor,Color flashColor)
    {
        _normalColor = normalColor;
        _flashingColor = flashColor;
        _fillImage.color = _normalColor;
        UnityEditor.EditorUtility.SetDirty(this);
    }
    public void SetFill(float startSegment,float endSegment)
    {

        Vector2 anchorMax = _fillRectTransform.anchorMax;
        Vector2 anchorMin = _fillRectTransform.anchorMin;
        float anchorX = endSegment / _progressBar.MaxValue;
        float anchorXMin = startSegment / _progressBar.MaxValue;
        anchorMax.x = anchorX;
        _fillRectTransform.anchorMax = anchorMax;
        if(anchorXMin<=anchorX)
        {
            anchorMin.x = anchorXMin;
            _fillRectTransform.anchorMin = anchorMin;
        }
        _fillRectTransform.sizeDelta = Vector2.zero;
    }
    public void StartFlashingLack()
    {
        if (_cor == null)
        {
            SetFill(0,_progressBar.MaxValue);
            _isFlashing = true;
            _cor = StartCoroutine(FlashLackCor());
        }
    }
    public void StartFlashing()
    {
        if(_cor==null)
        {
            _isFlashing = true;
            _cor=StartCoroutine(FlashCor());
        }
    }
    public void StopFlasing()
    {
        if (_cor != null)
        {
            _isFlashing = false;
            StopCoroutine(_cor);
            _fillImage.color = _normalColor;
            _cor = null;
        }
    }
    private IEnumerator FlashCor()
    {
        Vector4 vec = (_flashingColor - _normalColor);
        bool toFlashColor = true;
        float t = 0;
        while(_isFlashing)
        {
            if(toFlashColor)
            {
                _fillImage.color = Color.Lerp(_normalColor, _flashingColor, t/ _timeTOChangeColor);
            }
            else
            {
                _fillImage.color= Color.Lerp(_flashingColor, _normalColor, t / _timeTOChangeColor);
            }
            t += Time.deltaTime;
            if (t>=_timeTOChangeColor)
            {
                toFlashColor = !toFlashColor;
                t = 0;
            }
            yield return null;
        }
    }

    private IEnumerator FlashLackCor()
    {
        Vector4 vec = (_lackColor - _lackFlashColor);
        bool toFlashColor = true;
        float t = 0;
        while (_isFlashing)
        {
            if (toFlashColor)
            {
                _fillImage.color = Color.Lerp(_lackColor, _lackFlashColor, t / _timeTOChangeColor);
            }
            else
            {
                _fillImage.color = Color.Lerp(_lackFlashColor, _lackColor, t / _timeTOChangeColor);
            }
            t += Time.deltaTime;
            if (t >= _timeTOChangeColor)
            {
                toFlashColor = !toFlashColor;
                t = 0;
            }
            yield return null;
        }
    }


}
