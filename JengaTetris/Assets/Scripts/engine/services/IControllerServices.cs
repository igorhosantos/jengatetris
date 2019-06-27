using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.engine.services
{
    public interface IControllerServices
    {
        void NotifyMoveLeft();
        void NotifyMoveRight();
        void NotifyMoveDown();
        void NotifyRotateLeft();
        void NotifyRotateRight();
        void NotifyPause();
    }
}
