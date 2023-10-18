using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deplacement : MonoBehaviour
{

    public GameObject _object;
    public float speed = 20.0f;
    // Start is called before the first frame update
    void Start()
    {
        _object = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        _object.transform.Translate(_object.transform.forward * speed * Time.deltaTime, Space.World);
    }
}
