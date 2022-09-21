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
    public GameObject sistemaJogo;
    public GameObject VaraAmostra;
    public GameObject VaraHud;
    public GameObject BaldeHud;
    public new GameObject[] fruta;
    public bool isPaused;
    public float gravidade;
    public float coeficienteGravidade;
    public bool isDrowning;
    bool isFishing;
    public Text desafioTexto;
    float intervaloPeixe;
    bool peixePronto;
    float tempoPeixe;
    public int peixesPegos;
    bool peixePego;
    public bool isHoldingSomething;
    public float tempoRestante;
    public float intervaloFruta;
    public float tempoFruta;
    public int frutasColetadas;
    bool pegandoFrutas;
    public Animation pesca;
    public int desafiosCompletosSessao;


    // Start is called before the first frame update
    void Start()
    {
        pegandoFrutas = false;
        frutasColetadas = 0;
        isFishing = false;
        isDrowning = false;
        isPaused = false;
        ctr = GetComponent<CharacterController>();
        intervaloPeixe = Random.Range(5,10);
        tempoRestante = 30f;
        intervaloFruta = 1f;
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

        
        if(isFishing){
            Pescar();
            intervaloPeixe -= Time.deltaTime;
            VaraAmostra.SetActive(false);
            VaraHud.SetActive(true);
        }

        if(pegandoFrutas){
            PegarFruta();
        }
        
        
    }
    void FixedUpdate()
    {
        if(isFishing == false){
            Mover();
            VaraHud.SetActive(false);
        }
                 
    }
    void Mover()
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

    void Pescar(){
        if(peixesPegos >= 5){    
            desafiosCompletosSessao += 1;
            StartCoroutine(ExibirTexto("Você terminou esse desafio!", 3, Color.yellow, 3));
            isFishing = false;
            isHoldingSomething = false;
            VaraHud.SetActive(false);
        }

        
        if(intervaloPeixe <= 0f){
            if(tempoPeixe >= 0f){
                desafioTexto.text = "Peixe pronto!";
                peixePronto = true;
                tempoPeixe -= Time.deltaTime;
            }
            else{
                StartCoroutine(ExibirTexto("Oops!", 2, Color.red));
                intervaloPeixe = Random.Range(5,10);
            }
        }
        else{
            peixePronto = false;
            tempoPeixe = 1f;
        }

        if(peixePronto){
            if(Input.GetMouseButtonDown(0)){
                pesca.Play("Vara Pesca");
                peixePego = true;
                peixesPegos += 1;
                StartCoroutine(ExibirTexto("Boa pegada!", 3, Color.green));
                if(peixesPegos < 5){
                    StartCoroutine(ExibirTexto("Faltam "+ (-peixesPegos + 5) + " peixe(s)", 3, Color.green, 3));
                }
                peixePronto = false;          
            }
        }
        else{
            if(Input.GetMouseButtonDown(0)){
                StartCoroutine(ExibirTexto("Oops!", 2, Color.red));
                intervaloPeixe = Random.Range(5,10);          
            }
        }

        if(peixePego){
            intervaloPeixe = Random.Range(5,10);
            peixePego = false;
        }
           
    }

    void PegarFruta(){
        tempoRestante -= Time.deltaTime;
        tempoFruta -= Time.deltaTime;

        if(tempoRestante >= 0f){
            if(tempoFruta <= 0f){
                if(frutasColetadas < 7){
                    StartCoroutine(MostrarFruta(fruta[(int)Mathf.Round(Random.value*11f)]));
                    tempoFruta = 1;
                }    
            }
        }
        else{
            StartCoroutine(ExibirTexto("Acabou o tempo :/", 2, Color.red));
            pegandoFrutas = false;
            BaldeHud.SetActive(false);            
        }

        if(frutasColetadas >= 7){
            desafiosCompletosSessao += 1;
            StartCoroutine(ExibirTexto("Você terminou esse desafio!", 3, Color.yellow, 0));
            BaldeHud.SetActive(false);
            pegandoFrutas = false;
        }
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
        if(colisao.gameObject.tag == "fruta"){
            frutasColetadas += 1;
            colisao.gameObject.SetActive(false);
            
            if(frutasColetadas < 7){
                StopCoroutine(ExibirTexto("Faltam " + (-frutasColetadas + 7) + " frutas",1,Color.green));
                StartCoroutine(ExibirTexto("Faltam " + (-frutasColetadas + 7) + " frutas",1,Color.green));
            }
        }
    }

    void OnTriggerStay(Collider colisao){
        if(colisao.gameObject.tag == "agua"){
            isDrowning = true;        
        }
        if(colisao.gameObject.name == "Zona Pesca"){
            desafioTexto.text = "Jogar desafio: Pesca?";
            if(Input.GetKeyDown(KeyCode.Space)){
                isFishing = true;
                colisao.gameObject.SetActive(false);
                StartCoroutine(ExibirTexto("Ok, boa sorte!", 2));
            }
        }
        if(colisao.gameObject.tag == "balde"){
            desafioTexto.text = "Jogar desafio: Frutas?";
            if(Input.GetMouseButtonDown(0)){
                StartCoroutine(ExibirTexto("Boa sorte! então", 3, Color.green));
                colisao.gameObject.SetActive(false);
                BaldeHud.SetActive(true);
                pegandoFrutas = true;

            }
        }
        
    }

    void OnTriggerExit(Collider colisao){
        if(colisao.gameObject.tag == "agua"){
            coeficienteGravidade -= Time.deltaTime;
            isDrowning = false;

        }
        if(colisao.gameObject.name == "Zona Pesca"){
            if(isFishing == false){
                desafioTexto.text = "";
            }
        }
        if(colisao.gameObject.tag == "balde"){
            desafioTexto.text = "";
        }     
    }

    IEnumerator ExibirTexto(string conteudo, float tempoExibicao, Color corTexto){
        desafioTexto.text = conteudo;
        desafioTexto.color = corTexto;
        yield return new WaitForSeconds(tempoExibicao);
        desafioTexto.text = "";
        desafioTexto.color = Color.white;
    }

    IEnumerator ExibirTexto(string conteudo, float tempoExibicao){
        desafioTexto.text = conteudo;
        yield return new WaitForSeconds(tempoExibicao);
        desafioTexto.text = "";
    }

    IEnumerator ExibirTexto(string conteudo, float tempoExibicao, Color corTexto, float intervaloInicial){
        yield return new WaitForSeconds(intervaloInicial);
        desafioTexto.text = conteudo;
        yield return new WaitForSeconds(tempoExibicao);
        desafioTexto.text = "";
    }

    IEnumerator MostrarFruta(GameObject objeto){
        objeto.SetActive(true);
        yield return new WaitForSeconds(3);
        objeto.SetActive(false);
    }
}
