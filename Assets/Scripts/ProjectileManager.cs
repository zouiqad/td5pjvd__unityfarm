using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectileManager : MonoBehaviour
{

    private Stack<GameObject> projectiles;
    private float minX, minZ, maxX, maxZ;

    private Vector3 offSet = new Vector3(0.0f, 1.0f, 0.0f);
    public void Initialize(float minX, float maxX, float minZ, float maxZ)
    {
        this.minX = minX;
        this.maxX = maxX;
        this.minZ = minZ;
        this.maxZ = maxZ;
    }

    // Start is called before the first frame update
    void Start()
    {
        projectiles = new Stack<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (projectiles.Count > 0)
        {
            
            Vector3 clampedPosition = projectiles.Peek().transform.position;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
            clampedPosition.z = Mathf.Clamp(clampedPosition.z, minZ, maxZ);

            bool clamped = ((clampedPosition.x == maxX) || (clampedPosition.x == minX)) 
                               || ((clampedPosition.z == maxZ) || (clampedPosition.z == minZ));

            if (clamped)
            {
                Debug.Log("DESTROYED");
                Destroy(projectiles.Pop());
            }
        }
    }


    public void Shoot(GameObject projectile, Vector3 origin, Quaternion rotation)
    {
        if (Input.GetKeyDown("space"))
        {
            projectiles.Push(Instantiate(projectile, origin + offSet, rotation));
        }

    }
}
