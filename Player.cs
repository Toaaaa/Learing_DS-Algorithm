using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace class1_1
{
    class Pos
    {
        public Pos(int y, int x)
        {
            this.Y = y;
            this.X = x;
        }

        public int Y;
        public int X;
    }

     class Player
    {
        public int PosY { get; private set; }
        public int PosX { get; private set; }
        Random _random = new Random();
        Board _board;

        enum Dir
        {
            Up = 0,
            Left = 1,
            Down = 2,
            Right = 3
        }

        int _dir = (int)Dir.Up;
        List<Pos> _points = new List<Pos>();

        public void initialize(int posY, int posX, Board board)
        {
            PosY = posY;
            PosX = posX;
            _board = board;

            BFS();
            
            
        }

        voic BFS()
        {
            int[] deltaY = new int[] { -1, 0, 1, 0};//위의 Dir과 같은 방식의 방향을 나타내는 배열
            int[] deltaX = new int[] { 0, -1, 0, 1};

            bool[,] found = new bool[_board.Size, _board.Size];
            Pos[,] parent = new Pos[_board.Size, _board.Size];

            Queue<Pos> q = new Queue<Pos>();
            q.Enqueue(new Pos(PosY, PosX));
            found[PosY, PosX] = true;
            parent[PosY, PosX] = new Pos(PosY, PosX);  

            while(q.Count > 0)
            {
                Pos pos = q.Dequeue();
                int nowY = pos.Y;
                int nowX = pos.X;

                for(int i =0; i<4; i++)
                {
                    int nextY = nowY + deltaY[i];
                    int nextX = nowX + deltaX[i];
                    if(nextX < 0 || nextX >= _board.Size || nextY < 0 || nextY >= _board.Size) //갈수없는곳 or 범위 초과일떄
                        continue;
                    if (_board.Tile[nextY, nextX] == Board.TileType.Wall) //벽
                        continue;
                    if (found[nextY, nextX]) //이미 지나감
                        continue;
                    // 위 조건을 전부 통과시 처음 지나가는 점+ 갈수있는점
                    q.Enqueue(new Pos(nextY, nextX)); //해당점 예약
                    found[nextY, nextX] = true; //표시
                    parent[nextY, nextX] = new Pos(nowY, nowX);
                }
            }

            int y = _board.DestY;
            int x = _board.DestX;
            while (parent[y,x].Y !=y || parent[y,x].X != x)
            {
                _points.Add(new Pos(y, x));
                Pos pos = parent[y, x];
                y = pos.Y;
                x = pos.X;
            }
            _points.Add(new Pos(y, x));
            _points.Reverse();
        }

        void RightHand()
        {
            int[] frontY = new int[] { -1, 0, 1, 0 }; //이것을 이용하면 코드를 간소화 시켜서 방향을 구현할 수있다
            int[] frontX = new int[] { 0, -1, 0, 1 };
            int[] rightY = new int[] { 0, -1, 0, 1 };
            int[] rightX = new int[] { 1, 0, -1, 0 };

            _points.Add(new Pos(PosY, PosX));

            while (PosY != board.DestY || posX != board.DestX) //목적지 도착전까지 재생
            {
                //1. 현재 바라보는 방향에서 오른쪽으로 갈수 있는지 확인
                if (_board.Tile[PosY + rightY[_dir], PosX + rightX[_dir]] == Board.TileType.Empty)
                {
                    //오른쪽으로 회전 + 앞으로 이동
                    _dir = (_dir - 1 + 4) % 4; //방향전환
                    PosY = PosY + frontY[_dir];//앞으로 전진
                    PosX = PosX + frontX[_dir];
                    _points.Add(new Pos(PosY, PosX));//전진 데이터 저장
                }
                //2. 현재 바라보는 방향기준 앞으로 갈수있는지 확인
                else if (_board.Tile[PosY + frontY[_dir], PosX + frontX[_dir]] == Board.TileType.Empty) //앞으로 갈수 있을때
                {
                    //앞으로 전진
                    PosY = PosY + frontY[_dir];
                    PosX = PosX + frontX[_dir];
                    _points.Add(new Pos(PosY, PosX));//전진 데이터 저장
                }
                else
                {
                    //왼쪽방향으로 90도 회전
                    _dir = (_dir + 1 + 4) % 4;
                }
            }
        }

        const int Move_Tick = 100;
        int _SumTick = 0;
        int _lastIndex = 0;
        public void Update(int deltaTick)
        {
            if(_lastIndex >= _points.Count) //범위 초과시 리턴
                return;

            _SumTick += deltaTick;
            if(_SumTick >= Move_Tick)
            {
                _SumTick = 0;

                PosY = _points[_lastIndex].Y;
                PosX = _points[_lastIndex].X;
                _lastIndex++;
            }
        }
    }
}
