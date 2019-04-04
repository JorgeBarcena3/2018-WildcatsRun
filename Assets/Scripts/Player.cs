using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Relativo al gameobject player
/// </summary>
public class Player : MonoBehaviour
{

    /// <summary>
    /// Determina si el raton esta sobre el jugador
    /// </summary>
    private bool tocandoElJugador = false;

    /// <summary>
    /// Determina si el juego esta activo o no
    /// </summary>
    private bool seAcabo = true;

    /// <summary>
    /// Maximo de X e Y donde se permite el movimiento
    /// </summary>
    public Vector2 maxXAndY;

    /// <summary>
    /// Minimo de X e Y donde se permite el movimiento
    /// </summary>
    public Vector2 minXAndY;

    /// <summary>
    /// El jugador esta haciendo touch en la pantalla
    /// </summary>
    /// <returns>true = si</returns>
    public bool getPress()
    {

        return (Input.GetKey(KeyCode.Mouse0));
    }

    /// <summary>
    /// El jugador real esta tocando con el dedo/raton al jugador
    /// </summary>
    private void OnMouseOver()
    {
        //Activamos el buleano que determina si el raton esta sobre el objeto
        tocandoElJugador = true;

    }

    /// <summary>
    /// Distancia recorrida
    /// </summary>
    /// <returns>Cantidad de Y recorrida</returns>
    public string getDistancia()
    {
        return transform.position.y.ToString();

    }

    /// <summary>
    /// Devuelve varialbe "SeAcabo"
    /// </summary>
    /// <returns></returns>
    public bool GetSeAcabo()
    {
        return seAcabo;

    }

    /// <summary>
    /// Establece la varialbe "SeAcabo"
    /// </summary>
    /// <param name="a">Valor a establecer</param>
    public void setSeAcabo(bool a)
    {
        seAcabo = a;

    }

    /// <summary>
    /// Si se choca con enemigo
    /// </summary>
    /// <param name="collision">Elemento colisionador</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Final del juego
        if (collision.gameObject.tag == "Enemigo")
            seAcabo = true;
    }

    /// <summary>
    /// Funcion que se lanza cada Frame
    /// </summary>
    void Update()
    {
        
        //Si el jugador se encuentra jugando
        if (!seAcabo)
        {
            if (Input.GetKey(KeyCode.Mouse0) && tocandoElJugador)
            {

                //Convertimos la posicion a una posicion relativa al world
                Vector2 touch = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                //Respeta los limites de x e y
                touch.x = Mathf.Clamp(touch.x, minXAndY.x, maxXAndY.x);
                touch.y = Mathf.Clamp(touch.y, minXAndY.y, maxXAndY.y);

                //Aplicamos la poscion "posFinal" al jugador
                transform.position = new Vector3(touch.x, touch.y, -1);

            }
            else
                //La posicion correcta se restablece
                tocandoElJugador = false;

        }
        else
        {
            tocandoElJugador = false;
        }
    }
}
