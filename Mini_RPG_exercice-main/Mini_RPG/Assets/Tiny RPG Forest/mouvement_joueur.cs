using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouvement_joueur : MonoBehaviour
{
    public float speed = 5.0f; // 5 unité par seconde
    private Vector3 mouvement;
    private Vector3 dernierMouvement;
    public Animator anim;


    public int getDirection()
    {
        int direction = 0;
        float petiteValeur = 0.001f;

        if (dernierMouvement.x < -petiteValeur) // fait face à gauche
        {
            direction = 2;
        }
        else if (dernierMouvement.y > petiteValeur) // fait face avers le haut
        {
            direction = 1;
        }
        else if (dernierMouvement.y < -petiteValeur) // fait face vers le bas
        {
            direction = 3;
        }

        return direction;
    }

    public Vector3 GetDernierMouvement()
    {
        return dernierMouvement;
    }

    // Start is called before the first frame update
    void Start()
    {
        mouvement.z = 0.0f;
        dernierMouvement = new Vector3(0.0f, -1.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        // Gérer les entrées
        mouvement.x = Input.GetAxisRaw("Horizontal"); // -1 à gauche, 1 à droite
        mouvement.y = Input.GetAxisRaw("Vertical"); // -1 en haut, 1 en bas
        mouvement.z = 0.0f; // just to be safe!
        // Le GetAxisRaw va direct de 0 à 1 au lieu d'y aller doucement

        if(mouvement.sqrMagnitude > 0.001f)
        {
            dernierMouvement = mouvement;
        }
        
        // Gérer les animations
        anim.SetFloat("Horizontal", mouvement.x);
        anim.SetFloat("Vertical", mouvement.y);
        anim.SetFloat("Speed", mouvement.sqrMagnitude);
        // sqrMagnitude -> donne la valeur au carré pour ménager la distance de calcul
        // La distance précise entre deux objets est donnée par magnitude
    }

    private void FixedUpdate()
    {
        // Gérer le mouvement
        transform.position = transform.position + mouvement.normalized * Time.fixedDeltaTime * speed; //Time.fixedDeltaTime = 1/30 seconde
    }
}
