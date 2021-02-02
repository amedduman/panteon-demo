using UnityEngine;
using UnityEngine.UI;

public class EndGamePanel : MonoBehaviour
{
    private Image _image;

    private void OnEnable()
    {
        GameManager.OnGameEnded += ShowPanel;
    }
    private void OnDisable()
    {
        GameManager.OnGameEnded -= ShowPanel;
    }


    private void Awake()
    {
        _image = this.gameObject.GetComponent<Image>();
        _image.enabled = false;
    }

    private void ShowPanel()
    {
        _image.enabled = true;
    }
}
