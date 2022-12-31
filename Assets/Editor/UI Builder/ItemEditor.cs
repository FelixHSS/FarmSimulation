using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Collections.Generic;
using System;
using System.Linq;

public class ItemEditor : EditorWindow
{
    private ItemDataList_SO Database { get; set; }
    private List<ItemDetails> ItemList { get; set; }
    private VisualTreeAsset ItemRowTemplate { get; set; }
    private ScrollView ItemDetailsSection { get; set; }
    private ItemDetails ActiveItem { get; set; }
    private Sprite DefaultIcon { get; set; }
    private VisualElement IconPreview { get; set; }
    private ListView ItemListView { get; set; }

    [MenuItem("M STUDIO/ItemEditor")]
    public static void ShowExample()
    {
        ItemEditor wnd = GetWindow<ItemEditor>();
        wnd.titleContent = new GUIContent("ItemEditor");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        //VisualElement label = new Label("Hello World! From C#");
        //root.Add(label);

        // Import UXML
        VisualTreeAsset visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/UI Builder/ItemEditor.uxml");
        VisualElement labelFromUXML = visualTree.Instantiate();
        root.Add(labelFromUXML);

        //// A stylesheet can be added to a VisualElement.
        //// The style will be applied to the VisualElement and all of its children.
        //var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/UI Builder/ItemEditor.uss");
        //VisualElement labelWithStyle = new Label("Hello World! With Style");
        //labelWithStyle.styleSheets.Add(styleSheet);
        //root.Add(labelWithStyle);

        //retrive the template using absolute path
        ItemRowTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/UI Builder/ItemRowTemplate.uxml");
        //retrive default icon
        DefaultIcon = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/M Studio/Art/Items/Icons/icon_M.png");
        // find ListView in UI
        ItemListView = root.Q<VisualElement>("ItemList").Q<ListView>("ListView");

        ItemDetailsSection = root.Q<ScrollView>("ItemDetails");

        IconPreview = ItemDetailsSection.Q<VisualElement>("Icon");

        LoadDatabase();

        GenerateListView();
    }

    private void LoadDatabase()
    {
        string[] dataArray = AssetDatabase.FindAssets("ItemDataList_SO");

        if (dataArray.Length > 1)
        {
            string path = AssetDatabase.GUIDToAssetPath(dataArray[0]);
            Database = AssetDatabase.LoadAssetAtPath(path, typeof(ItemDataList_SO)) as ItemDataList_SO;
        }

        ItemList = Database.ItemDetailsList;
        //necessary
        EditorUtility.SetDirty(Database);
    }

    private void GenerateListView()
    {
        Func<VisualElement> makeItem = () => ItemRowTemplate.CloneTree();

        Action<VisualElement, int> bindItem = (e, i) =>
        {
            if (i < ItemList.Count)
            {
                if (ItemList[i].ItemIcon!= null)
                    e.Q<VisualElement>("Icon").style.backgroundImage = ItemList[i].ItemIcon.texture;
                e.Q<Label>("Name").text = ItemList[i] == null ? "NO NAME" : ItemList[i].ItemName;
            };
        };

        ItemListView.fixedItemHeight = 60;
        ItemListView.itemsSource = ItemList;
        ItemListView.makeItem = makeItem;
        ItemListView.bindItem = bindItem;

        ItemListView.onSelectionChange += OnListSelectionChange;
        //Detail information section is invisible before choose a item
        ItemDetailsSection.visible = false;
    }

    private void OnListSelectionChange(IEnumerable<object> selectedItem)
    {
        ActiveItem = (ItemDetails)selectedItem.First();
        GetItemDetails();
        ItemDetailsSection.visible = true;
    }

    private void GetItemDetails()
    {
        ItemDetailsSection.MarkDirtyRepaint();

        ItemDetailsSection.Q<IntegerField>("ItemID").value = ActiveItem.ItemID;
        ItemDetailsSection.Q<IntegerField>("ItemID").RegisterValueChangedCallback(evt =>
        {
            ActiveItem.ItemID = evt.newValue;
        });

        ItemDetailsSection.Q<TextField>("ItemName").value = ActiveItem.ItemName;
        ItemDetailsSection.Q<TextField>("ItemName").RegisterValueChangedCallback(evt =>
        {
            ActiveItem.ItemName = evt.newValue;
            ItemListView.Rebuild();
        });

        IconPreview.style.backgroundImage = ActiveItem.ItemIcon == null ? DefaultIcon.texture : ActiveItem.ItemIcon.texture;
        ItemDetailsSection.Q<ObjectField>("ItemIcon").RegisterValueChangedCallback(evt =>
        {
            Sprite newIcon = evt.newValue as Sprite;
            ActiveItem.ItemIcon = newIcon;

            IconPreview.style.backgroundImage = newIcon == null ? DefaultIcon.texture : newIcon.texture;
            ItemListView.Rebuild();
        });

        ItemDetailsSection.Q<ObjectField>("ItemSprite").value = ActiveItem.ItemOnWorldSprite;
        ItemDetailsSection.Q<ObjectField>("ItemSprite").RegisterValueChangedCallback(evt =>
        {
            ActiveItem.ItemOnWorldSprite = (Sprite)evt.newValue;
        });

        ItemDetailsSection.Q<EnumField>("ItemType").Init(ActiveItem.ItemType); //??
        ItemDetailsSection.Q<EnumField>("ItemType").value = ActiveItem.ItemType;
        ItemDetailsSection.Q<EnumField>("ItemType").RegisterValueChangedCallback(evt =>
        {
            ActiveItem.ItemType = (ItemType)evt.newValue;
        });

        ItemDetailsSection.Q<TextField>("Description").value = ActiveItem.ItemDescription;
        ItemDetailsSection.Q<TextField>("Description").RegisterValueChangedCallback(evt =>
        {
            ActiveItem.ItemDescription = evt.newValue;
        });

        ItemDetailsSection.Q<IntegerField>("ItemUseRadius").value = ActiveItem.ItemUseRadius;
        ItemDetailsSection.Q<IntegerField>("ItemUseRadius").RegisterValueChangedCallback(evt =>
        {
            ActiveItem.ItemUseRadius = evt.newValue;
        });

        ItemDetailsSection.Q<Toggle>("CanPickedup").value = ActiveItem.CanPicked;
        ItemDetailsSection.Q<Toggle>("CanPickedup").RegisterValueChangedCallback(evt =>
        {
            ActiveItem.CanPicked = evt.newValue;
        });

        ItemDetailsSection.Q<Toggle>("CanDropped").value = ActiveItem.CanDropped;
        ItemDetailsSection.Q<Toggle>("CanDropped").RegisterValueChangedCallback(evt =>
        {
            ActiveItem.CanDropped = evt.newValue;
        });

        ItemDetailsSection.Q<Toggle>("CanCarried").value = ActiveItem.CanCarried;
        ItemDetailsSection.Q<Toggle>("CanCarried").RegisterValueChangedCallback(evt =>
        {
            ActiveItem.CanCarried = evt.newValue;
        });

        ItemDetailsSection.Q<IntegerField>("Price").value = ActiveItem.ItemPrice;
        ItemDetailsSection.Q<IntegerField>("Price").RegisterValueChangedCallback(evt =>
        {
            ActiveItem.ItemPrice = evt.newValue;
        });

        ItemDetailsSection.Q<Slider>("SellPercentage").value = ActiveItem.SellPercentage;
        ItemDetailsSection.Q<Slider>("SellPercentage").RegisterValueChangedCallback(evt =>
        {
            ActiveItem.SellPercentage = evt.newValue;
        });
    }
}