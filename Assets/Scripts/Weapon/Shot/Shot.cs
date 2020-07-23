using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Shot
{

    public int HitDamage(int currentLive, int weaponDamage){
        return currentLive - weaponDamage; 
    }

    public void PlayAnimation(Animation animator, string animation){
        animator.Play(animation);
    } 

}
