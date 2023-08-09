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

        #region �ߡ���װ��

        //������object���洢���ݣ���Ȼ����װ�����
        //�������н���ֵ���ʹ洢ʱ������װ��
        //����ֵ���Ͷ���ȡ����ת��ʹ��ʱ�������ڲ���

        #endregion

        #region �ˡ�Hash�㷨����

        //��ϣ��ַ�� ��ϣ��������>�߼���ַ   f(key)����>Address

        //��ϣ�����Ķ��巽ʽ��

        //1.ȡ�෨�� f(key) = key%p  (pΪ������������ �������Ϊhash���еĿ�λ)  <��õķ���>
        //���֪�� Hash �����󳤶�Ϊ m������ȡ������m��������� p��Ȼ��Թؼ��ֽ���ȡ�����㣬address(key)=key % p������ p ��ѡȡ�ǳ��ؼ���p ѡ��ĺõĻ����ܹ����̶ȵؼ��ٳ�ͻ��p һ��ȡ������m�����������
        //Hash����������� �����˷ѿռ� ������С���ײ�����ͻ
        //Hash ���Сȷ��ͨ����������˼·��
        //������֪���洢��������������Ҫ���ݴ洢���� �� �ؼ��ֵķֲ��ص���ȷ�� Hash ��Ĵ�С��
        //���Ȳ�֪��������Ҫ�洢�ļ�¼��������Ҫ��̬ά��Hash�����������ʱ������Ҫ���¼��� Hash ��ַ��

        //2.ƽ��ȡ�з�
        //�Թؼ��ֽ���ƽ�����㣬ȡ������м伸λ��Ϊ Hash ��ַ���������¹ؼ������� { 421��423��436} ��ƽ��֮��Ľ��Ϊ { 177241��178929��190096} ����ô����ȡ�м����λ�� { 72��89��00} ��Ϊ Hash ��ַ��

        //3.ֱ�Ӷ�ַ��
        //ȡ�ؼ��ֻ��߹ؼ��ֵ�ĳ�����Ժ���Ϊ Hash ��ַ����address(key) = a * key + b; ��֪��ѧ����ѧ�Ŵ�2000��ʼ�����Ϊ4000������Խ�address(key) = key - 2000(����a = 1)��ΪHash��ַ��

        //4.�۵���
        //���ؼ��ֲ�ֳɼ����֣�Ȼ���⼸���������һ�����ض��ķ�ʽ����ת���γ�Hash��ַ����ͼ��� ISBN ��Ϊ 8903 - 241 - 23�����Խ� address(key)= 89 + 03 + 24 + 12 + 3 ��Ϊ Hash ��ַ��

        #endregion

        #region �ˡ�Hash��ͻ

        //Hash������ɢ�б������ǰ����ⳤ�ȵ����룬ͨ��ɢ���㷨����ɹ̶��������������������ɢ��ֵ��
        //��ʵ����ת����һ��ѹ��ӳ�䣬ɢ�б�Ŀռ�ͨ��С������Ŀռ䣬��ͬ��������ܻ�ɢ�г���ͬ����������Բ��ܴ�ɢ�б���Ψһ��ȷ������ֵ����ͳ�����Hash��ͻ��
        //Hash��ͻ��
        //����key������������һ������f(key)�õ��Ľ������Ϊ��ַȥ��ŵ�ǰ��key value��ֵ��(�����hashmap�Ĵ�ֵ��ʽ)������ȴ����������ĵ�ַ���Ѿ���ռ���ˡ��������ν��hash��ͻ��

        //���Hash��ͻ
        //1.���Ŷ�ַ��
        //ϸ�֣�(1).����̽�鷨�������ǰ��Ԫ��ռ���� �����ж���һ����Ԫ�Ƿ�ռ�� ֱ���ҵ����е�Ԫ���߱������е�ԪΪֹ 
        //(2).ƽ��̽�鷨���ӷ�����ͻ�ĵ�Ԫ�� ����f(key)+1��ƽ�� f(key)+2��ƽ��...
        //(3).˫ɢ�к���̽�鷨����������h1��h2 �Թؼ���Ϊ�Ա��� h1����һ����0��m-1֮�������Ϊɢ�е�ַ h2����һ����1��m-1�Һ�m����(���ܱ�m����)������Ϊ̽���ַ�Ĳ�������
        //(4).α���̽�鷨������f(69)=3 ��ͻ�� ��ʱ���Ҷ���һ��α��������� 2 5 9  ��ô��һ��ɢ�е�ַΪ f(3+2)=3 ���軹�ǳ�ͻ�� ��ô f(3+5)=1 

        //2.����ַ��(������)
        //�����й�ϣ��ַΪi��Ԫ�ع���һ����Ϊͬ������ĵ����������������ͷָ����ڹ�ϣ��ĵ�i����Ԫ�У�������ҡ������ɾ����Ҫ��ͬ������н��С�����ַ�������ھ������в����ɾ���������

        //3.��Hash��
        //���ַ�������ͬʱ��������ͬ�Ĺ�ϣ������ Hi = RH1��key��  i = 1��2������k������ϣ��ַHi = RH1��key��������ͻʱ���ټ���Hi = RH2��key��������ֱ����ͻ���ٲ��������ַ������ײ����ۼ����������˼���ʱ�䡣

        //4.�������������
        //���ַ����Ļ���˼���ǣ�����ϣ���Ϊ�����������������֣����Ǻͻ���������ͻ��Ԫ�أ�һ�����������

        #endregion




    }
}
