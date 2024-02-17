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
            //바라보는 방향 기준으로의 좌표 변화
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
