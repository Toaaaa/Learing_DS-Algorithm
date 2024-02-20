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
            {-1, 15, -1, 35, -1, -1, },
            {15, -1, 5, 10, -1, -1, },
            {-1, 5, -1, -1, -1, -1, },
            {35, 10, -1, -1, 5, -1, },
            {-1, -1, -1, 5, -1, 5, },
            {-1, -1, -1, -1, 5, -1, },//
        };

       public void Dijkstra(int start)
        {
            bool[] visited = new bool[6];
            int[] distance = new int[6];
            int[] parent = new int[6];
            Array.Fill(distance, Int32.MaxValue); //혼동하지 않게 매우 큰 수 입력

            distance[start] = 0;
            parent[start] = start;

            while (true)
            {
                // 제일 좋은 후보를 찾기. (가장 가까이에 있는것)

                //가장 유력한 후보의 거리 번호 저장
                int closest = Int32.MaxValue;
                int now = -1;

                for(int i=0; i<6; i++)
                {
                    //이미 방문한 곳은 스킾
                    if (visited[i])
                        continue;
                    //발견된적이 없거나, 후보보다 멀리 있을떄
                    if (distance[i] == Int32.MaxValue || distance[i] >= closest)
                        continue;
                    //현재 가장 좋은 후보, 정보 갱신
                    closest = distance[i];
                    now = i;
                }

                if (now == -1) //후보가 없다 >> 연결이 단절 or 전부 방문 완료
                    break;

                //제일 좋은 후보를 찾았으니 방문
                visited[now] = true;

                for(int next =0; next<6; next++)
                {
                    if (adj[now, next] == -1) //연결되어 있지 않음, 스킾
                        continue;
                    if (visited[next]) //이미 방문한 곳은 스킾
                        continue;

                    int nextDist = distance[now] + adj[now, next];
                    if (nextDist < distance[next]) //더 짧은 거리를 발견했을때
                    {
                        distance[next] = nextDist; //해당 거리를 최단 거리로 세팅
                        parent[next] = now;
                    }
                    
                }
            }

        }

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

            Queue<int> q = new Queue<int>();
            q.Enqueue(start);
            found[start] = true;

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

                }
            }

        }
    }

    class Program1
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph();

            // DFS : 깊이 우선 탐색
            //graph.DFS(5);
            //graph.DFS2(5);
            //graph.SearchAll(); //노드 연결이 끊겨 있을때도 전부 확인 하는 함수 

            // BFS : 너비 우선 탐색
            graph.Dijkstra(0);

        }
    }
}