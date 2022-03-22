using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Bullet : MonoBehaviour
{

    public TextMeshProUGUI explosiveButtonText;
    public bool explosiveAmmo;
    public GameObject collisionExplosion;

    public void EnableExplosion(){
        
        if(explosiveAmmo)
            explosiveButtonText.SetText("Explosive Gun");
        else
            explosiveButtonText.SetText("Shotgun");
        explosiveAmmo = !explosiveAmmo;
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if(explosiveAmmo && collision.gameObject.tag != "Player"){
            //Debug.Log("lelele");
            GameObject explosion = (GameObject)Instantiate(collisionExplosion, transform.position,transform.rotation);
            Destroy(explosion,2.0f);
            if(collision.gameObject.tag != "Wall")
                Destroy(collision.gameObject); //explosive gun mermisi duvarlar hariç objeleri yok eder
            return;
        }
           
    }
}
