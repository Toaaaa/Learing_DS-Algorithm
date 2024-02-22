using System;
using System.Collections.Generic;

namespace Exercise //힙트리 연습
{
    class PriorityQueue<T> where T : IComparable<T> // 인터페이스를 이용해 대소비교 가 가능한 type만 사용하게 제한
    {
        List<T> _heap = new List<T>();

        public void Push(T data) //bigO >> (logN)
        {
            //힙의 맨 끝에 새로운 데이터 삽입.
            _heap.Add(data);

            int now = _heap.Count - 1;
            while(now > 0)
            {
                //부모보다 강한지 약한지 체크
                int next = (now - 1) / 2;
                if (_heap[now].CompareTo( _heap[next]) < 0)
                    break; //실패

                T temp = _heap[now];
                _heap[now] = _heap[next];
                _heap[next] = temp; //값 교체 코드

                //검사 위치 이동
                now = next;
            }

        }

        public T Pop() //데이터 삭제
        {
            // 반환할 데이터 따로 저장
            T ret = _heap[0];

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

                if(left <= lastIndex && _heap[next].CompareTo(_heap[left]) < 0)
                    next = left;    

                if(right <= lastIndex && _heap[next].CompareTo (_heap[right]) <0 )
                    next = right; //위 두 if문을 실행 하면 3개중 가장 큰 값이 부모가 된다.

                if (next == now)
                    break;
                // 두 값을 교체
                T temp = _heap[now];
                _heap[now] = _heap[next];
                _heap[next] = temp; //값 교체 코드

                //검사위치 이동
                now = next;
            }

            return ret;
        }

        public int Count { get { return _heap.Count; }  }
    }
    
}