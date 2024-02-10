using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace class1_1
{
    internal class Board
    {
        public int[] _data = new int[25];//배열 , 연속된 방을 사용한다
        public List<int> _data2 = new List<int>();//동적 배열 >>처음부터 여유있게 배열칸을 보유, 유동적으로 활용가능
        public LinkedList<int> _data3 = new LinkedList<int>();//연결 리스트
        //123
        public void Initialize()
        {

        }
    }
}
