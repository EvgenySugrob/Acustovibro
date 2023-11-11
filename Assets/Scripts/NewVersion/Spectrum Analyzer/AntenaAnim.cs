using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntenaAnim : MonoBehaviour
{
    [SerializeField] Animator antenaAnim;

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

    public void AntenaOn()
    {
        antenaAnim.SetBool("isAcust", true);

    }
    public void AntenaOff()
    {
        antenaAnim.SetBool("isAcust", false);
    }

    public void Diactivate()
    {
        transform.localPosition = startPos;
    }
}
