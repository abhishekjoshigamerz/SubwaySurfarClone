using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
  
   
    void FixedUpdate() {
        
         transform.Rotate( 20 * Time.fixedDeltaTime,0,0);
    }
    
    
    
    private void OnTriggerEnter(Collider other) {
       
        if(other.tag == "Player")
        {
            GameManager.numberOfCoins += 1;
           
            Destroy(gameObject);
        }
    }
}
