using UnityEngine;

[System.Serializable]

public class M4A1 : MonoBehaviour
{

    public string model = "M4A1-S";
    public int firemode = 1; //0 = semi 1 = auto
    public int damage = 5;
    public int mag = 30;
    public int currentAmmo;
    public float reloadTime = 2f;
    public float range = 150f;
    public float force = 60f;
    public float firerateauto = 16f;
    public float firerate = 7f;
    public AudioSource firesound;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;



}
