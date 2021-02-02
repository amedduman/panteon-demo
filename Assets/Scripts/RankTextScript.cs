using UnityEngine;
using UnityEngine.UI;

public class RankTextScript : MonoBehaviour
{
    private Text _rankText;

    private void OnEnable()
    {
        GameManager.OnRaceStart += ShowText;

        GameManager.OnRaceEnded += HideText;

        GameManager.OnRankRefreshed += ChangeText;
    }
    private void OnDisable()
    {
        GameManager.OnRaceStart -= ShowText;

        GameManager.OnRaceEnded -= HideText;

        GameManager.OnRankRefreshed -= ChangeText;
    }


    private void Awake()
    {
        _rankText = this.gameObject.GetComponent<Text>();
        HideText();
    }


    private void ShowText()
    {
        _rankText.enabled = true;
    }

    private void HideText()
    {
        _rankText.enabled = false;
    }

    private void ChangeText(int rank)
    {
        _rankText.text = rank.ToString();
    }
}
