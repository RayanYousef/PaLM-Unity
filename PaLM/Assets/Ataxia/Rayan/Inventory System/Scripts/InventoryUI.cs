//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using DG.Tweening;
//using System;
//using Michsky.UI.Reach;
//using Unity.VisualScripting;
//using DG.Tweening;
//public class UIElement : MonoBehaviour
//{
//    public const float DefaultDuration = .5f;
//    public const Ease DefaultEase = Ease.OutElastic;


//    public bool isUp { get; protected set; }
//    public Action DoBeforeUp { get { return BeforeUp; } set { BeforeUp = value; } }
//    public Action OnUpComplete { get { return onUpComplete; } set { onUpComplete = value; } }
//    public Action OnDownComplete { get { return onDownComplete; } set { onDownComplete = value; } }

//    public Vector3 FinalUpValue 
//    {
//        get { return UpVal; } 
//        set 
//        { 
//            UpVal = value;
//        } 
//    }
//    public Vector3 FinalDownValue
//    {
//        get { return DownVal; }
//        set
//        {
//            DownVal = value;
//        }
//    }

//    private Action onUpComplete = null;
//    private Action BeforeUp = null;
//    private Action onDownComplete = null;
//    private Vector3 UpVal = Vector3.one;
//    private Vector3 DownVal = Vector3.zero;
//    protected void setIsUp(bool new_val) 
//    {
//        isUp = false;
//    }
//    public virtual void OnElementUp(float animation_duration = 0, Ease animationStyle = Ease.Linear)
//    {
//        BeforeUp?.Invoke();
        
//        if (animation_duration == 0)
//            animation_duration = DefaultDuration;

//        transform.gameObject.SetActive(true);
//        //DOTween.To(() => transform.position, x => transform.position = x, Vector3.one, animation_duration).SetEase(animationStyle);
//        transform.DOScale(UpVal, animation_duration).SetEase(animationStyle).onComplete = () =>
//                    {
//                        onUpComplete?.Invoke();
//                        isUp = true;
//                    };

//    }
//    public virtual void OnElementDown(float animation_duration = 0,Ease animationStyle = Ease.Linear) 
//    {
        
//        if (animation_duration == 0)
//            animation_duration = DefaultDuration;


//        transform.DOScale(DownVal, animation_duration).SetEase(animationStyle).OnComplete(
//            () => 
//                {
//                    transform.gameObject.SetActive(false);
//                    OnDownComplete?.Invoke();
//                    isUp = false;
//                }
//            );
//    }


//    #region statics
//    public static void UP(Transform target,float animation_duration, Ease animationStyle)
//    {
//        target.gameObject.SetActive(true);
//        DOTween.To(() => target.position, x => target.position = x, Vector3.one, animation_duration).SetEase(animationStyle);
//    }
//    public static void DOWN(Transform target, float animation_duration, Ease animationStyle)
//    {
//        DOTween.To(() => target.position, x => target.position = x, Vector3.zero, animation_duration)
//            .SetEase(animationStyle)
//            .onComplete =
//               () =>
//               {
//                   target.gameObject.SetActive(false);
//               }
//           ;
//    }
//    #endregion
//}
//public class InventoryUI : UIElement
//{


//    SO_InventoryItem.Type CurrentLoadType;
//    // To Be Removed
//    //public static InventoryUI Instance;

//    [SerializeField] InventorySystem Inventory_System;
//    [SerializeField] List<TMPro.TextMeshProUGUI> RealKrone_UI;

//    [SerializeField] UIElement ShopMenu;

//    [SerializeField] float animation_duration;
//    [SerializeField] Ease animation_style;

//    [SerializeField] EffectsUI Effects;
//    [SerializeField] GameObject GridGameObject;
//    [SerializeField] GameObject ItemPrefab;

//    [SerializeField] UIElement SelectionList;

//    [Header("'Test' Will Remove This")]
//    [SerializeField] GameObject CharacterOnDisplay;

//    private List<GameObject> spawnedItems;
//    int gridCurrentCount = 0;


//    RectTransform Rect;
//    int CurrentTeamMateIndex;
//    EquipSlot current_selected_type;

