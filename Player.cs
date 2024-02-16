using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace class1_1
{
     class Player
    {
        public int PosY { get; private set; }
        public int PosX { get; private set; }
        Random _random = new Random();
        Board _board;


        public void initialize(int posY, int posX, int DestY, int DestX, Board board)
        {
            PosY = posY;
            PosX = posX;

            _board = board;
        }


        const int Move_Tick = 100;
        int _SumTick = 0;
        public void Update(int deltaTick)
        {
            _SumTick += deltaTick;
            if(_SumTick >= Move_Tick)
            {
                _SumTick = 0;

                //여기에 0.1초마다 실행될 로직 대입
                int randValue =_random.Next(0, 4);
                switch (randValue)
                {
                    case 0://상
                        if(PosY -1 >=1 &&_board.Tile[PosY -1 , PosX] == Board.TileType.Empty)
                        {
                            PosY = PosY - 1;
                        }
                        break;
                    case 1://하
                        if (PosY + 1< _board.Size &&_board.Tile[PosY + 1, PosX] == Board.TileType.Empty)
                        {
                            PosY = PosY + 1;
                        }
                        break;
                    case 2://좌
                        if (PosX - 1 >= 1 && _board.Tile[PosY, PosX - 1] == Board.TileType.Empty)
                        {
                            PosX = PosX - 1;
                        }
                        break;
                    case 3://우
                        if (PosX + 1 < _board.Size && _board.Tile[PosY, PosX + 1] == Board.TileType.Empty)
                        {
                            PosX = PosX + 1;
                        }
                        break;
                }
            }
        }
    }
}
