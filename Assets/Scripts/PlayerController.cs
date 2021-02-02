using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    #region Serialize Fields

    [Range(0.1f, 10)] [SerializeField] private float _forwardSpeedMul = 1;
    [Range(0.1f, 10)] [SerializeField] private float _rightSpeedMul = 1;
    [SerializeField] private BoyAnimatonStateController _animCont;
    [SerializeField] private GameObject _platform;

    #endregion

    #region Public Variables

    [HideInInspector] public bool IsPainting = false;

    #endregion

    #region Private Variables
    private GameStates _currentState;
    private Rigidbody _rb;
    private Vector3 _movementDirection;
    private Bounds _platformBonds;
    private RotatePlayerBody _playerBody;
    private float _horizontalMov = 0;
    private float _verticalMove = 0;

    #endregion

    private enum GameStates
    {
        waitForRace,
        race,
        raceFinished,
        paint
    }


    void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        _currentState = GameStates.waitForRace;

        _platformBonds = _platform.gameObject.GetComponent<BoxCollider>().bounds;

        _playerBody = GetComponentInChildren<RotatePlayerBody>();
    }


    private void OnEnable()
    {
        GameManager.OnRaceStart += StartRace;
    }


    private void OnDisable()
    {
        GameManager.OnRaceStart -= StartRace;
    }


    private void StartRace()
    {
        _currentState = GameStates.race;
    }


    private void EndRace()
    {
        GameManager.EndRace();
    }


    void Update()
    {
        if (_currentState == GameStates.race)
        {
            if (Input.GetMouseButton(0))
            {
                _horizontalMov = Input.GetAxis("Mouse X");
            }
            else
            {
                // to reset HorizontalMove when player lift finger
                _horizontalMov = 0;
            }

            _verticalMove = 1;
            _animCont.SetWalkingAnim(true);
        }
    }


    void FixedUpdate()
    {
        if (_currentState == GameStates.race)
        {
            MovePlayer();
            // MovePP2();
        }
        else if (_currentState == GameStates.raceFinished)
        {
            // Wait for players to uphold their finger.
            if (Input.GetMouseButton(0))
            {
                return;
            }
            else
            {
                EndRace();
                // change state to paint. To prevent calling EndRace method more than once.
                _currentState = GameStates.paint;
            }
        }
    }


    private void StopPlayer()
    {
        _horizontalMov = 0f;
        _verticalMove = 0f;
        _animCont.SetWalkingAnim(false);
        _rb.constraints = RigidbodyConstraints.FreezeAll;
    }


    private void MovePlayer()
    {
        Vector3 pos = transform.position + new Vector3(_horizontalMov * _rightSpeedMul, 0, _verticalMove * _forwardSpeedMul) * Time.fixedDeltaTime;

        // to prevent player getting outside of the platform
        if (_platformBonds.Contains(pos))
        {
            _rb.MovePosition(pos);
            // rotate player body to direction of movement
            _playerBody.SetCharacterRotation();
        }
        else
        {
            // move just forward
            _rb.MovePosition(transform.position + new Vector3(0, 0, _verticalMove * _forwardSpeedMul) * Time.fixedDeltaTime);

            // // do not allow player to turn more when he/she reach bounds of platform
            _horizontalMov = 0;
            _playerBody.SetCharacterRotation();
        }
    }

    private void MovePP2()
    {
        Vector3 pos = transform.position + new Vector3(_horizontalMov * _rightSpeedMul, 0, _verticalMove * _forwardSpeedMul);

        // to prevent player getting outside of the platform
        if (_platformBonds.Contains(pos))
        {
            _rb.MovePosition(pos);
            // rotate player body to direction of movement
            _playerBody.SetCharacterRotation();
        }
        else
        {
            // move just forward
            _rb.MovePosition(transform.position + new Vector3(0, 0, _verticalMove * _forwardSpeedMul));

            // // do not allow player to turn more when he/she reach bounds of platform
            _horizontalMov = 0;
            _playerBody.SetCharacterRotation();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<FinishTarget>())
        {
            StopPlayer();

            _currentState = GameStates.raceFinished;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Obstacle>())
        {
            LoseGameProccess();
        }
    }


    private void LoseGameProccess()
    {
        GameManager.LoseGame();
    }

    public float GetHorizontalMov()
    {
        return _horizontalMov;
    }

    public float GetVerticalMov()
    {
        return _verticalMove;
    }
}
