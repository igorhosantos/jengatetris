using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.engine.piece
{
    public class Piece:NetworkBehaviour
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
