
////
using class1_1;

///
Board board = new Board();
board.Initialize(25);
Console.CursorVisible = false;

int LastTick = 0;
const int WaitTick = 1000 / 30;


while (true)
{
    #region FrameContr
    int currentTick = System.Environment.TickCount;//gets the current tick count
    int elapsedtick = currentTick - LastTick;

    //elapsedtick == 경과한 시간
    if (elapsedtick < WaitTick) //만약 경과한 시간이 1/30초보다 작다면 >> tickcount가 밀리세컨드이기에>>1000/30
        continue;
    LastTick = currentTick;
    #endregion

    Console.SetCursorPosition(0, 0);
    board.Render();
    
    
       
}