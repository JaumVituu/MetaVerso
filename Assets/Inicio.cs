using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inicio : MonoBehaviour
{
    public GameObject Vendedor;
    Transform Tvendedor;
    CharacterController CCvendedor;
    Animator Avendedor;

    // Start is called before the first frame update
    void Start()
    {
        Tvendedor = Vendedor.GetComponent<Transform>();
        CCvendedor = Vendedor.GetComponent<CharacterController>();
        Avendedor = Vendedor.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }
	private void OnTriggerEnter(Collider other)
	{
        if (other.tag == "Player")

        {
            Avendedor.SetBool("Andando", true);
            Vendedor.GetComponent<MoverNPC>().Andar = true;
            Destroy(gameObject);
        }
    }
}
