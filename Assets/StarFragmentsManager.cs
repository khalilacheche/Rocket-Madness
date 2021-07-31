using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarFragmentsManager : MonoBehaviour
{
    public GameObject leftPrefab;
    public GameObject rightPrefab;
    public GameObject middlePrefab;
    public GameObject singlePrefab;
    public GameObject fragmentPrefab;

    private GameObject[] placeholders = new GameObject[0];
    private List<GameObject> starFragments =  new List<GameObject>();

    private int numberOfFragments = 0;
    private int currentFragments = 0;
    private const float LEFT_MARGIN = 60f;
    private const float RIGHT_MARGIN = 30f;
    private const float MIDDLE_MARGIN = 30f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void initialize(int n)
    {
        
        if(n < 1){
            return;
        }
        EmpyOut();
        numberOfFragments = n;
        placeholders = new GameObject[n];
        float position = 0;
        if (n == 1){
            //Instantiate Single
            placeholders[0] = Instantiate(singlePrefab, new Vector3(0, 0, 0), Quaternion.identity, transform);
            placeholders[0].GetComponent<RectTransform>().anchoredPosition = new Vector3(position, 0, 0);
            return;
        }
        //Instantiate Left
        placeholders[0] = Instantiate(leftPrefab,Vector3.zero,Quaternion.Euler(0, 0, 180), transform);
        placeholders[0].GetComponent<RectTransform>().anchoredPosition = new Vector3(position, 0, 0);
        position += LEFT_MARGIN;
        
        n -= 2;
        for (int i = 0; i < n; ++i){
            //Instantiate middle at pos x 
            placeholders[i+1] = Instantiate(middlePrefab, new Vector3(position,0,0), Quaternion.identity, transform);
            placeholders[i+1].GetComponent<RectTransform>().anchoredPosition = new Vector3(position, 0, 0);
            if (i != n-1){
                position += MIDDLE_MARGIN;
            }
        }
        position += RIGHT_MARGIN;
        //Instantiate Right
        placeholders[numberOfFragments-1] = Instantiate(rightPrefab, new Vector3(position, 0, 0), Quaternion.identity, transform);
        placeholders[numberOfFragments - 1].GetComponent<RectTransform>().anchoredPosition = new Vector3(position, 0, 0);
    }
    public void addFragment(){
        if (currentFragments >= numberOfFragments)
            return;
        float position = (currentFragments +1) * 30;
        /*if (currentFragments > 0){
            position = LEFT_MARGIN + currentFragments * MIDDLE_MARGIN + (((currentFragments + 1) == numberOfFragments) ? RIGHT_MARGIN : LEFT_MARGIN);
        }  */
        GameObject starFragment = Instantiate(fragmentPrefab, new Vector3(position, 0, 0), Quaternion.identity, transform);
        starFragment.GetComponent<RectTransform>().anchoredPosition = new Vector3(position, 0, 0);
        starFragments.Add(starFragment);
        currentFragments += 1;

    }
    public void EmpyOut(){
        for (int i = 0; i < placeholders.Length; ++i )
        {
            Destroy(placeholders[i]);
        }
        placeholders = new GameObject[0];
        foreach (GameObject go in starFragments ){
            Destroy(go);
        }
        starFragments.Clear();
        numberOfFragments = 0;
        currentFragments = 0;
    }
}
