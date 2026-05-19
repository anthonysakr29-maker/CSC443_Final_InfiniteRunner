using System.Collections.Generic;
using UnityEngine;

public class ChunkCoinSpawner : MonoBehaviour
{
    private enum CoinPattern
    {
        Straight,
        ZigZagWide,
        DiagonalLeftToRight,
        DiagonalRightToLeft,
        Arc
    }

    [SerializeField] private GameObject coinPrefab;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnChance = 0.6f;
    [SerializeField] private int maxCoinLines = 2;
    [SerializeField] private int minCoinsPerPattern = 3;
    [SerializeField] private int maxCoinsPerPattern = 8;

    [Header("Placement")]
    [SerializeField] private float laneOffset = 2f;
    [SerializeField] private float coinHeight = 1f;
    [SerializeField] private float startZ = -10f;
    [SerializeField] private float spacingZ = 2f;
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private Vector3 coinCheckHalfExtents = new Vector3(0.35f, 0.35f, 0.35f);

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
            float zOffset = Random.Range(-4f, 4f);

            int coinCount = Random.Range(minCoinsPerPattern, maxCoinsPerPattern + 1);
            CoinPattern pattern = (CoinPattern)Random.Range(0, 5);

            for (int i = 0; i < coinCount; i++)
            {
                Vector3 localPos = GetCoinPosition(pattern, lane, i, coinCount, zOffset);

                if (CoinPositionBlocked(localPos))
                    continue;

                GameObject coin = Instantiate(coinPrefab, transform);
                coin.transform.localPosition = localPos;
                coin.transform.localRotation = Quaternion.identity;

                spawnedCoins.Add(coin);
            }
        }
    }

    private Vector3 GetCoinPosition(CoinPattern pattern, int startLane, int index, int totalCoins, float zOffset)
    {
        int lane = startLane;
        float y = coinHeight;
        float z = startZ + zOffset + index * spacingZ;

        switch (pattern)
        {
            case CoinPattern.ZigZagWide:
                lane = index % 2 == 0 ? -1 : 1;
                break;

            case CoinPattern.DiagonalLeftToRight:
                if (totalCoins <= 1)
                    lane = 0;
                else
                    lane = Mathf.RoundToInt(Mathf.Lerp(-1, 1, index / (float)(totalCoins - 1)));
                break;

            case CoinPattern.DiagonalRightToLeft:
                if (totalCoins <= 1)
                    lane = 0;
                else
                    lane = Mathf.RoundToInt(Mathf.Lerp(1, -1, index / (float)(totalCoins - 1)));
                break;

            case CoinPattern.Arc:
                lane = startLane;
                float t = totalCoins <= 1 ? 0f : index / (float)(totalCoins - 1);
                y = coinHeight + Mathf.Sin(t * Mathf.PI) * 1.25f;
                break;

            case CoinPattern.Straight:
            default:
                lane = startLane;
                break;
        }

        return new Vector3(lane * laneOffset, y, z);
    }

    private bool CoinPositionBlocked(Vector3 localPos)
    {
        Vector3 worldPos = transform.TransformPoint(localPos);

        return Physics.CheckBox(
            worldPos,
            coinCheckHalfExtents,
            transform.rotation,
            obstacleMask,
            QueryTriggerInteraction.Collide
        );
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