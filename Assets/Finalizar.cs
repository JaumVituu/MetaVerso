using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Finalizar : MonoBehaviour
{
    public GameObject Vendedor;
    Transform Tvendedor;
    CharacterController CCvendedor;
    Animator Avendedor;
    public GameObject aperteE;
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
        if (other.tag == "NPC")

        {
            Avendedor.SetBool("Andando", false);
            Vendedor.GetComponent<MoverNPC>().Andar = false;
            Vendedor.GetComponent<MoverNPC>().Pronto = true;
            aperteE.SetActive(true);
            Destroy(gameObject);
        }
    }
}
