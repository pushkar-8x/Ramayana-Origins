using MalbersAnimations.Conditions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] float Speed;
    [SerializeField] int Damage;
    [SerializeField] float knockback;

   
    private void Update()
    {
        
        transform.Translate(Vector3.forward * Speed * Time.deltaTime );
        
        Destroy(this.gameObject, 3f);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Enemies") return;
        if(other.gameObject.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(Damage);

            if (other.TryGetComponent<ForceReceiver>(out ForceReceiver forceReceiver))
            {

                forceReceiver.AddForce(transform.forward * knockback);
            }

            Destroy(this.gameObject);
            return;

        }
       
    }
}
