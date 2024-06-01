using UnityEngine;

public class UIContext {

    public Asset_Core asset;
    public UIRepo uIRepo;
    public Transform screenCanvas;
    public Transform hudCanvas;

    public void Inject(Asset_Core asset, Canvas screenCanvas, Canvas hudCanvas) {
        this.asset = asset;
        this.screenCanvas = screenCanvas.transform;
        this.hudCanvas = hudCanvas.transform;
        uIRepo = new UIRepo();
    }
}