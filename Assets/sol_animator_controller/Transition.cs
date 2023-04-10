using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transition : MonoBehaviour {

    private Animator animator;
    private bool applyRootMotion;
    private float pre_y;
    private Vector3 pos;
    private Vector3 orig_pos;
    private Quaternion orig_rot;
    private bool activeAnim;
    private string prevCommand;

    public AudioClip[] audioClips;
    public Text command;
    AudioSource audioSource;

    private void Start()
    {
        animator = GetComponent<Animator>();
        pos = this.transform.position;
        pre_y = pos.y;
        orig_pos = transform.position;
        orig_rot = transform.rotation;
        audioSource = GetComponent<AudioSource>();
        activeAnim = false;
        prevCommand = command.text;
    }

    void Update() {

        HandleAnimations();
    }

    void HandleAnimations()
    {
        if (prevCommand != command.text)
        {
            //ResetSoldiers();
            activeAnim = false;
            audioSource.loop = true;
        }
        Animator animator = GetComponent<Animator>();
        int trans = animator.GetInteger("trans_anime");

        if (Input.GetKeyDown(KeyCode.UpArrow))      //go straight
        {
            animator.applyRootMotion = true;
            trans = 1;
        }
        else if (command.text.Equals("Grenade") && !activeAnim)       //throw granade
        {
            audioSource.Stop();
            audioSource.loop = false;
            audioSource.clip = audioClips[3];
            audioSource.Play();
            animator.applyRootMotion = true;
            trans = 2;
            activeAnim = true;
            prevCommand = command.text;
        }
        else if (command.text.Equals("PlayDead") && !activeAnim)       //die
        {
            audioSource.Stop();
            animator.applyRootMotion = false;
            trans = 3;
            activeAnim = true;
            prevCommand = command.text;
        }
        else if (command.text.Equals("Dance") && !activeAnim)       //dance
        {
            audioSource.Stop();
            audioSource.clip = audioClips[1];
            audioSource.Play();
            activeAnim = true;
            prevCommand = command.text;
            animator.applyRootMotion = true;
            trans = 4;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))       //move back
        {
            animator.applyRootMotion = true;
            trans = 5;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))       //turn left
        {
            animator.applyRootMotion = true;
            trans = 6;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))      //turn  right
        {
            animator.applyRootMotion = true;
            trans = 7;
        }
        else if (command.text.Equals("PushUps") && !activeAnim)           //push up
        {
            audioSource.Stop();
            animator.applyRootMotion = false;
            trans = 8;
            activeAnim = true;
            prevCommand = command.text;
        }
        else if (command.text.Equals("Shoot") && !activeAnim)           //shoot
        {
            audioSource.Stop();
            audioSource.clip = audioClips[0];
            prevCommand = command.text;
            activeAnim = true;
            audioSource.Play();
            animator.applyRootMotion = false;
            trans = 9;
        }
        else if (command.text.Equals("Fight") && !activeAnim)           //fight
        {
            audioSource.Stop();
            prevCommand = command.text;
            activeAnim = true;
            animator.applyRootMotion = false;
            trans = 10;
        }
        else if (command.text.Equals("Salute") && !activeAnim)           //salute
        {
            audioSource.Stop();
            prevCommand = command.text;
            activeAnim = true;
            animator.applyRootMotion = false;
            trans = 11;
        }
        else if (command.text.Equals("Fly"))           //fly
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Stop();
                audioSource.clip = audioClips[2];
                audioSource.Play();
            }
            animator.applyRootMotion = false;
            trans = 12;
            pos = this.transform.position;
            pos.y += 0.03f;
            transform.Rotate(0, 720 * Time.deltaTime, 0);
            this.transform.position = pos;
        }
        else if (command.text.Equals("Kick") && !activeAnim)           //kick
        {
            audioSource.Stop();
            prevCommand = command.text;
            activeAnim = true;
            animator.applyRootMotion = false;
            trans = 13;
        }
        else if (command.text.Equals("Peace") && !activeAnim)         //reset
        {
            audioSource.Stop();
            prevCommand = command.text;
            activeAnim = true;
            animator.applyRootMotion = false;
            trans = 0;
            pos = this.transform.position;
            pos.y = pre_y;

            ResetSoldiers();
        }
        else
        {
            animator.applyRootMotion = true;
            trans = 100;
        }

        animator.SetInteger("trans_anime", trans);
    }

    void ResetSoldiers()
    {
        //reset to original
        this.transform.position = orig_pos;
        this.transform.rotation = orig_rot;
    }
}