using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace NOX
{
    public class Generator : MonoBehaviour
    {
        Animator _playerAnimator;

        [SerializeField] float screenWidthInPoints;

        [Header("Coins and Traps")]
        [SerializeField] GameObject[] availableObjects;
        [SerializeField] List<GameObject> objects;

        [SerializeField] float objectsMinDistance = 5.0f;
        [SerializeField] float objectsMaxDistance = 10.0f;

        [SerializeField] float objectsMinY = -1.4f;
        [SerializeField] float objectsMaxY = 1.4f;

        [SerializeField] float objectsMinRotation = -45.0f;
        [SerializeField] float objectsMaxRotation = 45.0f;


        [Header("Missiles")]
        [SerializeField] GameObject[] availableMissiles;
        [SerializeField] List<GameObject> missiles;

        [SerializeField] float missilePositionX = 11.0f;

        [SerializeField] float missilesMinY = -1.4f;
        [SerializeField] float missilesMaxY = 1.4f;

        [SerializeField] float missileRespawnTimeMin = 3.0f;
        [SerializeField] float missileRespawnTimeMax = 6.0f;

        [Header("Magic Shield")]

        [SerializeField] GameObject[] availableMagicShields;
        [SerializeField] List<GameObject> magicShields;

        [SerializeField] float magicShieldPositionX = 11.0f;

        [SerializeField] float magicShieldMinY = -1.4f;
        [SerializeField] float magicShieldMaxY = 1.4f;

        [SerializeField] float magicShieldRespawnTimeMin = 10.0f;
        [SerializeField] float magicShieldRespawnTimeMax = 40.0f;

        bool _isMagicShieldSpawned;
        public bool isAbilityUsed;

        private void Start()
        {
            _playerAnimator = GetComponent<Animator>();
            float height = 2.0f * Camera.main.orthographicSize;
            screenWidthInPoints = height * Camera.main.aspect;

            StartCoroutinesOnStart();
        }

        #region Object Spawner Methods
        private void AddObject(float lastObjectX)
        {
            int randomIndex = Random.Range(0, availableObjects.Length);

            GameObject obj = (GameObject)Instantiate(availableObjects[randomIndex]);

            float objectPositionX = lastObjectX + Random.Range(objectsMinDistance, objectsMaxDistance);

            float randomY = Random.Range(objectsMinY, objectsMaxY);
            obj.transform.position = new Vector3(objectPositionX, randomY, 0);

            float rotation = Random.Range(objectsMinRotation, objectsMaxRotation);
            obj.transform.rotation = Quaternion.Euler(Vector3.forward * rotation);

            objects.Add(obj);
        }

        private void GenerateObjectsIfRequired()
        {
            float playerX = transform.position.x;
            float removeObjectsX = playerX - screenWidthInPoints;
            float addObjectX = playerX + screenWidthInPoints;
            float farthestObjectX = 0;

            List<GameObject> objectsToRemove = new List<GameObject>();
            foreach (var obj in objects)
            {
                float objX = obj.transform.position.x;
                farthestObjectX = Mathf.Max(farthestObjectX, objX);
                if (objX < removeObjectsX)
                {
                    objectsToRemove.Add(obj);
                }
            }

            foreach (var obj in objectsToRemove)
            {
                objects.Remove(obj);
                Destroy(obj);
            }

            if (farthestObjectX < addObjectX)
            {
                AddObject(farthestObjectX);
            }
        }

        #endregion

        #region Missile Spawner Methods

        private void AddMissile()
        {
            int randomIndex = Random.Range(0, availableMissiles.Length);

            GameObject missile = (GameObject)Instantiate(availableMissiles[randomIndex]);

            float randomY = Random.Range(missilesMinY, missilesMaxY);
            missile.transform.position = new Vector3(missilePositionX, randomY, 0);

            missiles.Add(missile);
        }

        private void GenerateMissileIfRequired()
        {
            float playerX = transform.position.x;
            float removeMissileX = playerX - screenWidthInPoints;
            float addMissileX = playerX + screenWidthInPoints;
            float farthestMissileX = 0;

            List<GameObject> missilesToRemove = new List<GameObject>();
            foreach (var missile in missiles)
            {
                float missileX = missile.transform.position.x;
                farthestMissileX = Mathf.Max(farthestMissileX, missileX);
                if (missileX < removeMissileX)
                {
                    missilesToRemove.Add(missile);
                }
            }

            foreach (var missile in missilesToRemove)
            {
                missiles.Remove(missile);
                Destroy(missile);
            }

            AddMissile();
        }

        #endregion

        #region Magic Shield Spawner Methods

        private void AddMagicShield()
        {
            int randomIndex = Random.Range(0, availableMagicShields.Length);

            GameObject magicShield = (GameObject)Instantiate(availableMagicShields[randomIndex]);

            float randomY = Random.Range(magicShieldMinY, magicShieldMaxY);
            magicShield.transform.position = new Vector3(magicShieldPositionX, randomY, 0);

            magicShields.Add(magicShield);
        }

        private void GenerateMagicShieldIfRequired()
        {
            float playerX = transform.position.x;
            float removeMagicShieldX = playerX - screenWidthInPoints;
            float addMagicShieldX = playerX + screenWidthInPoints;
            float farthestMagicShieldX = 0;

            List<GameObject> magicShieldsToRemove = new List<GameObject>();
            foreach (var magicShield in magicShields)
            {
                float magicShieldX = magicShield.transform.position.x;
                farthestMagicShieldX = Mathf.Max(farthestMagicShieldX, magicShieldX);
                if (magicShieldX < removeMagicShieldX)
                {
                    magicShieldsToRemove.Add(magicShield);
                }
            }

            foreach (var magicShield in magicShieldsToRemove)
            {
                magicShields.Remove(magicShield);
                Destroy(magicShield);
            }

            if (!_isMagicShieldSpawned)
            {
                AddMagicShield();
                _isMagicShieldSpawned = true;
            }

        }

        public void CheckIfMagicShieldCanSpawn()
        {
            if (_isMagicShieldSpawned)
            {
                Debug.Log("Deactivated Shield Spawn");
                StopCoroutine(MagicShieldGeneratorCheck());
            }
        }

        #endregion

        #region Abilities

        public void Ability()
        {
            List<GameObject> missilesToRemove = new List<GameObject>();
            foreach (var missile in missiles)
            {
               missilesToRemove.Add(missile);
            }

            foreach (var missile in missilesToRemove)
            {
                missiles.Remove(missile);
                Destroy(missile);
            }

            List<GameObject> objectsToRemove = new List<GameObject>();
            foreach (var obj in objects)
            {
               objectsToRemove.Add(obj);
            }

            foreach (var obj in objectsToRemove)
            {
                objects.Remove(obj);
                Destroy(obj);
            }

            _playerAnimator.Play("Attack");
            isAbilityUsed = true;
        }

        #endregion


        #region Coroutines

        private void StartCoroutinesOnStart()
        {
            StartCoroutine(ObjectGeneratorCheck());
            StartCoroutine(MissileGeneratorCheck());
            StartCoroutine(MagicShieldGeneratorCheck());
        }

        private IEnumerator ObjectGeneratorCheck()
        {
            while (true)
            {
                GenerateObjectsIfRequired();
                yield return new WaitForSeconds(0.25f);
            }
        }

        private IEnumerator MissileGeneratorCheck()
        {
            yield return new WaitForSeconds(2);
            while (true)
            {
                float missileRespawnTime = Random.Range(missileRespawnTimeMin, missileRespawnTimeMax);

                GenerateMissileIfRequired();
                yield return new WaitForSeconds(missileRespawnTime);
            }
        }

        private IEnumerator MagicShieldGeneratorCheck()
        {
            while (!_isMagicShieldSpawned)
            {
                float magicShieldRespawnTime = Random.Range(magicShieldRespawnTimeMin, magicShieldRespawnTimeMax);

                yield return new WaitForSeconds(magicShieldRespawnTime);
                GenerateMagicShieldIfRequired();
            }
        }

        #endregion

    }
}


