using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class OpponentCharacterMovement : MonoBehaviour
{
    [SerializeField] private FinishTarget _target;
    [SerializeField] [Range(1, 20)] private float _hrzSpeed = 3;



    [HideInInspector] public bool IsStopped = true;
    [HideInInspector] public bool _goRight = false;
    [HideInInspector] public bool _moveHrz = false;

    private Vector3 _startPos;
    private Vector3 _targetPos;
    private Rigidbody _rb;
    private float _rightVal = 0;
    private float _forwardVal = 0;
    private float _dodgingForwardSpeed = 2;
    private NavMeshAgent _navAgent;


    private void OnEnable()
    {
        GameManager.OnRaceStart += StartRace;
    }


    private void OnDisable()
    {
        GameManager.OnRaceStart -= StartRace;
    }


    private void Awake()
    {
        _rb = this.gameObject.GetComponent<Rigidbody>();
        _startPos = this.transform.position;
        _navAgent = this.gameObject.GetComponent<NavMeshAgent>();
    }


    private void Update()
    {
        // move opponent fo forward and make dodge from dynamic obstacles when it is necessary 
        if (!IsStopped)
        {
            if (_moveHrz)
            {
                _rightVal = _goRight ? _hrzSpeed : -_hrzSpeed;
                _forwardVal = _dodgingForwardSpeed;
            }
            else
            {
                _rightVal = 0;
                _forwardVal = 0;
            }
            // to dodge from dyn. obs while show respect to nav area
            _navAgent.Move(new Vector3(_rightVal, 0, _forwardVal) * Time.deltaTime);
        }
    }


    void StartRace()
    {
        StartCoroutine(StartRun());
    }


    // return a random point in bounds of box collider
    private Vector3 GetTargetPos()
    {
        Bounds _targetBounds = _target.GetComponent<BoxCollider>().bounds;

        float _randomZ = Random.Range(_targetBounds.min.z, _targetBounds.max.z);

        float _randomX = Random.Range(_targetBounds.min.x, _targetBounds.max.x);

        _targetPos = new Vector3(_randomX, 0, _randomZ);

        return _targetPos;
    }


    // to prevent all opponents start together.
    private IEnumerator StartRun()
    {
        float delayBeforeStartToRun = Random.value;
        yield return new WaitForSeconds(delayBeforeStartToRun);

        _navAgent.SetDestination(GetTargetPos());
        MoveAgent();
    }


    public void StopAgent()
    {
        IsStopped = true;
        _navAgent.isStopped = true;
    }


    public void MoveAgent()
    {
        IsStopped = false;
        _navAgent.isStopped = false;
    }


    private void StartAgain()
    {
        StopAgent();
        this.transform.position = _startPos;
        MoveAgent();
    }


    // Entering finish collider box
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<FinishTarget>())
        {
            Debug.Log("finish");
            StartCoroutine(FreezeAgentMovement());
        }
    }


    // Disable rigidbody and stopping this character
    private IEnumerator FreezeAgentMovement()
    {
        float _delayBeforeStop = Random.value;
        yield return new WaitForSeconds(_delayBeforeStop);

        // change isStopped value because it is a trigger for animator
        StopAgent();

        // to avoid any physical reactions between agents and player
        Rigidbody _rb = this.GetComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.FreezeAll;

    }

    // Colliding with any obstacle
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Obstacle>())
        {
            StartAgain();
        }
    }

}
