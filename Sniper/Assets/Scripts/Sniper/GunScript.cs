using UnityEngine;
using System.Collections;

public class GunScript : MonoBehaviour	{

    public SteamVR_TrackedObject controller;
    public bool isPickedUp = false;

    // Scope Variables
    public bool isThereScope = false;
    public Camera scopeCamera;
    public float newMinFOV = 1f;
    public float newMaxFOV = 66f;

    //Setup different weapon strengths
    public float bulletSpeedMultiplier = 1;

    [Header("Bullets Left")]
	//How many bullets there are left
	public int bulletsLeft;

	//Check when reloading and out of ammo
	bool outOfAmmo = false;
	bool isReloading = false;

	//Used for firerate
	float lastFired;

	[Header("Spawnpoints & Prefabs")]
	public Transform
		casingSpawnPoint;
	public Transform casingPrefab;
	public Transform magSpawnPoint;
	public Transform emptyMagPrefab;
	//The raycast will start at the bullet spawnpoint, going forward
	public Transform bulletSpawnPoint;

	[Header("Customizable Options")]
	public int
		magazineSize;
	public float muzzleFlashDuration = 0.02f;
	public float fireRate;
	public float reloadDuration = 1.5f;
	[Range(1f, 4f)]
	public float
		lightIntensity = 2.0f;
	[Range(5f, 50f)]
	public float
		lightRange = 10.0f;

	//All weapon types
	[System.Serializable]
	public class weaponType
	{  
		
		[Header("Sniper")]
		public bool sniper;
		public bool sniperSilencer;
		public bool sniper3;
		public bool sniper6;
	}
	public weaponType WeaponType;

	//All animations
	[System.Serializable]
	public class animations
	{  
		//Animations
		public string fullMagInAnim;
		public string recoilAnim;
		public string reloadAnim;
		//Animation for "slide" and "bolt action"
		public string slideReloadAnim;
		//Used when shooting
		public string slideEjectAnim;
		public string reloadDownAnim;
		public string reloadUpAnim;
		//Animation for reloading slider, also for machine gun top, and sawn off shotgun barrel
		public string reloadSlideCloseAnim;
		public string reloadSlideOpenAnim;
		
		[Header("Sniper & Shotgun Bullet Animation")]
		//Used for sniper and shotgun when reloading
		public string
			bulletInAnim;
		
	}
	public animations Animations;

	//Components
	[System.Serializable]
	public class components
	{  
		[Header("Muzzleflash Holders")]
		public GameObject
			sideMuzzle;
		public GameObject topMuzzle;
		public GameObject frontMuzzle;
		//Array of muzzleflash sprites
		public Sprite[] muzzleflashSideSprites;
		[Header("Holders")]
		public GameObject
			holder;
		public GameObject slider;
		public GameObject ejectSlider;
		public GameObject mag;
		public GameObject fullMag;
		
		//For sniper 
		[Header("Sniper")]
		public GameObject
			bullet;
		
		[Header("Bullet Strip Components")]
		public GameObject[]
			bullets;
		public Transform[] bulletSpawnpoints;
		public Transform bulletPrefab;
		public GameObject sphereRotate;
		[Header("Light Front")]
		public Light
			lightFlash;
		[Header("Particle System")]
		public ParticleSystem
			smokeParticles;
		public ParticleSystem
			sparkParticles;
		
		[Header("Particle Systems")]
		public ParticleSystem
			smokeParticlesBack;
		[Header("Light Back")]
		public Light
			rpgLightBack;

    }

	public components Components;

	//All audio sources
	[System.Serializable]
	public class audioSources
	{  
		[Header("Shoot Sounds")]
		public AudioSource shootSound;

		[Header("Reload Sounds")]
		public AudioSource mainReloadSound;
		public AudioSource sliderReloadSound;
		public AudioSource sliderSound;
        public AudioSource shellInsertSound;

        public AudioSource removeMagSound;
		public AudioSource insertMagSound;

		public AudioSource outOfAmmoClickSound;

	}

	public audioSources AudioSources;

