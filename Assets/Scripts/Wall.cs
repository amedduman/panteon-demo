using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] PlayerController _player;
    private void OnEnable()
    {
        GameManager.OnRaceEnded += SetUpWall;
    }


    private void OnDisable()
    {
        GameManager.OnRaceEnded -= SetUpWall;
    }


    private void Awake()
    {
        ShowWall(false);
    }


    private void SetUpWall()
    {
        this.transform.position = GetWallPos();
        ShowWall(true);
    }


    private Vector3 GetWallPos()
    {
        return new Vector3(_player.transform.position.x, this.transform.position.y, this.transform.position.z);
    }


    private void ShowWall(bool visibility)
    {
        MeshRenderer[] renderers = this.gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer item in renderers)
        {
            item.enabled = visibility;
        }
    }
}
