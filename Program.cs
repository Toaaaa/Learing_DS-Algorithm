
////
Console.CursorVisible = false;

int LastTick = 0;
const int WaitTick = 1000 / 30;
const char Circle = '\u25cf';

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
    
    for(int i=0; i < 25; i++)
    {
        for (int j = 0; j < 25; j++)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(Circle);
        }
        Console.WriteLine();
    }
       
}