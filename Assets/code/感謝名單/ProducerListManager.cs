using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ProducerListManager : MonoBehaviour
{
    // 自定义数据结构，表示制作人员信息
    public struct ProducerInfo
    {
        public string name;
        public string role;
    }

    public Text producerListText;
    public List<ProducerInfo> producerList;

    void Start()
    {
        // 初始化制作人员列表
        producerList = new List<ProducerInfo>();
        producerList.Add(new ProducerInfo { name = "曹宥翔", role = "Designer" });
        producerList.Add(new ProducerInfo { name = "Jane Smith", role = "Programmer" });

        // 在UI上显示制作人员名单
        UpdateProducerListUI();
    }

    void UpdateProducerListUI()
    {
        string listText = "Producer List:\n";
        foreach (ProducerInfo producer in producerList)
        {
            listText += producer.name + " - " + producer.role + "\n";
        }
        producerListText.text = listText;
    }
}
