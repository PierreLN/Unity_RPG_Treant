using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chasseur : MonoBehaviour
{

    private Vector3 mouvement;
    public Animator anim;
    public GameObject cible; // Pas besoin de d�finir exactement c'est quoi la cible
    public GameObject explosion;
    public GameObject joueur;
    public float speed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        mouvement.z = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //Choisir le mouvement
        Vector3 direction = cible.transform.position - transform.position;
        mouvement = direction.normalized;

        // Regarder dans une direction, c'est souvent r�gl� par une soustraction de vecteur

        // Animation
        anim.SetFloat("Horizontal", mouvement.x);
        anim.SetFloat("Vertical", mouvement.y);
        anim.SetFloat("Speed", mouvement.sqrMagnitude);

        // walk-front_joueur = x : 0 y : -1
        // walk-back_joueur = x : 0 y : 1
        // walk-left = x : -1 y : 0
        // walk-right = x : 1 y : 0


    }

    private void FixedUpdate()
    {
        transform.position = transform.position + mouvement * speed * Time.fixedDeltaTime;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject);
        Instantiate(explosion, transform.position, Quaternion.identity);
    }
}
