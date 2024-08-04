using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public int damage = 0;
    public int changer = 5;


    public int GetDamage()
    {
        int currendDamage = damage - (Random.Range(-changer,changer));
        return currendDamage;
    }

    public void SetDamage(int value)
    {
        damage = value;
    } 
}
