using System;
using System.Collections.Generic;

namespace Exercise //힙트리 연습
{
    class PriorityQueue<>
    {
        List<int> _heap = new List<int>();

        public void Push(int data) //bigO >> (logN)
        {
            //힙의 맨 끝에 새로운 데이터 삽입.
            _heap.Add(data);

            int now = _heap.Count - 1;
            while(now > 0)
            {
                //부모보다 강한지 약한지 체크
                int next = (now - 1) / 2;
                if (_heap[now] < _heap[next])
                    break; //실패

                int temp = _heap[now];
                _heap[now] = _heap[next];
                _heap[next] = temp; //값 교체 코드

                //검사 위치 이동
                now = next;
            }

        }

        public int Pop() //데이터 삭제
        {
            // 반환할 데이터 따로 저장
            int ret = _heap[0];

            int lastIndex = _heap.Count - 1;
            _heap[0] = _heap[lastIndex];
            _heap.RemoveAt(lastIndex);
            lastIndex--;

            //자식보다 강한지 약한지 체크
            //교체시에는 더 큰수와 교체 
            int now = 0;
            while (true)
            {
                int left = 2 * now + 1; //현재 위치로 자식 위치 번호 찾기
                int right = 2 * now + 2;//현재 위치로 자식 위치 번호 찾기

                int next = now;

                if(left <= lastIndex && _heap[next] < _heap[left])
                    next = left;    

                if(right <= lastIndex && _heap[next] < _heap[right])
                    next = right; //위 두 if문을 실행 하면 3개중 가장 큰 값이 부모가 된다.

                if (next == now)
                    break;
                // 두 값을 교체
                int temp = _heap[now];
                _heap[now] = _heap[next];
                _heap[next] = temp; //값 교체 코드

                //검사위치 이동
                now = next;
            }

            return ret;
        }

        public int Count()
        {
            return _heap.Count;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            PriorityQueue q = new PriorityQueue();
            q.Push(20);
            q.Push(10);
            q.Push(30);
            q.Push(90); 
            q.Push(40);

            while(q.Count() > 0)
            {
                Console.WriteLine( q.Pop());
            }

        }
      
    }

}