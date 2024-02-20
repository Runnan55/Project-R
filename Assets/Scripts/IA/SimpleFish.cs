using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFish : MonoBehaviour
{

    public int rutina;
    public float contador;
    //public Animator animator;
    public Quaternion angulo;
    public float grado;

    public GameObject target;
    public bool huyendo = false;

    // Start is called before the first frame update
    void Start()
    {
        //  animator = GetComponent<Animator>();
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        comportamiento();
    }

    public void comportamiento()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > 5)
        {
            //animator.SetBool("run", false);


            contador += 1 * Time.deltaTime;
            if (contador >= 4)
            {
                rutina = Random.Range(0, 2);
                contador = 0;
            }

            switch (rutina)
            {
                case 0:
                    //animator.SetBool("walk", false);
                    break;
                case 1: //determina direccion de desplazamiento
                    grado = Random.Range(0, 360);
                    angulo = Quaternion.Euler(0, grado, 0);
                    rutina++;
                    break;
                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                    transform.Translate(Vector3.forward * 2 * Time.deltaTime);
                    //animator.SetBool("walk", true);
                    break;

            }
        }
        else
        {
            if (Vector3.Distance(transform.position, target.transform.position) > 1 && !huyendo)
            {
                var lookPos = transform.position - target.transform.position; 
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 3);
                transform.Translate(Vector3.forward * 4 * Time.deltaTime);

            }
            else
            {
                //animator.SetBool("walk", false);
                //animator.SetBool("run", false);

                //animator.SetBool("attack", true);
                huyendo = true;
            }
        }
    }

    public void FinalAtaque()
    {
        //animator.SetBool("attack", false);
        huyendo = false;

    }


}
