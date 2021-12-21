﻿using UnityEngine;
using UnityEngine.EventSystems;

using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("3Go - UI")]
    [Tooltip("Call OnEnter on all UIViewControllers attached to a target UIView.")]
    public class UIViewControllerEnter : FsmStateAction
    {
        [RequiredField]
        [CheckForComponent(typeof(UIView))]
        [Tooltip("The GameObject with an UIView component.")]
        public FsmOwnerDefault gameObject;

        public override void Reset()
        {
            gameObject = null;
        }

        public override void OnEnter()
        {
            GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go != null)
            {
                UIView uiView = go.GetComponent<UIView>();
                if (uiView != null)
                {
                    OnEnterView(uiView.gameObject);
                }
            }

            Finish();
        }

        private void OnEnterView(GameObject i_Root)
        {
            if (i_Root == null)
                return;

            UIViewController[] uiViewControllers = i_Root.GetComponentsInChildren<UIViewController>(true);

            for (int viewControllerIndex = 0; viewControllerIndex < uiViewControllers.Length; ++viewControllerIndex)
            {
                UIViewController uiViewController = uiViewControllers[viewControllerIndex];
                uiViewController.OnEnter();
            }
        }
    }
}