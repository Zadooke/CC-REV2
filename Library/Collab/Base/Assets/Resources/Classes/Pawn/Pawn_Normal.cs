﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn_Normal : Class
{
    public override float MovementSpeed
    {
        get
        {
            return 250;
        }
    }
    public override float AttackSpeed
    {
        get
        {
            return 0.25f;
        }
    }
    public override float AttackDuration
    {
        get
        {
            return 0.5f;
        }
    }


    public override void Attack(Vector2 direction)
    {
        base.Attack(direction);

        // Verifica qual das areas de ataque está no angulo da direção
        foreach (var area in _attackAreas)
        {
            var areaPos = area.localPosition;
            var angle = Vector2.Angle(direction, areaPos);

            Debug.DrawRay(transform.position, direction, Color.red);

            if (direction.magnitude < 0.7f)
            {
                area.GetComponent<AttackArea>().Selected = false;
            }

            if (angle <= 30)
            {
                area.GetComponent<AttackArea>().Selected = true;

                // Checa se pode atacar
                if (CanAttack)
                {
                    StartCoroutine(area.GetComponent<AttackArea>().CAttack());
                    CanAttack = false;
                }
            }
            else
            {
                area.GetComponent<AttackArea>().Selected = false;
            }
        }
    }

    void Start()
    {

        Type = CHESSPIECE.PAWN;
        // Carreaga a Area de Ataque
        this.AttackArea = Resources.Load<GameObject>("Classes/Pawn/NormalAttackArea");

        // Instancia a Area de Ataque
        _attackArea = Instantiate(AttackArea, transform, false);

        // Adiciona a Habilidade correspondente
        GetComponent<HabilityManager>().Hability = new InvincibleDash(this.GetComponent<Piece>());

        base.Start();
    }

    void Update()
    {
        base.Update();
    }
}
