using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody rb;

    public float speed = 1;
    public float rotationSpeed = 1;
    public float maxRotation = 30f;
    private Transform parts;
    private float timeBetweenMoveSound;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        parts = transform.Find("Parts");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 rotate =  new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        this.transform.position += move * speed * Time.deltaTime;
        Vector3 newRotation = new Vector3(
            Mathf.Min(maxRotation, parts.transform.rotation.z + rotate.z * rotationSpeed),
            0f, 
            Mathf.Min(maxRotation, parts.transform.rotation.x - rotate.x * rotationSpeed)
            );
        parts.localRotation = Quaternion.Euler(newRotation);
        //rb.AddForce(move * speed);

        if (move.x != 0 || move.y != 0 || move.z != 0)
        {
            // Play move sound
            AudioSource MoveAudio = GameObject.Find("Audio").GetComponent<audioSources>().sources[2];
            if (!MoveAudio.isPlaying){
                MoveAudio.Play();
            }
            
        }



    }
}
