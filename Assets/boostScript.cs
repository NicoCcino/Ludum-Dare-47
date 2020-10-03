using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boostScript : MonoBehaviour
{
    public string boostType;
    public LoopPoints loopScript;

    public float enlargeAmount = 10f;
    public float reduceSpeedAmount = 10f;

    public GameObject invisibilityUI;

    // Start is called before the first frame update
    void Start()
    {
        if (loopScript == null)
        {
            loopScript = GameObject.FindObjectOfType<LoopPoints>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Collision with boost");

        if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // give boost
            Debug.Log("Boost!");
            if (boostType == "enlarge")
            {
                BoostEnlarge();
                Destroy(this.gameObject);
            }
            if (boostType == "invisibility")
            {
                BoostInvisibility();
            }

        }
        if (collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            // Destroy(this.gameObject);
        }
    }

    private void BoostEnlarge()
    {
        loopScript.enlargeBonus += enlargeAmount;
    }

    private void BoostReduceSpeed()
    {
        loopScript.shrinkSpeed -= reduceSpeedAmount;
    }

    private void BoostInvisibility()
    {
        invisibilityUI.SetActive(true);
        Destroy(this.gameObject);
    }
}
