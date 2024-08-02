using System;
using UnityEngine;
using UnityEngine.UI;

namespace Framework.Buttons
{
    [RequireComponent(typeof(Button))]
    public abstract class FunctionButton : MonoBehaviour
    {
        protected virtual void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        protected virtual void OnDestroy()
        {
            GetComponent<Button>().onClick.RemoveListener(OnClick);
        }

        protected abstract void OnClick();
    }
}