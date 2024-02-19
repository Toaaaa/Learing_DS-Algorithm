
using System;
using System.Collections.Generic;

namespace class1_1
{
    static void Main(string[] args)
    {
        Board board = new Board();
        Player player = new Player();
        board.Initialize(25, player);
        player.initialize(1, 1, board);

        Console.cursorVisible = false;

        const int WAIT_TICK = 1000 / 30;

        int lastTick = 0;
        while (true)
        {
            #region 프레임 관리
            int currentTick = System.Environment.TickCount;
            if (currentTick - lastTick < WAIT_TICK)
                continue;
            lastTick = currentTick;
            #endregion

            //입력
            player.Update(deltaTick);
            //로직
            //출력
            Console.SetCursorPosition(0, 0);
            board.Render();
        }

    }
}