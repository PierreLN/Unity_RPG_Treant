using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouvement_fleche : MonoBehaviour
{
    public float speed = 8.0f;
    private Rigidbody2D rig;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        rig.velocity = transform.right * speed;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //transform.position = transform.position + transform.right * Time.fixedDeltaTime * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject); // Important sinon on détruit le script!
    }
}
