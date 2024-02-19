using System;
using System.Collections.Generic;

namespace Learning1
{
    //스택 : LIFO>>후입선출
    //큐 : FIFO >>선입선출

    class Graph
    {
        int[,] adj = new int[6, 6]
        {
            {0, 1, 0, 1, 0, 0, },
            {1, 0, 1, 1, 0, 0, },
            {0, 1, 0, 0, 0, 0, },
            {1, 1, 0, 0, 1, 0, },
            {0, 0, 0, 1, 0, 1, },
            {0, 0, 0, 0, 1, 0, },//
        };

        List<int>[] adj2 = new List<int>[]
        {
            new List<int>(){ 1, 3 },
            new List<int>(){ 0, 2, 3 },
            new List<int>(){ 1 },
            new List<int>(){ 0, 1, 4 },
            new List<int>(){ 3,  5 },
            new List<int>(){ 4 },
        };

        // 1.now 부터 시작하고
        // 2. now와 연결된 점들을 하나씩 확인, 미방문 상태라면 방문

        /*public void DFS(int now, bool[] visited)
        {
            Console.WriteLine(now);
            _visited[now] = true; // 방문했음을 표시

            for ( int next = 0; next < 6; next++)
            {
                if(adj[now, next] == 0)//연결되어 있지 않을때, 스킾       
                    continue;
                if (_visited[next])//다음갈 노드가 이미 방문완료일때, 스킾
                    continue;
                DFS(next, visited); //(재귀함수) 위 두개가 해당 사항이 아닐때, 다음 노드에서 시작
            }
        }

        public void DFS2(int now, bool[] visited)
        {
            Console.WriteLine(now);
            _visited[now] = true; // 방문했음을 표시

            foreach (int next in adj2[now])
            {
                //이미 연결된 정보만 다루기에 연결이 안되어있는 상황 배제
                if (_visited[next])//다음갈 노드가 이미 방문완료일때, 스킾
                    continue;
                DFS2(next, visited); //(재귀함수) 위 조건이 해당 사항이 아닐때, 다음 노드에서 시작
            }
        }

        public void SearchAll()
        {
            bool[] _visited = new bool[6];
            for (int now = 0; now < 6; now++)
            {
                if (_visited[now] == false)
                    DFS2(now, _visited);
            }
        }*/

        public void BFS(int start)
        {
            bool[] found = new bool[6];
            int[] parent = new int[6];
            int[] distance = new int[6];

            Queue<int> q = new Queue<int>();
            q.Enqueue(start);
            found[start] = true;
            parent[start] = start;
            distance[start] = 0;

            while (q.Count > 0) //q 대기열에 하나라도 있을떄 계속 반복
            {
                int now = q.Dequeue();
                Console.WriteLine(now);

                for (int next = 0; next < 6; next++)
                {
                    if (adj[now, next] == 0) //연결되어 있지 않음, 스킾
                        continue;
                    if (found[next]) //연결 되어 있지만, 이미 방문한 상태, 스킾
                        continue;
                    q.Enqueue(next);//연결은 되어 있지만, 방문하지 않았을 경우, 예약 목록에 넣어줌
                    found[next] = true; //방문 했음을 표시
                    parent[next] = now;
                    distance[next] = distance[now] + 1; //이전에서 now 까지의 거리에서 +1
                    //이렇게 경로를 거치면 많은 정보를 얻을수 있다
                }
            }

        }
    }

    class Program1
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph();

            // DFS : 깊이 우선 탐색 >> 다양한 용도 사용
            //graph.DFS(5);
            //graph.DFS2(5);
            //graph.SearchAll(); //노드 연결이 끊겨 있을때도 전부 확인 하는 함수 

            // BFS : 너비 우선 탐색 >> 최단거리 길찾기 특화
            graph.BFS(0);

        }
    }
}
