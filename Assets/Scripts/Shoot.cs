using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Shoot : MonoBehaviour
{


   public Bullet bullet; 
    public GameObject[] bullets; //oyunun kasmaması adına mermileri daha sonra silmek için burada tutuyorum,
    public float shootForce;
    private Bullet currentBullet;
    public bool shotgunMode;  //true: shotgun, false: explosive gun
    int bulletsShot; //toplam atılan mermi
    private Animator animator;

    public Transform firePoint; //merminin çıktığı nokta

    public TextMeshProUGUI ammunitionDisplay; //bulletsShot'u ekranda gösteriyor

    private float randSpreadX,randSpreadZ; //rastgele mermi saçılma oranları

    public void Awake(){
        animator = GetComponentInChildren<Animator>();
        shotgunMode = true;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ShootBullet();
        }
        
        ammunitionDisplay.SetText("Bullets Shot:" + bulletsShot);
    }
    public void switchToExplosive(){
        shotgunMode = !shotgunMode;
    }
    private void ShootBullet()
    {
        Ray ray = new Ray(firePoint.position, firePoint.forward);


        Vector3 targetPoint = ray.GetPoint(2);

    
        Vector3 direction = targetPoint - firePoint.position;
        if(shotgunMode==true){
            for(int i=0;i<8;i++){
                randSpreadX = Random.Range(-0.2f,0.2f);
                randSpreadZ = Random.Range(-0.2f,0.2f);
                direction += new Vector3(randSpreadX,0,randSpreadZ);
                currentBullet = Instantiate(bullet, firePoint.position, Quaternion.identity);
                if(bullet.explosiveAmmo)
                    currentBullet.explosiveAmmo = true;
                currentBullet.GetComponent<Rigidbody>().AddForce(direction*shootForce , ForceMode.Impulse);
            }
            bulletsShot+=8;
        }
        else{
            
            currentBullet = Instantiate(bullet, firePoint.position, Quaternion.identity);
            currentBullet.gameObject.transform.localScale += new Vector3(0.4f,0.4f,0.4f); //explosive gun mermisi daha büyük
            currentBullet.GetComponent<Rigidbody>().AddForce(direction*shootForce, ForceMode.Impulse);
            bulletsShot+=1;
        }



        
        Invoke("DestroyBullet",5.0f); //3 saniye sonra mermileri sil
    }

 void DestroyBullet()
 {
      bullets = GameObject.FindGameObjectsWithTag ("bullet");
     
     for(var i = 1 ; i < bullets.Length ; i ++)
     {
         Destroy(bullets[i]);
     }
 }


  



}
