using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tireR_fleche : MonoBehaviour
{
    public GameObject fleche;
    public mouvement_joueur scriptMouvementJoueur;
    private Vector3 dernierMouvement;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            GameObject inst = Instantiate(fleche, transform.position, Quaternion.identity);

            int direction = scriptMouvementJoueur.getDirection();
            inst.transform.Rotate(Vector3.forward, direction * 90.0f);
            // inst.transform.Rotate(Vector3.forward, 270.0f); //3 x 90
        }
    }
}
