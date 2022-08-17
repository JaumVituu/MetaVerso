using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverNPC : MonoBehaviour
{
    public bool Andar;
    public bool Pronto = false;
    CharacterController ctr;
    // Start is called before the first frame update
    void Start()
    {
        ctr = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
		if (Andar) 
        {
            ctr.Move(transform.forward * 3 * Time.deltaTime);

        }
    }
}
