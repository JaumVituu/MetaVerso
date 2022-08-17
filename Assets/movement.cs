using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class movement : MonoBehaviour
{
    CharacterController ctr;
    public float Speed = 9;
    public GameObject box;
    public GameObject NPC;
    public GameObject Lupa;

    // Start is called before the first frame update
    void Start()
    {
        ctr = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        mover();        
    }
    void mover()
    {
        float x,z;
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        Vector3 moverment = transform.right * x + transform.forward * z;
        ctr.Move(moverment*Speed*Time.deltaTime);

    }

}
