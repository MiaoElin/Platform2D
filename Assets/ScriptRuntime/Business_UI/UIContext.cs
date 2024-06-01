using UnityEngine;

public class UIContext {

    public Asset_Core asset;
    public UIRepo uIRepo;
    public Transform screenCanvas;

    public void Inject(Asset_Core asset, Canvas screenCanvas) {
        this.asset = asset;
        this.screenCanvas = screenCanvas.transform;
        uIRepo = new UIRepo();
    }
}