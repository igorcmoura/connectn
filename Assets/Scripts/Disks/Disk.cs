using System;
using System.Collections;
using UnityEngine;

namespace ConnectN.Disks
{
    [RequireComponent(typeof(Rigidbody))]
    public class Disk : MonoBehaviour, IDisk
    {
        private Rigidbody _rb;

        public Player Owner { get; private set; }

        public Action OnFinishDropping { get; set; }

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.isKinematic = true;
            if (Owner != null)
                GetComponent<Renderer>().material.color = Owner.Color;
        }

        public IDisk Instantiate(Player owner)
        {
            var newDisk = Instantiate(this);
            newDisk.Owner = owner;
            return newDisk;
        }

        public void SetCurrentPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void MoveTo(Vector3 position)
        {
            SetCurrentPosition(position);
        }

        public void Drop()
        {
            _rb.isKinematic = false;
            StartCoroutine(WaitForDrop());
        }

        private IEnumerator WaitForDrop()
        {
            yield return new WaitForSeconds(1);
            OnFinishDropping?.Invoke();
        }
    }
}
