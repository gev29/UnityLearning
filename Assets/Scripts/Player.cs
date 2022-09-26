using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 direction = new Vector2(horizontal, vertical).normalized;

        transform.Translate(direction * Time.deltaTime * movementSpeed, Space.Self);
        // The same as - transform.localPosition = transform.localPosition + direction * Time.deltaTime * movementSpeed;
    }
}
