using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public InputField login;
    public InputField senha;
    public new GameObject[] componenteCanvas1;
    public Canvas canvasLogin;

    void Start()
    {
        PlayerPrefs.DeleteAll();
    }

    public void Cadastrar(){
        if(PlayerPrefs.HasKey(login.text + " login")||string.IsNullOrWhiteSpace(login.text)){
           StartCoroutine(MostrarTexto(componenteCanvas1[0]));
           Debug.Log("Nome indisponível");
        }
        if(string.IsNullOrWhiteSpace(senha.text)){
           StartCoroutine(MostrarTexto(componenteCanvas1[1]));
           Debug.Log("Senha indisponível");
        }

        if(PlayerPrefs.HasKey(login.text + " login") == false && string.IsNullOrWhiteSpace(login.text) == false && string.IsNullOrWhiteSpace(senha.text) == false){
            PlayerPrefs.SetString(login.text + " login", login.text);
            PlayerPrefs.SetString(login.text + " senha", senha.text);
            StartCoroutine(MostrarTexto(componenteCanvas1[2]));
        }
        else{
            Debug.Log("Não funcionou");
        }
    }

    public void Entrar(){
        if(PlayerPrefs.HasKey(login.text + " login") && PlayerPrefs.HasKey(login.text + " senha")){
            canvasLogin.gameObject.SetActive(false);
            SceneManager.LoadScene("SampleScene");
        }
    }


    IEnumerator MostrarTexto(GameObject texto){
        texto.SetActive(true);
        yield return new WaitForSeconds(3);
        texto.SetActive(false);
    }
}
