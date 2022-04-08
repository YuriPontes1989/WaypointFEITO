using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollow : MonoBehaviour
{
    //definindo um array de gameobjects para armazenar os waypoints e uma variavel para armazenar o waypoint atual
    public GameObject[] waypoints;
    int currentWP = 0;
    //iniciando  a variavel da velocidade,precisão e a velocidade da rotação 
    float speed = 2.0f;
    float accuracy = 2.0f;
    float rotSpeed = 0.4f;
    // Start is called before the first frame update
    void Start()
    {
        //achar o gameobject que está com a tag waypoint
        waypoints = GameObject.FindGameObjectsWithTag("waypoint");
    }
    // Update is called once per frame
    void LateUpdate()
    {
        //se o tamanho do array waypoints for igual a zero  retorne
        if (waypoints.Length == 0) return;
        //criando um vector três  que indica  a posição x,y e z do waypoint atual
        Vector3 lookAtGoal = new Vector3(waypoints[currentWP].transform.position.x,
        this.transform.position.y,
        waypoints[currentWP].transform.position.z);
        //definindo a direção e rotação apartir do vetor anterior
        Vector3 direction = lookAtGoal - this.transform.position;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
        Quaternion.LookRotation(direction),
        Time.deltaTime * rotSpeed);
        //se o tamanho do vetor direção for menor que  a precisão
        if (direction.magnitude < accuracy)
        {
            // incrementamos   o valor de CWP em 1 indicando o proximo waypoint
            currentWP++;
            //se o valor  do WP atual for maior que o tamanho do array waypoint
            if (currentWP >= waypoints.Length)
            {
                //zeramos o valor de CWP
                currentWP = 0;
            }
        }
        //iniciamos o movimento  do objeto
        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
