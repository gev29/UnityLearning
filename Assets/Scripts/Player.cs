using UnityEngine;

public class Player : APlayer2D
{
    [SerializeField] private float rotationLerpCoef;

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (!GameManager.Instance.GameFinished)
        {
            MovePlayer();
            RotatePlayer();

            if (Input.GetMouseButtonUp(0))
            {
                Shoot();
            }
        }
    }

    protected override void MovePlayer()
    {
        transform.Translate(direction * Time.deltaTime * movementSpeed, Space.World);
        // The same as - transform.localPosition = transform.localPosition + direction * Time.deltaTime * movementSpeed;
    }

    protected override void RotatePlayer()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z;

        Vector2 direction = mousePosition - transform.position;
        //transform.LookAt(mousePosition); will not work in 2D
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction); ;
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationLerpCoef);
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
