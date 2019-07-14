using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.engine.session;
using UnityEngine;
using UnityEngine.UI;

public class HudView : MonoBehaviour {

    [SerializeField] private Text stackedLabel;
    [SerializeField] private Text fallLabel;

    public void UpdateStacked(int currrent) => stackedLabel.text = "Stackeds: " + currrent + "/" + Session.MAX_TO_WIN;
    public void UpdateFalls(int currrent) => fallLabel.text = "Falls:" + currrent + "/" + Session.MAX_TO_LOSE;
}
