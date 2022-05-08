using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceFlagScript : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] GameObject incorrectPlacementText;

    Vector3 position;
    Plane field = new Plane(Vector3.up, 0);

    bool buttonClicked = false;

    private void Update()
    {
        if (buttonClicked)
        {
            if (Input.GetMouseButtonDown(0)) {
                float distance;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (field.Raycast(ray, out distance))
                {
                    position = ray.GetPoint(distance);
                }
                Debug.Log("Position: " + position);

                if (CheckCorrectPlacement())
                {
                    //postaw flagê
                }
                else
                {
                    incorrectPlacementText.SetActive(true);
                }

                buttonClicked = false;
            }
        }
    }
    public void PlaceFlag()
    {
        incorrectPlacementText.SetActive(false);
        buttonClicked = true;
    }

    public bool CheckCorrectPlacement()
    {
        return true;
    }
}
