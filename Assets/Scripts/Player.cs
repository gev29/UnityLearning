using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        RotatePlayer();
    }

    private void MovePlayer()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 direction = new Vector2(horizontal, vertical).normalized;

        transform.Translate(direction * Time.deltaTime * movementSpeed, Space.Self);
        // The same as - transform.localPosition = transform.localPosition + direction * Time.deltaTime * movementSpeed;
    }

    private void RotatePlayer()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z;

        Vector2 direction = mousePosition - transform.position;
        //transform.LookAt(mousePosition); will not work in 2D
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        //transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
        //transform.rotation = Quaternion.Euler(Vector3.forward * (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90));


        // ScreenToWorldPoint will not work for more complicated cases or with perspective camera
        // instead use raycasting like this:
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Plane plane = new Plane(Vector3.forward, Vector3.forward * 2);
        //if (plane.Raycast(ray, out float distance))
        //{
        //    Vector3 result = ray.GetPoint(distance);
        //}
    }
}
