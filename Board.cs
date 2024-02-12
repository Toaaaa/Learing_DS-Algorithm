using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace class1_1
{
    class MyList<T>
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
    }

   class Board
    {
        public int[] _data = new int[25];//배열 , 연속된 방을 사용한다
        public MyList<int> _data2 = new MyList<int>();//동적 배열 >>처음부터 여유있게 배열칸을 보유, 유동적으로 활용가능
        public LinkedList<int> _data3 = new LinkedList<int>();//연결 리스트
        //123
        public void Initialize()
        {
            _data2.Add(101);
            _data2.Add(102);
            _data2.Add(103);
            _data2.Add(104);
            _data2.Add(105);

            int temp = _data2[2];

            _data2.RemoveAt(2); //3번째 데이터를 삭제 (0,1,2,3,4)
        }
    }
}
