namespace Console_Project_Dungeon_Invade
{
    /// <summary>
    /// 1. 타이틀 스크린 //클리어
    /// 2. 첫 번째 맵 출력  //클리어
    /// 2.1 캐릭터 이동구현  //클리어
    /// 2.1.1 재시작 구현
    /// 2.2 벽충돌판정 구현  //클리어
    /// 2.3 소코반 처럼 박스 미는거 구현  //클리어
    /// 2.4 열쇠 습득 및 열쇠 사용 구현+ 무기+ 방어구 습득 구현
    /// 2.5 새로운 방을 열엇을 때, 맵 추가하는거 구현 //클리어
    /// 3. 상태창 및 대사 출력   // 클리어
    /// 3.1 적 조우 시 전투화면 변경 // 클리어
    /// 3.2 전투 화면 종료 후 원래 화면으로 // 클리어
    /// 3.3 체력, 장비(교체불가), 경험치,레벨,공격력,방어력 구현 // 클리어
    /// 4 마지막 보스 쓰러뜨리면 게임 끝
    /// 4.1 승리 시 게임루프 종료 후 엔딩 크레딧 출력
    /// 4.2 타이틀 스크린으로 돌아감
    /// 5. 전투 시 잘못 입력 받았을 때, 예외 처리에 대사 출력하기(Thread sleep 추가)
    /// 5.1. 입력을 ReadKey로 변경하기
    /// </summary>
    internal class Program
    {
        // 캐릭터 스탯
        struct Stat
        {
            public int hp;
            public int attackPoint;
            public int defence;
            public Weapon weapon;
            public Armor armor;
            // 공격력은 적 조우시, 적의 방어력도 변수로 작용
        }

