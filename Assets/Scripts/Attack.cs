using UnityEngine;

public class Attack : MonoBehaviour
{
    public int attackDamage = 20;
    public Vector2 knockback = Vector2.zero;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null) //check if it can be hit (if it has damageable component)
        {
            Vector2 deliveredKnockback = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y); // if parent facing left localscale, knockback inverts x value so it faces left

            bool gotHit =  damageable.Hit(attackDamage, deliveredKnockback);

            if (gotHit)
            {
                Debug.Log(collision.name + " hit for " + attackDamage);
            }
        }
    }
}
