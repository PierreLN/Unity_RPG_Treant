using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chasseur : MonoBehaviour
{

    private Vector3 rayon_normalized;
    public Animator anim;
    public GameObject cible; // Pas besoin de définir exactement c'est quoi la cible
    public GameObject explosion;
    public GameObject joueur;
    public float speed = 1.0f;
    private bool estActif = false;
    public float distanceVue = 1.0f;
    private Rigidbody2D rig;

    public LayerMask maskRayon;



    // Start is called before the first frame update
    void Start()
    {
        rayon_normalized.z = 0.0f;
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 depart = transform.position;
        Vector3 arrivee = cible.transform.position;
        float longueur_de_vue = 5.0f;
        //Choisir le mouvement
        Vector3 rayon = cible.transform.position - transform.position;
        rayon_normalized = rayon.normalized;

        Color couleur = Color.magenta;
        Color couleur_nope = Color.blue;
        Color couleur_meh = Color.yellow;

        Debug.DrawRay(depart, rayon_normalized * longueur_de_vue, couleur);

        Physics2D.Raycast(depart, rayon_normalized, distanceVue);
        RaycastHit2D hit = Physics2D.Raycast(depart, rayon_normalized, distanceVue, maskRayon);

        // Regarder dans une direction, c'est souvent réglé par une soustraction de vecteur
        if (hit.collider != null) 
        {
            Debug.DrawLine(depart, hit.point, couleur_meh);
            estActif = false;

            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("joueur"))
            {
                estActif = true;
                Debug.DrawRay(depart, rayon_normalized * longueur_de_vue, couleur);
            }
        }
        else 
        {
            Debug.DrawRay(depart, rayon_normalized * distanceVue, couleur_nope);
            estActif = false; 
        }

        if (estActif)
        {
            // Animation
            anim.SetFloat("Horizontal", rayon_normalized.x);
            anim.SetFloat("Vertical", rayon_normalized.y);
            anim.SetFloat("Speed", rayon_normalized.sqrMagnitude);

            // walk-front_joueur = x : 0 y : -1
            // walk-back_joueur = x : 0 y : 1
            // walk-left = x : -1 y : 0
            // walk-right = x : 1 y : 0
        }
        else
        {
            anim.SetFloat("Horizontal", 0);
            anim.SetFloat("Vertical", 0);
            anim.SetFloat("Speed", 0);
        }
    }

    private void FixedUpdate()
    {
        if (estActif)
        {
            rig.velocity =  rayon_normalized * speed;
        }
        else
        {
            rig.velocity = rayon_normalized * 0;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Destroy(this.gameObject);
        Instantiate(explosion, transform.position, Quaternion.identity);
    }
}
