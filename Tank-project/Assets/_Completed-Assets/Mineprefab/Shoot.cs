using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private Transform redTank;
    [SerializeField] private Transform blueTank;
    [SerializeField] private Transform blueCannon;
    [SerializeField] private Transform redCannon;
    public float range;

    
    // Start is called before the first frame update
    void Start()
    {
        redCannon = GameObject.FindGameObjectWithTag("RedTank").transform;
        blueCannon = GameObject.FindGameObjectWithTag("BlueTank").transform;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(LookAtTheEnemy.instance.enemy.gameObject.tag == "RedTank")
        {
            if(Physics.Raycast(firePoint.transform.position, firePoint.transform.forward, out hit, range))
        
        }
    }
}
