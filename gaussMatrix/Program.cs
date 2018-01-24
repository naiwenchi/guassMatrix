using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gaussMatrix
{
    class Program
    {
        static void Main(string[] args)
        {
            double[,] A = { { 2,1, -5, 1,8}, { 1,-3,0,-6, 9},{ 0,2,-1,2,-5}, { 1,4,-7,6,0} };
            GaussProcess myGauss = new GaussProcess(A);
            myGauss.performGauss();


        }//end main




    }; //end Program class

    class GaussProcess
    {
        public double[,] matrix;
        public double[] result; 

        public GaussProcess(double[,] _matrix)
        {
            this.matrix = _matrix;
            this.result = new double[this.matrix.GetLength(0)];

        }

        public void performGauss()
        {
            for (int k = 0; k != this.matrix.GetLength(0); k++)
            {
                Console.WriteLine("第{0}步驟：", k+1);
                this.printArray();
                Console.WriteLine();

                this.selectMaxElement(this.matrix, 0);
                Console.WriteLine("選取主元素之後的矩陣：");
                this.printArray();
                Console.WriteLine();

                double elementLine = this.matrix[k, k];

                for (int l = 0; l != this.matrix.GetLength(1); l++)              
                    this.matrix[k, l] /= elementLine; //將列上所有元素除以對角元素，使對角元素為1
                //此外，不用擔心除以零的問題，因為selectMaxElement函式會保證對角元素不為零
                Console.WriteLine("將第{0}行中的a[{0},{0}]化約為1之後的矩陣：", k+1);
                this.printArray();
                Console.WriteLine();

                //開始執行高斯消去法的消元動作，由對角元素所在之「下一列」一直往下做到列尾巴
                for (int i = k + 1; i != this.matrix.GetLength(0); i++)
                {
                    double reduceBase = this.matrix[i, k]; //第k列
                    for (int j = k; j != this.matrix.GetLength(1); j++)
                        this.matrix[i, j] -= reduceBase * this.matrix[k, j];
                }

                Console.WriteLine("消元之後的矩陣：");
                this.printArray();
                Console.WriteLine();

            }// end for k

            //最後回代求解
            this.result[this.matrix.GetLength(0) - 1] = this.matrix[this.matrix.GetLength(0) - 1, this.matrix.GetLength(1) - 1];
            for (int i = this.matrix.GetLength(0) - 1; i != 0; i--)
            {
                this.result[i] = this.matrix[i, this.matrix.GetLength(1) - 1];
                for (int j = i + 1; j != this.matrix.GetLength(1); j++)
                    this.result[i] -= this.matrix[i, j] * this.result[i];
            }


        }//end fun


        public void printArray()
        {
            for (int i = 0; i != this.matrix.GetLength(0); i++)
            {
                for (int j = 0; j != this.matrix.GetLength(1); j++)
                {
                    Console.Write(this.matrix[i, j] + "\t");
                }//end j
                Console.WriteLine();
            }//emd i
        }//end fun


        /// <summary>
        /// 高斯消去法就是對針對(1,1),(2,2),(3,3),...,(n,n)作處理，這是我所謂的對角元素
        /// </summary>
        /// <param name="arr">輸入的矩陣</param>
        /// <param name="line">正在處理當中的「對角元素」之行列數（行數等於列數）</param>
        private void selectMaxElement(double[,] arr, int line)
        {
            int n = arr.GetLength(0); //列數
            int m = arr.GetLength(1); //行數

            double max = arr[line, line]; //用來比大小的最大值，初始值會是一個「對角元素」
            double t = 0; //用來swap的元素
            int rowIndexToBeSwap = 0; //找到最大值的「列數」，比方說line=2, x=5的時候，會將第二列與第五列對調

            for (int i = line + 1; i != n; i++)
            {
                //以對角元素(2,2)舉例，它必須和(3,2),(4,2),...,(n,2)比較，
                //比方說(5,2)是最大值的話，最後就會把第二列與第五列對調
                if (Math.Abs(arr[i, line]) >= Math.Abs(max)) //有比較大才發生調換
                {
                    max = arr[i, line]; //一路用比大的方式調換max的內容，這做法有點像泡沫排序的理念
                                        //如果迴圈跑完一輪雖然不知道發生幾次調換，但一定會得到最大值
                    rowIndexToBeSwap = i; //並且把列號記錄下來
                }//end if

            }//end for i

            if (rowIndexToBeSwap != 0) //有一種情況是該列本來就是最大值，所以不用做交換，這時rowIndexToBeSwap維持初始值0
            {
                for (int i = 0; i != m; i++) //現在針對兩個要調換列的每一行做swap
                {
                    t = arr[rowIndexToBeSwap, i];
                    arr[rowIndexToBeSwap, i] = arr[line, i];
                    arr[line, i] = t;
                }//end for i
            }//end if



        }//end fun


    };
}
