using UnityEngine;
using System.Collections;

namespace Assets.Scripts.engine.services
{
    public interface IPlayerControllerServices
    {
        void NotifyMoveLeft(short clientId);
        void NotifyMoveRight(short clientId);
        void NotifyMoveDown(short clientId);
        void NotifyRotateLeft(short clientId);
        void NotifyRotateRight(short clientId);
    }
}