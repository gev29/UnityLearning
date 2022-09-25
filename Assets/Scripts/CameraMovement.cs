using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private const float DistanceFromPlayer = -10f;

    [SerializeField] private Player player;

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 playerPosition = player.transform.position;
        playerPosition.z += DistanceFromPlayer;
        transform.position = playerPosition;
    }
}
