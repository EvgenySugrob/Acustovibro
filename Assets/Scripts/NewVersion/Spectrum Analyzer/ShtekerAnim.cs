using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShtekerAnim : MonoBehaviour
{
    [SerializeField] Animator shtekerAnim;

    [SerializeField] List<GameObject> buttonAssistanceList;
    [SerializeField] Vector3 startPos;

    private void Start()
    {
        startPos = transform.localPosition;
    }

    public void DisableMainFunction()
    {
        foreach (GameObject button in buttonAssistanceList) 
        {
            button.SetActive(false);
        }
    }

    public void EnableMainFunction()
    {
        foreach (GameObject button in buttonAssistanceList)
        {
            button.SetActive(true);
        }
    }

    public void ShtekerOn()
    {
        shtekerAnim.SetBool("isVibro", true);

    }
    public void ShtekerOff()
    {
        shtekerAnim.SetBool("isVibro", false);
 
    }

    public void Diactivate()
    {
        transform.localPosition = startPos;
    }
}
