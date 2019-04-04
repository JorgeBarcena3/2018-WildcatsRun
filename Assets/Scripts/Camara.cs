using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Relativo a la camara
/// </summary>
public class Camara : MonoBehaviour
{

    /// <summary>
    /// Posicion final de la camara
    /// </summary>
    private Vector3 posFinal;

    /// <summary>
    /// Margen x
    /// </summary>
    public float margenX = 1f;

    /// <summary>
    /// Margen Y
    /// </summary>
    public float margenY = 1f;

    /// <summary>
    /// Maximo de X e Y donde se permite el movimiento
    /// </summary>
    public Vector2 maxXAndY;

    /// <summary>
    /// Minimo de X e Y donde se permite el movimiento
    /// </summary>
    public Vector2 minXAndY;

    /// <summary>
    /// Compontente transform del jugador
    /// </summary>
    private Transform player;

    /// <summary>
    /// Comprueba el margen de X
    /// </summary>
    /// <returns>Verdadero si es bien</returns>
    private bool checkMargenX()
    {
        
        return Mathf.Abs(transform.position.x - player.position.x) > margenX;
    }

    /// <summary>
    /// Comprueba el margen de Y
    /// </summary>
    /// <returns>Verdadero si es bien</returns>
    private bool checkMargenY()
    {

        return Mathf.Abs(transform.position.y - player.position.y) > margenY;
    }

    /// <summary>
    /// Establece la posicion de la camara en relacion al personaje
    /// </summary>
    private void seguirAlJugador()
    {
        //Posiciones de la camara
        float camaraX = transform.position.x;
        float camaraY = transform.position.y;

        
        if (checkMargenX())
        {
            //Busca al jugador
            camaraX = Mathf.Lerp(transform.position.x, player.position.x, Time.deltaTime * 2);
        }


        if (checkMargenY())
        {
            //Busca al jugador
            camaraY = Mathf.Lerp(transform.position.y, player.position.y, Time.deltaTime * 2);
        }

       // Establece en maximo y minimo de X e Y
        camaraX = Mathf.Clamp(camaraX, minXAndY.x, maxXAndY.x);
        camaraY = Mathf.Clamp(camaraY, minXAndY.y, maxXAndY.y);

        // Movemos la posicion de la camara
        transform.position = new Vector3(camaraX, camaraY, transform.position.z);
    }

    /// <summary>
    /// Funcion que se lanza al iniciar el script
    /// </summary>
    void Start()
    {
        //Inicicalizamos componente transform del jugador
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    /// <summary>
    /// Funcion que se lanza cada Frame
    /// </summary>
    void Update()
    {
        //Seguir al jugador
        seguirAlJugador();

    }

}

