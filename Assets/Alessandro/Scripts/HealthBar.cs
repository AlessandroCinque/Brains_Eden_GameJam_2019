using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public bool GetDamage;
    [SerializeField] private Image bar;
   private float maxHP = 100f;
    public float HP;

    private bool collidingWithExplosion = false;
    public float collisionRadius = 1;
    public Transform playerPos;
    // Use this for initialization
    void Start ()
    {
        bar = GetComponent<Image>();
        HP = maxHP;
        GetDamage = false;
    }
  
	// Update is called once per frame
	void Update ()
    {
        float ratio = HP / maxHP;
        //bar.rectTransform.localScale = new Vector3(ratio, 1.0f, 1.0f);
        //print("Ratio is" + ratio);

        Vector3 vect3Pos = playerPos.position;
        Collider[] colliders = Physics.OverlapSphere(vect3Pos, collisionRadius);

        foreach (Collider hit in colliders)
        {
            if (hit.gameObject.tag == "Explosion")
            {
                collidingWithExplosion = true;
            }
        }

        //Debug.Log(collidingWithExplosion);

        if (collidingWithExplosion)
        {
            Debug.Log("Player Dead!"); // edit here
            GetDamage = true;
        }
        else { GetDamage = false; }

        bar.fillAmount = ratio;
        if (GetDamage)
        {
            Damage(5);
        }
    }
    private void Damage(float damage)
    {
        HP -= damage;
        if (HP <0)
        {
            HP = 0;
        }
        //==============
    }
}
