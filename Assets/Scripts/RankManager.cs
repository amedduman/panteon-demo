using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RankManager : MonoBehaviour
{
    private class Competitors
    {
        public GameObject Competitor { get; set; }
        public int Rank { get; set; }
        public bool IsPlayer { get; set; }
    }

    #region Serialize Fields

    [SerializeField] private GameObject[] _opponents;
    [SerializeField] private GameObject _player;

    #endregion

    private float _rankCalculationInteval = 0.1f;


    List<Competitors> _competitors = new List<Competitors>();

    void Awake()
    {
        for (int i = 0; i < _opponents.Length; i++)
        {
            Competitors opponent = new Competitors { };

            opponent.Competitor = _opponents[i];
            opponent.Rank = i + 1;
            opponent.IsPlayer = false;

            _competitors.Add(opponent);
        }

        Competitors player = new Competitors { };
        player.Competitor = _player;
        player.Rank = _opponents.Length + 1;
        player.IsPlayer = true;
        _competitors.Add(player);

    }


    void Start()
    {
        InvokeRepeating("SetRanks", 0f, _rankCalculationInteval);
    }

    void SetRanks()
    {
        _competitors = _competitors.OrderBy(x => x.Competitor.transform.position.z).Reverse().ToList();

        for (int i = 0; i < _competitors.Count; i++)
        {
            _competitors.ElementAt(i).Rank = i + 1;
            if (_competitors.ElementAt(i).IsPlayer)
            {
                GameManager.RefreshRank(i + 1);
            }
        }

    }
}

