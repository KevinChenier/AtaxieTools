using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
using System;

public class HandTrackingGrabber : OVRGrabber
{
    private OVRHand hand;
    public float pinchThreshold = 0.7f;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        hand = GetComponent<OVRHand>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        CheckIndexPinch();
    }

    private void CheckIndexPinch()
    {
        float pinchStrength = hand.GetFingerPinchStrength(OVRHand.HandFinger.Index);
        bool isPinching = pinchStrength > pinchThreshold;

        if (!m_grabbedObj && isPinching && m_grabCandidates.Count > 0)
        {
            OVRInput.SetControllerVibration(0.4f, 0.4f, OVRInput.Controller.RTouch);
            GrabBegin();
        }
        else if (m_grabbedObj && !isPinching)
            GrabEnd();
    }
}
