using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace class1_1
{
    /*class MyList<T>
    {
        const int DEFUALT_SIZE = 1;
        T[] _data = new T[DEFUALT_SIZE];
        public int Count = 0; //실제로 사용중인 데이터 개수
        public int Capacity { get { return _data.Length; } } //예약된 데이터 개수
        
        public void Add(T item) //**ORDER가 O(N)이 아니라 예외케이스로 O(1)이다.
        {
            //1. 공간이 충분히 남아있는지 확인
            if(Count >= Capacity) //공간이 부족할떄
            {
                //공간을 다시 늘려서 확보
                T[] newArray = new T[Count * 2];
                for(int i =0; i<Count; i++)
                {
                    newArray[i] = _data[i]; //기존의 data에 있던 데이터를 newarray로 하나씩 옮겨 주기                    
                }
                _data = newArray; // 전체를 _data로 인정하기.
            }
            //2. 공간에 데이터 넣어주기
            _data[Count] = item;
            Count++;
        }
        public T this[int index]//O(1)이다.
        {
            get { return _data[index]; }
            set { _data[index] = value; }
        }
        public void RemoveAt(int index)//O(N)이다. 데이터 크기에 비례하는 for문을사용.
        {
            for(int i =index; i< Count -1; i++)
            {
                _data[i] = _data[i + 1];              
            }
            _data[Count - 1] = default(T); //끝 데이터는 해당 데이터를 해당 형태에 맞는 기본값으로 초기화해줌.
            Count--;
        }
    }*/

   class MyLinkedListNode<T>
    {
        public T Data;
        public MyLinkedListNode<T> Next;
        public MyLinkedListNode<T> Prev;
    }
    
    class MyLinkedList<T>
    {
        public MyLinkedListNode<T> Head = null;
        public MyLinkedListNode<T> Tail = null;
        public int Count = 0;

        public MyLinkedListNode<T> AddLast(T data)//마지막 순번 방에 T 를추가 //O(1)
        {
            MyLinkedListNode<T> newRoom = new MyLinkedListNode<T>();
            newRoom.Data = data;

            if(Head == null) //만약 방이 하나도 없으면 , 새로추가하는 방이 head가 됨
            {
                Head = newRoom;
            }

            if(Tail != null) //tail 방 확인후, 서로 연결해 주는 작업
            {
                Tail.Next = newRoom;
                newRoom.Prev = Tail;
            }

            Tail = newRoom;
            Count++;
            return newRoom;

        }

        public void Remove(MyLinkedListNode<T> room)//O(1)
        {
            if(Head == room) //지우려는 방이 head 인 경우, 다음방을 head로 지정
            {
                Head = Head.Next;
            }
            if(Tail == room) //지우려는 방이 tail 인경우, 이전방을 tail로 지정
            {
                Tail = Tail.Prev;
            }
            if(room.Prev != null)
            {
                room.Prev.Next = room.Next;
            }
            if(room.Next != null)
            {
                room.Next.Prev = room.Prev; 
            }
        }
    }
    class Board
    {
        public int[] _data = new int[25];//배열 , 연속된 방을 사용한다
        //public MyList<int> _data2 = new MyList<int>();//동적 배열 >>처음부터 여유있게 배열칸을 보유, 유동적으로 활용가능
        public MyLinkedList<int> _data3 = new MyLinkedList<int>();//연결 리스트
        
        public void Initialize()
        {
            _data3.AddLast(101);
            _data3.AddLast(102);
            MyLinkedListNode<int> node = _data3.AddLast(103);
            _data3.AddLast(104);
            _data3.AddLast(105);

            _data3.Remove(node);
         }
    }
}
