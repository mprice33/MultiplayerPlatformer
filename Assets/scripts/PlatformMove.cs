using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    public GameObject cube;
    public float movementSpeed = 10;
    public float center;
    public bool end;
    void Start()
    {
       center = cube.transform.position.x;
        end = true;
    }
    void Update()
    {
        transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
        
        if (cube.transform.position.x > (center + 10) || cube.transform.position.x < (center - 10))
        {
            movementSpeed *= -1;
        }
    }
}
