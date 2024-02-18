
////
using class1_1;

///
Board board = new Board();
Player player = new Player();   
board.Initialize(25,player);
player.initialize(1, 1, board);
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
    int deltaTick = currentTick - LastTick;
    LastTick = currentTick;
    #endregion

    //입력

    //로직
    player.Update(deltaTick);

    //렌더링
    Console.SetCursorPosition(0, 0);
    board.Render();
    
    
       
}