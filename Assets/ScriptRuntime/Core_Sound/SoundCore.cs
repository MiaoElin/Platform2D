using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SoundCore {

    public AudioSource prefab;
    AsyncOperationHandle prefab_Ptr;
    public AudioSource bgmPlayer;
    public AudioSource ownerRun;
    public AudioSource[] ownerSkillPlayers;
    public AudioSource[] lootPlayers;
    public AudioSource[] roleSkillPlayers;
    public AudioSource[] roleEntrancePlayers;
    public AudioSource[] roleDeadPlayers;

    public SoundCore() {
        ownerSkillPlayers = new AudioSource[5];
        lootPlayers = new AudioSource[2];
        roleSkillPlayers = new AudioSource[8];
        roleEntrancePlayers = new AudioSource[8];
        roleDeadPlayers = new AudioSource[4];
    }

    public void LoadAll() {
        Transform sfx = new GameObject("SFXGROUP").transform;
        var ptr = Addressables.LoadAssetAsync<GameObject>("AudioSource");
        prefab = ptr.WaitForCompletion().GetComponent<AudioSource>();
        prefab_Ptr = ptr;

        bgmPlayer = GameObject.Instantiate(prefab, sfx);
        ownerRun = GameObject.Instantiate(prefab, sfx);
        for (int i = 0; i < ownerSkillPlayers.Length; i++) {
            ownerSkillPlayers[i] = GameObject.Instantiate(prefab, sfx);
        }

        for (int i = 0; i < lootPlayers.Length; i++) {
            lootPlayers[i] = GameObject.Instantiate(prefab, sfx);
        }

        for (int i = 0; i < roleSkillPlayers.Length; i++) {
            roleSkillPlayers[i] = GameObject.Instantiate(prefab, sfx);
        }

        for (int i = 0; i < roleEntrancePlayers.Length; i++) {
            roleEntrancePlayers[i] = GameObject.Instantiate(prefab, sfx);
        }

        for (int i = 0; i < roleDeadPlayers.Length; i++) {
            roleDeadPlayers[i] = GameObject.Instantiate(prefab, sfx);
        }
    }

    internal void Role_Dead_Play(AudioClip clip, float volume) {
        foreach (var player in roleDeadPlayers) {
            if (!player.isPlaying) {
                player.clip = clip;
                player.volume = volume;
                player.Play();
            }
        }
    }

    public void Unload() {
        if (prefab_Ptr.IsValid()) {
            Addressables.Release(prefab_Ptr);
        }
    }

    public void BGM_Play(AudioClip clip, float volume) {
        bgmPlayer.loop = true;
        if (!bgmPlayer.isPlaying) {
            bgmPlayer.clip = clip;
            bgmPlayer.volume = volume;
            bgmPlayer.Play();
        }
    }

    public void OwnerSkillPlay(AudioClip clip, float volume) {
        foreach (var palyer in ownerSkillPlayers) {
            if (!palyer.isPlaying) {
                palyer.clip = clip;
                palyer.Play();
                palyer.volume = volume;
                return;
            }
        }
    }

    public void RoleSkillPlay(AudioClip clip, float volume) {
        foreach (var palyer in roleSkillPlayers) {
            if (!palyer.isPlaying) {
                palyer.clip = clip;
                palyer.volume = volume;
                palyer.Play();
                return;
            }
        }
    }

    public void Loot_Open_Play(AudioClip clip, float volume) {
        foreach (var palyer in lootPlayers) {
            if (!palyer.isPlaying) {
                palyer.clip = clip;
                palyer.volume = volume;
                palyer.Play();
                return;
            }
        }
    }
}