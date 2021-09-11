using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ConnectN.Disks
{
    [RequireComponent(typeof(Rigidbody), typeof(AudioSource))]
    public class Disk : MonoBehaviour, IDisk
    {
        private float _height;
        private bool _dropped = false;
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

        //private void FixedUpdate()
        //{
        //    if (_dropped && _rb.position.y < _height) {
        //        _rb.velocity = new Vector3(_rb.velocity.x, 10, _rb.velocity.z);
        //        //var y = Mathf.Max(_rb.position.y, _height);
        //        //_rb.position = new Vector3(_rb.position.x, y, _rb.position.z);
        //        //_rb.AddForce(Vector3.up * 100);
        //        //transform.position = new Vector3(transform.position.x, _height, transform.position.z);
        //    }
        //}

        //private void OnCollisionEnter(Collision collision)
        //{
        //    if (!_dropped) {
        //        _height = Mathf.Round(transform.position.y);
        //        _dropped = true;
        //        OnFinishDropping?.Invoke();
        //    }
        //}

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
            _dropped = true;
            _height = Mathf.Round(transform.position.y);
            //StartCoroutine(WaitForStability());
        }

        private IEnumerator WaitForStability()
        {
            yield return new WaitForSeconds(5);
            _rb.isKinematic = true;
            _rb.MovePosition(new Vector3(_rb.position.x, _height, _rb.position.y));
        }
    }
}
