using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.engine.piece
{
    public class Piece:MonoBehaviour
    {
        [SerializeField] public int id;
        [SerializeField] private float dropChance;
        [SerializeField] private bool isPowerUp;


        public override bool Equals(object other)
        {
            return id == (other as Piece).id;
        }
    }
}
