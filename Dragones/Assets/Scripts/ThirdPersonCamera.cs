using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Vector3 offset; //Distancia de la camara y el jugador
    private Transform target; //Target es a donde apunta, en este caso es al jugador
    [Range(0,1)]public float lerpValue;
    //Rango
    public float sensibilidad;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").transform; //Find busca una etiqueta en nuestra escena
    }

    void LateUpdate() //Es lo ultimo que se ejecuta
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, lerpValue);
        //.lerp mueve la posicion del objeto de un vector a otro
        // lerpValue dice a unity como de rapido tiene que pasar de una posicion a otra 
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X")* sensibilidad, Vector3.up)* offset; //AngleAxis gira algo con respecto al eje
        
        transform.LookAt(target);
    }

}
