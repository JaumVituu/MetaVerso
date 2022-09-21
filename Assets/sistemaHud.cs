using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sistemaHud : MonoBehaviour
{
    public Text tempoTexto;
    public Text jogadorTexto;
    public Text desafioTexto;
    float tempoSecao;

    void Start(){
        tempoSecao = 0f;
    }

    void Update()
    {
        tempoSecao += Time.deltaTime;
        tempoTexto.text = "Tempo de Sessão: "+TimeFormat(tempoSecao);
        jogadorTexto.text = PlayerPrefs.GetString("currentSession","PlayerName");

    }

    string TimeFormat(float time){
        int seconds = (int) time % 60;
        int minutes = (int) time/60;
        return string.Format("{0:00}:{1:00}",minutes, seconds);
    }
}
