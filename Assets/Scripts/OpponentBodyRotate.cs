using UnityEngine;

public class OpponentBodyRotate : MonoBehaviour
{
    [SerializeField] OpponentCharacterMovement _opponent;
    [SerializeField] [Range(1, 10)] float _rotationSpeed = 4;


    private Vector3 _moveDir;
    private Vector3 _right = new Vector3(1, 0, 1);
    private Vector3 _left = new Vector3(-1, 0, 1);
    private Vector3 _forward = new Vector3(0, 0, 1);

    void Update()
    {
        if (_opponent._moveHrz)
        {
            if (_opponent._goRight)
            {
                _moveDir = _right;

                SetBodyRot(_moveDir);
            }
            else
            {
                _moveDir = _left;

                SetBodyRot(_moveDir);
            }

        }
        else
        {
            _moveDir = _forward;

            SetBodyRot(_moveDir);

        }

    }

    private void SetBodyRot(Vector3 _moveDir)
    {
        Quaternion rotTo = Quaternion.LookRotation(_moveDir, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(this.transform.rotation, rotTo, _rotationSpeed);
    }
}