        enum Weapon
        {
            맨손, 녹슨과도 /*= 3*/, 단검/* = 5*/, 기사의검/* = 10*/, 성검/* = 25*/
        }
        enum Armor
        {
            후드티, 가죽잠바/* = 3*/, 가짜근육슈트/* = 5*/, 체인메일/* = 15*/, 방탄슈트/* = 30*/
        }
        // 캐릭터들의 위치
        struct Position
        {
            public int x;
            public int y;
        }
        struct EnemyStat
        {
            public string name;
            public int hp;
            public int attackPoint;
            public int defence;
        }
        static void TitleScreen()
        {
            Console.WriteLine("◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("    .■■■■■■■■■■■■■■■■:     ■■                                                                      ");
            Console.WriteLine("    ■■■■■■■■■■■■■■■■-    .■■:    ■■■■■■■■■■■■   ■■     ■■■■■■          ■■■                 ■■-     ");
            Console.WriteLine("   ■■■.                  ■■■     ■■■■■■■■■■■■■■ ■■                    ■■■   -■■■■■■■■■■.   ■■:     ");
            Console.WriteLine("   ■■         :■■■■■■■■■■■■:            .■■■.   ■■     .■■■■■■■■■■■■  ■■■           .■■:  .■■.     ");
            Console.WriteLine("  ■■■.         =■■■■■■■■■■+           .+■■■.    ■■     ■■■■■■■■■■■■■  ■■■           ■■■   :■■      ");
            Console.WriteLine("  ■■■■■■■■■■           .■■:         .■■■■     . ■■         ■■■■■      ■■■          :■■.   -■■■■■■  ");
            Console.WriteLine("   -■■■■■■■            ■■■        -■■■■■■  ■■■■■■■       ■■■■  ■■    .■■■    :■■■■■■■+    =■■      ");
            Console.WriteLine("                      .■■:      ■■■■   ■■ .     ■■     ■■■■■■  ■■    :■■■    -■■■■■■:.    +■■■■■■  ");
            Console.WriteLine("                      ■■■       ■■.      ■■    ■■     ■■■■■     ■■   :■■■    ■■■         .#■       ");
            Console.WriteLine("                     .■■.       ■■        ■    ■     ■■■         ■■■ :■■■    ■■■         :%■       ");
            Console.WriteLine("        ■■■                                                      .   :■■    .■■■■■■■     -%■       ");
            Console.WriteLine("        ■■■                        ■■■                         :■■■■■■■■■■:              .::       ");
            Console.WriteLine("        ■■■                        .■■■                      .■■■■■■■■■■■■■.       .■■■■■■■■■.     ");
            Console.WriteLine("       .■■■■■■■■■■■■.               ■■■■                     ■■+        .■■:    .+■■■■■■■■■■■■     ");
            Console.WriteLine("        .■■■■■■■■■■■■■:              .■■■■■■■■■■■            ■■:        ■■■                :■■     ");
            Console.WriteLine("                                      ■■:                   :■■.       .■■■                ■■■     ");
            Console.WriteLine("                                                            -■■■■■■■■■■■■■.               .■■.     ");
            Console.WriteLine("                                                             .■■■■■■■■■■.                          ");
            Console.ResetColor();
            Console.WriteLine("◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇");
            Console.WriteLine("");
            Console.WriteLine("게임을 시작하려면 아무키나 눌러주세요 ... ");
            Console.ReadKey(true);
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("게임 시작 !!!");
            Thread.Sleep(1000);
            Console.Clear();
        }

        // 타이틀 스크린 출력, 맵 제작
        static void GameStart(out char[,] map)
        {
            TitleScreen();
            // 게임 설정
            Console.CursorVisible = false;

            map = new char[,]
                { // 첫 맵 = 10x10       //[5]         // [9]                                   //[19]
                    {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ' },
                    {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ' },
                    {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ' },
                    {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ' },
           /*[4]*/  {'1','▒','▒','▒','▒','▒','2','▒','▒','▒','▒','▒','▒','▒','▒','▒','▒','▒','▒','▒' },
                    {'▒',' ',' ',' ',' ','▒',' ',' ','↙','▒','▒',' ',' ',' ',' ',' ',' ',' ',' ','▒' },
                    {'▒',' ','■',' ','□','▒',' ',' ',' ','▒','▒',' ',' ',' ',' ',' ',' ',' ',' ','▒' },
                    {'▒',' ',' ',' ',' ','▥',' ',' ',' ','▒','▒',' ',' ',' ',' ',' ',' ',' ',' ','▒' },
            /*[8]*/ {'▒','▒','▒','▒','▒','▒','▥','▒','▒','▒',' ',' ',' ',' ',' ',' ',' ',' ',' ','▒' },
                    {'3','◎','▒',' ',' ',' ',' ',' ',' ','▒','▒',' ',' ',' ',' ',' ',' ',' ',' ','▒' },
                    {'▒',' ','▒','●','▒',' ','▒','■','▒','▒','▒',' ',' ',' ',' ',' ',' ',' ',' ','▒' },
                    {'▒',' ',' ',' ','▒',' ',' ',' ','□','▒','▒',' ',' ',' ',' ',' ',' ',' ',' ','▒' },
           /*[12]*/ {'▒','▥','▒','▒','▒','▒','▒','▒','▒','▒','▒',' ',' ',' ',' ',' ',' ',' ',' ','▒' },
                    {'4',' ',' ',' ',' ',' ',' ',' ',' ','▒','▒',' ',' ',' ',' ',' ',' ',' ',' ','▒' },
                    {'▒',' ',' ',' ',' ',' ',' ',' ',' ','▒','▒',' ',' ',' ',' ',' ',' ',' ',' ','▒' },
                    {'▒',' ',' ',' ','■',' ','■',' ',' ','▒','▒',' ',' ',' ',' ',' ',' ',' ',' ','▒' },
                    {'▒',' ','▒',' ','▒','▒','▒','▒','▒','▒','▒',' ',' ',' ',' ',' ',' ',' ',' ','▒' },
                    {'▒',' ',' ',' ','□','▒',' ',' ',' ','▒','▒',' ',' ',' ',' ',' ',' ',' ',' ','▒' },
                    {'▒',' ',' ',' ','□','▒',' ',' ',' ','▒','▒',' ',' ',' ',' ',' ',' ',' ',' ','▒' },
                    {'▒',' ','■',' ','□','▒',' ',' ',' ','▒','▒',' ',' ',' ',' ',' ',' ',' ',' ','▒' },
                    {'▒','▥','▒','▒','▒','▒',' ',' ',' ','▒','▒',' ',' ',' ',' ',' ',' ',' ',' ','▒' },
                    {'▒',' ',' ',' ',' ',' ',' ',' ',' ','▒','▒',' ',' ',' ',' ',' ',' ',' ',' ','▒' },
                    {'▒',' ',' ',' ',' ',' ',' ',' ',' ','▒','▒',' ',' ',' ',' ',' ',' ',' ',' ','▒' },
                    {'▒',' ',' ',' ',' ',' ',' ',' ',' ','▒','▒',' ',' ',' ',' ',' ',' ',' ',' ','▒' },
                    {'▒',' ',' ',' ',' ',' ',' ',' ',' ','▒','▒',' ',' ',' ',' ',' ',' ',' ',' ','▒' },
                    {'▒',' ',' ',' ',' ',' ',' ',' ',' ','▒','▒',' ',' ',' ',' ',' ',' ',' ',' ','▒' },
   /*비밀방[26]*/   {'▒','▥','▒','▒','▒','▒','▒','▥','▒','▒','▒',' ',' ',' ',' ',' ',' ',' ',' ','▒' },
                    {'▒',' ',' ','▒',' ','▒','▒',' ','▒','▒','▒',' ',' ',' ',' ',' ',' ',' ',' ','▒' },
                    {'▒','▒','◎','▒',' ','▒',' ',' ','↙','▒','▒',' ',' ',' ',' ',' ',' ',' ',' ','▒' },
                    {'▒','▒','▒','▒','▒','▒','▒','▒','▒','▒','▒','▒','▒','▒','▒','▒','▒','▒','▒','▒' },

                };

        }

        //맵출력
        static void PrintMap(char[,] map, ref int mapClear)
        {
            Console.SetCursorPosition(0, 4);
            //방을 클리어 했나 판별
            // 방 1을 못깬경우            
            int i = 4;
            int j = 0;
            int iEnd = 0;
            int jEnd = 0;
            if (mapClear == 0)
            {
                iEnd = 9;
                jEnd = 6;
            }
            // 방 1을 깬경우
            else if (mapClear == 1)
            {
                iEnd = 9;
                jEnd = 10;
            }
            // 방 2를 깬경우
            else if (mapClear == 2)
            {
                iEnd = 13;
                jEnd = 10;
            }
            // 방 3을 깬경우
            else if (mapClear == 2)
            {
                iEnd = 13;
                jEnd = 10;
            }


            // 맵 클리어에 따른 맵 출력
            for (i = 4; i < iEnd; i++)
            {
                for (j = 0; j < jEnd; j++)
                {
                    //벽 출력
                    if (map[i, j] == '▒')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write(map[i, j]);
                        Console.ResetColor();
                    }
                    //박스 출력
                    else if (map[i, j] == '■')
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(map[i, j]);
                        Console.ResetColor();
                    }
                    // 골인 출력
                    else if (map[i, j] == '▣')
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(map[i, j]);
                        Console.ResetColor();
                    }
                    // 칼 출력
                    else if (map[i, j] == '↙')
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write(map[i, j]);
                        Console.ResetColor();
                    }
                    //방패 출력
                    else if (map[i, j] == '◎')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write(map[i, j]);
                        Console.ResetColor();
                    }
                    //슬라임
                    else if (map[i, j] == '●')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write(map[i, j]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(map[i, j]);
                    }

                }
                Console.WriteLine();
            }

        }
        // 방1 을 깬 경우
        //else if (mapClear == 1)
        //{
        //    for (i = 4; i < 9; i++)
        //    {
        //        for (j = 0; j < 10; j++)
        //        {
        //            if (map[i, j] == '▒')
        //            {
        //                Console.ForegroundColor = ConsoleColor.DarkRed;
        //                Console.Write(map[i, j]);
        //                Console.ResetColor();
        //            }
        //            else if (map[i, j] == '■')
        //            {
        //                Console.ForegroundColor = ConsoleColor.Yellow;
        //                Console.Write(map[i, j]);
        //                Console.ResetColor();
        //            }
        //            else if (map[i, j] == '▣')
        //            {
        //                Console.ForegroundColor = ConsoleColor.Green;
        //                Console.Write(map[i, j]);
        //                Console.ResetColor();
        //            }
        //            // 칼 출력
        //            else if (map[i,j] == '↙')
        //            {
        //                Console.ForegroundColor = ConsoleColor.Cyan;
        //                Console.Write(map[i, j]);
        //                Console.ResetColor();
        //            }
        //            else
        //            {
        //                Console.Write(map[i, j]);
        //            }
        //        }
        //Console.WriteLine();
        //}
        //}
        //전체 맵출력
        //for (int i = 0; i < map.GetLength(0); i++)
        //{
        //    for (int j = 0; j < map.GetLength(1); j++)
        //    {
        //        Console.Write(map[i, j]);
        //    }
        //    Console.WriteLine();
        //}
        //}
        //플레이어 출력
        static void PrintPlayer(Position playerPos)
        {
            Console.SetCursorPosition(playerPos.x, playerPos.y);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("▲");
            Console.ResetColor();
        }     

/////////메인 함수//////////////////
        static void Main(string[] args)
        {
            char[,] gameMap;
            GameStart(out gameMap);
            bool gameOver = false;
            Position playerPos;
            playerPos.x = 1;
            playerPos.y = 5;
            int mapClear = 0;
            int[,] secretRoom; // 비밀방 구현
            bool encounterEnemy = false;

            // 플레이어의 상황 초기화
            Stat playerStat;
            playerStat.hp = 100;
            playerStat.weapon = Weapon.맨손;
            playerStat.armor = Armor.후드티;
            int weaponAttack = (int)playerStat.weapon;
            int armorDefence = (int)playerStat.armor;
            playerStat.attackPoint = 3 + weaponAttack;
            playerStat.defence = 1 + armorDefence;

            // 적 목록 생성
            EnemyStat[] enemy;
            enemy = new EnemyStat[4];
            int enemyNum = 0;
            
            //[0] 슬라임 상태 초기화
            enemy[0].name = "슬라임";
            enemy[0].hp = 15;
            enemy[0].attackPoint = 2;
            enemy[0].defence = 0;

            //[1] 고블린 상태 초기화
            enemy[1].name = "고블린";
            enemy[1].hp = 30;
            enemy[1].attackPoint = 4;
            enemy[1].defence = 2;


            // 게임 이벤트 대화창 초기화
            string gameDialog = "";


            while (gameOver == false)
            {

                
                //적과 조우
                if (encounterEnemy)
                {
                    Console.Clear();
                    // 게임 상태창
                    GameTextPrint(ref gameDialog, playerStat);
                    EncounterEnemy(ref encounterEnemy, ref playerStat, ref enemy, ref enemyNum, ref gameDialog);
                }
                else
                {
                    // 게임 상태창
                    GameTextPrint(ref gameDialog, playerStat);
                    //GameTextPrint();
                    //게임루프
                    GameRender(gameMap, playerPos, ref mapClear);
                    ConsoleKey key = PlayerInput();
                    GameUpdate(gameMap, key, ref playerPos, ref mapClear, ref playerStat, ref gameDialog, ref encounterEnemy, ref enemyNum);
                    IsClearGame();
                }


            }
            GameEnd();
        }

