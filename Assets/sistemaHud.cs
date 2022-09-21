using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sistemaHud : MonoBehaviour
{
    public Text tempoTexto;
    public Text jogadorTexto;
    public Text desafioTexto;
    public GameObject Player;
    bool desafioCompleto;
    float tempoSecao;

    void Start(){
        tempoSecao = 0f;
    }

    void Update()
    {
        tempoSecao += Time.deltaTime;
        tempoTexto.text = "Tempo de Sessão: "+TimeFormat(tempoSecao);
        jogadorTexto.text = PlayerPrefs.GetString("currentSession","PlayerName");
        if(desafioCompleto){
            desafioTexto.text = "Desafios completos hoje: " + Player.GetComponent<movement>().desafiosCompletosSessao;
        }
        else{
            desafioTexto.text = "Nenhum desafio feito :/";
        }

        if(Player.GetComponent<movement>().peixesPegos == 5 || Player.GetComponent<movement>().frutasColetadas >= 7){
            desafioCompleto = true;
        }

    }

    string TimeFormat(float time){
        int seconds = (int) time % 60;
        int minutes = (int) time/60;
        return string.Format("{0:00}:{1:00}",minutes, seconds);
    }
}
