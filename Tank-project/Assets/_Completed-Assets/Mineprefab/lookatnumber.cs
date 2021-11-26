using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookatnumber : MonoBehaviour
{
    public Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = new Vector3(mainCamera.transform.position.x,mainCamera.transform.position.y,mainCamera.transform.position.z);
        transform.LookAt(v);
    }
}
