using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouvement_joueur : MonoBehaviour
{
    public float speed = 5.0f; // 5 unit� par seconde
    private Vector3 mouvement;
    private Vector3 dernierMouvement;
    public Animator anim;
    private Rigidbody2D rig;


    public int getDirection()
    {
        int direction = 0;
        float petiteValeur = 0.001f;

        if (dernierMouvement.x < -petiteValeur) // fait face � gauche
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

        rig = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        // G�rer les entr�es
        mouvement.x = Input.GetAxisRaw("Horizontal"); // -1 � gauche, 1 � droite
        mouvement.y = Input.GetAxisRaw("Vertical"); // -1 en haut, 1 en bas
        mouvement.z = 0.0f; // just to be safe!
        // Le GetAxisRaw va direct de 0 � 1 au lieu d'y aller doucement

        if(mouvement.sqrMagnitude > 0.001f)
        {
            dernierMouvement = mouvement;
        }
        
        // G�rer les animations
        anim.SetFloat("Horizontal", mouvement.x);
        anim.SetFloat("Vertical", mouvement.y);
        anim.SetFloat("Speed", mouvement.sqrMagnitude);
        // sqrMagnitude -> donne la valeur au carr� pour m�nager la distance de calcul
        // La distance pr�cise entre deux objets est donn�e par magnitude
    }

    private void FixedUpdate()
    {
        // G�rer le mouvement
        // transform.position = transform.position + mouvement.normalized * Time.fixedDeltaTime * speed; //Time.fixedDeltaTime = 1/30 seconde


        rig.velocity = mouvement.normalized * speed;

    }
}
