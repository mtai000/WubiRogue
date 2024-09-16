
using System.Collections.Generic;
using System.Linq;

public class WubiTreeNode
{
    public List<string> Data { get; set; }
    public Dictionary<char, WubiTreeNode> Children { get; set; }
    
    public WubiTreeNode(List<string> data)
    {
        Data = data;
        Children = new Dictionary<char, WubiTreeNode>();
    }

    public void AddChild(char key, WubiTreeNode child)
    {
        if (!Children.ContainsKey(key))
        {
            Children.Add(key, child);
        }
    }

    public WubiTreeNode GetChild(char key)
    {
        if (Children.ContainsKey(key))
        {
            return Children[key];
        }
        return null;
    }

    public List<string> GetCurAndChildData()
    {
        List<string> ret = new List<string>();
        ret.AddRange(Data);
        foreach (char key in Children.Keys)
        {
            ret.AddRange(Children[key].GetCurAndChildData());
        }
        return ret;
    }

}
