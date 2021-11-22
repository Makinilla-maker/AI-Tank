using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTheEnemy : MonoBehaviour
{
    public static LookAtTheEnemy instance;
    public Transform enemy;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = new Vector3(enemy.transform.position.x,transform.position.y,enemy.transform.position.z);
        transform.LookAt(v);
    }
}
