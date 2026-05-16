using UnityEngine;

public class RunnerPlatform : MonoBehaviour
{
    [SerializeField] private float platformHeight = 2f;

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
            player.SetGroundHeight(platformHeight);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
            player.ClearGroundHeight(platformHeight);
    }
}