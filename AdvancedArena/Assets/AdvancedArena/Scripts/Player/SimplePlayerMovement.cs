using UnityEngine;

public class SimplePlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Start()
    {
        Debug.Log("PLAYER HAREKET SCRIPTİ ÇALIŞTI!");
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(
            horizontal,
            0f,
            vertical
        );

        transform.position +=
            direction.normalized *
            moveSpeed *
            Time.deltaTime;

        if (direction != Vector3.zero)
        {
            Debug.Log("PLAYER HAREKET EDİYOR!");
        }
    }
}