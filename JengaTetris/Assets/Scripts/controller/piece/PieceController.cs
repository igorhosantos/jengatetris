using System.Collections.Generic;
using Assets.Scripts.engine.piece;
using UnityEngine;

namespace Assets.Scripts.controller.piece
{
    public class PieceController:Singleton<PieceController>
    {
        private Dictionary<Piece, GameObject> pool;

        public void StartPool()
        {
            GameObject[] gb = Resources.LoadAll<GameObject>("Prefab/Pieces");
            pool = new Dictionary<Piece, GameObject>();

            foreach (var t in gb)
                pool.Add(t.GetComponent<Piece>(),t);
           
        }

        public List<Piece> GetAllPieces()
        {
            List<Piece> lp = new List<Piece>();
            foreach (var keyValuePair in pool)
            {
                lp.Add(keyValuePair.Key);
            }

            return lp;
        }


        public GameObject RetrievePiece(Piece p)
        {
            return pool[p];
        }

        public Piece RetrievePieceById(int id)
        {
            foreach (var keyValuePair in pool)
            {
                if (keyValuePair.Key.id == id) return keyValuePair.Key;
            }

            return null;
        }
    }
}
