using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    private float range = 25f;
    public Inventory inventory;
    [SerializeField]
    private Camera cam;
    public PlayerDie PlayerDie;
    [SerializeField]
    private LayerMask mask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            RaycastHit _hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, range, mask))
            {
                Debug.Log("We hit " + _hit.collider.name);
            }
            else
                Debug.Log("Hit nothing");
            if (_hit.collider.name == "M4A1 Sopmod")
                inventory.M4A1 = true;
            if (_hit.collider.name == "Chili")
                PlayerDie.health += 50f;
        }
    }
}
