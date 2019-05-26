using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GravitationalFieldAnimator : MonoBehaviour {
    public float duration;
    TweenParams tParms;
    float radius;
    // Use this for initialization
    void Start()
    {
        radius = transform.parent.GetChild(transform.GetSiblingIndex() + 1).GetComponent<CircleCollider2D>().radius;
        PlayAnimation();
    }
    // Update is called once per frame
        void Update () {
        

    }


    void PlayAnimation()
    {
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(transform.DOScale(Vector3.one / 2, duration)).Insert(0,GetComponent<SpriteRenderer>().DOFade(0.2f,duration)).SetEase(Ease.Linear).OnComplete(() => {

            transform.localScale = Vector3.one * radius /2 ;
            GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 1);
            PlayAnimation();
        });
    }
}
