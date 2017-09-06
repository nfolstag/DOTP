using Assets.Codes.Animations;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Codes.CardsCollection
{
    public class ItemsEventHandler : EventTrigger
    {

        private AnimationHandler animHandler;
        private Animation anim;

        public void Start()
        {
            animHandler = AnimationHandler.GetInstance();
            anim = gameObject.AddComponent<Animation>();
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            animHandler.DoMenuItemEnterAnimation(anim);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            animHandler.DoMenuItemExitAnimation(anim);
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            switch(gameObject.name)
            {
                case (Constants.ITEM_BACK):
                    break;
                case (Constants.ITEM_FILTER):
                    break;
                case (Constants.ITEM_RARROW):
                    CardsPanelHandler.MoveToRight();
                    break;
                case (Constants.ITEM_LARROW):
                    CardsPanelHandler.MoveToLeft();
                    break;
            }
        }
    }
}