using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfWithNet.Models
{
    public class Node<T>
    {
        public T Data { set; get; }          //数据域,当前结点数据
        public Node<T> Next { set; get; }    //位置域,下一个结点地址

        public Node(T item)
        {
            this.Data = item;
            this.Next = null;
        }

        public Node()
        {
            this.Data = default(T);
            this.Next = null;
        }
    }

    public class LinkList<T>
    {
        public Node<T> Head { set; get; } //单链表头
        public int Pointer { set; get; }
        //构造
        public LinkList()
        {
            Clear();
        }

        /// <summary>
        /// 求单链表的长度
        /// </summary>
        /// <returns></returns>
        public int GetLength()
        {
            Node<T> p = Head;
            int length = 0;
            while (p != null)
            {
                p = p.Next;
                length++;
            }
            return length;
        }

        /// <summary>
        /// 判断单键表是否为空
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            if (Head == null)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 清空单链表
        /// </summary>
        public void Clear()
        {
            Head = null;
        }

        /// <summary>
        /// 获得当前位置单链表中结点的值
        /// </summary>
        /// <param name="i">结点位置</param>
        /// <returns></returns>
        public T GetNodeValue(int i)
        {
            if (IsEmpty() || i < 1 || i > GetLength())
            {
                Console.WriteLine("单链表为空或结点位置有误！");
                return default(T);
            }

            Node<T> A = new Node<T>();
            A = Head;
            int j = 1;
            while (A.Next != null && j < i)
            {
                A = A.Next;
                j++;
            }

            return A.Data;
        }

        public int GetPosition(T item)
        {
            if (!IsEmpty())
            {
                Node<T> A = new Node<T>();
                A = Head;
                int i = 1;
                while (A !=null && !A.Data.Equals(item)  )
                {
                    A = A.Next;
                    i++;
                }
                if (i < GetLength()+1)
                {
                    return i;
                }
                else
                {
                    return -1;
                }

            }
            else
            {
                return -1;
            }
            
        }

        /// <summary>
        /// 增加新元素到单链表末尾
        /// </summary>
        public void Append(T item)
        {
            Node<T> foot = new Node<T>(item);
            Node<T> A = new Node<T>();
            if (Head == null)
            {
                Head = foot;
                return;
            }
            A = Head;
            while (A.Next != null)
            {
                A = A.Next;
            }
            A.Next = foot;
            Pointer = GetLength();
        }

        /// <summary>
        /// 增加单链表插入的位置
        /// </summary>
        /// <param name="item">结点内容</param>
        /// <param name="n">结点插入的位置</param>
        public void Insert(T item, int n)
        {
            if (IsEmpty() || n < 1 || n > GetLength())
            {
                Console.WriteLine("单链表为空或结点位置有误！");
                return;
            }

            if (n == 1)  //增加到头部
            {
                Node<T> H = new Node<T>(item);
                H.Next = Head;
                Head = H;
                return;
            }

            Node<T> A = new Node<T>();
            Node<T> B = new Node<T>();
            B = Head;
            int j = 1;
            while (B.Next != null && j < n)
            {
                A = B;
                B = B.Next;
                j++;
            }

            if (j == n)
            {
                Node<T> C = new Node<T>(item);
                A.Next = C;
                C.Next = B;
            }
        }

        /// <summary>
        /// 删除单链表结点
        /// </summary>
        /// <param name="i">删除结点位置</param>
        /// <returns></returns>
        public void Delete(int i)
        {
            if (IsEmpty() || i < 1 || i > GetLength())
            {
                Console.WriteLine("单链表为空或结点位置有误！");
                return;
            }

            Node<T> A = new Node<T>();
            if (i == 1)   //删除头
            {
                A = Head;
                Head = Head.Next;
                return;
            }
            Node<T> B = new Node<T>();
            B = Head;
            int j = 1;
            while (B.Next != null && j < i)
            {
                A = B;
                B = B.Next;
                j++;
            }
            if (j == i)
            {
                A.Next = B.Next;
            }
        }

        /// <summary>
        /// 显示单链表
        /// </summary>
        public void Dispaly()
        {
            Node<T> A = new Node<T>();
            A = Head;
            while (A != null)
            {
                Console.WriteLine(A.Data);
                A = A.Next;
            }
        }

        public void Append1(T item)
        {

            if (GetPosition(item) != -1)
            {
                Delete(GetPosition(item));                
            }
            Append(item);

        }
    }
 }
