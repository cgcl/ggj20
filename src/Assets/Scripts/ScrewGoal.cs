using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewGoal : MonoBehaviour
{
    public float screwHP = 60f;
    public bool isScrewed = true;
    
    public float unscrewSpeed = 3f;
    public void Unscrew()
    {
        screwHP -= (Time.deltaTime * unscrewSpeed);
        transform.Rotate(0f, 0f ,Time.deltaTime*unscrewSpeed);
        if(screwHP <= 0)
        {
            isScrewed = false;
            FinishScrewing();
        }
    }
    void FinishScrewing()
    {
        StartCoroutine("AnimateUnscrew");
    }

    public IEnumerator AnimateUnscrew()
    {
        yield return new WaitForEndOfFrame();
        //TODO RODAR ANIMACAO DE PARAFUSO
        yield return new WaitForSeconds(1f);
        

    }
}
