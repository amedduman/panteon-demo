using UnityEngine;


[RequireComponent(typeof(CapsuleCollider))]
public class HorizontalObstacleMovement : MonoBehaviour
{
    [SerializeField] [Range(0.1f, 1.5f)] private float _speedMultiplier = 1;
    [SerializeField] private Vector3 _startPos;
    [SerializeField] private Vector3 _endPos;
    [SerializeField] private bool _setStartPos = false;
    [SerializeField] private bool _setEndPos = false;
    private float _fractionOfPatrol = 0;
    private Vector3 _tempMark;


    // Set start and end pos from inspector
    void OnValidate()
    {
        SetPos();
    }


    void Update()
    {
        _fractionOfPatrol += Time.deltaTime * _speedMultiplier;
        transform.position = Vector3.Lerp(_startPos, _endPos, _fractionOfPatrol);
        if (_fractionOfPatrol > 1)
        {
            _fractionOfPatrol = 0;
            _tempMark = _startPos;
            _startPos = _endPos;
            _endPos = _tempMark;

        }
    }


    private void SetPos()
    {
        if (_setStartPos)
        {
            _startPos = transform.position;
            print(_startPos);
            _setStartPos = false;
        }

        if (_setEndPos)
        {
            _endPos = transform.position;
            _setEndPos = false;
        }
    }
}
