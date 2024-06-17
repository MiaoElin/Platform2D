using UnityEngine;

public class UIContext {

    public Asset_Core asset;
    public UIRepo uIRepo;
    public HUD_HintsRepo hud_HintsRepo;
    public HUD_HPBarRepo hud_HPBarRepo;
    public HUD_HurtInfoRepo hUD_HurtInfoRepo;

    public Transform screenCanvas;
    public Transform hudCanvas;
    public EventCenter eventCenter;

    public void Inject(Asset_Core asset, Canvas screenCanvas, Canvas hudCanvas, EventCenter eventCenter) {
        this.asset = asset;
        this.screenCanvas = screenCanvas.transform;
        this.hudCanvas = hudCanvas.transform;
        this.eventCenter = eventCenter;
        uIRepo = new UIRepo();
        hud_HintsRepo = new HUD_HintsRepo();
        hud_HPBarRepo = new HUD_HPBarRepo();
        hUD_HurtInfoRepo = new HUD_HurtInfoRepo();

    }
}