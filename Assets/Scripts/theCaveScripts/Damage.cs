using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public int damage = 0;


    public int GetDamage()
    {
        return damage;
    }

    public void SetDamage(int value)
    {
        damage = value;
    } 
}
