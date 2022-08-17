using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AperteE : MonoBehaviour
{
    public GameObject Box;
    public GameObject Lupa;
    public GameObject next;
    public Animator anima;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Box.active == true && Input.GetKey(KeyCode.E)) 
        {
            anima.SetTrigger("Ataque");
            Lupa.SetActive(true);
            next.SetActive(true);
            Destroy(Box);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")

        {
            Box.SetActive(true);
        }
    }
}