//    //private HeldItem held;
//    //public void HighlightMe(InventoryItem item)
//    //{
//    //    Inventory_System.EquipTeamMemberItem(CurrentTeamMateIndex, item, current_selected_type);
//    //   if(held == null) 
//    //        held = CharacterOnDisplay.GetComponent<HeldItem>();
        
//    //    // Not The Best Thing To Do at All
//    //    //Transform go = held.ParentHolder.GetComponentInChildren<Transform>();
//    //    Destroy(held.CurrentHeld);
//    //    //
        
//    //    held.CurrentHeld = Instantiate(item.ItemPrefab, held.ParentHolder);

//    //    //Effects.HighlitedItem = item;
//    //}


//    private void Awake()
//    {
        
//        ServiceLocator.Add(this);
//    }


//    private void Start()
//    {
        
//        Inventory_System = ServiceLocator.Get<InventorySystem>();

//        spawnedItems = new List<GameObject>();


//        SetRealKroneUI(Inventory_System.RealKrones);

//        if (Rect == null)
//            Rect = SelectionList.GetComponent<RectTransform>();

//        Populate();
//        SelectionList.DoBeforeUp = () =>
//        {
//            Populate();
//        };

//        setIsUp(false);
//    }



//    /// <summary>
//    /// iterates over all RealKrones Texts and updates them alongside applying a simple punch effect
//    /// </summary>
//    /// <param name="value"></param>
//    /// <summary>the new value</summary>
//    private void SetRealKroneUI(float value)
//    {
//        Vector3 OG_pos;
//        foreach (TMPro.TextMeshProUGUI item in RealKrone_UI)
//        {
//            item.text = value + "";

//            OG_pos = item.gameObject.transform.position;

//            item.transform.DOPunchPosition(Vector3.up, animation_duration, 10, 0);
//        }
//    }
//    private void Flush()
//    {
//        foreach (GameObject obj in spawnedItems)
//            Destroy(obj);
//        spawnedItems.Clear();
//        gridCurrentCount = 0;
//    }
//    /// <summary>
//    /// Called each time the selection Menu is summoned 
//    /// ,It Populates the selection menu according to the Inventory_System Object's items
//    /// </summary>
//    private void Populate()
//    {
//        SetRealKroneUI(Inventory_System.RealKrones);
//        //construct the grid

//        if (gridCurrentCount < Inventory_System.Items.Count)
//        {

//            int i = gridCurrentCount;

//            for (; i < Inventory_System.Items.Count; i++)
//            {
//                spawnedItems.Add(SpawnItems(Inventory_System.Items[i]));
//            }
//            gridCurrentCount = ServiceLocator.Get<InventorySystem>().Items.Count;
//        }


//        //Hide all but leave the ones required 
//        for (int i = 0; i < spawnedItems.Count; i++)
//        {
//            if (spawnedItems[i].GetComponent<ItemSlotUI>().Item.ItemType == CurrentLoadType)
//            {
//                spawnedItems[i].SetActive(true);
//            }
//            else
//            {
//                spawnedItems[i].SetActive(false);
//            }
//        }
//    }

//    /// <summary>
//    /// Instantiates a child game object to the grid Selection Menu
//    /// </summary>
//    /// <param name="item"></param>
//    /// <returns> The inventory item that would be shown on the instantiated prefab(itemslot) </returns>
//    GameObject SpawnItems(SO_InventoryItem item)
//    {

//        GameObject GO = Instantiate(ItemPrefab, GridGameObject.transform);
//        ItemSlotUI UI = GO.GetComponent<ItemSlotUI>();
//        UI.ShopMode = false;
//        UI.Item = item;
//        return GO;
//    }



//    /// <summary>
//    /// Method to close the Selection Menu in case a mouse click is pressed out of its panel
//    /// </summary>
//    /// <param name="panel"></param>
//    /// <summayr>The panel (SelectionList)</summayr>
//    private void HideIfClickedOutside(GameObject panel)
//    {

