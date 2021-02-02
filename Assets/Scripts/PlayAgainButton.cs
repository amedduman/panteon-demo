using UnityEngine;
using UnityEngine.UI;
public class PlayAgainButton : MonoBehaviour
{
    private Image _image;
    private Button _button;


    private void OnEnable()
    {
        GameManager.OnGameEnded += ShowButton;
    }


    private void OnDisable()
    {
        GameManager.OnGameEnded -= ShowButton;
    }

    private void Awake()
    {
        _image = this.gameObject.GetComponent<Image>();
        _button = this.gameObject.GetComponent<Button>();
        _image.enabled = false;
        _button.enabled = false;
    }
    private void Start()
    {
        _image.enabled = false;
        _button.enabled = false;
    }

    private void ShowButton()
    {
        _image.enabled = true;
        _button.enabled = true;
    }

    public void ButtonPressed()
    {
        GameManager.RestartGame();
    }


}