	void Start ()
	{
		//Set the magazine size
		bulletsLeft = magazineSize;

		//Make sure the light is off
		//Disable for silenced weapons and hand grenade since they dont need it
		if (!WeaponType.sniperSilencer) {

			Components.lightFlash.GetComponent<Light> ().enabled = false;

			//Set the light values
			Components.lightFlash.intensity = lightIntensity;
			Components.lightFlash.range = lightRange;
		}

		//Hide the muzzleflashes at start
		//Disable for rpg, grenade launcher, silenced weapons and hand grenadesince they dont use any muzzleflashes
		if (!WeaponType.sniperSilencer) {

			Components.sideMuzzle.GetComponent<Renderer> ().enabled = false;
			Components.topMuzzle.GetComponent<Renderer> ().enabled = false;
		}

		//Hide the front muzzleflash at start
		//Disable for shotgun, sawn off shotgun, rpg, grenade launcher, and silenced weapons since they dont have a front muzzle
		if (WeaponType.sniper == true || WeaponType.sniper3 == true || WeaponType.sniper6 == true) {

			//Hide the front muzzleflash
			Components.frontMuzzle.GetComponent<SpriteRenderer> ().enabled = false;
		}
	}

	//Reload
	IEnumerator Reload ()
	{
		//If sniper is true
		if (WeaponType.sniper == true || WeaponType.sniperSilencer == true) {
			//Play reload and slider eject animation
			Components.holder.GetComponent<Animation> ().Play
				(Animations.reloadDownAnim);
			Components.ejectSlider.GetComponent<Animation> ().Play 
				(Animations.reloadSlideOpenAnim);

			//Play main reload sound
			AudioSources.mainReloadSound.Play();

            Components.bullet.GetComponent<Animation>().PlayQueued
                    (Animations.bulletInAnim);

            StartCoroutine(ShellInsertSound(1));
		}

		//If sniper 6 is true
		if (WeaponType.sniper6 == true) {
			
			//Play reload animation
			Components.holder.GetComponent<Animation> ().Play
				(Animations.reloadAnim);

			//Play main reload sound
			AudioSources.mainReloadSound.Play ();
			//Play slider sound
			AudioSources.sliderSound.Play ();

		}
		
		//Refill bullets
		bulletsLeft = magazineSize;

		//Disable for sniper, shotgun, sawn off shotgun and rpg since they dont have a mag
		if (WeaponType.sniper3 == true || WeaponType.sniper6 == true ) {

            //Play main reload sound
            AudioSources.mainReloadSound.Play();
            Components.mag.GetComponent<MeshRenderer> ().enabled = true;

		}

		//If sniper is true 
		if (WeaponType.sniper == true || WeaponType.sniperSilencer == true) {
			//Play animations and wait
			yield return new WaitForSeconds (0.2f);
			Components.ejectSlider.GetComponent<Animation> ().Play 
				(Animations.reloadSlideCloseAnim);

			//Play slider sound
			AudioSources.sliderSound.Play();

			yield return new WaitForSeconds (0.2f);
			Components.holder.GetComponent<Animation> ().Play
				(Animations.reloadUpAnim);
			//Wait some more before being able to shoot
			yield return new WaitForSeconds (0.1f);
			
			//Enable shooting again
			outOfAmmo = false;
		}

		//If sniper3 is true
		if (WeaponType.sniper3 == true) {
			//Wait for the slider animation to finish
			yield return new WaitForSeconds (0.05f);

			//Play slider reload animation
			Components.slider.GetComponent<Animation> ().Play
				(Animations.slideReloadAnim);

			//Play slider reload sound
			AudioSources.sliderReloadSound.Play();

			//Let reload animation finish before enabling shooting
			yield return new WaitForSeconds(0.15f);

			//Enable shooting again
			outOfAmmo = false;
			isReloading = false;

		}

		//If sniper6 is true
		if (WeaponType.sniper6 == true) {
			//Wait for reload animation to finish
			yield return new WaitForSeconds(0.1f);

			//Play slider close animation
			Components.slider.GetComponent<Animation> ().Play
				(Animations.reloadSlideCloseAnim);

			//Play slider sound
			AudioSources.sliderSound.Play ();
			
			//Let reload animation finish before enabling shooting
			yield return new WaitForSeconds(0.15f);
			
			//Enable shooting again
			outOfAmmo = false;
			isReloading = false;
            
		}
	}	

	IEnumerator ShellInsertSound(int times)
	{
		for(int i=0; i<times; i++)
		{
			AudioSources.shellInsertSound.Play ();
			yield return new WaitForSeconds(0.55f);
		}
	}

    public GameObject BulletPrefab;

