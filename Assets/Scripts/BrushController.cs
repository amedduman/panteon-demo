using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BrushController : MonoBehaviour
{
    #region SerializeFields

    [Range(1, 100)] [SerializeField] float _speedMultiplier = 1;
    [SerializeField] private GameObject _wall;

    #endregion

    #region private variables

    private Rigidbody _rb;
    private Vector3 _moveDir;
    private BoxCollider _wallBoxCol;
    private Vector3 _newPos;
    private float _hMove;
    private float _vMove;
    private bool _isPainting = false;

    #endregion


    private void OnEnable()
    {
        GameManager.OnRaceEnded += StartPainting;

        GameManager.OnGameEnded += StopPainting;
    }


    private void OnDisable()
    {
        GameManager.OnRaceEnded -= StartPainting;

        GameManager.OnGameEnded -= StopPainting;
    }


    private void StartPainting()
    {
        _isPainting = true;
    }

    private void StopPainting()
    {
        _isPainting = false;
    }


    private void Start()
    {
        _rb = this.gameObject.GetComponent<Rigidbody>();

        // use this additional box collider that, is ticked as IsTrigger, to determine bounds for brush.
        _wallBoxCol = _wall.gameObject.GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (_isPainting)
        {
            MoveBrush();
        }
    }

    private void MoveBrush()
    {
        if (Input.GetMouseButton(0))
        {
            _hMove = Input.GetAxis("Mouse X");
            _vMove = Input.GetAxis("Mouse Y");

            _moveDir = new Vector3(_hMove, _vMove, 0);

            _newPos = transform.position + _moveDir * Time.fixedDeltaTime * _speedMultiplier;

            // to prevent brush move outside of the wall bounds
            if (_wallBoxCol.bounds.Contains(_newPos))
            {
                _rb.MovePosition(_newPos);
            }
        }
    }
}
