using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HashTablePractice : MonoBehaviour
{

    #region HashTable
    //HashTable �ֳ�ɢ�б� �ǻ��ڼ��Ĺ�ϣ������֯�����ļ�/ֵ��
    //������Ҫ������������ݲ�ѯ��Ч��
    //����ʹ�ü������ٷ��ʼ����е�Ԫ��

    #endregion

    void Start()
    {
        #region һ������

        Hashtable hashTable = new Hashtable();

        #endregion

        #region  ��������

        //����  hashTable.Add(Key,Value)
        //���ܳ�����ͬ��Key
        hashTable.Add("Name", "Zhuhongli");
        hashTable.Add("Age", 22);
        hashTable.Add(007, "�ϰ�ʱ��");
        hashTable.Add(true, false);

        #endregion


        #region  ����ɾ��

        //ɾ��  hashTable.Remove(Key)
        //1.Remove�����Ӧ��Keyֵ Remove���ɾ����Ӧ��Key��ֵ
        //2.Removeû�е�Key û�з�Ӧ ���ᱨ��
        //3.������е�key ����Clear()����

        hashTable.Remove("Name");
        hashTable.Remove(007);
        hashTable.Remove(true);
        hashTable.Clear();

        #endregion

        #region �ġ�����/��ȡ

        //1.ͨ���������ֵ
        var Name = hashTable["Name"];
        var Age = hashTable["age"];
        var buer = hashTable[true];
        //�����ȡ����ֵ ��ô�᷵�ؿ�
        var value = hashTable[1231];  //bull  

        //2.�ж��Ƿ��������/ֵ��
        hashTable.Contains(1);       //ͨ�������ж�
        hashTable.ContainsKey(1);    //ͨ�������ж�
        hashTable.ContainsValue(1);  //ͨ��ֵ���ж�

        //ע�ⲻ��ͨ��ֵ���Ҷ�Ӧ�ļ�

        #endregion

        #region �塢��

        //��������ֵ��ֱ�Ӹ�ֵ
        hashTable["Name"] = "BuXiangJiaBan";

        #endregion

        #region ��������

        //��ȡ��/ֵ����  ���ǲ�����Forѭ���õ����еļ�ֵ�� ��Ϊkey��ȷ��
        int hashTbaleCount = hashTable.Count;

        //����hashTable�ļ���ֵ
        foreach(object item in hashTable.Keys)
        {
            Debug.Log("����" + item);
            Debug.Log("ֵ��" + hashTable[item]);
        }

        //����hashTable�ļ���ֵ
        foreach (DictionaryEntry item in hashTable)
        {
            Debug.Log("����" + item.Key);
            Debug.Log("ֵ��" + item.Value);
        }

        //ͨ������������ �����̣�
        IDictionaryEnumerator myEnumerator = hashTable.GetEnumerator();
        bool flag = myEnumerator.MoveNext();
        while (flag)
        {
            Debug.Log("����" + myEnumerator.Key + "ֵ��" + myEnumerator.Value);
            flag = myEnumerator.MoveNext();
        }

        #endregion

        #region ��װ��

        //������object���洢���ݣ���Ȼ����װ�����
        //�������н���ֵ���ʹ洢ʱ������װ��
        //����ֵ���Ͷ���ȡ����ת��ʹ��ʱ�������ڲ���

        #endregion

    }
}
