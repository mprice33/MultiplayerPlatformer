using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformUp : MonoBehaviour {

    public GameObject cube;
    public float movementSpeed = 7;
    public float center;
    void Start()
    {
        center = cube.transform.position.y;
    }
    void Update()
    {
        transform.Translate(Vector3.up * movementSpeed * Time.deltaTime);

        if (cube.transform.position.y > (center + 10) || cube.transform.position.y < (center - 7))
        {
            movementSpeed *= -1;
        }
    }
}
