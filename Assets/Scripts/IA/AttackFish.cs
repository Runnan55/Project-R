using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackFish : MonoBehaviour
{

    public int rutina;
    public float contador;
    //public Animator animator;
    public Quaternion angulo;
    public float grado;
    public float speed = 1;
    public float maxSpeed = 2;
    public float distancia = 5;

    public bool atacando = false;
    public int damageAmount = 10;
    public GameObject target;
    private bool puedeMorder = true;
        public AudioSource fxAudioSource; 



    // Start is called before the first frame update
    void Start()
    {
        //  animator = GetComponent<Animator>();
        target = GameObject.Find("Player");
        fxAudioSource = GameManager.Instance.GetComponent<SoundManager>().fxAudioSource;

    }

    // Update is called once per frame
    void Update()
    {
        comportamiento();
    }

    public void comportamiento()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > distancia)
        {
            //animator.SetBool("run", false);


            contador += 1 * Time.deltaTime;
            if (contador >= 2)
            {
                rutina = Random.Range(0, 2);
                contador = 0;
                atacando = false;
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
                    transform.Translate(Vector3.forward * speed * Time.deltaTime);
                    //animator.SetBool("walk", true);
                    break;

            }
        }
        else
        {
            if (Vector3.Distance(transform.position, target.transform.position) > 1 && !atacando)
            {
                var lookPos = target.transform.position - transform.position;
                //lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 3);
                //animator.SetBool("walk", false);

                //animator.SetBool("run", true);
                transform.Translate(Vector3.forward * maxSpeed * Time.deltaTime);
                //animator.SetBool("attack", false);
                atacando = false;

            }
            else
            {
                //animator.SetBool("walk", false);
                //animator.SetBool("run", false);

                //animator.SetBool("attack", true);
                atacando = true;
            }
        }
    }

    public void FinalAtaque()
    {
        //animator.SetBool("attack", false);
        atacando = false;

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && puedeMorder)
        {
            Debug.Log("�Mordisco!");
            GameManager.Instance.GetComponent<SoundManager>().PlayFx(AudioFx.attack, fxAudioSource, false);


            puedeMorder = false;

            Invoke("ResetearCapacidadDeMorder", 0.5f);
        }
    }

    private void ResetearCapacidadDeMorder()
    {
        puedeMorder = true;
    }
}
