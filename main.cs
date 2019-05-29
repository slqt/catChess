using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace catChess
{

    class Program
    {
        public static int[] maps = new int[100]; //声明一个静态数组 用来存储玩家A跟玩家B的坐标
        //两个玩家的坐标
        static int[] Players = new int[2];
        //存储两个玩家的姓名
        static string[] playerNames = new string[2];
        //两个玩家的标记，拥有某一个玩家暂停 
        static bool[] GamePause = new bool[2];
        static void Main(string[] args)
        {
            GameShow();//调用游戏提示信息
            #region 提示用户输入玩家姓名及姓名的判断
            Console.WriteLine("请输入玩家A的姓名：");
            playerNames[0] = Console.ReadLine();
            while (playerNames[0] == "")
            {
                Console.WriteLine("玩家A的名字不能为空，请重新输入");
                playerNames[0] = Console.ReadLine();
            }
            Console.WriteLine("请输入玩家B的姓名：");
            playerNames[1] = Console.ReadLine();
            while (playerNames[1] == "" || playerNames[1] == playerNames[0])
            {
                if (playerNames[1] == "")
                {
                    Console.WriteLine("玩家B的名字不能为空,请重新输入");
                    playerNames[1] = Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("玩家B的名字不能与玩家A的名字相同，请重新输入");
                    playerNames[1] = Console.ReadLine();
                }
            }
            #endregion
            Console.Clear();//玩家姓名输入OK 清屏
            GameShow();
            Console.WriteLine("{0}的士兵用A表示", playerNames[0]);
            Console.WriteLine("{0}的士兵用B表示", playerNames[1]);
            GameMap();//调用地图
            ShowMap();//画图
            #region 玩游戏 
            while (Players[0] < 99 && Players[1] < 99)
            // 当玩家A 和玩家B 么有一个人到达终点的时候，两个玩家继续玩游戏
            {
                if (GamePause[0] == false)
                {
                    PlayGame(0);
                }
                else
                {
                    GamePause[0] = false;
                }
                if (Players[0] >= 99)
                {
                    Console.WriteLine("玩家{0}无耻的赢了玩家{1}", playerNames[0], playerNames[1]);
                    break;
                }
                if (GamePause[1] == false)
                {
                    PlayGame(1);
                }
                else
                {
                    GamePause[1] = false;
                }
                if (Players[1] >= 99)
                {
                    Console.WriteLine("玩家{0}无耻的赢了玩家{1}", playerNames[1], playerNames[0]);
                    break;
                }
            }//while
            #endregion
            Console.ReadKey();//等待
        }
        /// <summary>
        /// 两个玩家玩游戏
        /// </summary>
        public static void PlayGame(int playNumber)
        {
            Random r = new Random();
            int rNumber = r.Next(1, 7);
            Console.WriteLine("玩家{0}按任意键开始郑骰子", playerNames[playNumber]);
            Console.ReadKey(true);
            Console.WriteLine("玩家{0}掷骰子，掷出了{1}", playerNames[playNumber], rNumber);
            Players[playNumber] += rNumber;
            Console.ReadKey(true);
            Console.WriteLine("玩家{0}：请按任意键开始移动！！！", playerNames[playNumber]);
            Console.ReadKey(true);
            Console.WriteLine("玩家{0}：移动完了", playerNames[playNumber]);
            Console.ReadKey(true);
            // 玩家A有可能踩到了B ,空格，幸运轮盘，地雷，暂停，时空隧道
            if (Players[playNumber] == Players[1 - playNumber])
            {
                Console.WriteLine("{0}玩家踩到了{1}玩家,{2}玩家退6格", playerNames[playNumber], playerNames[1 - playNumber], playerNames[1 - playNumber]);
                Players[1 - playNumber] -= 6;
                Console.ReadKey(true);
            }
            else // 玩家A踩到了关卡
            {
                // 玩家的坐标
                changePots();
                switch (maps[Players[playNumber]])
                {
                    case 0:
                        Console.WriteLine("玩家{0}踩到了方块，安全", playerNames[playNumber]);
                        Console.ReadKey(true);
                        break;

                    case 1:
                        Console.WriteLine("玩家{0}踩到了幸运轮盘，请选择：1--交换位置 2--轰炸对方", playerNames[playNumber]);
                        string input = Console.ReadLine();
                        while (true)
                        {
                            if (input == "1")
                            {
                                Console.WriteLine("玩家{0}选择与玩家{1}交换位置", playerNames[playNumber], playerNames[1 - playNumber]);
                                Console.ReadKey(true);
                                int temp = Players[playNumber];
                                Players[playNumber] = Players[1 - playNumber];
                                Players[1 - playNumber] = temp;
                                Console.WriteLine("位置交换完成！！！按任意键继续游戏！！!");
                                Console.ReadKey(true);
                                break;
                            }
                            else if (input == "2")
                            {
                                Console.WriteLine("玩家{0}选择了轰炸玩家{1}，玩家{2}倒退6格", playerNames[playNumber], playerNames[1 - playNumber], playerNames[1 - playNumber]);
                                Console.ReadKey(true);
                                Players[1 - playNumber] -= 6;
                                Console.WriteLine("玩家B倒退6格", playerNames[1 - playNumber]);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("只能输入1或者2 1--交换位置 2--轰炸对方");
                                input = Console.ReadLine();
                            }
                        }
                        break;
                    case 2:
                        Console.WriteLine("玩家{0}踩到了地雷，退6格", playerNames[playNumber]);
                        Console.ReadKey(true);
                        Players[playNumber] -= 6;
                        break;
                    case 3:
                        Console.WriteLine("玩家{0}猜到了暂停，暂停一回合", playerNames[playNumber]);
                        GamePause[playNumber] = true;
                        break;//暂停一回合是整个游戏最复杂的逻辑，需要最后再写
                    case 4:
                        Console.WriteLine("玩家{0}踩到了时空隧道，前进10格", playerNames[playNumber]);
                        Players[playNumber] += 10;
                        Console.ReadKey(true);
                        break;
                }//switch
            }//else
            changePots();
            Console.Clear();// 清屏 
            ShowMap();
        }

        /// <summary>
        /// 当玩家坐标发生改变的时候
        /// 限定玩家不能跳出地图的界限
        /// </summary>
        public static void changePots()
        {
            if (Players[0] < 0)
            {
                Players[0] = 0;
            }
            if (Players[0] >= 99)
            {
                Players[0] = 99;
            }
            if (Players[1] < 0)
            {
                Players[1] = 0;
            }
            if (Players[1] >= 99)
            {
                Players[1] = 99;
            }
        }

        /// <summary>
        /// 画游戏开头提示信息
        /// </summary>
        /// 
        public static void GameShow()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("*****************************");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("*****************************");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("**** Weiterli 飞行棋V1.0 ****");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("*****************************");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("*****************************");
        }


        //int[] luckyturn={6,23,40,55,69,83};//幸运轮盘
        //int[] lanmine ={5,13,17,33,38,50,64,80,94};//地雷☆
        //int[]pause={9,27,60,93};//暂停
        //int[] timeTunnel={20,25,45,63,72,88,90}//时空隧道
        /// <summary>
        ///初始化游戏地图
        ///（数组长度100，索引 0-99）
        ///<para>luckyturn幸运轮盘lanMine地雷 pause暂停 timeTunnel时空隧道</para>
        ///<para>注意：把数组当中的数字转换成控制台中显示的特殊字符串的过程就是初始化地图</para>
        /// </summary>
        public static void GameMap()
        {
            int[] luckyturn = { 6, 23, 40, 55, 69, 83 };//幸运轮盘
            for (int i = 0; i < luckyturn.Length; i++)
            {
                //    int index = luckyturn[i];
                maps[luckyturn[i]] = 1;
            }
            int[] landmine = { 5, 13, 17, 33, 38, 50, 64, 80, 94 };//地雷☆
            for (int i = 0; i < landmine.Length; i++)
            {
                maps[landmine[i]] = 2;
            }
            int[] pause = { 9, 27, 60, 93 };//暂停
            for (int i = 0; i < pause.Length; i++)
            {
                maps[pause[i]] = 3;
            }
            int[] timeTunnel = { 20, 25, 45, 63, 72, 88, 90 };//时空隧道
            for (int i = 0; i < timeTunnel.Length; i++)
            {
                maps[timeTunnel[i]] = 4;
            }
        }


        /// <summary>
        /// 画地图
        /// </summary>
        public static void ShowMap()
        {
            Console.WriteLine("图例：幸运轮盘：◎  地雷：☆  暂停：▲  时空隧道：卐");
            #region 第一横行
            for (int i = 0; i < 30; i++)
            {
                Console.Write(DrawMaps(i));// 画关卡
            }//for
            #endregion
            Console.WriteLine();
            #region 画第一个竖行 
            //第一竖行30-35
            for (int i = 30; i < 35; i++)
            {
                for (int j = 0; j <= 28; j++)
                {
                    Console.Write("  ");
                }
                Console.Write(DrawMaps(i));
                ;// 画关卡

                Console.WriteLine();

            }
            #endregion
            #region 画第二横行
            for (int i = 64; i >= 35; i--)
            {
                Console.Write(DrawMaps(i));
            }
            Console.WriteLine();//画完第二横行应当换行
            #endregion
            #region 画第二竖行
            for (int i = 65; i <= 69; i++)
            {
                Console.WriteLine(DrawMaps(i));
            }
            #endregion 
            #region 画第三横行
            for (int i = 70; i <= 99; i++)
            {
                Console.Write(DrawMaps(i));
            }
            Console.WriteLine();//画完第三横行应当换行
            #endregion

        }


        /// <summary>
        /// 画关卡的方法
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static string DrawMaps(int i)
        {
            string str = "";
            #region 画关卡
            if (Players[0] == Players[1] && Players[0] == i)
            {
                str = "<>";
            }
            else if (Players[0] == i)
            {
                str = "Ａ";
            }
            else if (Players[1] == i)
            {
                str = "Ｂ";
            }
            else
            {
                switch (maps[i])
                {
                    case 0:

                        Console.ForegroundColor = ConsoleColor.White;
                        str = "□";
                        break;
                    case 1:

                        Console.ForegroundColor = ConsoleColor.Red;
                        str = "◎";
                        break;
                    case 2:

                        Console.ForegroundColor = ConsoleColor.Green;
                        str = "☆";
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        str = "▲";
                        break;
                    case 4:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        str = "卐";
                        break;
                }//swicth
            }//if
            #endregion
            return str;
        }

    }
}
