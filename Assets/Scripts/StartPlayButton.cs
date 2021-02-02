using UnityEngine;

public class StartPlayButton : MonoBehaviour
{
    public void ButtonPressed()
    {
        GameManager.StartRace();
        gameObject.SetActive(false);
    }
}
