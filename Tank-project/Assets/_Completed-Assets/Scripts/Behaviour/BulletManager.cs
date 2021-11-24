using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField] public List<GameObject> bullets = null;
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        bullets = new List<GameObject>();
        AddBullets();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddBullets()
    {
        for (int i = 1; i <= 3; ++i)
        {
            bullets.Add(prefab);
        }
    } 
}
