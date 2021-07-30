using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GravitationalFieldAnimator : MonoBehaviour {
    private const float CONVERSION_RATE = 0.4f;
    private const float MOONSCALE_TO_MASKSCALE = 2f/3;
    private const float DURATION_FACTOR = 30f;
    public float duration;
    TweenParams tParms;
    private float outerRadius;
    private Vector3 innerTargetScale = Vector3.one; // 1,1,1
    private Vector3 outerTargetScale;  
    private Vector3 maskScale; // = 2/3 * moon_scale

    // Use this for initialization
    void Start()
    {
        outerRadius = transform.parent.parent.GetChild(transform.parent.GetSiblingIndex() + 1).GetComponent<CircleCollider2D>().radius;
        Vector3 moonScale = transform.parent.parent.GetChild(0).transform.localScale;
        maskScale = (MOONSCALE_TO_MASKSCALE) * moonScale;
        transform.parent.transform.localScale = maskScale;
        outerTargetScale = Vector3.one * outerRadius * CONVERSION_RATE;
        outerTargetScale.x = outerTargetScale.x / maskScale.x;
        outerTargetScale.y = outerTargetScale.y / maskScale.y;
        float gravityScale = transform.parent.parent.GetChild(transform.parent.GetSiblingIndex() + 1).GetComponent<gravity>().gravityScale;
        duration = DURATION_FACTOR / Mathf.Abs(gravityScale);//Mathf.Sqrt(Mathf.Abs(gravityScale));
        PlayAnimation();
    }
    // Update is called once per frame
    void Update () {
        

    }


    void PlayAnimation()
    {
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(transform.DOScale(innerTargetScale, duration/2)).Insert(0, GetComponent<SpriteRenderer>().DOFade(0.2f, duration/2)).SetEase(Ease.Linear);
        mySequence.Append(transform.DOScale(outerTargetScale, duration/2)).Insert(duration/2, GetComponent<SpriteRenderer>().DOFade(1f, duration/2)).SetEase(Ease.Linear).OnComplete(() => {
            transform.localScale = outerTargetScale; //conversion rate between normal scale and radius of collider
            GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 1);
            PlayAnimation();
        });
    }
}
