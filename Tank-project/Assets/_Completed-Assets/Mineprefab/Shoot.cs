using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private Transform blueCannon;
    [SerializeField] private Transform redCannon;
    
    public Rigidbody m_Shell;   

    public float range;

    
    // Start is called before the first frame update
    void Start()
    {
        redCannon = GameObject.FindGameObjectWithTag("RedCannon").transform;
        blueCannon = GameObject.FindGameObjectWithTag("BlueCannon").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(gameObject.tag == "RedCannon")
        {
            RaycastHit hit;
            if(Physics.Raycast(redCannon.position, redCannon.forward, out hit, range))
            {
                Debug.Log(hit.transform.tag);

                if(hit.transform.tag == "BlueTank" || hit.transform.tag == "BlueCannon")
                {
                    Debug.DrawLine(gameObject.transform.position,hit.transform.position,Color.red);
                    ShootBullet();
                }
            }
        
        }
        else
        {
            RaycastHit hit2;
            if(Physics.Raycast(blueCannon.position, blueCannon.forward, out hit2, range))
            {
                Debug.Log(hit2.transform.tag);

                if(hit2.transform.tag == "RedTank" || hit2.transform.tag == "RedCannon")
                {
                    Debug.DrawLine(gameObject.transform.position,hit2.transform.position,Color.blue);
                    ShootBullet();
                }
            }
        }
    }
    void ShootBullet()
    {
         Rigidbody shellInstance =
                Instantiate (m_Shell, gameObject.transform.position, gameObject.transform.rotation) as Rigidbody;

            // Set the shell's velocity to the launch force in the fire position's forward direction.
            //shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward; 
    }
}
