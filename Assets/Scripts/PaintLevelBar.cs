using UnityEngine;
using UnityEngine.UI;

public class PaintLevelBar : MonoBehaviour
{
    private Material _loadingBarMat;

    private void OnEnable()
    {
        PaintManager.OnPainting += SetPaintLevelBarPercent;

        GameManager.OnRaceEnded += MakePaintLevelBarVisible;
    }


    private void OnDisable()
    {
        PaintManager.OnPainting -= SetPaintLevelBarPercent;

        GameManager.OnRaceEnded -= MakePaintLevelBarVisible;
    }

    private void Awake()
    {
        this.gameObject.GetComponent<Image>().enabled = false;
        _loadingBarMat = this.gameObject.GetComponent<Image>().material;

        _loadingBarMat.SetFloat("_level", 0);
    }

    private void SetPaintLevelBarPercent(float percent)
    {
        _loadingBarMat.SetFloat("_level", percent);
    }


    private void MakePaintLevelBarVisible()
    {
        this.gameObject.GetComponent<Image>().enabled = true;
    }
}
