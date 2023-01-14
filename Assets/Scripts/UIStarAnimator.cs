using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIStarAnimator : MonoBehaviour
{
    public List<GameObject> stars;
    public float duration;
    public int vibrato;
    public float elasticity;


    public bool gameCompleted;
    public bool allStars;
    public bool fullHealth;

    public bool animationComplete;
    // Start is called before the first frame update
    void Start()
    {
        ResetAnim();

    }

    public void StartAnimation(bool gameCompleted, bool allStarFragments, bool fullHealth)
    {
        bool[] states = { gameCompleted, fullHealth, allStarFragments };
        startChain(0, states);
    }
    public void startChain(int i,bool[]states)
    {
        if (i >= 3)
        {
            animationComplete = true;
            return;
        }
        if (states[i])
        {
            stars[i].SetActive(true);
            stars[i].transform.DOPunchScale(Vector3.one, duration, vibrato, elasticity).OnComplete(()=> startChain(i + 1, states));
            return;
        }
        startChain(i + 1,states);
        
    }
    public void ResetAnim()
    {
        animationComplete = false;
        foreach (GameObject star in stars)
        {
            star.SetActive(false);
        }
    }
}
