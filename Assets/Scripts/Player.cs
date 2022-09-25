using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const float MovementSpeed = 5f;

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 offset = new Vector2(horizontal, vertical).normalized;

        transform.Translate(offset * Time.deltaTime * MovementSpeed, Space.Self);
    }

    private void LateUpdate()
    {
        
    }

    private void FixedUpdate()
    {
        
    }


    private void OnDisable()
    {

    }

    private void OnDestroy()
    {
        
    }

}
