﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    class Program
    {


        static void Main(string[] args)
        {
            HashSet<int> l = new HashSet<int>(Enumerable.Range(0, 15));
            foreach (var i in l)
            {
                Console.WriteLine(i);
            }
            
            int n = 19;

            var fa = new FireflyAlgorithm();
            fa.SetTestData(A, B, n);
            fa.imax = 1000;
            fa.gamma = 1.0;
            fa.alfa = 3;
            fa.m = 10;

            fa.m = 10;
//            fa.TmpRun(10, 10, 1.0, 1.0, 1,  A, B);
            fa.InitializeAlgorithm();
            fa.RunAlgorithm();
            Console.ReadKey();
        }


         static int[,] A = {{ 0,  12,  36  ,28  ,52  ,44 ,110, 126  ,94,  63 ,130, 102,  65,  98 ,132, 132 ,126, 120 ,126},
                    { 12,   0,  24 , 75  ,82  ,75 ,108  ,70 ,124  ,86  ,93 ,106  ,58 ,124 ,161 ,161  ,70  ,64,  70},
 {36,  24,   0,  47,  71 , 47, 110,  73, 126 , 71 , 95, 110 , 46 ,127 ,163 ,163 , 73 , 67,  73},
{ 28  ,75  ,47   ,0  ,42  ,34 ,148 ,111 ,160  ,52  ,94 ,148  ,49 ,117 ,104 ,109 ,111 ,105 ,111},
 {52  ,82  ,71  ,42   ,0  ,42 ,125 ,136 ,102  ,22  ,73 ,125  ,32  ,94 ,130 ,130 ,136 ,130 ,136},
{ 44  ,75  ,47  ,34  ,42   ,0 ,148 ,111 ,162  ,52  ,96 ,148  ,49 ,117 ,152 ,152 ,111 ,105 ,111},
{110 ,108 ,110 ,148 ,125 ,148   ,0  ,46  ,46 ,136  ,47  ,30 ,108  ,51  ,79  ,79  ,46  ,47  ,41},
{126  ,70  ,73 ,111 ,136 ,111  ,46   ,0  ,69 ,141  ,63  ,46 ,119  ,68 ,121 ,121  ,27  ,24  ,36},
 {94 ,124 ,126 ,160 ,102 ,162  ,46  ,69   ,0 ,102  ,34  ,45  ,84  ,23  ,80  ,80  ,69  ,64  ,51},
{ 63  ,86  ,71  ,52  ,22  ,52 ,136 ,141 ,102   ,0  ,64 ,118  ,29  ,95 ,131 ,131 ,141 ,135 ,141},
{130  ,93  ,95  ,94  ,73  ,96  ,47  ,63  ,34  ,64   ,0  ,47  ,56  ,54  ,94  ,94  ,63  ,46  ,24},
{102 ,106 ,110 ,148 ,125 ,148  ,30  ,46  ,45 ,118  ,47   ,0 ,100  ,51  ,89  ,89  ,46  ,40  ,36},
 {65  ,58  ,46  ,49  ,32  ,49 ,108 ,119  ,84  ,29  ,56 ,100   ,0  ,77 ,113 ,113 ,119 ,113 ,119},
{ 98 ,124 ,127 ,117  ,94 ,117  ,51  ,68  ,23  ,95  ,54  ,51  ,77   ,0  ,79  ,79  ,68  ,62  ,51},
{132 ,161 ,163 ,104 ,130 ,152  ,79 ,121  ,80 ,131  ,94  ,89 ,113  ,79   ,0  ,10 ,113 ,107 ,119},
{132 ,161 ,163 ,109 ,130 ,152  ,79 ,121  ,80 ,131  ,94  ,89 ,113  ,79  ,10   ,0 ,113 ,107 ,119 },
{126  ,70  ,73 ,111 ,136 ,111  ,46  ,27  ,69 ,141  ,63  ,46 ,119  ,68 ,113 ,113   ,0   ,6  ,24},
{120  ,64  ,67 ,105 ,130 ,105  ,47  ,24  ,64 ,135  ,46  ,40 ,113  ,62 ,107 ,107   ,6   ,0  ,12},
{126  ,70  ,73 ,111 ,136 ,111  ,41  ,36  ,51 ,141  ,24  ,36 ,119  ,51 ,119 ,119  ,24  ,12   ,0}};

            static int[,] B = {
                            {
                                0, 76687, 0, 415, 545, 819, 135, 1368, 819, 5630, 0, 3432, 9082, 1503, 0, 0, 13732, 1368,
                                1783
                            },
                            {
                                76687, 0, 40951, 4118, 5767, 2055, 1917, 2746, 1097, 5712, 0, 0, 0, 268, 0, 1373, 268, 0, 0
                            },
                            {0, 40951, 0, 3848, 2524, 3213, 2072, 4225, 566, 0, 0, 404, 9372, 0, 972, 0, 13538, 1368, 0}
                            ,
                            {415, 4118, 3848, 0, 256, 0, 0, 0, 0, 829, 128, 0, 0, 0, 0, 0, 0, 0, 0},
                            {545, 5767, 2524, 256, 0, 0, 0, 0, 47, 1655, 287, 0, 42, 0, 0, 0, 226, 0, 0},
                            {819, 2055, 3213, 0, 0, 0, 0, 0, 0, 926, 161, 0, 0, 0, 0, 0, 0, 0, 0},
                            {135, 1917, 2072, 0, 0, 0, 0, 0, 196, 1538, 196, 0, 0, 0, 0, 0, 0, 0, 0},
                            {1368, 2746, 4225, 0, 0, 0, 0, 0, 0, 0, 301, 0, 0, 0, 0, 0, 0, 0, 0},
                            {819, 1097, 566, 0, 47, 0, 196, 0, 0, 1954, 418, 0, 0, 0, 0, 0, 0, 0, 0},
                            {5630, 5712, 0, 829, 1655, 926, 1538, 0, 1954, 0, 0, 282, 0, 0, 0, 0, 0, 0, 0},
                            {0, 0, 0, 128, 287, 161, 196, 301, 418, 0, 0, 1686, 0, 0, 0, 0, 226, 0, 0},
                            {3432, 0, 404, 0, 0, 0, 0, 0, 0, 282, 1686, 0, 0, 0, 0, 0, 0, 0, 0},
                            {9082, 0, 9372, 0, 42, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                            {1503, 268, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                            {0, 0, 972, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 99999, 0, 0, 0},
                            {0, 1373, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 99999, 0, 0, 0, 0},
                            {13732, 268, 13538, 0, 226, 0, 0, 0, 0, 0, 226, 0, 0, 0, 0, 0, 0, 0, 0},
                            {1368, 0, 1368, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                            {1783, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
                        };

    }

}
