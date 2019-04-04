using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Relativo al manejo de los enemigos
/// </summary>
public class Enemigos : MonoBehaviour
{

    /// <summary>
    /// Establece el gameobject respectivo del jugador
    /// </summary>
    private GameObject player;

    /// <summary>
    /// Posibles skins del jugador (En proceso)
    /// </summary>
    public Sprite[] skins;

    /// <summary>
    /// Establece la camara principal
    /// </summary>
    private GameObject camara;

    /// <summary>
    /// Velocidad de los enemigos
    /// </summary>
    private float velocidad = 2;

    /// <summary>
    /// Rigidbody2D de los enemigos
    /// </summary>
    private Rigidbody2D rb;

    /// <summary>
    /// Seleccioma la skin correspondiente
    /// </summary>
    private void setSkin() {
        SpriteRenderer img = GetComponent<SpriteRenderer>();
        img.sprite = skins[Random.Range(0, 3)];
    }

    /// <summary>
    /// Funcion que se lanza al iniciar el script
    /// </summary>
    void Start()
    {
        do
        {
            player = GameObject.Find("Player");

        } while (player == null);

        setSkin();

        //Almacenamos componentes
        camara = GameObject.FindGameObjectWithTag("MainCamera");
        rb = GetComponent<Rigidbody2D>();


    }

    /// <summary>
    /// Funcion que se lanza cada Frame
    /// </summary>
    void Update()
    {  

        //Vector direccion + distancia
        Vector3 VectorQueUneJugadoryEnemigo = player.transform.position - transform.position;
        //Vector distancia
        float distanciHaciaElJugador = VectorQueUneJugadoryEnemigo.magnitude;
        //Vector direccion
        Vector3 vectorDireccionHaciaElJugador = VectorQueUneJugadoryEnemigo / distanciHaciaElJugador;

        #region Rotacion

        float angle = Mathf.Atan2(VectorQueUneJugadoryEnemigo.y, VectorQueUneJugadoryEnemigo.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);

        #endregion
       

        //Se aplica direccion deseada
        rb.velocity = vectorDireccionHaciaElJugador * velocidad;

        //Se establece la Z
        transform.position = new Vector3(transform.position.x, transform.position.y, -1);


    }

    /// <summary>
    /// Cuando no se vea por pantalla se elimina
    /// </summary>
    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }




}
