using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static event Action OnRaceStart;
    public static event Action OnRaceEnded;
    public static event Action OnGameEnded;
    public static event Action<int> OnRankRefreshed;


    private void Awake()
    {
        Application.targetFrameRate = 30;
        // turn time to normal after LoseGame
        Time.timeScale = 1;
    }

    public static void StartRace()
    {
        OnRaceStart?.Invoke();
    }
    public static void EndRace()
    {
        OnRaceEnded?.Invoke();
    }

    public static void EndGame()
    {
        OnGameEnded?.Invoke();
    }

    public static void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public static void RefreshRank(int rank)
    {
        OnRankRefreshed?.Invoke(rank);
    }

    public static void LoseGame()
    {
        Time.timeScale = 0.1f;
        EndGame();
    }
}
