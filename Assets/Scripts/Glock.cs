using UnityEngine;

[System.Serializable]

public class Glock : MonoBehaviour
{

    public string model = "Glock";
    public int firemode = 0; //0 = semi 1 = auto
    public int damage = 10;
    public int mag = 18;
    public int currentAmmo;
    public float reloadTime = 1f;
    public float range = 100f;
    public float force = 30f;
    public float firerateauto = 14f;
    public float firerate = 5f;
    public AudioSource firesound;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;



}
