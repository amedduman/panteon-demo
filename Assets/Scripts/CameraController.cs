using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _player;
    private float _distance = 10;
    void LateUpdate()
    {
        this.transform.position = new Vector3(_player.transform.position.x, this.transform.position.y, _player.transform.position.z - _distance);
    }

}
