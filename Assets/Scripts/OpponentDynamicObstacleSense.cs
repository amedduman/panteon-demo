using UnityEngine;


public class OpponentDynamicObstacleSense : MonoBehaviour
{
    [SerializeField] OpponentCharacterMovement _opponentChar;

    Quaternion _initialRotation;

    void Awake()
    {
        _initialRotation = this.gameObject.transform.rotation;
    }


    void Update()
    {
        SetDetectorRotation();
    }


    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<HorizontalObstacleMovement>())
        {
            _opponentChar._moveHrz = true;

            if (IsAtRight(other))
            {
                _opponentChar._goRight = false;

            }

            else
            {
                _opponentChar._goRight = true;

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _opponentChar._moveHrz = false;
    }


    private bool IsAtRight(Collider other)
    {
        float otherX = other.gameObject.transform.position.x;
        float charX = _opponentChar.transform.position.x;

        return true ? otherX > charX : false;
    }


    private void SetDetectorRotation()
    {
        // don't let parent override child's rotation
        // face detector always forward
        this.transform.rotation = _initialRotation;
    }
}
