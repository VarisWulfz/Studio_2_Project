using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InventoryCheck : MonoBehaviour
{
    public GameObject Model;
    private Collider player;
    public FMODUnity.StudioEventEmitter modelSoundEmit;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player = other;
            Inventory testInv = other.GetComponent<Inventory>();

            if (testInv.inventory.Count >= 5)
            {
                Model.SetActive(true);
                modelSoundEmit.Play();
            }

        }
    }
}
