using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Relativo a pintar tiles
/// </summary>
public class Tiles : MonoBehaviour
{

    /// <summary>
    /// Componente Tilemap
    /// </summary>
    private Tilemap tilemap;

    /// <summary>
    /// Gameobject del player
    /// </summary>
    private GameObject player;

    /// <summary>
    /// Posibles tiles
    /// </summary>
    public TileBase[] tileCampo;

    /// <summary>
    /// Posibles tiles
    /// </summary>
    public TileBase[] tileTD;

    /// <summary>
    /// Posibles tiles
    /// </summary>
    public TileBase[] tileLateral;

    /// <summary>
    /// Posicion inicial de  generacion del tiles
    /// </summary>
    private Vector3Int posGeneracion = new Vector3Int(0, 0, 0);

    /// <summary>
    /// Tipos de tiles
    /// </summary>
    public enum tipoDeTile { lateralIzq, lateralDech, campo };

    /// <summary>
    /// Tipo de tile asignado
    /// </summary>
    public tipoDeTile tipoDeTiles;

    /// <summary>
    /// Cuadricula donde se generan las tiles
    /// </summary>
    private Grid grid;

    /// <summary>
    /// Numero maximo de tiles creadas
    /// </summary>
    public int maxRendered = 3;

    /// <summary>
    /// Se generan 3 tiles para comenzaar la partida
    /// </summary>
    public void generacionInicial()
    {

        //clearTiles();

        #region GeneracionInicial

        //Establecemos la Y del jugador
        posGeneracion = grid.WorldToCell(player.transform.position);
        //X siempre es 0
        posGeneracion.x = 0;

        //Si es tipo campo
        if (tipoDeTiles == tipoDeTile.campo)
        {
            for (int i = 0; i < maxRendered; i++)
            {

                //Si no hay Tile se coloca otra
                if (!tilemap.GetTile(new Vector3Int(posGeneracion.x, posGeneracion.y + 1, posGeneracion.z)))
                    tilemap.SetTile(posGeneracion, tileCampo[1]);
            }
        }

        //Si es tipo lateralIzq
        if (tipoDeTiles == tipoDeTile.lateralIzq)
        {
            for (int i = 0; i < maxRendered; i++)
            {


                //Si no hay Tile se coloca otra
                if (!tilemap.GetTile(new Vector3Int(posGeneracion.x, posGeneracion.y + 1, posGeneracion.z)))
                    tilemap.SetTile(posGeneracion, tileLateral[0]);

            }
        }

        //Si es tipo lateralDech
        if (tipoDeTiles == tipoDeTile.lateralDech)
        {
            for (int i = 0; i < maxRendered; i++)
            {


                //Si no hay Tile se coloca otra
                if (!tilemap.GetTile(new Vector3Int(posGeneracion.x, posGeneracion.y + 1, posGeneracion.z)))
                    tilemap.SetTile(posGeneracion, tileLateral[1]);

            }
        }

        #endregion

    }

    /// <summary>
    /// Se eliminan todas las tiles anteriores
    /// </summary>
    public void clearTiles()
    {

        tilemap.ClearAllTiles();
    }

    /// <summary>
    /// Pinta el Tile del TD
    /// </summary>
    private void creacionDeTD()
    {

        Vector3Int pos = grid.WorldToCell(player.transform.position);
        pos.x = 0;

        if (posGeneracion.y - pos.y < maxRendered)
        {

            posGeneracion.y++;

            if (!tilemap.GetTile(new Vector3Int(posGeneracion.x, posGeneracion.y + 1, posGeneracion.z)))
            {
                //Pinta el TD
                tilemap.SetTile(posGeneracion, tileTD[Random.Range(0, tileTD.Length)]);
                //Elimina los renderizados anteriores
                tilemap.SetTile(new Vector3Int(posGeneracion.x, posGeneracion.y - 4, posGeneracion.z), null);

            }


        }


    }

    /// <summary>
    /// Pinta el tile lateral
    /// </summary>
    /// <param name="a"> 0 = izquierda // 1 = derecha</param>
    private void creacionTilesLateral(int a)
    {

        Vector3Int pos = grid.WorldToCell(player.transform.position);
        pos.x = 0;

        if (posGeneracion.y - pos.y < maxRendered)
        {

            posGeneracion.y++;
            if (!tilemap.GetTile(new Vector3Int(posGeneracion.x, posGeneracion.y + 1, posGeneracion.z)))
            {
                //Pinta el tile correspondiente
                tilemap.SetTile(posGeneracion, tileLateral[a]);
                //Elimina tiles anteriores
                tilemap.SetTile(new Vector3Int(posGeneracion.x, posGeneracion.y - 4, posGeneracion.z), null);
            }
        }

    }

    /// <summary>
    /// Pinta el tiles del campo
    /// </summary>
    private void creacionTilesCampo()
    {

        Vector3Int pos = grid.WorldToCell(player.transform.position);
        pos.x = 0;

        if (posGeneracion.y - pos.y < maxRendered)
        {

            posGeneracion.y++;
            if (!tilemap.GetTile(new Vector3Int(posGeneracion.x, posGeneracion.y + 1, posGeneracion.z)))
            {
                //Pinta tile correspondiente
                tilemap.SetTile(posGeneracion, tileCampo[Random.Range(0, tileCampo.Length)]);
                //Elimina render de tiles anteriores
                tilemap.SetTile(new Vector3Int(posGeneracion.x, posGeneracion.y - 4, posGeneracion.z), null);
            }

        }



    }

    /// <summary>
    /// Funcion que se lanza al iniciar el script
    /// </summary>
    void Start()
    {


        //Se accede al jugador
        player = GameObject.FindGameObjectWithTag("Player");

        //Se accede al componente Tilemap
        tilemap = GetComponent<Tilemap>();

        //Se guarda el componente grid
        grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<Grid>();

        //clearTiles();
        generacionInicial();


    }

    /// <summary>
    /// Funcion que se lanza cada Frame
    /// </summary>
    void Update()
    {


        if (tipoDeTiles == tipoDeTile.campo)
        {
            if (grid.WorldToCell(player.transform.position).y % 8 == 0 && grid.WorldToCell(player.transform.position).y != 0)
                creacionDeTD();
            else
                creacionTilesCampo();
        }
        else if (tipoDeTiles == tipoDeTile.lateralIzq)
            creacionTilesLateral(0);
        else if (tipoDeTiles == tipoDeTile.lateralDech)
            creacionTilesLateral(1);

    }

}
