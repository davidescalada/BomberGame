using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    private CharacterController characterController;

    public float speed = 5f;
    public GameObject bombPrefab;
    private GameObject lastBomb;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        SpawnBomb();

        if (lastBomb != null && Vector3.Distance(transform.position, lastBomb.transform.position) > 1.0f)
        {
            EnableBombCollision();  
        }
    }

    private void Move()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        characterController.Move(move * Time.deltaTime * speed);
    } 

    private void SpawnBomb()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           lastBomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
           Collider bombCollider = lastBomb.GetComponent<Collider>();
           Collider playerCollider = GetComponent<Collider>();

            if (bombCollider != null && playerCollider != null)
            {
                Physics.IgnoreCollision(bombCollider, playerCollider, true);
            }
        }

      

    }

    private void EnableBombCollision()
    {
        if (lastBomb != null)
        {
            Collider bombCollider = lastBomb.GetComponent<Collider>();
            Collider playerCollider = GetComponent<Collider>();

            if (bombCollider != null && playerCollider != null)
            {
                Physics.IgnoreCollision(bombCollider, playerCollider, false);
            }

            lastBomb = null;
        }
    }
}
