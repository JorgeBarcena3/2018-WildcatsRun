using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Referente a todo lo que sucede en el Canvas y puntuacion
/// </summary>
public class UserIterference : MonoBehaviour {

    /// <summary>
    /// Texto que almacena la puntuacion del jugador y la muestra por pantalla
    /// </summary>
    public Text puntuacion;

    /// <summary>
    /// Panel que muestra la informacion final al usario a traves de la pantalla
    /// </summary>
    public GameObject panelFinal;

    /// <summary>
    /// Panel que muestra la informacion inicial al usario a traves de la pantalla
    /// </summary>
    public GameObject Home;

    /// <summary>
    /// Variable que almacena la puntuacion del jugador
    /// </summary>
    private float score;

    /// <summary>
    /// Panel que muestra la informacion inicial al usario a traves de la pantalla
    /// </summary>
    public GameObject HUD;

    /// <summary>
    /// Establece si el jugador se encuentra en estado de juego o no
    /// </summary>
    private bool jugando = false;

    /// <summary>
    /// Campos para mostrar las puntuaciones
    /// </summary>
    public Text Score, maxScore, attemps;

    /// <summary>
    /// Multiplicador de la puntuacion
    /// </summary>
    private float contador = 0;

    /// <summary>
    /// Devuelve la puntuacion actual del jugador
    /// </summary>
    /// <returns>Puntuacion del jugador</returns>
    public float getScore()
    {
        return score;
    }

    /// <summary>
    /// Posiciona la puntuacion del jugador
    /// </summary>
    /// <param name="b">Puntuacion que queremos asignar</param>
    public void setScore(float b)
    {
        score = b;
        
    }

    /// <summary>
    /// Posiciona el contoador que lleva los puntos
    /// </summary>
    /// <param name="a">Valor del contador</param>
    public void setContador(float a) {
        contador = a;
    }

    /// <summary>
    /// Cambia el texto del hud
    /// </summary>
    /// <param name="a">Valor del texto</param>
    public void setTextHud(string a) {
        puntuacion.text = a;
    }

    /// <summary>
    /// Reinicia los valores iniciales
    /// </summary>
    public void reiniciarPartidaUI()
    {
        setScore(0);
        setContador(0);
        setTextHud("PUNTOS");
    }

    /// <summary>
    /// Almacena las puntuaciones obtenidas en una base de datos
    /// </summary>
    public void setStats()
    {

        //Almacena la Maxima puntuacion
        if (score > PlayerPrefs.GetFloat("MaxScore", 0f))
            PlayerPrefs.SetFloat("MaxScore", score);

        //Almacena los intentos
        PlayerPrefs.SetInt("Intentos", PlayerPrefs.GetInt("Intentos", 0) + 1);

    }

    /// <summary>
    /// Muestra las puntuaciones obtenidas en los campos correspondientes
    /// </summary>
    public void getStats()
    {
       
        Score.text = score.ToString("F2");
        maxScore.text = PlayerPrefs.GetFloat("MaxScore", 0f).ToString("F2");
        attemps.text = PlayerPrefs.GetInt("Intentos", 2).ToString();

    }

    /// <summary>
    /// Cuando termina la partida se muestran puntuaciones
    /// </summary>
    /// <param name="a">Si la partida ha terminado o no</param>
    public void finalPartida(bool a)
    {
        //Se desactiva/actuva el hud
        HUD.SetActive(!a);

        //Se activa/desactiva las stats
        panelFinal.SetActive(a);

    }

    /// <summary>
    /// Devuelve la variable jugando
    /// </summary>
    /// <returns>Varialbe jugando</returns>
    public bool getJugando() {
        return jugando;
    }

    /// <summary>
    /// Establece si el juego esta activo o no
    /// </summary>
    /// <param name="a">El juego esta activo o no</param>
    public void setJugando(bool a)
    {
        jugando = a;
    }

    /// <summary>
    /// Funcion que se lanza cada Frame
    /// </summary>
    void Update () {

        //Si jugando se cumple
        if (jugando)
        {
            Home.SetActive(false);
            HUD.SetActive(true);

            //Contador esta activo
            contador += Time.deltaTime;

            setScore(contador * 6.3f);
            setTextHud( score.ToString("F1") + " puntos");

        }
	}

}
