using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]



public class PlayerDie : MonoBehaviour
{
    public float health = 100f;
    public Text healthtext;
    public GameObject player;
    public Text warntext;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (health >= 101f)
            health = 100f;
        if (health <= 30f)
            warntext.text = "Heal by eating at the table";
        else
            warntext.text = "";
        if (health <= 0f)
            player.gameObject.SetActive(false);


        //give UI button to restart
        healthtext.text = health.ToString();
    }
}
