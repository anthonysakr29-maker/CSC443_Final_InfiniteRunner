using System.Collections.Generic;
using UnityEngine;

public class ChunkCoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnChance = 0.6f;
    [SerializeField] private int maxCoinLines = 2;
    [SerializeField] private int coinsPerLine = 5;

    [Header("Placement")]
    [SerializeField] private float laneOffset = 2f;
    [SerializeField] private float coinHeight = 1f;
    [SerializeField] private float startZ = -10f;
    [SerializeField] private float spacingZ = 2f;

    private readonly List<GameObject> spawnedCoins = new();

    private void OnEnable()
    {
        SpawnCoins();
    }

    private void OnDisable()
    {
        ClearCoins();
    }

    private void SpawnCoins()
    {
        ClearCoins();

        if (coinPrefab == null) return;

        int linesToSpawn = Random.Range(0, maxCoinLines + 1);

        for (int line = 0; line < linesToSpawn; line++)
        {
            if (Random.value > spawnChance) continue;

            int lane = Random.Range(-1, 2);
            float x = lane * laneOffset;

            float zOffset = Random.Range(-4f, 4f);

            for (int i = 0; i < coinsPerLine; i++)
            {
                Vector3 localPos = new Vector3(
                    x,
                    coinHeight,
                    startZ + zOffset + i * spacingZ
                );

                GameObject coin = Instantiate(coinPrefab, transform);
                coin.transform.localPosition = localPos;
                coin.transform.localRotation = Quaternion.identity;

                spawnedCoins.Add(coin);
            }
        }
    }

    private void ClearCoins()
    {
        for (int i = spawnedCoins.Count - 1; i >= 0; i--)
        {
            if (spawnedCoins[i] != null)
                Destroy(spawnedCoins[i]);
        }

        spawnedCoins.Clear();
    }
}