    //Show muzzleflash
    IEnumerator Muzzleflash ()
	{

        //Disable raycast bullet for rpg and grenade launcher, since they dont use it
        GameObject go = Instantiate(BulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.transform.rotation) as GameObject;
        //Add velocity to the non-physics bullet
        go.GetComponent<SniperBullet>().currentVelocity = (Ballistics.bulletSpeed * bulletSpeedMultiplier) * bulletSpawnPoint.transform.forward;


        //Chooses a random muzzleflash from the array
        //Disable for rpg, grenade launcher, and silenced weapons since they dont use muzzleflashes
        if (!WeaponType.sniperSilencer) {

			Components.sideMuzzle.GetComponent<SpriteRenderer> ().sprite = Components.muzzleflashSideSprites 
			[Random.Range (0, Components.muzzleflashSideSprites.Length)];
			Components.topMuzzle.GetComponent<SpriteRenderer> ().sprite = Components.muzzleflashSideSprites 
			[Random.Range (0, Components.muzzleflashSideSprites.Length)];

			//Show the muzzleflashes
			Components.sideMuzzle.GetComponent<SpriteRenderer> ().enabled = true;
			Components.topMuzzle.GetComponent<SpriteRenderer> ().enabled = true;
			
			//Disable for shotgun and sawn off shotgun, and silenced weapons, since they dont use the front muzzle
			if (WeaponType.sniper == true || WeaponType.sniper6 == true) {
				Components.frontMuzzle.GetComponent<SpriteRenderer> ().enabled = true;
			}
		}
		
		//Show the light flash
		//Disable for silenced weapons since they dont need it
		if (!WeaponType.sniperSilencer) {

			Components.lightFlash.GetComponent<Light> ().enabled = true;
		}

		
		//Wait for set amount of time, default value 0.02
		yield return new WaitForSeconds (muzzleFlashDuration);
		
		//Hide the muzzleflashes, disable for rpg, grenade launcher, and silenced weapons since they doent have muzzleflashes
		if (!WeaponType.sniperSilencer ) {

			Components.sideMuzzle.GetComponent<SpriteRenderer> ().enabled = false;
			Components.topMuzzle.GetComponent<SpriteRenderer> ().enabled = false;
		}

		//Disable for shotgun and sawn off shotgun, and silenced weapons since they dont use the front muzzle
		if (WeaponType.sniper == true || WeaponType.sniper3 == true || WeaponType.sniper6 == true) {

			Components.frontMuzzle.GetComponent<SpriteRenderer> ().enabled = false;
		}
		
		//Hide the light flash
		//Disable for silenced weapons since they dont need it
		if (!WeaponType.sniperSilencer) {

			Components.lightFlash.GetComponent<Light> ().enabled = false;
		}
	}	

	//Bolt action reload for sniper and pump reload for shotgun
	IEnumerator PumpOrBoltActionReload ()
	{
		

		//Disable shooting
		isReloading = true;
		
		//Wait before playing the pump animation
		yield return new WaitForSeconds (0.35f);
		Components.ejectSlider.GetComponent<Animation> ().Play 
			(Animations.slideReloadAnim);

		//Wait for the animation to finish
		yield return new WaitForSeconds (0.08f);
		
		//Spawn shellcasing
		Instantiate (casingPrefab, casingSpawnPoint.transform.position, 
		            casingSpawnPoint.transform.rotation);
		
		//Wait before being able to shoot again
		yield return new WaitForSeconds (0.25f);
		
		//Enable shooting
		isReloading = false;

		
	}

	//Delay when shooting, used for grenade launcher, sniper 3 and sniper 6
	IEnumerator ShootingDelay () {
		//Disable shooting
		outOfAmmo = true;
		isReloading = true;

		//Wait some time
		yield return new WaitForSeconds (0.4f);
		//Enable shooting again
		outOfAmmo = false;
		isReloading = false;
	}

    void dropMagazine() {
        isReloading = true;

        //Spawn empty magazine
        //Disable for sniper, shotgun, sawn off shotgun and rpg since they dont have a mag
        if (WeaponType.sniper3 == true) {

            Instantiate(emptyMagPrefab, magSpawnPoint.transform.position,
                            magSpawnPoint.transform.rotation);
            //Hide the magazine
            Components.mag.GetComponent<MeshRenderer>().enabled = false;
            //Play remove mag sound
            AudioSources.removeMagSound.Play();
        }else if (WeaponType.sniper6 == true) {
            //Spawn the empty mag prefab
            Instantiate(emptyMagPrefab, magSpawnPoint.transform.position, magSpawnPoint.transform.rotation);
            //Play the slider open animation
            Components.ejectSlider.GetComponent<Animation>().Play(Animations.reloadSlideOpenAnim);
            //Play remove mag sound
            AudioSources.removeMagSound.Play();
        }
    }

