using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegarLupa : MonoBehaviour
{
    public GameObject Box;
    public GameObject Lupa;
    public GameObject Lupa2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Box.active == true && Input.GetKey(KeyCode.G))
        {
            
            Lupa.SetActive(false);
            Lupa2.SetActive(true);
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
