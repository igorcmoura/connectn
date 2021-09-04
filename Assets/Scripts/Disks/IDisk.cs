using System;
using UnityEngine;

namespace ConnectN.Disks
{
    public interface IDisk
    {
        Player Owner { get; }
        Action OnFinishDropping { get; set; }
        IDisk Instantiate(Player owner);
        void SetCurrentPosition(Vector3 position);
        void MoveTo(Vector3 position);
        void Drop();
    }
}
