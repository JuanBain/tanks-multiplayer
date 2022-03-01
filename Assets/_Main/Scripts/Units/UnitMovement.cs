using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Mirror;


public class UnitMovement : NetworkBehaviour
{
   [SerializeField] private NavMeshAgent agent = null;

    #region Server

    [Command]
    public void CmdMove(Vector3 position)
    {
        Debug.Log("CmdMove");
        if (!NavMesh.SamplePosition(position, out NavMeshHit hit, 1f, NavMesh.AllAreas)) { return; }
       
        agent.SetDestination(hit.position);
    }

    #endregion

    #region Client

    //public override void OnStartAuthority()
    //{
    //    maincamera = Camera.main;
    //}

    //[ClientCallback]

    //private void Update()
    //{
    //    if (!hasAuthority) { return; }

    //    if (!Input.GetMouseButtonDown(1)) { return; }

    //    Ray ray = maincamera.ScreenPointToRay(Input.mousePosition);

    //    if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity)) { return; }

    //    CmdMove(hit.point);
    //}

    #endregion
}
