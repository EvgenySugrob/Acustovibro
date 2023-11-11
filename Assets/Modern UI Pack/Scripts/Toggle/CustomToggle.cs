using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ColorUtility = UnityEngine.ColorUtility;

namespace Michsky.MUIP
{
    [RequireComponent(typeof(Toggle))]
    [RequireComponent(typeof(Animator))]

    public class CustomToggle : MonoBehaviour
    {
        [HideInInspector] public Toggle toggleObject;
        [HideInInspector] public Animator toggleAnimator;

        [Header("Settings")]
        public bool invokeOnAwake;
 
        void Awake()
        {
            if (toggleObject == null) 
            { 
                toggleObject = gameObject.GetComponent<Toggle>(); 
            }
            if (toggleAnimator == null) 
            { 
                toggleAnimator = toggleObject.GetComponent<Animator>(); 
            }

            toggleObject.onValueChanged.AddListener(UpdateStateDynamic);
            UpdateState();           

            if (invokeOnAwake == true) 
            { 
                toggleObject.onValueChanged.Invoke(toggleObject.isOn); 
            }
        }

        public void UpdateState()
        {
            StopCoroutine("DisableAnimator");
            toggleAnimator.enabled = true;

            if (toggleObject.isOn) 
            { 
                toggleAnimator.Play("On Instant"); 
            }
            else
            {
                toggleAnimator.Play("Off Instant");
            }
            StartCoroutine("DisableAnimator");
        }

        public void UpdateStateDynamic(bool value)
        {
            StopCoroutine("DisableAnimator");
            toggleAnimator.enabled = true;

            if (toggleObject.isOn) 
            { 
                toggleAnimator.Play("Toggle On"); 
            }
            else 
            { 
                toggleAnimator.Play("Toggle Off"); 
            }

            StartCoroutine("DisableAnimator");
        }

        IEnumerator DisableAnimator()
        {
            yield return new WaitForSeconds(0.6f);
            toggleAnimator.enabled = false;
        }


        //Use this method and its overloads for toggle interactable, else wouldn't working visualize disabling.
        //This is because you cannot subscribe to the standard interactable flag. 
        public void ToggleInteractable()
        {
            ToggleInteractable(!toggleObject.interactable);
        }
        
        public void ToggleInteractable(bool interactable)
        {
            toggleObject.interactable = interactable;

            StopCoroutine("DisableAnimator");
            toggleAnimator.enabled = true;

            if (toggleObject.interactable)
            {
                if (toggleObject.isOn)
                {
                    toggleAnimator.Play("On Instant");
                }
                else
                {
                    toggleAnimator.Play("Off Instant");
                }
            }
            else
            {
                if (toggleObject.isOn)
                {
                    toggleAnimator.Play("Disabled On");
                }
                else
                {
                    toggleAnimator.Play("Disabled Off");
                }
            }
            StartCoroutine("DisableAnimator");
        }
    }
}