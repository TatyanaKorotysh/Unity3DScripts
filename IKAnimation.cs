using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKAnimation : MonoBehaviour
{
    Animator anim; 
    bool interact; // указывает, происходит ли взаимодействие
    Vector3 positionForIК; // позиция объекта для взаимодействия
    float weight = 0f;

    void Start() { anim = GetComponent<Animator>(); }

    void OnAnimatorIK() 
    {
        if (interact)
        {
            if (weight < 1) weight += 0.01f;
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, weight);
            anim.SetIKPosition(AvatarIKGoal.RightHand, positionForIК);
            anim.SetLookAtWeight(weight);
            anim.SetLookAtPosition(positionForIК); //указываем куда нужно смотреть
        }
        else if (weight > 0) 
        {
            weight -= 0.02f; 
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, weight);
            anim.SetIKPosition(AvatarIKGoal.RightHand, positionForIК);
            anim.SetLookAtWeight(weight);
            anim.SetLookAtPosition(positionForIК);
        }
    }    

    public void StartInteraction(Vector3 pos)
    {
        positionForIК = pos;
        interact = true;
    }
    public void StopInteraction() {
        interact = false;
    }
}
