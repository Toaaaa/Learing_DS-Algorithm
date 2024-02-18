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
            {1, 1, 0, 0, 0, 0, },
            {0, 0, 0, 0, 0, 1, },
            {0, 0, 0, 0, 1, 0, },
        };

        List<int>[] adj2 = new List<int>[]
        {
            new List<int>(){ 1, 3 },
            new List<int>(){ 0, 2, 3 },
            new List<int>(){ 1 },
            new List<int>(){ 0, 1 },
            new List<int>(){  5 },
            new List<int>(){ 4 },
        };

        // 1.now 부터 시작하고
        // 2. now와 연결된 점들을 하나씩 확인, 미방문 상태라면 방문
        bool[] _visited = new bool[6];
        public void DFS(int now)
        {
            Console.WriteLine(now);
            _visited[now] = true; // 방문했음을 표시

            for ( int next = 0; next < 6; next++)
            {
                if(adj[now, next] == 0)//연결되어 있지 않을때, 스킾       
                    continue;
                if (_visited[next])//다음갈 노드가 이미 방문완료일때, 스킾
                    continue;
                DFS(next); //(재귀함수) 위 두개가 해당 사항이 아닐때, 다음 노드에서 시작
            }
        }

        public void DFS2(int now)
        {
            Console.WriteLine(now);
            _visited[now] = true; // 방문했음을 표시

            foreach (int next in adj2[now])
            {
                //이미 연결된 정보만 다루기에 연결이 안되어있는 상황 배제
                if (_visited[next])//다음갈 노드가 이미 방문완료일때, 스킾
                    continue;
                DFS2(next); //(재귀함수) 위 조건이 해당 사항이 아닐때, 다음 노드에서 시작
            }
        }

        public void SearchAll()
        {
            _visited = new bool[6];
            for (int now = 0; now < 6; now++)
            {
                if (_visited[now] == false)
                    DFS2(now);
            }
        }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            // DFS : 깊이 우선 탐색
            Graph graph = new Graph();
            //graph.DFS(5);
            //graph.DFS2(5);
            graph.SearchAll();

            // BFS : 너비 우선 탐색

        }
    }
}