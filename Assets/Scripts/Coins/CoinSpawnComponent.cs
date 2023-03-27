using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawnComponent : MonoBehaviour
{
    public bool Spawned { get; private set; } = false;
    public Vector3 Position => transform.position;
    public void CoinSpawned() => Spawned = true;
    public void DeInit() => Spawned = false;
}
