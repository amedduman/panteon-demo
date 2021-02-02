using UnityEngine;

public class RotatePlayerBody : MonoBehaviour
{
    [SerializeField] PlayerController _player;
    private Vector3 _movementDirection;
    [SerializeField] float _rotationSpeed = 1;


    public void SetCharacterRotation()
    {
        _movementDirection = new Vector3(_player.GetHorizontalMov(), 0, _player.GetVerticalMov());
        _movementDirection.Normalize();

        Quaternion rotTo = Quaternion.LookRotation(_movementDirection, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(this.transform.rotation, rotTo, _rotationSpeed);

    }
}
