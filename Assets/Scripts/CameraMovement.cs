using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform targetObj;
    [SerializeField] private Vector3 distanceFromPlayer;
    [SerializeField] private float movementLerpCoef;

    // We're using LateUpdate to be sure player's movement calculations are finished for this frame
    void LateUpdate()
    {
        Vector3 targetPos = targetObj.transform.position + distanceFromPlayer;
        Vector3 camerPos = transform.position;

        Vector3 resultPos = Vector3.Lerp(camerPos, targetPos, Time.deltaTime * movementLerpCoef);
        // below for fixed camera velocity
        //Vector3 resultPos = Vector3.Lerp(camerPos, targetPos, (Time.deltaTime * movementSpeed) / (targetPos - camerPos).magnitude);

        transform.position = resultPos;
    }
}
