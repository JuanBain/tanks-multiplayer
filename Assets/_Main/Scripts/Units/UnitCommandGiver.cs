using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCommandGiver : MonoBehaviour
{
    [SerializeField] private UnitselectionHandler unitselectionHandler = null;

    private Camera mainCamera;
    [SerializeField] private LayerMask layerMask = new LayerMask();

    private void Start()
    {
        mainCamera = Camera.main;

        GameOverHandler.ClientOnGameOver += ClientHandleGameOver;
        
    }

    private void OnDestroy()
    {
        GameOverHandler.ClientOnGameOver -= ClientHandleGameOver;
    }

    

    private void Update()
    {
        if (!Input.GetMouseButtonDown(1)) { return; }

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask)) { return; }

        if(hit.collider.TryGetComponent<Targetable>(out Targetable target))
        {
            if (target.hasAuthority)
            {
                TryMove(hit.point);
                return;
            }
            TryTarget(target);
            return;
        }

        TryMove(hit.point);

    }

    private void TryMove(Vector3 point)
    {
        foreach (Unit unit in unitselectionHandler.SelectedUnits)
        {
            unit.GetUnitMovement().CmdMove(point);
        }
    }
    private void TryTarget(Targetable target)
    {
        foreach (Unit unit in unitselectionHandler.SelectedUnits)
        {
            unit.GetTargeter().CmdSetTarget(target.gameObject);
        }
    }

    private void ClientHandleGameOver(string winnerName)
    {
        enabled = false;
    }
}
