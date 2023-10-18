using System.Collections;
using UnityEngine;

public class ControllerJoueur : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public float rotationSpeed = 120.0f;
    private float currentRotation = 0.0f;

    private float minX, minZ, maxX, maxZ;

    public GameObject player;
    public GameObject projectile;
    public GameObject terrain;


    public ProjectileManager projectileManager;


    // Start is called before the first frame update
    void Start()
    {
        terrain = GameObject.Find("Ground");


        // Calculate boundaries
        MeshRenderer terrainRenderer = terrain.GetComponent<MeshRenderer>();

        Vector3 planeCenter = terrainRenderer.bounds.center;
        Vector3 planeExtents = terrainRenderer.bounds.extents;

        minX = planeCenter.x - planeExtents.x;
        maxX = planeCenter.x + planeExtents.x;
        minZ = planeCenter.z - planeExtents.z;
        maxZ = planeCenter.z + planeExtents.z;

        projectileManager.Initialize(minX, maxX, minZ, maxZ);
    }

    // Update is called once per frame
    void Update()
    {
        // Get inputs 
        float translation = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;

        // Player movements
        player.transform.Translate(translation * player.transform.forward, Space.World);

        currentRotation += rotation;
        player.transform.rotation = Quaternion.Euler(0.0f, currentRotation, 0.0f);

        // get player position
        Vector3 clampedPosition = player.transform.position;

        // Clamp player within terrain
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, minZ, maxZ);

        // Update position
        player.transform.position = clampedPosition;

        projectileManager.Shoot(projectile, transform.position, transform.rotation);
       
    }




}
