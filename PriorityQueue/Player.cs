using Exercise;
using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithm
{
	class Pos
	{
		public Pos(int y, int x) { Y = y; X = x; }
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

		public void Initialize(int posY, int posX, Board board)
		{
			PosY = posY;
			PosX = posX;
			_board = board;

			AStar();
		}

		struct PQNode : IComparable<PQNode>
		{
			public int F;
			public int G;
			public int Y;
			public int X;

            public int CompareTo(PQNode other)
            {
				if (F == other.F)
					return 0;
				return F < other.F ? 1 : -1;
            }
        }

		void AStar()
		{
			//UP, LEFT, DOWN, RIGHT
			int[] deltaY = new int[] { -1, 0, 1, 0 };
			int[] deltaX = new int[] { 0, -1, 0, 1 };
			int[] cost = new int[] { 1, 1, 1, 1 };

			//점수 매기기 
			// F = G + H
			// F >> 최종점수, 작을수록 좋다. + 경로에따라 달라짐
			// G >> 시작점에서 해당좌표까지 이동하는데 드는 비용 + 경로에 따라서 달라짐
			// H >> 목적지에서 얼마나 가까운지, 작을수록 좋다. + 경로에따라 달라지지 않음

			// 방문했는지 여부 >> 방문 = CLOSED 상태 (y,x)
			bool[,] closed = new bool[_board.Size, _board.Size]; // CloseList
																 // 가는길을 한번 이라도 발견 했는지
			int[,] open = new int[_board.Size, _board.Size]; // OpenList
			for (int y = 0; y < _board.Size; y++)
				for (int x = 0; x < _board.Size; x++)
					open[y, x] = Int32.MaxValue; //초기값 세팅

			Pos[,] parent = new Pos[_board.Size, _board.Size];//부모 세팅

			//오픈리스트에 있는 정보들중 , 가장 좋은 후보를 뽑아오기 위한 도구
			PriorityQueue<PQNode> pq = new PriorityQueue<PQNode>();

			//시작점 발견
			open[PosY, PosX] = Math.Abs(_board.DestY - PosY) + Math.Abs(_board.DestX - PosX); //G 값은 0 이고, H는 목적지까지의 거리로,두 좌표값의 절대값거리 계산
			pq.Push(new PQNode() { F = Math.Abs(_board.DestY - PosY) + Math.Abs(_board.DestX - PosX), G = 0, Y = PosY, X = PosX });
			parent[PosY, PosX] = new Pos(PosY, PosX);

			while (true)
			{
				//제일 좋은 후보를 찾기
				PQNode node = pq.Pop();
				//동일한 좌표를 여러경로로 찾아서, 더빠른 경로로인해 이미 방문한 경우 스킵하기.
				if (closed[node.Y, node.X])
					continue;
				//방문하기
				closed[node.Y, node.X] = true;

				if (node.Y == _board.DestY && node.X == _board.DestX)
					break;
				//상하좌우등 이동할 수 있는 좌표인지 확인 + 예약한다
				for (int i = 0; i < deltaX.Length; i++)
				{
					int nextY = node.Y + deltaY[i];
					int nextX = node.X + deltaX[i];//상하좌우의 이동 적용
					//유효 범위를 벗어났으면 스킾하기
                    if (nextX < 0 || nextX >= _board.Size || nextY < 0 || nextY >= _board.Size)
                        continue;
					//벽으로 막혔을때 스킾하기
                    if (_board.Tile[nextY, nextX] == Board.TileType.Wall)
                        continue;
					//이미 방문한 곳이면 스킵
                    if (closed[nextY, nextX])
                        continue;

					//비용 계산
					int g = node.G + cost[i];//시작점에서 이동하는데 드는 비용 
					int h = Math.Abs(_board.DestY - nextY) + Math.Abs(_board.DestX - nextX);//목적지까지의 거리
					//다른경로에서 더 빠른 길을 찾았으면 해당 데이터(g+h)는 스킾
					if (open[nextY, nextX] < g + h)
                        continue;

					//예약 진행
					open[nextY, nextX] = g + h;
					pq.Push(new PQNode() { F = g + h, G = g, Y = nextY, X = nextX });
					parent[nextY, nextX] = new Pos(node.Y, node.X); //빠른 길을 찾게 해주는 부모 등록
                }
            }
			CalPathFromParent(parent);

        }


		void BFS()
		{
			int[] deltaY = new int[] { -1, 0, 1, 0 };
			int[] deltaX = new int[] { 0, -1, 0, 1 };

			bool[,] found = new bool[_board.Size, _board.Size];
			Pos[,] parent = new Pos[_board.Size, _board.Size];

			Queue<Pos> q = new Queue<Pos>();
			q.Enqueue(new Pos(PosY, PosX));
			found[PosY, PosX] = true;
			parent[PosY, PosX] = new Pos(PosY, PosX);

			while (q.Count > 0)
			{
				Pos pos = q.Dequeue();
				int nowY = pos.Y;
				int nowX = pos.X;

				for (int i = 0; i < 4; i++)
				{
					int nextY = nowY + deltaY[i];
					int nextX = nowX + deltaX[i];

					if (nextX < 0 || nextX >= _board.Size || nextY < 0 || nextY >= _board.Size)
						continue;
					if (_board.Tile[nextY, nextX] == Board.TileType.Wall)
						continue;
					if (found[nextY, nextX])
						continue;

					q.Enqueue(new Pos(nextY, nextX));
					found[nextY, nextX] = true;
					parent[nextY, nextX] = new Pos(nowY, nowX);
				}
			}

			CalPathFromParent(parent);
		}
		
		void CalPathFromParent(Pos[,] parent)
		{
            int y = _board.DestY;
            int x = _board.DestX;
            while (parent[y, x].Y != y || parent[y, x].X != x)
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
			// 현재 바라보고 있는 방향을 기준으로, 좌표 변화를 나타낸다
			int[] frontY = new int[] { -1, 0, 1, 0 };
			int[] frontX = new int[] { 0, -1, 0, 1 };
			int[] rightY = new int[] { 0, -1, 0, 1 };
			int[] rightX = new int[] { 1, 0, -1, 0 };

			_points.Add(new Pos(PosY, PosX));
			// 목적지 도착하기 전에는 계속 실행
			while (PosY != _board.DestY || PosX != _board.DestX)
			{
				// 1. 현재 바라보는 방향을 기준으로 오른쪽으로 갈 수 있는지 확인.
				if (_board.Tile[PosY + rightY[_dir], PosX + rightX[_dir]] == Board.TileType.Empty)
				{
					// 오른쪽 방향으로 90도 회전
					_dir = (_dir - 1 + 4) % 4;
					// 앞으로 한 보 전진.
					PosY = PosY + frontY[_dir];
					PosX = PosX + frontX[_dir];
					_points.Add(new Pos(PosY, PosX));
				}
				// 2. 현재 바라보는 방향을 기준으로 전진할 수 있는지 확인.
				else if (_board.Tile[PosY + frontY[_dir], PosX + frontX[_dir]] == Board.TileType.Empty)
				{
					// 앞으로 한 보 전진.
					PosY = PosY + frontY[_dir];
					PosX = PosX + frontX[_dir];
					_points.Add(new Pos(PosY, PosX));
				}
				else
				{
					// 왼쪽 방향으로 90도 회전
					_dir = (_dir + 1 + 4) % 4;
				}
			}
		}

		const int MOVE_TICK = 100;
		int _sumTick = 0;
		int _lastIndex = 0;
		public void Update(int deltaTick)
		{
			if (_lastIndex >= _points.Count)
			{
				_lastIndex = 0;
				_points.Clear();
				_board.Initialize(_board.Size, this);
				Initialize(1, 1, _board);
			}

			_sumTick += deltaTick;
			if (_sumTick >= MOVE_TICK)
			{
				_sumTick = 0;

				PosY = _points[_lastIndex].Y;
				PosX = _points[_lastIndex].X;
				_lastIndex++;
			}
		}
	}
}
