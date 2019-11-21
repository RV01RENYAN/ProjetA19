﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCasting : MonoBehaviour
{
    private float distanceToObj;    // Distance entre le personnage et l'objet saisi
    private Rigidbody attachedObject;   // Objet saisi, null si aucun objet saisi

    public const int RAYCASTLENGTH = 100;   // Longueur du rayon issu de la caméra


    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = new Vector2(16, 16);   // Offset du centre du curseur
    public Texture2D cursorOff, cursorDragged, cursorDraggable; // Textures à appliquer aux curseurs

    void Start()
    {
        distanceToObj = -1;
        Cursor.SetCursor(cursorOff, hotSpot, cursorMode);
        Cursor.visible = true;
    }

    void Update()
    {
        // Le raycast attache un objet cliqué
        RaycastHit hitInfo;
        Ray ray = GetComponentInChildren<Camera>().ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * RAYCASTLENGTH, Color.blue);
        bool rayCasted = Physics.Raycast(ray, out hitInfo, RAYCASTLENGTH);

        if (rayCasted)
        {
            rayCasted = hitInfo.transform.CompareTag("Draggable") || hitInfo.transform.CompareTag("Fish") || hitInfo.transform.CompareTag("Skewer");
        }
        // rayCasted est true si un objet possédant le tag draggable est détécté

        if (Input.GetMouseButtonDown(0))    // L'utilisateur vient de cliquer
        {
            if (rayCasted)
            {
                Debug.Log("Object attached");
                attachedObject = hitInfo.rigidbody;
                attachedObject.isKinematic = true;
                distanceToObj = hitInfo.distance;
                Cursor.SetCursor(cursorDragged, hotSpot, cursorMode);
            }
        }

        else if (Input.GetMouseButtonUp(0) && attachedObject != null)   // L'utilisateur relache un objet saisi
        {
            attachedObject.isKinematic = false;
            attachedObject = null;
            Debug.Log("Object detached");
            if (rayCasted)
            {
                Cursor.SetCursor(cursorDraggable, hotSpot, cursorMode);
            }
            else
            {
                Cursor.SetCursor(cursorOff, hotSpot, cursorMode);
            }
        }

        if (Input.GetMouseButton(0) && attachedObject != null) // L'utilisateur continue la saisie d'un objet
        {
            attachedObject.MovePosition(ray.origin + (ray.direction * distanceToObj));
        }

        else  // L'utilisateur bouge la sourie sans cliquer 
        {
            if (rayCasted)
            {
                Cursor.SetCursor(cursorDraggable, hotSpot, cursorMode);
            }
            else
            {
                Cursor.SetCursor(cursorOff, hotSpot, cursorMode);
            }
        }
    }
}