        // 화면 렌더링
        static void GameRender(char[,] map, Position playerPos, ref int mapClear)
        {

            PrintMap(map, ref mapClear);
            PrintPlayer(playerPos);
        }
        // 플레이어 입력
        static ConsoleKey PlayerInput()
        {
            return Console.ReadKey(true).Key;
        }
        // 게임 업데이트
        static void GameUpdate(char[,] map, ConsoleKey key, ref Position playerPos, ref int mapClear, ref Stat playerStat, ref string text,ref bool encounterEnemy, ref int enemyNum)
        {
            bool[] isClearMap = new bool[10];
            isClearMap[0] = IsClearMap1(map);
            isClearMap[1] = IsClearMap2(map);
            isClearMap[2] = IsClearMap3(map);
            if (isClearMap[0])
            {
                mapClear = 1;
            }
            if (isClearMap[1])
            {
                mapClear = 2;
            }
            if (isClearMap[2])
            {
                mapClear = 3;
            }


            // 플레이어 이동 최신화
            PlayerMove(map, key, ref playerPos, ref playerStat, ref text,ref encounterEnemy, ref enemyNum);



            //게임 클리어 판별
            bool isClear = false;
            isClear = IsClearGame();

            if (isClear == true)
            {
                GameEnd();
            }
        }
        static void PlayerMove(char[,] map, ConsoleKey key, ref Position playerPos, ref Stat playerStat, ref string text, ref bool encounterEnemy, ref int enemyNum)
        {
            Position tarPos, overPos;
            switch (key)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    tarPos.x = playerPos.x;
                    tarPos.y = playerPos.y - 1;
                    overPos.x = playerPos.x;
                    overPos.y = playerPos.y - 2;
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    tarPos.x = playerPos.x - 1;
                    tarPos.y = playerPos.y;
                    overPos.x = playerPos.x - 2;
                    overPos.y = playerPos.y;
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    tarPos.x = playerPos.x;
                    tarPos.y = playerPos.y + 1;
                    overPos.x = playerPos.x;
                    overPos.y = playerPos.y + 2;
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    tarPos.x = playerPos.x + 1;
                    tarPos.y = playerPos.y;
                    overPos.x = playerPos.x + 2;
                    overPos.y = playerPos.y;
                    break;
                default:
                    return;
            }
            // 1.진행방향에 빈칸이 없을 경우
            if (map[tarPos.y, tarPos.x] != ' ')
            {
                // 1.1 박스일 경우
                if (map[tarPos.y, tarPos.x] == '■')
                {
                    //1.1.1 그 앞칸이 빈 칸일 경우
                    if (map[overPos.y, overPos.x] == ' ')
                    {
                        map[overPos.y, overPos.x] = '■';
                        map[tarPos.y, tarPos.x] = ' ';
                        playerPos.x = tarPos.x;
                        playerPos.y = tarPos.y;
                    }
                    // 1.1.2 그 앞칸이 골일 경우
                    else if (map[overPos.y, overPos.x] == '□')
                    {
                        map[overPos.y, overPos.x] = '▣';
                        map[tarPos.y, tarPos.x] = ' ';
                        playerPos.x = tarPos.x;
                        playerPos.y = tarPos.y;
                    }
                    //1.1.4 그외에 벽이나 막히는 것들은 못감
                }
                // 1.2. 벽인 경우
                else if (map[tarPos.y, tarPos.x] == '▒')
                {
                    //암것도 안함
                }
                // 1.3. 골박스일 경우(상자인 경우)
                else if (map[tarPos.y, tarPos.x] == '▣')
                {
                    //1.3.1 그 앞에 빈 칸일 경우
                    if (map[overPos.y, overPos.x] == ' ')
                    {
                        map[overPos.y, overPos.x] = '■';
                        map[tarPos.y, tarPos.x] = '□';
                        playerPos.x = tarPos.x;
                        playerPos.y = tarPos.y;
                    }
                    //1.3.2 그 앞에 골일 경우
                    if (map[overPos.y, overPos.x] == '□')
                    {
                        map[overPos.y, overPos.x] = '▣';
                        map[tarPos.y, tarPos.x] = '□';
                        playerPos.x = tarPos.x;
                        playerPos.y = tarPos.y;
                    }
                    //1.3.3 벽, 그외에 것들은 못감
                }
                // 1.4. 골일 경우
                else if (map[tarPos.y, tarPos.x] == '□')
                {
                    playerPos.x = tarPos.x;
                    playerPos.y = tarPos.y;
                }
                //1.5. 무기와 방패일 경우
                else if (map[tarPos.y, tarPos.x] == '↙')
                {
                    // 무기 습득 로그
                    text = "무기를 주웠다!             ";
                    map[tarPos.y, tarPos.x] = ' ';
                    playerPos.x = tarPos.x;
                    playerPos.y = tarPos.y;
                    //무기 바뀜
                    playerStat.weapon++;

                }
                else if (map[tarPos.y, tarPos.x] == '◎')
                {
                    // 방어구 습득 로그
                    text = "방어구를 주웠다!             ";
                    map[tarPos.y, tarPos.x] = ' ';
                    playerPos.x = tarPos.x;
                    playerPos.y = tarPos.y;
                    //방어구 바뀜
                    playerStat.armor++;
                }
                // 1.6 슬라임일 경우
                else if (map[tarPos.y, tarPos.x] == '●')
                {
                    // 적과 조우
                    text = "적과의 전투 돌입            ";
                    map[tarPos.y, tarPos.x] = ' ';
                    playerPos.x = tarPos.x;
                    playerPos.y = tarPos.y;
                    encounterEnemy = true;
                    enemyNum = 0;
                    

                }
            }
            // 가는 방향이 빈 칸일 경우
            else if (map[tarPos.y, tarPos.x] == ' ')
            {
                map[tarPos.y, tarPos.x] = ' ';
                playerPos.x = tarPos.x;
                playerPos.y = tarPos.y;

            }
        }
        static void EncounterEnemy(ref bool encounterEnemy, ref Stat playerStat,ref EnemyStat[] enemy,ref int enemyNum,ref string text)
        {
            bool isAlive = true;
                        
            Console.SetCursorPosition(0,4);
            Console.WriteLine("******************************************************************************");
            Console.WriteLine($"|| {enemy[enemyNum].name}과 맞닥뜨렸다!!!\n|| 적 :  | 체력 : {enemy[enemyNum].hp} | 공격력 : {enemy[enemyNum].attackPoint} | 방어력 : {enemy[enemyNum].defence}");
            Console.WriteLine($"|| 행동을 선택해주세요 !   ");
            Console.WriteLine($"|| 1. 공격한다          | 2. 방어한다");
            Console.WriteLine("******************************************************************************");
            
            if (enemy[enemyNum].hp <= 0)
            {
                // 적이 살아있나 체크
                encounterEnemy = false;
                Console.WriteLine("{0}을 쓰러뜨렸다!!!", enemy[enemyNum].name);
                text = "전투에서 승리했다! 체력 풀 회복!";
                Thread.Sleep(1500);
                playerStat.hp = 100;
                Console.Clear();
            }
            
            else
            {
                // 플레이어가 살아있나 체크
                if (playerStat.hp <= 0)
                {
                    isAlive = false;
                }
                else
                {
                    // 전투 구현
                    ConsoleKey choice = Console.ReadKey(true).Key;
                    switch (choice)
                    {
                        case ConsoleKey.D1:
                        case ConsoleKey.NumPad1:
                            //크리티컬 만들어보기

                            Random rand = new Random();
                            int randomDmg;
                            randomDmg = rand.Next(5);

                            int enemyAttackDamage = enemy[enemyNum].attackPoint * (100 - playerStat.defence) / 100;
                            playerStat.hp -= enemyAttackDamage;
                            
                            if (randomDmg >= 4)
                            {
                                int attackDamage = 2*playerStat.attackPoint * (100 - enemy[enemyNum].defence) / 100;
                                enemy[enemyNum].hp -= attackDamage;
                                Console.ForegroundColor = ConsoleColor.Red;
                                Thread.Sleep(300);
                                Console.WriteLine("크리티컬!!! {0}의 데미지를 입혔다!", attackDamage);
                                Console.ResetColor();
                                Thread.Sleep(500);
                                Console.WriteLine(" 적의 공격! {0}의 데미지!",  enemyAttackDamage);
                                Thread.Sleep(1000);
                            }
                            else
                            {
                                int attackDamage = playerStat.attackPoint * (100 - enemy[enemyNum].defence) / 100;
                                enemy[enemyNum].hp -= attackDamage;
                                Thread.Sleep(300);
                                Console.WriteLine("{0}의 데미지를 입혔다!", attackDamage);
                                Thread.Sleep(500);
                                Console.WriteLine("적의 공격! {0}의 데미지!", enemyAttackDamage);
                                Thread.Sleep(1000);
                            }                             


                            break;
                        case ConsoleKey.D2:
                        case ConsoleKey.NumPad2:
                            int defenceAttack = enemy[enemyNum].attackPoint * (100 - playerStat.defence * 3) / 100;
                            playerStat.hp -= defenceAttack;
                            Thread.Sleep(300);
                            Console.WriteLine("방어했다! 적의 공격! {0}의 데미지!", defenceAttack);
                            Thread.Sleep(1000);
                            break;


                    }
                }
            }
            

            
        }
        // 게임의 상태 창 밎 텍스트 출력
        static void GameTextPrint(ref string text, Stat playerStat)
        {
            // 체력, 공격력, 방어력, 무기, 방어구
            // 이벤트 로그 출력

            int weaponAttack = (int)playerStat.weapon;
            int armorDefence = (int)playerStat.armor;
            playerStat.attackPoint = 3 + weaponAttack * 4;
            playerStat.defence = 1 + armorDefence * 3;

            // 체력 상태 표시
            string lifeBar = "";
            if (playerStat.hp == 100)
            {
                lifeBar = "■■■■■";
            }
            else if (playerStat.hp > 80)
            {
                lifeBar = "■■■■□";
            }
            else if (playerStat.hp > 60)
            {
                lifeBar = "■■■□□";
            }
            else if (playerStat.hp > 40)
            {
                lifeBar = "■■□□□";
            }
            else
            {
                lifeBar = "■□□□□";
            }

            //상태창 내용
            // 인게임 상태창 첫 줄
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("========================================");
            Console.WriteLine("↕ 체력: {0} {1} | 상태창: {2}", lifeBar, playerStat.hp, text);
            Console.WriteLine("↕ 공격력: {0} | 방어력: {1} | 무기: {2} | 방어구: {3}", playerStat.attackPoint, playerStat.defence, playerStat.weapon, playerStat.armor);
            Console.WriteLine("========================================");
        }


