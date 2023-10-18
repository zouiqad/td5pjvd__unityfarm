using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalMove : MonoBehaviour
{
    public float moveSpeed = 10.0f; // Move speed
    public float changeDirectionInterval = 2.0f; // Time between changing direction

    private Vector3 newDirection;
    private float lastDirectionChangeTime;

    private GameObject terrain;
    private float minX, minZ, maxX, maxZ;

    // Start is called before the first frame update
    void Start()
    {
        terrain = GameObject.Find("Ground");
        MeshRenderer terrainRenderer = terrain.GetComponent<MeshRenderer>();

        minX = terrainRenderer.bounds.min.x;
        maxX = terrainRenderer.bounds.max.x;
        minZ = terrainRenderer.bounds.min.z;
        maxZ = terrainRenderer.bounds.max.z;

        lastDirectionChangeTime = Time.time;
        GetRandomDirection();

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastDirectionChangeTime > changeDirectionInterval)
        {
            GetRandomDirection();
        }

        MoveRandom();
        checkDestroy();
    }


    private void GetRandomDirection()
    {
        newDirection = new Vector3(Random.Range(minX, maxX), 0.0f, Random.Range(minZ, maxZ));
        lastDirectionChangeTime = Time.time;
}

    private void MoveRandom()
    {
        transform.LookAt(newDirection);
        transform.Translate(transform.forward * Time.deltaTime * moveSpeed);
    }


    private void checkDestroy()
    {
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, minZ, maxZ);

        bool clamped = ((clampedPosition.x == maxX) || (clampedPosition.x == minX))
                           || ((clampedPosition.z == maxZ) || (clampedPosition.z == minZ));

        if (clamped)
        {
            Debug.Log("DESTROYED");
            Destroy(this.gameObject);
        }
    }
}