    void OnTriggerEnter(Collider col) {

        if (WeaponType.sniper == true && col.gameObject.tag == "Magazine" && isReloading) {
            Destroy(col.gameObject);
            isReloading = false;
            StartCoroutine(Reload());
        }else if (WeaponType.sniper3 == true && col.gameObject.tag == "Magazine2" && isReloading) {
            Destroy(col.gameObject);
            isReloading = false;
            StartCoroutine(Reload());
        } else if (WeaponType.sniper6 == true && col.gameObject.tag == "Magazine3" && isReloading) {
            Destroy(col.gameObject);
            isReloading = false;
            StartCoroutine(Reload());
        }
    }

	void Update ()
	{

        //If sniper, silenced sniper, or shotgun is true

        if (isPickedUp) {

            var device = SteamVR_Controller.Input((int)controller.index);

            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) && !isReloading) {
                float touchX = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).x;
                if (WeaponType.sniper3 == true || WeaponType.sniper6 == true) {
                    if (touchX > 0.5) {
                        dropMagazine();
                    }
                }   
            }

            if (WeaponType.sniper == true || WeaponType.sniperSilencer == true) {
                //Shoot when left click is pressed
                device = SteamVR_Controller.Input((int)controller.index);
                if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger) && !outOfAmmo && !isReloading) {
                    //Save bullet count
                    DataHolder.totalBullets = DataHolder.totalBullets + 1;
                    if (DataHolder.inMission) {
                        DataHolder.sessionBullets = DataHolder.sessionBullets + 1;
                    }

                    //Muzzleflash
                    device.TriggerHapticPulse(2000);
                    StartCoroutine(Muzzleflash());
                    StartCoroutine(PumpOrBoltActionReload());

                    //Play recoil animation
                    Components.holder.GetComponent<Animation>().Play
                        (Animations.recoilAnim);

                    //Remove 1 bullet everytime you shoot
                    bulletsLeft -= 1;

                    //Play shoot sound
                    AudioSources.shootSound.Play();

                    //Play smoke particles
                    Components.smokeParticles.Play();
                }
            }

            //If sniper 3 or sniper 6 is true
            if (WeaponType.sniper3 == true || WeaponType.sniper6 == true) {
                //Shoot when left click is pressed
                
                device = SteamVR_Controller.Input((int)controller.index);
                if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger) && !outOfAmmo && !isReloading) {
                    //Save bullet count
                    DataHolder.totalBullets = DataHolder.totalBullets + 1;
                    if (DataHolder.inMission) {
                        DataHolder.sessionBullets = DataHolder.sessionBullets + 1;
                    }
                    //Muzzleflash
                    device.TriggerHapticPulse(2000);
                    StartCoroutine(Muzzleflash());

                    //Play recoil animation
                    Components.holder.GetComponent<Animation>().Play
                        (Animations.recoilAnim);
                    Components.ejectSlider.GetComponent<Animation>().Play
                        (Animations.slideEjectAnim);

                    //Remove 1 bullet everytime you shoot
                    bulletsLeft -= 1;

                    //Play shoot sound
                    AudioSources.shootSound.Play();
                    //Play slider reload sound
                    AudioSources.sliderReloadSound.Play();

                    //Play smoke particles
                    Components.smokeParticles.Play();

                    //Spawn casing
                    Instantiate(casingPrefab, casingSpawnPoint.transform.position,
                                 casingSpawnPoint.transform.rotation);

                    //Start the shooting delay
                    StartCoroutine(ShootingDelay());
                }
            }

            //If out of ammo
            if (bulletsLeft == 0) {
                outOfAmmo = true;
                if (WeaponType.sniper == true) {
                    isReloading = true;
                }
            }

            if (isThereScope || WeaponType.sniper == true) {
                if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad)) {
                    float touchY = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).y;
                    if (touchY > 0.5) {
                        float fov;
                        if (scopeCamera.fieldOfView < 7) {
                            fov = scopeCamera.fieldOfView - 1f;
                        } else {
                            fov = scopeCamera.fieldOfView - 10f;
                        }
                        if (fov >= newMinFOV) {
                            scopeCamera.fieldOfView = fov;
                        }
                    } else if (touchY < -0.5) {
                        float fov;
                        if (scopeCamera.fieldOfView < 6) {
                            fov = scopeCamera.fieldOfView + 1f;
                        } else {
                            fov = scopeCamera.fieldOfView + 10f;
                        }

                        if (fov <= newMaxFOV) {
                            scopeCamera.fieldOfView = fov;
                        }

                    }
                }
            }

            if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger) && outOfAmmo && !isReloading) {
                //Play dry fire sound if clicking when out of ammo
                AudioSources.outOfAmmoClickSound.Play();
            }
        }

	}
}