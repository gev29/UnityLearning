using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private const float DistanceFromPlayer = -10f;

    [SerializeField] private Transform targetObj;
    [SerializeField] private float movementSpeed;

    // We're using LateUpdate to be sure player's movement calculations are finished for this frame
    void LateUpdate()
    {
        Vector3 targetPos = targetObj.transform.position;
        Vector3 camerPos = transform.position;
        camerPos.z = 0;

        Vector3 resultPos = Vector3.Lerp(camerPos, targetPos, (Time.deltaTime * movementSpeed) / (targetPos - camerPos).magnitude);

        resultPos.z += DistanceFromPlayer;
        transform.position = resultPos;
    }
}
