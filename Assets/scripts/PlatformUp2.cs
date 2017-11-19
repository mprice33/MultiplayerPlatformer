using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformUp2 : MonoBehaviour {

    public GameObject cube;
    public float movementSpeed = 15;
    public float center;
    void Start()
    {
        center = cube.transform.position.y;
    }
    void Update()
    {
        transform.Translate(Vector3.up * movementSpeed * Time.deltaTime);

        if (cube.transform.position.y > (center + 20) || cube.transform.position.y < (center - 50))
        {
            movementSpeed *= -1;
        }
    }
}
