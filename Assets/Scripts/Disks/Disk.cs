using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ConnectN.Disks
{
    [RequireComponent(typeof(Rigidbody), typeof(AudioSource))]
    public class Disk : MonoBehaviour, IDisk
    {
        private Rigidbody _rb;
        private AudioSource _audioSource;
        private bool collided = false;

        public Player Owner { get; private set; }

        public Action OnFinishDropping { get; set; }

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
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

        private void OnCollisionEnter(Collision collision)
        {
            if (!collided) {
                _audioSource.pitch = Random.Range(1f, 1.2f);
                _audioSource.Play();
                collided = true;
            }
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