//        if (Input.GetMouseButtonDown(0) && panel.activeSelf &&
//            !RectTransformUtility.RectangleContainsScreenPoint(
//                Rect,
//                Input.mousePosition,
//                Camera.main))
//        {
//            //panel.SetActive(false);
//            if (SelectionList.isUp)
//                SelectionList.OnElementDown(animation_duration, animation_style);
//        }
//    }


//    private void Update()
//    {
//        HideIfClickedOutside(SelectionList.gameObject);
//        if (RealKrone_UI[0].text != Inventory_System.RealKrones.ToString())
//            SetRealKroneUI(Inventory_System.RealKrones);
//    }

//    #region BUTTONS
//    public void BTN_WEAPON_MAIN()
//    {
//        if (!SelectionList.isUp)
//        //{
//        {
//            current_selected_type = EquipSlot.WEAPON_MAIN;
//            CurrentLoadType = SO_InventoryItem.Type.WEAPON;
//            SelectionList.OnElementUp(animation_duration, animation_style);
//        }
//        //}
//        //else { 
//        //    SelectionList.OnElementDown(animation_duration, animation_style);
//        //}
//    }
//    public void BTN_WEAPON_SECONDARY()
//    {
//        if (!SelectionList.isUp)
//        {
//            current_selected_type = EquipSlot.WEAPON_SECONDARY;
//            CurrentLoadType = SO_InventoryItem.Type.SECONDARY;
//            SelectionList.OnElementUp(animation_duration, animation_style);
//        }
//        //else
//        //{
//        //    SelectionList.OnElementDown(animation_duration, animation_style);
//        //}
//    }
//    public void BTN_ARMOR()
//    {
//        if (!SelectionList.isUp)
//        {
//            current_selected_type = EquipSlot.ARMOR;
//            CurrentLoadType = SO_InventoryItem.Type.ARMOR;
//            SelectionList.OnElementUp(animation_duration, animation_style);
//        }
//        //else
//        //{

//        //    SelectionList.OnElementDown(animation_duration, animation_style);
//        //}
//    }
//    public void BTN_CYBERNITICS()
//    {
//        if (!SelectionList.isUp)
//        {
//            current_selected_type = EquipSlot.CYBERNETICS;
//            CurrentLoadType = SO_InventoryItem.Type.CYBERNETICS;
//            SelectionList.OnElementUp(animation_duration, animation_style);
//        }
//    }
//    public void BTN_CONSUMABLES()
//    {
//        if (!SelectionList.isUp)
//        {
//            current_selected_type = EquipSlot.CONSUMABLE_1;
//            CurrentLoadType = SO_InventoryItem.Type.CONSUMABLE_1;
//            SelectionList.OnElementUp(animation_duration, animation_style);
//        }
//    }

//    public void BTN_CONSUMABLES_2()
//    {
//        if (!SelectionList.isUp)
//        {
//            current_selected_type = EquipSlot.CONSUMABLE_2;
//            CurrentLoadType = SO_InventoryItem.Type.CONSUMABLE_1;
//            SelectionList.OnElementUp(animation_duration, animation_style);
//        }
//    }

//    public void BTN_GO_SHOP()
//    {
//        //SelectionList.OnElementDown(animation_duration, animation_style);
//        SelectionList.OnElementDown(animation_duration, animation_style);
//        ShopMenu.OnElementUp(animation_duration, animation_style);
//    }

//    public void BTN_CHARACTER_1()
//    {
//        CurrentTeamMateIndex = 0;
//    }
//    public void BTN_CHARACTER_2()
//    {
//        CurrentTeamMateIndex = 1;
//    }
//    public void BTN_CHARACTER_3()
//    {
//        CurrentTeamMateIndex = 2;
//    }
//    public void BTN_CHARACTER_4()
//    {
//        CurrentTeamMateIndex = 3;
//    }
//    public void BTN_CHARACTER_5()
//    {
//        CurrentTeamMateIndex = 4;
//    }


//    public void BTN__CUSTOMIZATIONS() 
//    {

//        this.OnElementUp(animation_duration, animation_style);
//    }
//    public void BTN__CUSTOMIZATIONS__DOWN() 
//    {

//        this.OnElementDown(animation_duration, animation_style);
//    }
//    #endregion
//}