        static bool IsClearGame()
        {
            bool success = false;
            if (success == true)
            {
                return true;
            }
            return false;
        }
        // 방 클리어 확인
        static bool IsClearMap1(char[,] map)
        {
            for (int i = 5; i < 8; i++)
            {
                for (int j = 1; j <= 4; j++)
                {
                    // 박스를 다 넣었을 때
                    if (map[i, j] == '□')
                    {
                        return false;
                    }
                }
            }
            //잠긴 문 열기
            map[7, 5] = ' ';
            return true;
        }
        static bool IsClearMap2(char[,] map)
        {
            for (int i = 5; i < 8; i++)
            {
                for (int j = 6; j <= 8; j++)
                {
                    // 칼을 습득해야 클리어
                    if (map[i, j] == '↙')
                    {
                        return false;
                    }
                }
            }
            map[8, 6] = ' ';
            return true;
        }
        // 방 클리어 확인
        static bool IsClearMap3(char[,] map)
        {
            for (int i = 9; i < 12; i++)
            {
                for (int j = 1; j <= 9; j++)
                {
                    // 박스를 다 안 넣었을 때, 슬라임을 다 안잡았을 때
                    if (map[i, j] == '□' || map[i,j]=='●')
                    {
                        return false;
                    }
                }
            }
            //잠긴 문 열기
            map[12, 1] = ' ';
            return true;
        }
        static void GameEnd()
        {

        }
    }
}
