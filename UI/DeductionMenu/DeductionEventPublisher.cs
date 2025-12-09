using System;
using General;


namespace Misc.UI.Deduction
{
    public class DeductionEventPublisher
    {
        private static DeductionEventPublisher instance;
        protected DeductionEventPublisher() { }

        public static DeductionEventPublisher i
        {
            get
            {
                if (instance == null) instance = new DeductionEventPublisher();
                return instance;
            }
        }

        public delegate void OnClueHover(object sender, EventArgs e, string name);
        public event OnClueHover onClueHover;
        public void DoOnClueHover(object sender, EventArgs e, string name) => onClueHover?.Invoke(sender, e, name);

        public delegate void OnClueDehover(object sender, EventArgs e);
        public event OnClueDehover onClueDehover;
        public void DoOnClueDehover(object sender, EventArgs e) => onClueDehover?.Invoke(sender, e);

        public delegate void OnMovingToRound(object sender, EventArgs e, int round);
        public event OnMovingToRound onMovingToRound;
        public void DoMovingToRound(object sender, EventArgs e, int round) => onMovingToRound?.Invoke(sender, e, round);
        public delegate void OnFail(object sender, EventArgs e);
        public event OnFail onFail;
        public void DoFail(object sender, EventArgs e) => onFail?.Invoke(sender, e);


        public delegate void OnAssumptionMade(object sender, EventArgs e, bool result);
        public event OnAssumptionMade onAssumptionMade;
        public void DoAssumptionMade(object sender, EventArgs e, bool result) => onAssumptionMade?.Invoke(sender, e, result);


        public delegate void OnWaitForMouseClick(object sender, EventArgs e);
        public event OnWaitForMouseClick onWaitForMouseClick;
        public void WaitForMouseClick(object sender, EventArgs e) => onWaitForMouseClick?.Invoke(sender, e);
        public delegate void OnMouseClicked(object sender, EventArgs e);
        public event OnMouseClicked onMouseClicked;
        public void MouseClicked(object sender, EventArgs e) => onMouseClicked?.Invoke(sender, e);
        
        public delegate void OnSceneMoving(object sender, EventArgs e, bool doesItBreak = false);
        public event OnSceneMoving onSceneMoving;
        public void SceneMoving(object sender, EventArgs e, bool doesItBreak = false) => onSceneMoving?.Invoke(sender, e, doesItBreak);
        
        public delegate void OnDeductionCompleted(object sender, EventArgs e, Character character, bool everythingIsOkay);
        public event OnDeductionCompleted onDeductionCompleted;
        public void DeductionCompleted(object sender, EventArgs e, Character character, bool everythingIsOkay) => onDeductionCompleted?.Invoke(sender, e, character, everythingIsOkay);



    }
}