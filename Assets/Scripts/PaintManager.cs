using System;
using UnityEngine;

public class PaintManager : MonoBehaviour
{
    #region Serialize Fields

    [SerializeField] private int _brushSize = 20;
    // [SerializeField] private int _brushSizeY = 20;
    [SerializeField] private GameObject _wall;
    [SerializeField] [Range(0, 1)] private float _WinLimit = 0.99f;
    [SerializeField] private Color32 _paintColor = Color.red;

    #endregion


    #region Private Variables

    private Renderer _rend;
    private Texture2D _tex;
    private Vector2 _pixelUV;
    private RaycastHit _hit;
    private Vector2 _currentTargetPixel;
    private int _rows;
    private int _columns;
    private int _totalPixelCount;
    private int _paintedPixelCount;
    private bool _isPainting;
    private int _radius;
    private Vector2Int _rawPixelUV;
    private int _radiusSqr;
    #endregion

    public static event Action<float> OnPainting;

    private void OnEnable()
    {
        GameManager.OnRaceEnded += AllowPainting;
    }

    private void OnDisable()
    {
        GameManager.OnRaceEnded -= AllowPainting;
    }

    private void AllowPainting()
    {
        _isPainting = true;
    }


    void Start()
    {

        _rend = _wall.GetComponent<Renderer>();
        _tex = _rend.material.mainTexture as Texture2D;
        _rows = _brushSize;
        _columns = _brushSize;
        _radius = _brushSize;
        _totalPixelCount = _tex.GetPixels32().Length;
        _radiusSqr = _radius * _radius;

        ResetTexture();
    }

    private void ResetTexture()
    {
        Color32 _resetColor = new Color32(255, 255, 255, 1);
        Color32[] _resetColorArray = _tex.GetPixels32();

        for (int i = 0; i < _resetColorArray.Length; i++)
        {
            _resetColorArray[i] = _resetColor;
        }

        _tex.SetPixels32(_resetColorArray);
        _tex.Apply();
    }

    void Update()
    {
        if (_isPainting && Input.GetMouseButton(0))
        {
            PaintProccess();
        }
    }

    private void PaintProccess()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out _hit))
        {
            SetTargetPixel();

            Paint();

            UpdatePaintLevel();
        }

    }

    private void UpdatePaintLevel()
    {
        float _paintLevel = (float)_paintedPixelCount / _totalPixelCount;
        if (_paintLevel >= _WinLimit)
        {
            GameManager.EndGame();
        }
        else
        {
            OnPainting?.Invoke(_paintLevel);
        }
    }


    int i;
    int j;
    int _distanceSqr;
    private void Paint()
    {

        for (i = -_radius; i < _radius; i++)
        {
            for (j = -_radius; j < _radius; j++)
            {
                _distanceSqr = GetDistanceSqr(_rawPixelUV.x + i, _rawPixelUV.y + j);
                if (_distanceSqr < _radiusSqr)
                {
                    if (_tex.GetPixel(_rawPixelUV.x + i, _rawPixelUV.y + j) != _paintColor)
                    {
                        _tex.SetPixel(_rawPixelUV.x + i, _rawPixelUV.y + j, _paintColor);
                        _paintedPixelCount++;
                    }
                }
            }

        }

        _tex.Apply();
    }

    // instead of returning square root I will compare square of distance with square of radius
    private int GetDistanceSqr(int x, int y)
    {

        int xDis = _rawPixelUV.x - x;
        int yDis = _rawPixelUV.y - y;


        return (xDis * xDis) + (yDis * yDis);
    }
    private void SetTargetPixel()
    {
        _pixelUV = _hit.textureCoord;
        _pixelUV.x *= _tex.width;
        _pixelUV.y *= _tex.height;

        // to avoid casting in further steps
        _rawPixelUV.x = (int)_pixelUV.x;
        _rawPixelUV.y = (int)_pixelUV.y;

        _currentTargetPixel = _pixelUV;
    }
}
