
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.StandaloneInputModule;

class Shot : MonoBehaviour
{
    [SerializeField] private TextMesh textMesh;
    [SerializeField] private TextMesh codeMesh;
    [SerializeField] private GameObject selectBoxGroup;
    [SerializeField] TextAsset codeAsset;
    [SerializeField] private TextMesh nextPage;
    [SerializeField] private TextMesh prevPage;
    [SerializeField] private TextMesh curPage;
    [SerializeField] private GameObject spawnObj;
    TextMesh[] textMeshes;
    int page = 0;
    int idx = 0;
    bool b_ready_write = false;
    Dictionary<string, List<string>> keyValuePairs = new Dictionary<string, List<string>>();
    string prev_str = "";
    WubiTreeNode wubiTreeRoot;
    private Color cus_gray = Color.white * 0.2f;
    List<string> matchesInput = new List<string>();
    private void LoadAsset()
    {
        string[] lines = codeAsset.text.Split('\n').Select(x => x.Trim()).ToArray();
        foreach (string line in lines)
        {
            var pairs = line.Split(',');
            if (keyValuePairs.ContainsKey(pairs[0]))
                keyValuePairs[pairs[0]].Add(pairs[1].Trim());
            else
            {
                keyValuePairs[pairs[0]] = new List<string> { pairs[1].Trim() };
            }
        }
    }

    private void LoadAssetToTree()
    {
        WubiTreeNode root = new WubiTreeNode(new List<string> { });
        if (codeAsset == null)
        {
            Debug.LogError("CSV file not assigned.");
            return;
        }

        string[] lines = codeAsset.text.Split('\n');
        foreach (string line in lines)
        {
            string[] fields = line.TrimEnd().Split(',');
            if (fields.Length != 2)
            {
                continue;
            }
            WubiTreeNode cur = root;
            foreach (char c in fields[0])
            {
                if (!cur.Children.ContainsKey(c))
                {
                    WubiTreeNode child = new WubiTreeNode(new List<string> { });
                    cur.AddChild(c, child);
                }
                cur = cur.Children[c];

            }
            cur.Data.Add(fields[1] + "(" + fields[0] + ")");
        }
        wubiTreeRoot = root;
    }

    private void Awake()
    {
        //LoadAsset();
        LoadAssetToTree();
        textMeshes = selectBoxGroup.GetComponentsInChildren<TextMesh>();

        foreach (TextMesh t in textMeshes)
        {
            t.text = "";
        }
        textMesh.text = "";
        codeMesh.text = "";
        setPageTextAsDefault();
    }

    private bool backspaceChar(TextMesh tm)
    {
        if (tm.text.Length > 0)
        {
            tm.text = tm.text.Substring(0, tm.text.Length - 1);
            return true;
        }
        return false;
    }
    int GetMatchNumber()
    {
        if (!keyValuePairs.ContainsKey(codeMesh.text)) return 0;
        return keyValuePairs[codeMesh.text].Count;
    }

    void CatchInputString()
    {
        bool b_refresh_inputcode = false;
        bool b_refresh_matches = false;
        foreach (char c in Input.inputString)
        {
            var key = c;
            if (key <= 'Z' && key >= 'A')
            {
                key = char.ToLower(c);
            }
            if (key <= 'z' && key >= 'a')
            {
                if (codeMesh.text.Length >= 4)
                {
                    WriteToTextBox();
                    codeMesh.text = "";
                }
                codeMesh.text += key;
                b_refresh_inputcode = true;
            }
            else if (key - '0' <= textMeshes.Count() && key >= '1')
            {
                if (page * textMeshes.Count() + key - '0' < GetMatchNumber())
                {
                    idx = key - '0' - 1;
                    //write to text box
                    b_ready_write = true;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            b_ready_write = true;
            //write to text box
        }
        else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (!backspaceChar(codeMesh))
                backspaceChar(textMesh);
            b_refresh_inputcode = true;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (idx > 0) idx--;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (idx < textMeshes.Count() - 1 && page * textMeshes.Count() + idx + 1 < matchesInput.Count)
                idx++;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (hasNextPage())
            {
                page++;
                b_refresh_matches = true;
            }

        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (page > 0)
            {
                page--;
                b_refresh_matches = true;
            }
        }

        if (b_refresh_inputcode)
        {
            MatchFromTree();
            setSelectIdxAsDefault();
            b_refresh_matches = true;
            b_refresh_inputcode = false;
        }
        if (b_refresh_matches)
        {
            RefreshTextMeshes();
            b_refresh_matches = false;
        }

    }

    void setSelectIdxAsDefault()
    {
        page = 0;
        idx = 0;
    }
    void setPageTextAsDefault()
    {
        nextPage.color = cus_gray;
        prevPage.color = cus_gray;
        curPage.text = "- / -";
    }
    bool hasNextPage()
    {
        return (page + 1) * textMeshes.Count() < matchesInput.Count;
    }
    void HighLightSelectItem()
    {
        for (int i = 0; i < textMeshes.Length; i++)
        {
            if (i == idx)
            {
                textMeshes[i].color = Color.red;
            }
            else
                textMeshes[i].color = Color.white;
        }
    }
    void RefreshTextMeshes()
    {
        if (matchesInput.Count == 0)
        {
            foreach (var tm in textMeshes)
            {
                tm.text = "";
            }
            setPageTextAsDefault();
            return;
        }

        for (int i = 0; i < textMeshes.Length; i++)
        {
            if (page * textMeshes.Length + i < matchesInput.Count)
                textMeshes[i].text = (i + 1).ToString() + "." + matchesInput[page * textMeshes.Length + i];
            else
                textMeshes[i].text = "";
        }

        prevPage.color = page == 0 ? cus_gray : Color.white;
        nextPage.color = hasNextPage() ? Color.white : cus_gray;
        curPage.text = (page + 1).ToString() + "/" + ((matchesInput.Count - 1) / 7 + 1).ToString();

    }

    void MatchFromTree()
    {
        if (codeMesh.text == "")
        {
            matchesInput.Clear();
            return;
        }
        var cur = wubiTreeRoot;
        foreach (var c in codeMesh.text)
        {
            if (!cur.Children.ContainsKey(c))
            {
                matchesInput.Clear();
                return;
            }
            cur = cur.Children[c];
        }
        matchesInput = cur.GetCurAndChildData().ToList();
    }

    void ShotMainBullet()
    {
        // shot main bullet
    }
    void WriteToTextBox()
    {
        if (!b_ready_write) return;
        if (page * textMeshes.Count() + idx < matchesInput.Count)
            textMesh.text += matchesInput[page * textMeshes.Count() + idx].Split('(')[0];
        b_ready_write = false;
        var matchesObj = Utils.FindGameObjectsWithName(spawnObj, textMesh.text);
        
        if (matchesObj.Count > 0)
        {
            if (matchesObj.Count == 1 && matchesObj[0].name.Equals(textMesh.text))
            {
                ShotMainBullet();
                textMesh.text = string.Empty;
            }
        }
        else
        {
            textMesh.text = string.Empty;
        }

        setPageTextAsDefault();
        setSelectIdxAsDefault();
        codeMesh.text = string.Empty;
        matchesInput.Clear();
        RefreshTextMeshes();
    }

    private void Update()
    {
        CatchInputString();
        HighLightSelectItem();
        WriteToTextBox();
    }
}