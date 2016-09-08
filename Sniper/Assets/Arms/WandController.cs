﻿using UnityEngine;
using System.Collections;
 using System.Collections.Generic;

public class WandController : MonoBehaviour {
    private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
        private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
        private SteamVR_TrackedObject trackedObj;
  
     HashSet<InteractableItem> objectsHoveringOver = new HashSet<InteractableItem>();
     private InteractableItem closestItem;
     private InteractableItem interactingItem;

        // Use this for initialization
        void Start() {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
          }
  
        void update() { 
         if (controller.GetPressDown(gripButton)) {
             float minDistance = float.MaxValue;
 
             float distance;
             foreach (InteractableItem item in objectsHoveringOver) {
                 distance = (item.transform.position - transform.position).sqrMagnitude;
 
                 if (distance<minDistance) {
                     minDistance = distance;
                     closestItem = item;
                 }
             }
 
             interactingItem = closestItem;
 
             if (interactingItem) {
                 if (interactingItem.IsInteracting()) {
                     interactingItem.EndInteraction(this);
                 }
 
                 interactingItem.BeginInteraction(this);
             }
         }

 
         if (controller.GetPressUp(gripButton) && interactingItem != null) {
             interactingItem.EndInteraction(this);
}
  	}
  
      private void OnTriggerEnter(Collider collider) {
    
    InteractableItem collidedItem = collider.GetComponent<InteractableItem>();
            if (collidedItem) {
        objectsHoveringOver.Add(collidedItem);
                }
}

private void OnTriggerExit(Collider collider) {
        InteractableItem collidedItem = collider.GetComponent<InteractableItem>();
            if (collidedItem) {
        objectsHoveringOver.Remove(collidedItem);
                }
}
  } 