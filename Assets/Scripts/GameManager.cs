using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Maneja la logistica del juego
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Intentos por partida
    /// </summary>
    private int intentos = 1;

    /// <summary>
    /// Gameobject campo
    /// </summary>
    private Campo campo;

    /// <summary>
    /// Gameobject player
    /// </summary>
    private Player player;

    /// <summary>
    /// Gameobject de los tiles
    /// </summary>
    private Tiles tileCampo, tileDech, tileIzq;

    /// <summary>
    /// Gameobject del UI
    /// </summary>
    private UserIterference UI;

    /// <summary>
    /// Gameobject del animator
    /// </summary>
    private Animator anim, animUI;

    /// <summary>
    /// Musica del juego
    /// </summary>
    private AudioSource musica;

    /// <summary>
    /// establece si comenzar el juego
    /// </summary>
    private bool estoyJugando = false;

    /// <summary>
    /// Canciones que se pueden reproducir
    /// </summary>
    public AudioClip[] canciones;

    /// <summary>
    /// Array auxiliar que almacena gameobject temporales
    /// </summary>
    private GameObject[] arrAux;

    /// <summary>
    /// Establece la musica a reproducir
    /// </summary>
    private void setMusica()
    {

        musica.clip = canciones[Random.Range(0, canciones.Length)];
        musica.Play();
    }

    /// <summary>
    /// Cambia el audio de mute a un-mute
    /// </summary>
    /// <param name="audio">Audio que queremos cambiar</param>
    public void cambiarAudio(AudioSource audio)
    {

        audio.mute = !audio.mute;
    }

    /// <summary>
    /// Inicializa el campo la primera vez
    /// </summary>
    private void prepararCampo()
    {

        #region tiles     
        //Borramos Tiles
        tileCampo.clearTiles();
        //Borramos Tiles
        tileDech.clearTiles();
        //Borramos Tiles
        tileIzq.clearTiles();

        //Generamos tilesIniciales
        tileCampo.generacionInicial();
        //Generamos tilesIniciales
        tileDech.generacionInicial();
        //Generamos tilesIniciales
        tileIzq.generacionInicial();

        #endregion

       

    }

    /// <summary>
    /// Reiniciar valores iniciales
    /// </summary>
    public void reiniciar()
    {

        jugoFinalizado();
        //Devolvemos el tiempo
        Time.timeScale = 1;

        #region Camara
        //Reiniciamos posicion Camara
        GameObject.FindGameObjectWithTag("MainCamera").transform.position = new Vector3(3.64f, 2.24f, -10);
        #endregion

        #region Enemigos
        //Eliminamos enemigos anteriores
        arrAux = GameObject.FindGameObjectsWithTag("Enemigo");
        for (int i = 0; i < arrAux.Length; i++)
            Destroy(arrAux[i].gameObject);
        #endregion

        #region Campo
        //Reiniciamos campo
        GameObject.Find("CAMPO").transform.position = new Vector3(0, 0, 1);
        //Establecemos Z predetermianda
        campo.setZ(20);
        #endregion

        #region UI
        //Desactivamos panel de scoreFinal
        UI.finalPartida(false);
        //La score es 0
        UI.setScore(0);
        //Panel Inicial
        UI.Home.SetActive(true);
        //Escondemos el HUD
        UI.HUD.SetActive(false);
        //Reiniciamos partida
        UI.reiniciarPartidaUI();
        #endregion

        #region Animaciones
        anim.SetBool("Playing", false);
        #endregion

        #region tiles     
        //Borramos Tiles
        tileCampo.clearTiles();
        //Borramos Tiles
        tileDech.clearTiles();
        //Borramos Tiles
        tileIzq.clearTiles();

        if (intentos != 1)
        {
            //Generamos tilesIniciales
            tileCampo.generacionInicial();
            //Generamos tilesIniciales
            tileDech.generacionInicial();
            //Generamos tilesIniciales
            tileIzq.generacionInicial();
        }
        #endregion

        #region Player
        //Reiniciamos posicion jugador
        player.transform.position = new Vector3(3.53f, 0.02f, -5);
        
        #endregion



    }

    /// <summary>
    /// Reinicia la escena de manera que al siguiente frame ya estas jugando
    /// </summary>
    public void reiniciarDeUna()
    {

        jugoFinalizado();
        //Devolvemos el tiempo
        Time.timeScale = 1;

        #region Camara
        //Reiniciamos posicion Camara
        GameObject.FindGameObjectWithTag("MainCamera").transform.position = new Vector3(3.64f, 2.24f, -10);
        #endregion

        #region Enemigos
        //Eliminamos enemigos anteriores
        arrAux = GameObject.FindGameObjectsWithTag("Enemigo");
        for (int i = 0; i < arrAux.Length; i++)
            Destroy(arrAux[i].gameObject);
        #endregion

        #region Campo
        //Reiniciamos campo
        GameObject.Find("CAMPO").transform.position = new Vector3(0, 0, 1);
        //Establecemos Z predetermianda
        campo.setZ(20);
        #endregion

        #region UI
        //Desactivamos panel de scoreFinal
        UI.finalPartida(false);
        //La score es 0
        UI.setScore(0);
        //Panel Inicial
        UI.Home.SetActive(true);
        //Escondemos el HUD
        UI.HUD.SetActive(false);
        //Reiniciamos partida
        UI.reiniciarPartidaUI();
        #endregion

        #region Animaciones
        anim.SetBool("Playing", false);
        #endregion

        #region tiles     
        //Borramos Tiles
        tileCampo.clearTiles();
        //Borramos Tiles
        tileDech.clearTiles();
        //Borramos Tiles
        tileIzq.clearTiles();

        if (intentos != 1)
        {
            //Generamos tilesIniciales
            tileCampo.generacionInicial();
            //Generamos tilesIniciales
            tileDech.generacionInicial();
            //Generamos tilesIniciales
            tileIzq.generacionInicial();
        }
        #endregion

        #region Player
        //Reiniciamos posicion jugador
        player.transform.position = new Vector3(3.53f, 0.02f, -5);
        player.setSeAcabo(false);
        #endregion

    }


    /// <summary>
    /// Boton inicial, no se inicia el juego pero está apunto
    /// </summary>
    public void preparado()
    {

        player.setSeAcabo(false);


    }

    /// <summary>
    /// Animacion del menu
    /// </summary>
    /// <returns></returns>
    public void animacion()
    {

        animUI.SetTrigger("OutIn");
        animUI.SetBool("OutIn 0", true);
        Invoke("preparado", 1f);

    }

    /// <summary>
    /// Boton incial start
    /// </summary>
    public void preparadoStart()
    {

        animacion();
    }

    /// <summary>
    /// Final del juego
    /// </summary>
    public void jugoFinalizado()
    {
        //Paramos animacion de correr
        anim.SetBool("Playing", false);
        //Comunicamos que la partida ha acabado
        UI.setJugando(false);
        //La velocidad del campo es 0
        campo.setVelocity(0);
        //Comunicamos que la partida ha acabado
        campo.setGaming(false);
        //Detenemos el tiempo del juego
        Time.timeScale = 0;
        //Almacenamos score
        UI.setStats();
        //Mostramos score
        UI.getStats();
        ////Comunicamos que la partida ha acabado
        UI.finalPartida(true);


        estoyJugando = false;
        player.setSeAcabo(true);
        


    }

    /// <summary>
    /// Acciones que se ralizan en el gameplay
    /// </summary>
    private void duranteElJuego()
    {
        anim.SetBool("Playing", true);
        estoyJugando = true;
        UI.setJugando(true);
        campo.setVelocity(5);
        campo.setGaming(true);

    }

    /// <summary>
    /// Funcion que se lanza al iniciar el script
    /// </summary>
    void Start()
    {

        #region Inicializacion de las varialbes

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        anim = GameObject.Find("Player_Interpolation").GetComponent<Animator>();

        campo = GameObject.FindGameObjectWithTag("Campo").GetComponent<Campo>();

        tileCampo = GameObject.FindGameObjectWithTag("TileCampo").GetComponent<Tiles>();

        tileIzq = GameObject.FindGameObjectWithTag("TileIzq").GetComponent<Tiles>();

        tileDech = GameObject.FindGameObjectWithTag("TileDech").GetComponent<Tiles>();

        UI = GameObject.Find("Canvas").GetComponent<UserIterference>();

        animUI = GameObject.Find("HOME").GetComponent<Animator>();

        musica = GameObject.Find("Musica").GetComponent<AudioSource>();

        #endregion

        setMusica();

        campo.setVelocity(0);

        prepararCampo();

        //   reiniciar();

    }

    /// <summary>
    /// Funcion que se lanza cada Frame
    /// </summary>
    void Update()
    {
        Debug.Log(intentos);

        if (!player.GetSeAcabo())
        {

            duranteElJuego();

            if (UI.getScore() > 100)
                campo.setVelocidadGeneracionEnemigos(3);
            if (UI.getScore() > 200)
                campo.setVelocidadGeneracionEnemigos(2);
            if (UI.getScore() > 300)
                campo.setVelocity(7);
        }
        else if (estoyJugando == true)
        {
            intentos++;
            jugoFinalizado();

        } 

    }

    private void Awake()
    {
        
    }

}
