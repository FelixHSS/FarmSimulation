using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Collections.Generic;
using System;


public class ItemEditor : EditorWindow
{
    private ItemDataList_SO Database { get; set; }
    private List<ItemDetails> ItemList { get; set; }
    private VisualTreeAsset ItemRowTemplate { get; set; }
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
        // find ListView in UI
        ItemListView = root.Q<VisualElement>("ItemList").Q<ListView>("ListView");

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

        ItemListView.itemsSource = ItemList;
        ItemListView.makeItem = makeItem;
        ItemListView.bindItem = bindItem;
    }
}