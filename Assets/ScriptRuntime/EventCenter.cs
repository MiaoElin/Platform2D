using UnityEngine;
using System;

public class EventCenter {
    public Action OnStartGameHandle;
    public void StartGame() {
        OnStartGameHandle.Invoke();
    }

    public Action OnExitGameHandle;
    public void ExitGame() {
        OnExitGameHandle.Invoke();
    }

    public Action OnRestartHandle;
    public void RestartGame() {
        OnRestartHandle.Invoke();
    }

    public Action OnBackToMenuHandle;
    public void BackToMenu() {
        OnBackToMenuHandle.Invoke();
    }

    public Action OnStopBossWaveHande;

    internal void StopBossWaveHandle() {
        OnStopBossWaveHande.Invoke();
    }

    public Action<int> OnAltarTimeIsEndHandle;
    internal void AltarTimeIsEnd(int id) {
        OnAltarTimeIsEndHandle.Invoke(id);
    }
}