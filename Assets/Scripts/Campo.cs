using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Referente a todo lo que sucede en el campo de juego
/// </summary>
public class Campo : MonoBehaviour
{

    /// <summary>
    /// Velocidad de desplazamiento del campo
    /// </summary>
    private float velocity;

    /// <summary>
    /// Determina el prefab del enemigo
    /// </summary>
    public GameObject enemigo;

    /// <summary>
    /// Determina el gameobjct donde se almacenaran los enemigos
    /// </summary>
    private GameObject campo;

    /// <summary>
    /// Determina si el jugador esta jugando o no
    /// </summary>
    private bool gaming = false;

    /// <summary>
    /// Cada cuanto tiempo se cambia la velocidad de generacion de enemigos
    /// </summary>
    private int velocidadGeneracionEnemigos = 4;

    /// <summary>
    /// Contador que establce cada cuanto se generan los enemigos
    /// </summary>
    private float contador = 2.5f;

    /// <summary>
    /// Establece la profundidad del campo
    /// </summary>
    /// <param name="a">Profundidad deseada</param>
    public void setZ(int a) {

        transform.position = new Vector3(transform.position.x, transform.position.y, a);
    }
    
    /// <summary>
    /// Establece una velocidad de spawn de enemigos predeterminada
    /// </summary>
    /// <param name="a">Velocidad deseada</param>
    public void setVelocidadGeneracionEnemigos(int a)
    {

        velocidadGeneracionEnemigos = a;

    }

    /// <summary>
    /// Establece la velocidad del campo
    /// </summary>
    /// <param name="a">Velocidad deseada</param>
    public void setVelocity(float a)
    {
        velocity = a;
    }

    /// <summary>
    /// Genera un enemigo en una posicion, y dentroo del objeto padre: Campo
    /// </summary>
    private void generateEnemigo()
    {
        Vector3 pos = new Vector3(Random.Range(1.22f, 6.11f), 8.5f, -1);
        Instantiate(enemigo, pos, Quaternion.identity, campo.transform);

    }

    /// <summary>
    /// Establece la variable gaming
    /// </summary>
    /// <param name="a">Valor que quremos actualizar</param>
    public void setGaming(bool a) {
        gaming = a;
    }

    /// <summary>
    /// Devuelve el valor de la variable gaming
    /// </summary>
    /// <returns>Valor variable gaming</returns>
    public bool getGaming() {
        return gaming;
    }

    /// <summary>
    /// Funcion que se lanza al iniciar el script
    /// </summary>
    void Start()
    {

        setZ(20);

        //Almacena la variable CAMPO
        campo = GameObject.Find("CAMPO");

    }

    /// <summary>
    /// Funcion que se lanza cada Frame
    /// </summary>
    void Update()
    {
        //Si el jugador esta en partida
        if (gaming)
        {
            //El contado sigue sumando
            contador += Time.deltaTime;

            //Si la division entre el contador y velocidadGeneracionEnemigos tiene resto 0
            if ((int)contador % velocidadGeneracionEnemigos == 0)
            {
                generateEnemigo();
                //Se inicializa contador
                contador = 1;
            }

            //El campo se deplaza hacia abajo
            transform.Translate(Vector3.down * velocity * Time.deltaTime);

        }
    }
    
}
