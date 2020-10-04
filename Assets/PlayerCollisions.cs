using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    public float forceSpeed = 10f;
    public gameManager gm;
    public AudioSource explosionAudio;

    // Start is called before the first frame update
    void Start()
    {
        if (gm == null){
            gm = GameObject.Find("GameManager").GetComponent<gameManager>();
        }
        if (explosionAudio == null)
        {
            explosionAudio = GameObject.Find("Audio").GetComponent<audioSources>().sources[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Asteroid"))
        {
            //Debug.Log("Impact!");
            // Vector3 impactPoint = wall.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
            //ContactPoint contact = collision.contacts[0];
            //Debug.Log(contact);
            //Vector3 direction = (wall.transform.position - impactPoint).normalized;
            //Vector3 direction = -this.gameObject.transform.position;
            //Debug.Log("direction ="+direction);
            //this.GetComponent<Rigidbody>().AddForce(forceSpeed * direction,ForceMode.Impulse);
            //this.gameObject.GetComponent<Rigidbody>().AddExplosionForce(forceSpeed, impactPoint, 10f);
            Debug.Log("GAME OVER");
            // Play sound
            explosionAudio.Play();

            gm.restartLevel();
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Debug.Log("GAME OVER");
            // Play sound
            AudioSource zapAudio = GameObject.Find("Audio").GetComponent<audioSources>().sources[5];
            zapAudio.Play();

            gm.restartLevel();
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Exit"))
        {
            AudioSource warpAudio = GameObject.Find("Audio").GetComponent<audioSources>().sources[4];
            warpAudio.Play();
            gm.nextLevel();
        }

        }

}
