using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CoinsSpawnManager : MonoBehaviour
{
    [SerializeField] private CoinSpawnComponent[] _spawnPoints;
    [SerializeField] private CoinComponent _coinPrefab;

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        while (true)
        {
            int id = Random.Range(0, _spawnPoints.Length);

            if (_spawnPoints[id].Spawned) continue;

            CoinComponent c = Instantiate(_coinPrefab, _spawnPoints[id].Position, Quaternion.identity);
            c.onCollisionWithPlayer.AddListener(OnCollisionWithPlayer);
            _spawnPoints[id].CoinSpawned();
            break;
        }
    }

    private void OnCollisionWithPlayer()
    {
        Spawn();

        for(int i = 0; i < _spawnPoints.Length; i++)
            _spawnPoints[i].DeInit();

        GameManager.Instance.AddCoins();
    }
}
