﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BA
{
    class Program
    {

        public static int[,] A = {{53, 66, 66, 66, 66, 53, 53, 53, 53, 53, 73, 53, 53, 53, 66, 53, 53, 53, 53, 85, 73, 73, 73, 73, 53, 53},
{66, 53, 66, 66, 66, 53, 53, 53, 53, 53, 53, 73, 53, 53, 66, 53, 53, 53, 53, 73, 85, 73, 73, 73, 53, 53},
{66, 66, 53, 66, 66, 53, 53, 53, 53, 53, 53, 53, 73, 53, 66, 53, 53, 53, 53, 73, 73, 85, 73, 73, 53, 53},
{66, 66, 66, 53, 66, 53, 53, 53, 53, 53, 53, 53, 53, 73, 73, 53, 53, 53, 53, 73, 73, 73, 85, 85, 53, 53},
{66, 66, 66, 66, 53, 53, 53, 53, 53, 53, 53, 53, 53, 53, 73, 53, 53, 53, 53, 73, 73, 73, 85, 85, 53, 53},
{53, 53, 53, 53, 53, 53, 66, 66, 66, 66, 53, 53, 53, 53, 53, 73, 73, 53, 53, 53, 53, 53, 53, 53, 85, 85},
{53, 53, 53, 53, 53, 66, 53, 66, 66, 66, 53, 53, 53, 53, 53, 73, 73, 53, 53, 53, 53, 53, 53, 53, 85, 85},
{53, 53, 53, 53, 53, 66, 66, 53, 66, 66, 53, 53, 53, 53, 53, 66, 53, 73, 53, 53, 53, 53, 53, 53, 73, 73},
{53, 53, 53, 53, 53, 66, 66, 66, 53, 66, 53, 53, 53, 53, 53, 66, 53, 53, 73, 53, 53, 53, 53, 53, 73, 73},
{53, 53, 53, 53, 53, 66, 66, 66, 66, 53, 53, 53, 53, 53, 53, 66, 53, 53, 53, 53, 53, 53, 53, 53, 73, 73},
{66, 66, 66, 66, 66, 53, 53, 53, 53, 53, 53, 53, 53, 53, 66, 53, 53, 53, 53, 73, 73, 73, 73, 73, 53, 53},
{66, 66, 66, 66, 66, 53, 53, 53, 53, 53, 53, 53, 53, 53, 66, 53, 53, 53, 53, 73, 73, 73, 73, 73, 53, 53},
{66, 66, 66, 66, 66, 53, 53, 53, 53, 53, 53, 53, 53, 53, 66, 53, 53, 53, 53, 73, 73, 73, 73, 73, 53, 53},
{66, 66, 66, 66, 66, 53, 53, 53, 53, 53, 53, 53, 53, 53, 66, 53, 53, 53, 53, 73, 73, 73, 73, 73, 53, 53},
{66, 66, 66, 66, 66, 53, 53, 53, 53, 53, 53, 53, 53, 66, 53, 53, 53, 53, 53, 73, 73, 73, 73, 73, 53, 53},
{53, 53, 53, 53, 53, 66, 66, 66, 66, 66, 53, 53, 53, 53, 53, 53, 66, 53, 53, 53, 53, 53, 53, 53, 73, 73},
{53, 53, 53, 53, 53, 66, 66, 66, 66, 66, 53, 53, 53, 53, 53, 66, 53, 53, 53, 53, 53, 53, 53, 53, 73, 73},
{53, 53, 53, 53, 53, 66, 66, 66, 66, 66, 53, 53, 53, 53, 53, 66, 53, 53, 53, 53, 53, 53, 53, 53, 73, 73},
{53, 53, 53, 53, 53, 66, 66, 66, 66, 66, 53, 53, 53, 53, 53, 66, 53, 53, 53, 53, 53, 53, 53, 53, 73, 73},
{85, 66, 66, 66, 66, 53, 53, 53, 53, 53, 66, 53, 53, 53, 66, 53, 53, 53, 53, 53, 73, 73, 73, 73, 53, 53},
{66, 85, 66, 66, 66, 53, 53, 53, 53, 53, 53, 66, 53, 53, 66, 53, 53, 53, 53, 73, 53, 73, 73, 73, 53, 53},
{66, 66, 85, 66, 66, 53, 53, 53, 53, 53, 53, 53, 66, 53, 66, 53, 53, 53, 53, 73, 73, 53, 73, 73, 53, 53},
{66, 66, 66, 85, 85, 53, 53, 53, 53, 53, 53, 53, 53, 66, 66, 53, 53, 53, 53, 73, 73, 73, 53, 66, 53, 53},
{66, 66, 66, 85, 85, 53, 53, 53, 53, 53, 53, 53, 53, 66, 66, 53, 53, 53, 53, 73, 73, 73, 66, 53, 53, 53},
{53, 53, 53, 53, 53, 85, 85, 66, 66, 66, 53, 53, 53, 53, 53, 66, 66, 53, 53, 53, 53, 53, 53, 53, 53, 66},
{53, 53, 53, 53, 53, 85, 85, 66, 66, 66, 53, 53, 53, 53, 53, 66, 66, 53, 53, 53, 53, 53, 53, 53, 66, 53}};

        public static int[,] B = {{10, 176, 340, 135, 19, 99, 246, 19, 1267, 19, 9, 596, 250, 1545, 11, 203, 42, 878, 405, 925, 718, 301, 2, 6, 70, 11},
{153, 3, 1, 2, 149, 0, 0, 0, 115, 8, 0, 269, 0, 1, 78, 1, 0, 153, 25, 9, 59, 2, 0, 0, 4, 0},
{339, 0, 129, 2, 939, 0, 0, 496, 402, 0, 13, 112, 1, 6, 1028, 0, 11, 159, 14, 317, 164, 0, 0, 0, 1, 0},
{402, 0, 3, 5, 2976, 0, 3, 5, 394, 6, 0, 0, 22, 3, 204, 0, 0, 125, 52, 4, 498, 1, 0, 0, 3, 0},
{188, 33, 585, 169, 349, 176, 196, 17, 130, 21, 3, 647, 836, 2494, 35, 246, 55, 1809, 3731, 1385, 783, 243, 2, 147, 8, 24},
{239, 3, 0, 1, 184, 165, 1, 0, 239, 0, 1, 45, 0, 0, 234, 0, 1, 183, 23, 1, 51, 0, 0, 0, 0, 0},
{168, 0, 0, 0, 449, 0, 12, 3, 132, 0, 0, 29, 14, 114, 77, 1, 0, 234, 3, 30, 79, 0, 0, 0, 13, 0},
{241, 0, 1, 0, 317, 0, 1, 0, 104, 0, 0, 8, 3, 19, 83, 0, 0, 25, 1, 3, 25, 1, 1, 0, 9, 0},
{212, 104, 354, 197, 1104, 142, 144, 0, 17, 2, 5, 689, 226, 1188, 946, 74, 318, 569, 1205, 1063, 13, 179, 0, 61, 2, 6},
{56, 1, 0, 0, 93, 0, 0, 0, 1, 0, 1, 1, 0, 1, 106, 0, 0, 0, 1, 0, 81, 0, 0, 0, 0, 0},
{13, 0, 0, 0, 8, 1, 0, 2, 30, 0, 1, 3, 2, 0, 4, 0, 0, 5, 3, 1, 2, 0, 0, 0, 0, 0},
{1260, 4, 5, 9, 2480, 3, 32, 5, 675, 0, 3, 528, 22, 0, 279, 14, 28, 3, 95, 66, 301, 5, 1, 0, 14, 0},
{554, 145, 1, 3, 1261, 0, 2, 1, 467, 1, 4, 4, 253, 15, 318, 251, 0, 1, 8, 7, 101, 0, 0, 0, 9, 0},
{357, 4, 523, 491, 1205, 89, 184, 2, 493, 6, 5, 5, 13, 295, 347, 9, 60, 16, 1101, 2070, 95, 80, 0, 0, 5, 15},
{4, 98, 239, 109, 26, 51, 81, 7, 459, 13, 1, 336, 478, 2290, 16, 178, 25, 640, 193, 149, 1281, 45, 7, 5, 60, 7},
{807, 0, 3, 0, 505, 3, 0, 38, 118, 0, 0, 356, 4, 0, 651, 128, 1, 759, 47, 102, 145, 0, 0, 0, 0, 0},
{0, 0, 0, 0, 5, 0, 0, 0, 3, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1250, 1, 0, 0, 0, 0},
{1008, 15, 155, 184, 2531, 17, 106, 5, 762, 0, 4, 28, 199, 174, 719, 35, 22, 141, 464, 465, 100, 68, 1, 1, 11, 0},
{404, 4, 102, 7, 1127, 18, 3, 14, 590, 4, 16, 19, 51, 14, 527, 157, 55, 13, 566, 839, 359, 1, 0, 2, 17, 6},
{732, 8, 13, 8, 1819, 2, 1, 53, 1374, 0, 1, 10, 9, 7, 364, 5, 3, 811, 303, 225, 232, 0, 1, 0, 15, 5},
{125, 102, 138, 98, 985, 22, 52, 10, 529, 38, 1, 247, 59, 752, 19, 117, 7, 1211, 569, 481, 1, 257, 3, 277, 3, 5},
{247, 0, 0, 1, 659, 0, 0, 0, 320, 0, 0, 2, 0, 0, 204, 0, 0, 76, 0, 0, 19, 2, 0, 0, 0, 0},
{13, 0, 0, 0, 3, 0, 0, 0, 2, 0, 0, 1, 0, 0, 6, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0},
{15, 0, 21, 0, 29, 0, 0, 0, 29, 0, 0, 2, 1, 0, 2, 39, 2, 0, 3, 35, 1, 3, 0, 5, 1, 0},
{26, 0, 2, 1, 57, 0, 1, 0, 1, 0, 1, 10, 7, 3, 7, 21, 0, 3, 80, 3, 2, 2, 0, 0, 0, 0},
{10, 0, 1, 0, 23, 0, 0, 0, 5, 0, 0, 0, 0, 2, 13, 1, 0, 0, 3, 0, 0, 0, 1, 0, 2, 3}};

        static void Main(string[] args)
        {
            BeesAlgorithm BA = new BeesAlgorithm(100, 10, 3, 40, 20, 0.2, 100000);
            BA.SetTestData(A, B, 26);
            BA.runAlgorithm();

            /*foreach(int i in BA.costs)
                Console.WriteLine(i);*/
            Console.In.Read();
        }
    }
}