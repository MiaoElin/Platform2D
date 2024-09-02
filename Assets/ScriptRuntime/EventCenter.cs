using UnityEngine;
using System;

public class EventCenter {
    public event Action OnStartGameHandle;
    public void StartGame() {
        OnStartGameHandle.Invoke();
    }

    public event Action OnExitGameHandle;
    public void ExitGame() {
        OnExitGameHandle.Invoke();
    }

    public event Action OnRestartHandle;
    public void RestartGame() {
        OnRestartHandle.Invoke();
    }

    public event Action OnBackToMenuHandle;
    public void BackToMenu() {
        OnBackToMenuHandle.Invoke();
    }

    public event Action OnNewGameHandle;

    internal void NewGame() {
        OnNewGameHandle.Invoke();
    }

    public event Action OnResumeGameHandle;
    internal void ResumeGame() {
        OnResumeGameHandle.Invoke();
    }

    public event Action OnStopBossWaveHande;

    internal void StopBossWaveHandle() {
        OnStopBossWaveHande.Invoke();
    }

    public event Action<int> OnAltarTimeIsEndHandle;
    internal void AltarTimeIsEnd(int id) {
        OnAltarTimeIsEndHandle.Invoke(id);
    }
}