using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vmaya.UI.Components;

namespace Vmaya.UI
{
    public class ExampleWindows : MonoBehaviour
    {
        public Window windowTemplate;
        public ModalWindow modalWindowTemplate;
        public void createWindow()
        {
            Instantiate(windowTemplate, transform);
        }

        public void createModal()
        {
            Instantiate(modalWindowTemplate, transform);
        }
    }
}
