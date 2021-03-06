﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPathState : State
{
   
    public override void Enter(Piece piece)
    {
        // _player_ref = GameManager.Instance._player;
        base.Enter(piece);

        main_script.SetColor(Color.red);

        main_script.Use_Path = true;
        unit.StartCoroutine(unit.RequestNewPathTo(player.transform.position));

    }
    public override void Execute(Piece piece)
    {

        //if((player.transform.position - main_script.transform.position).magnitude <= 6 )
        //{
        //    // player ja chegou perto 
        //    // muda de estado 
        //    main_script._StateMachine.ChangeState(new HuntState());
        //}

        //unit.StopCoroutine(unit.RequestNewPathTo(player.transform.position));

        unit.StopAllCoroutines();
        unit.StartCoroutine(unit.RequestNewPathTo(player.transform.position));
        //if (unit.FinishedPath())  
        //{
        //    // player ainda longe
        //    // persegue player
        //    unit.StartCoroutine(unit.RequestNewPathTo(player.transform.position));
        //}


        base.Execute(piece);
    }
    public override void Exit(Piece piece)
    {
        base.Exit(piece);
    }

    IEnumerator CPromove()
    {
        yield return new WaitForSeconds(1);
        PieceManager.Instance.PromoveEnemy(main_script);
    }


    bool CheckForAreaDangers()
    {
        var hits = Physics2D.RaycastAll(main_script.transform.position, main_script.EnemyDirPlayer(), 5);
        foreach (var hit in hits)
        {
            if (hit.collider.GetComponent<FloorSpike>() != null)
            {
                return true;
            }
        }
        return false;
    }



    //// será usada pelas classes filhas de follow
    protected bool PlayerIsRanged()
    {
        CHESSPIECE T = GameManager.Instance.Player.GetComponent<Player>().GetClass.Type;
        if (T == CHESSPIECE.QUEEN || T == CHESSPIECE.TOWER || T == CHESSPIECE.BISHOP)
            return true;

        return false;
    }

    //protected void CheckNearWalls()
    //{
    //    Vector3 cam = Camera.main.transform.position;
    //    Vector3 pos = main_script.transform.position;
    //    if()
    //}



}