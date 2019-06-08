using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Крестики_нолики_ООП
{
    public partial class Form1 : Form
    {

      
        //Users p1 = new Users();
        
        public static int[] GamePoleMap = {
                             0,0,0,
                             0,0,0,
                             0,0,0
                         };
        public static int Player = 0, Computer = 0;
        //поле из 9 картинок
        public static PictureBox[] GamePole = new PictureBox[9];
        
        //переменные для хранение выбора пользователя и игрока кто кем будет играть.
        //int Player = 0, Computer = 0;

        //игровое поле в виде цифр для просчета выигрыша
      /*  int[] GamePoleMap = {
                             0,0,0,
                             0,0,0,
                             0,0,0
                         };*/

        //список имен картинок используемых в игре
     public  static string[] ImgName =
        {
            "pust.jpg", //пустой блок
           "cres.jpg", //крестик
            "null.png"
        };
        void MainPole()
        {
            
            //задаем начало рисования поля
            int
                DX = 0,
                DY = 0;

            //размеры картинки
            int
                HeighP = 95, //высота
                WhidthP = 100,  //ширина  
                                //счетчик подсчета картинок
                IndexPicture = 0;
            //имя в ячейке будет начинаться с этих символов
            string NAME = "P_";

            //цикл расстановки ячеек по Y
            for (int YY = 0; YY < 3; YY++)
            {
                //цикл расстановки ячеек по X
                for (int XX = 0; XX < 3; XX++)
                {
                    GamePole[IndexPicture] = new PictureBox()
                    {
                        Name = NAME + IndexPicture,                 //Задаем имя картинки
                        Height = HeighP,                            //задаем размер по Y
                        Width = WhidthP,                            //задаем размер 
                        Image = Image.FromFile("pust.jpg"),        //загружаем изображение пустого поля
                        SizeMode = PictureBoxSizeMode.StretchImage, //заставляем изображение сжаться по размерам картинки
                        Location = new Point(DX, DY)
                    };

                    GamePole[IndexPicture].Click += Picture_Click;

                    panel3.Controls.Add(GamePole[IndexPicture]); //размещаем картинку на пенале управления
                    //рассчитываем новое имя
                    IndexPicture++;

                    DX += WhidthP; //рассчитываем координаты по X для следующей картинки
                }
                DY += HeighP; //рассчитываем координаты по Y для следующей картинки
                DX = 0; //обнуляем позицию для координаты X
            }
        }
       private void Picture_Click(object sender, EventArgs e)
        {
            if (Users.CanStap())
            {
                PictureBox ClickImage = sender as PictureBox;
                string[] ParsName = ClickImage.Name.Split('_');

                int IndexSelectImage = Convert.ToInt32(ParsName[1]);

                GamePole[IndexSelectImage].Image = Image.FromFile(ImgName[Player]);
                GamePoleMap[IndexSelectImage] = Player;

                if (!Users.TestWin(Player))
                {
                    //блокируем поле чтобы игрок не смог ходить
                    Users.LoockPole();
                    //Шаг ПК
                    Users.PC_Step();
                    //пробуем разблокировать поле 
                    Users.UnLoockPole();
                }
                else
                {
                    panel4.Visible = true;
                    label3.Text = "Вы выиграли";
                    timer1.Enabled = false;
                    Users.LoockPole();
                }
            }
        }
        /*void LoockPole()
        {
            //блокируем все поле чтобы игрок не мог на него нажать
            foreach (PictureBox P in GamePole) P.Enabled = false;
        }
        void UnLoockPole()
        {
            int Indexx = 0;
            //разблокируем поля но только те которые не заполнены
            foreach (PictureBox P in GamePole)
            {
                //если поле равно 0 значит есть смысл его открывать
                if (GamePoleMap[Indexx++] == 0) P.Enabled = true;
            }
        }
      */

        public void Form1_Load(object sender, EventArgs e)
        {
            //создаем поле игрока
            MainPole();
           int X3 = panel3.Location.X,
            X4 = panel4.Location.X,
            Y3 = panel3.Location.Y,
            Y4 = panel4.Location.Y;
            //позиционируем панели на форме
            panel3.Location = new Point(27, 12);
            panel4.Location = new Point(600, 12);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Timer = 0;
            label4.Text = "Игра не начата";
            //показ выбора Х и О
            panel2.Visible = true;
            //прячем панель главного меню
            panel1.Visible = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //игрок выбрал Х
            Player = 1;
            //компьютер играет О
            Computer = 2;
            //прячем 2 панель
            panel2.Visible = false;
            //показываем поле игры на 3 панели
            panel3.Visible = true;
            timer1.Enabled = true;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //игрок выбрал О
            Player = 2;
            //компьютер играет Х
            Computer = 1;
            //прячем 2 панель
            panel2.Visible = false;
            //показываем поле игры на 3 панели
            panel3.Visible = true;
            timer1.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //прячем панель 4
            panel4.Visible = false;
            //прячем панель 3
            panel3.Visible = false;
            //обнуляем карту игры
            GamePoleMap = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            //обнуляем изображение поля
            foreach (PictureBox P in GamePole) P.Image = Image.FromFile(ImgName[0]);
            //обнуляем выбор игрока
            Player = 0;
            //обнуляем выбор ПК
            Computer = 0;
            //пробуем разблокировать поле 
            Users.UnLoockPole();
            //показываем меню игры
            panel1.Visible = true;
        }


        int Timer=0;
        public void timer1_Tick(object sender, EventArgs e)
        {
            Timer++;
            
            label4.Text = "Время игры:" + Timer;
        }
        Users us;
        

        public Form1()
        {
            InitializeComponent();

        us = new Users(ref label3);
        }
    }











    public  class Users:Form1
    {

        //Form1 form = null;
       public static Label lb;

        //Users pp = new Users(this);
       
      
        public Users(ref Label lb)
        {
          
            this.lb = lb;
        }
        public static void LoockPole()
        {
         
            //блокируем все поле чтобы игрок не мог на него нажать
            foreach (PictureBox P in GamePole) P.Enabled = false;
        }
    public static void UnLoockPole()
        {
          
            int Indexx = 0;
            //разблокируем поля но только те которые не заполнены
            foreach (PictureBox P in GamePole)
            {
                //если поле равно 0 значит есть смысл его открывать
                if (GamePoleMap[Indexx++] == 0) P.Enabled = true;
            }
        }
   /*  public   int[] GamePoleMap = {
                             0,0,0,
                             0,0,0,
                             0,0,0
                         };
        public int Player = 0, Computer = 0;*/
    public static bool CanStap()
        {
           
            //перебираем все значения игрового поля
            foreach (int s in GamePoleMap)
                //если нашли 0 значит есть куда ходить
                if (s == 0) return true;

            //проверяем не выиграл ли игрок
            if (TestWin(Player))
            {
               
                lb.Text = "Вы выиграли";
                LoockPole();
                //прячем панель игры
                form.panel4.Visible = true;
                //если не нашли то ходить больше нельзя
                return false;
            }
            //проверяем не выиграл ли игрок
            if (TestWin(Computer))
            {
               
                lb.Text = "Вы проиграли";
                form.panel4.Visible = true;
                //прячем панель игры
                LoockPole();
               form.panel3.Visible = false;
                return false;
            }


            //если ходить больше нельзя и никто не выиграл значит пишем что ничья
          
                lb.Text = "Ничья";
                //прячем панель игры

                form.panel4.Visible = true;
                LoockPole();
            

               
            
       
            return false;
        }
        //функция проверки на победу передаем значение кого будем проверять
     public  static bool TestWin(int WHO)
        {
            //список вариантов выигрышных комбинаций
        int[,] WinVariant =
            {      {    //1 вариант
                    1,1,1,  //Х Х Х
                    0,0,0,  //_ _ _
                    0,0,0   //_ _ _
                }, {    //2 вариант
                    0,0,0,  //_ _ _
                    1,1,1,  //Х Х Х
                    0,0,0   //_ _ _
                }, {    //3 вариант
                    0,0,0,  //_ _ _
                    0,0,0,  //_ _ _
                    1,1,1   //Х Х Х
                }, {    //4 вариант
                    1,0,0,  //Х _ _
                    1,0,0,  //Х _ _
                    1,0,0   //Х _ _
                }, {    //5 вариант
                    0,1,0,  //_ Х _
                    0,1,0,  //_ Х _
                    0,1,0   //_ Х _
                }, {    //6 вариант
                    0,0,1,  //_ _ Х
                    0,0,1,  //_ _ Х
                    0,0,1   //_ _ Х
                }, {    //7 вариант
                    1,0,0,  //Х _ _
                    0,1,0,  //_ Х _
                    0,0,1   //_ _ Х
                }, {    //8 вариант
                    0,0,1,   //_ _ Х
                    0,1,0,   //_ Х _
                    1,0,0    //Х _ _
                }
            };

            //получаем  поле
            int[] TestMap = new int[GamePoleMap.Length];
            //просчитываем поле
            for (int I = 0; I < GamePoleMap.Length; I++)
                //если номер в ячейке нам подходит записываем в карту 1
                if (GamePoleMap[I] == WHO) TestMap[I] = 1;

            //выбираем вариант для сравнения 
            for (int Variant_Index = 0; Variant_Index < WinVariant.GetLength(0); Variant_Index++)
            {
                //счетчик для подсчета соотвествий
                int WinState = 0;
                for (int TestIndex = 0; TestIndex < TestMap.Length; TestIndex++)
                {
                    //если параметр равен 1 то проверяем его иначе 0 тоже = 0
                    if (WinVariant[Variant_Index, TestIndex] == 1)
                    {
                        //если в параметр  в варианте выигрыша совпал с вариантом на карте считаем это в параметре WinState
                        if (WinVariant[Variant_Index, TestIndex] == TestMap[TestIndex]) WinState++;
                    }
                    //если найдены 3 совпадения значит это и есть выигрышная комбинация
                    if (WinState == 3) return true;
                }

            }

            return false;

        }
      public static void PC_Step()
        {
            Form1 form = null;
            //объявляем функцию генерации случайных чисел 
            Random Rand = new Random();
            GENER:

            if (CanStap())
            {
                //получаем число от 0 до 8
                int IndexStep = Rand.Next(0, 8);

                //смотрим если ячейка пуста то ставим туда символ ПК
                if (GamePoleMap[IndexStep] == 0)
                {
                    //рисуем нужную картинку
                    GamePole[IndexStep].Image = Image.FromFile(ImgName[Computer]);
                    //записываем в поле игры ход компьютера
                   GamePoleMap[IndexStep] = Computer;

                }
                else goto GENER;
                if (TestWin(Computer))
                {
                  
                        form.panel4.Visible = true;
                        lb.Text = "Вы Проиграли";
                        form.timer1.Enabled = false;
                    
                 
                }
            }
        }

        
    }


}
