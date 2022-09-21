using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;


public class movement : MonoBehaviour
{
    CharacterController ctr;
    public float Speed = 9;
    public GameObject box;
    public GameObject NPC;
    public GameObject Lupa;
    public GameObject pausaMenu;
    public bool isPaused;
    public float gravidade;
    public float coeficienteGravidade;
    public bool isDrowning;


    // Start is called before the first frame update
    void Start()
    {
        isDrowning = false;
        isPaused = false;
        ctr = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            isPaused = !isPaused;
        }
        
        if(isPaused){
            PausaJogo();
        }
        else{
            ContinuaJogo();
        }

        
    }
    void FixedUpdate()
    {
        mover();        
    }
    void mover()
    {
        
        float x,z;
        Vector3 moverment;
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        if(ctr.isGrounded == false){
            if(isDrowning == false){
                coeficienteGravidade += Time.deltaTime*5;
            }
            else{
                coeficienteGravidade -= Time.deltaTime;
            }
            moverment = transform.right * x + transform.forward * z + new Vector3 (0,-(gravidade + coeficienteGravidade),0);
        }
        else{
            if(isDrowning == false){
                coeficienteGravidade = 0f;
            }
            moverment = transform.right * x + transform.forward * z;
            
        }
        moverment.Normalize();
        ctr.Move(moverment*Speed*Time.deltaTime);  
    }
    public void PausaJogo(){
        Time.timeScale = 0f;
        pausaMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ContinuaJogo(){
        Time.timeScale = 1f;
        pausaMenu.SetActive(false);
        if(isPaused){
            isPaused = !isPaused;
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Deslogar(){
        SceneManager.LoadScene("Login");
    }

    public void SairDoJogo(){
        Application.Quit();
    }

    void OnTriggerEnter(Collider colisao){
        if(colisao.gameObject.tag == "agua"){
            isDrowning = true;        
        }     
    }

    void OnTriggerExit(Collider colisao){
        if(colisao.gameObject.tag == "agua"){
            coeficienteGravidade -= Time.deltaTime;
            isDrowning = false;

        }     
    }

}
