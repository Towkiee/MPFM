using CCWin;
using CyUSB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using IniFile;
using System.Timers;

namespace MPFM
{
    public partial class MPFM : Skin_VS
    {
        public MPFM()
        {
            InitializeComponent();
            hisCw_serialdataGrid2_initizlize(); //数据保存位置初始化
            inifile_initizlize();               //ini文件初始化
            //set_mesh_chart();//网格均值图属性设置

        }

    public static byte[] ArrayCRCHigh = new byte[]  {
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
            0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40 };

        public static byte[] ArrayCRCLow = new byte[] {
            0x00, 0xC0, 0xC1, 0x01, 0xC3, 0x03, 0x02, 0xC2, 0xC6, 0x06, 0x07, 0xC7,
            0x05, 0xC5, 0xC4, 0x04, 0xCC, 0x0C, 0x0D, 0xCD, 0x0F, 0xCF, 0xCE, 0x0E,
            0x0A, 0xCA, 0xCB, 0x0B, 0xC9, 0x09, 0x08, 0xC8, 0xD8, 0x18, 0x19, 0xD9,
            0x1B, 0xDB, 0xDA, 0x1A, 0x1E, 0xDE, 0xDF, 0x1F, 0xDD, 0x1D, 0x1C, 0xDC,
            0x14, 0xD4, 0xD5, 0x15, 0xD7, 0x17, 0x16, 0xD6, 0xD2, 0x12, 0x13, 0xD3,
            0x11, 0xD1, 0xD0, 0x10, 0xF0, 0x30, 0x31, 0xF1, 0x33, 0xF3, 0xF2, 0x32,
            0x36, 0xF6, 0xF7, 0x37, 0xF5, 0x35, 0x34, 0xF4, 0x3C, 0xFC, 0xFD, 0x3D,
            0xFF, 0x3F, 0x3E, 0xFE, 0xFA, 0x3A, 0x3B, 0xFB, 0x39, 0xF9, 0xF8, 0x38,
            0x28, 0xE8, 0xE9, 0x29, 0xEB, 0x2B, 0x2A, 0xEA, 0xEE, 0x2E, 0x2F, 0xEF,
            0x2D, 0xED, 0xEC, 0x2C, 0xE4, 0x24, 0x25, 0xE5, 0x27, 0xE7, 0xE6, 0x26,
            0x22, 0xE2, 0xE3, 0x23, 0xE1, 0x21, 0x20, 0xE0, 0xA0, 0x60, 0x61, 0xA1,
            0x63, 0xA3, 0xA2, 0x62, 0x66, 0xA6, 0xA7, 0x67, 0xA5, 0x65, 0x64, 0xA4,
            0x6C, 0xAC, 0xAD, 0x6D, 0xAF, 0x6F, 0x6E, 0xAE, 0xAA, 0x6A, 0x6B, 0xAB,
            0x69, 0xA9, 0xA8, 0x68, 0x78, 0xB8, 0xB9, 0x79, 0xBB, 0x7B, 0x7A, 0xBA,
            0xBE, 0x7E, 0x7F, 0xBF, 0x7D, 0xBD, 0xBC, 0x7C, 0xB4, 0x74, 0x75, 0xB5,
            0x77, 0xB7, 0xB6, 0x76, 0x72, 0xB2, 0xB3, 0x73, 0xB1, 0x71, 0x70, 0xB0,
            0x50, 0x90, 0x91, 0x51, 0x93, 0x53, 0x52, 0x92, 0x96, 0x56, 0x57, 0x97,
            0x55, 0x95, 0x94, 0x54, 0x9C, 0x5C, 0x5D, 0x9D, 0x5F, 0x9F, 0x9E, 0x5E,
            0x5A, 0x9A, 0x9B, 0x5B, 0x99, 0x59, 0x58, 0x98, 0x88, 0x48, 0x49, 0x89,
            0x4B, 0x8B, 0x8A, 0x4A, 0x4E, 0x8E, 0x8F, 0x4F, 0x8D, 0x4D, 0x4C, 0x8C,
            0x44, 0x84, 0x85, 0x45, 0x87, 0x47, 0x46, 0x86, 0x82, 0x42, 0x43, 0x83,
            0x41, 0x81, 0x80, 0x40 };


        //平滑常数
        //5点sgolay平滑
        public static double[] q_m_5x = new double[5] { -0.0857, 0.3429, 0.4857, 0.3429, -0.0857 };

        public static double[,] q_s_5x = new double[2, 5] { { 0.8857, 0.2571, -0.0857, -0.1429, 0.0857 },
                                                            {0.2571, 0.3714, 0.3429, 0.1714, -0.1429 } };

        public static double[,] q_e_5x = new double[2, 5] { { -0.1429, 0.1714, 0.3429, 0.3714, 0.2571 },
                                                            { 0.0857, -0.1429, -0.0857, 0.2571, 0.8857} };
        //23点sgolay平滑
        public static double[] q_m_23x = new double[23] { -0.0522, -0.0261, -0.0025, 0.0186, 0.0373, 0.0534, 0.0671, 0.0783, 0.0870, 0.0932, 0.0969, 0.0981, 0.0969, 0.0932, 0.0870, 0.0783, 0.0671, 0.0534, 0.0373, 0.0186, -0.0025, -0.0261, -0.0522 };

        public static double[,] q_s_23x = new double[11, 23]
            { {3.3043e-001,  2.7391e-001 , 2.2174e-001 , 1.7391e-001 , 1.3043e-001 , 9.1304e-002 , 5.6522e-002 , 2.6087e-002 , -4.1633e-017 ,-2.1739e-002 ,-3.9130e-002 ,-5.2174e-002 ,-6.0870e-002 ,-6.5217e-002 ,-6.5217e-002 ,-6.0870e-002 , -5.2174e-002 ,-3.9130e-002 ,-2.1739e-002 ,           0 , 2.6087e-002 , 5.6522e-002 , 9.1304e-002 },
              { 2.7391e-001 , 2.3083e-001 , 1.9091e-001 , 1.5415e-001 , 1.2055e-001 , 9.0119e-002 , 6.2846e-002 , 3.8735e-002 ,  1.7787e-002 ,-1.7347e-017 ,-1.4625e-002 ,-2.6087e-002 ,-3.4387e-002 ,-3.9526e-002 ,-4.1502e-002 ,-4.0316e-002 , -3.5968e-002 ,-2.8458e-002 ,-1.7787e-002 ,-3.9526e-003 , 1.3043e-002 , 3.3202e-002 , 5.6522e-002} ,
              { 2.2174e-001 , 1.9091e-001 , 1.6217e-001 , 1.3552e-001 , 1.1095e-001 , 8.8481e-002 , 6.8097e-002 , 4.9802e-002 ,  3.3597e-002 , 1.9481e-002 , 7.4534e-003 ,-2.4845e-003 ,-1.0333e-002 ,-1.6093e-002 ,-1.9763e-002 ,-2.1344e-002 , -2.0836e-002 ,-1.8238e-002 ,-1.3552e-002 ,-6.7758e-003 , 2.0892e-003 , 1.3043e-002 , 2.6087e-002} ,
              {1.7391e-001 , 1.5415e-001 , 1.3552e-001 , 1.1801e-001 , 1.0164e-001 , 8.6392e-002 , 7.2276e-002 , 5.9289e-002 ,  4.7431e-002 , 3.6702e-002 , 2.7103e-002 , 1.8634e-002 , 1.1293e-002 , 5.0819e-003 ,-1.7347e-017 ,-3.9526e-003 , -6.7758e-003 ,-8.4698e-003 ,-9.0344e-003 ,-8.4698e-003, -6.7758e-003 ,-3.9526e-003 ,-1.3878e-017 },
              {1.3043e-001 , 1.2055e-001 , 1.1095e-001 , 1.0164e-001 , 9.2603e-002 , 8.3851e-002 , 7.5381e-002 , 6.7194e-002 ,  5.9289e-002 , 5.1666e-002 , 4.4325e-002 , 3.7267e-002 , 3.0491e-002 , 2.3998e-002 , 1.7787e-002 , 1.1858e-002 ,  6.2112e-003 , 8.4698e-004 ,-4.2349e-003 ,-9.0344e-003, -1.3552e-002 ,-1.7787e-002 ,-2.1739e-002} ,
              {9.1304e-002 , 9.0119e-002 , 8.8481e-002 , 8.6392e-002 , 8.3851e-002 , 8.0858e-002 , 7.7414e-002 , 7.3518e-002 ,  6.9170e-002 , 6.4370e-002 , 5.9119e-002 , 5.3416e-002 , 4.7261e-002 , 4.0655e-002 , 3.3597e-002 , 2.6087e-002 ,  1.8125e-002 , 9.7120e-003 ,8.4698e-004 ,-8.4698e-003 ,-1.8238e-002 ,-2.8458e-002 ,-3.9130e-002  },
              {5.6522e-002 , 6.2846e-002 , 6.8097e-002 , 7.2276e-002 , 7.5381e-002 , 7.7414e-002 , 7.8374e-002 , 7.8261e-002 ,  7.7075e-002 , 7.4816e-002 , 7.1485e-002 , 6.7081e-002 , 6.1604e-002 , 5.5054e-002 , 4.7431e-002 , 3.8735e-002 ,  2.8967e-002 , 1.8125e-002 , 6.2112e-003 ,-6.7758e-003, -2.0836e-002 ,-3.5968e-002, -5.2174e-002 },
              {2.6087e-002 , 3.8735e-002 , 4.9802e-002 , 5.9289e-002 , 6.7194e-002 , 7.3518e-002 , 7.8261e-002 , 8.1423e-002 ,  8.3004e-002 , 8.3004e-002 , 8.1423e-002 , 7.8261e-002 , 7.3518e-002 , 6.7194e-002 , 5.9289e-002 , 4.9802e-002 ,  3.8735e-002 , 2.6087e-002 , 1.1858e-002 ,-3.9526e-003, -2.1344e-002 ,-4.0316e-002, -6.0870e-002} ,
              {-4.1633e-017 , 1.7787e-002 , 3.3597e-002 , 4.7431e-002 , 5.9289e-002 , 6.9170e-002 , 7.7075e-002 , 8.3004e-002 ,  8.6957e-002 , 8.8933e-002 , 8.8933e-002 , 8.6957e-002 , 8.3004e-002 , 7.7075e-002 , 6.9170e-002 , 5.9289e-002 ,  4.7431e-002 , 3.3597e-002 , 1.7787e-002 ,-1.7347e-017 ,-1.9763e-002 ,-4.1502e-002 ,-6.5217e-002 },
              { -2.1739e-002 ,-1.7347e-017 , 1.9481e-002 , 3.6702e-002 , 5.1666e-002 , 6.4370e-002 , 7.4816e-002 , 8.3004e-002    ,  8.8933e-002 , 9.2603e-002 , 9.4015e-002 , 9.3168e-002 , 9.0062e-002 , 8.4698e-002 , 7.7075e-002 , 6.7194e-002 ,  5.5054e-002 , 4.0655e-002 , 2.3998e-002 , 5.0819e-003 ,-1.6093e-002 ,-3.9526e-002 ,-6.5217e-002},
              {-3.9130e-002 ,-1.4625e-002 , 7.4534e-003 , 2.7103e-002 , 4.4325e-002 , 5.9119e-002 , 7.1485e-002 , 8.1423e-002 ,  8.8933e-002 , 9.4015e-002 , 9.6669e-002 , 9.6894e-002 , 9.4692e-002 , 9.0062e-002 , 8.3004e-002 , 7.3518e-002 ,  6.1604e-002 , 4.7261e-002 , 3.0491e-002 , 1.1293e-002 ,-1.0333e-002 ,-3.4387e-002 ,-6.0870e-002 }};

        public static double[,] q_e_23x = new double[11, 23]
            { {-6.0869565e-002,-3.4387352e-002,-1.0333145e-002,1.1293055e-002,3.0491248e-002,4.7261434e-002,6.1603614e-002,7.3517787e-002,8.3003953e-002,9.0062112e-002,9.4692264e-002,9.6894410e-002,9.6668549e-002,9.4014681e-002,8.8932806e-002,8.1422925e-002,7.1485037e-002,5.9119142e-002,4.4325240e-002,2.7103331e-002,7.4534161e-003,-1.4624506e-002,-3.9130435e-002 },
              {-6.5217391e-002,-3.9525692e-002,-1.6092603e-002,5.0818746e-003,2.3997741e-002,4.0654997e-002,5.5053642e-002,6.7193676e-002,7.7075099e-002,8.4697911e-002,9.0062112e-002,9.3167702e-002,9.4014681e-002,9.2603049e-002,8.8932806e-002,8.3003953e-002,7.4816488e-002,6.4370412e-002,5.1665726e-002,3.6702428e-002,1.9480519e-002,1.3877788e-017,-2.1739130e-002 },
              {-6.5217391e-002,-4.1501976e-002,-1.9762846e-002,1.3877788e-017,1.7786561e-002,3.3596838e-002,4.7430830e-002,5.9288538e-002,6.9169960e-002,7.7075099e-002,8.3003953e-002,8.6956522e-002,8.8932806e-002,8.8932806e-002,8.6956522e-002,8.3003953e-002,7.7075099e-002,6.9169960e-002,5.9288538e-002,4.7430830e-002,3.3596838e-002,1.7786561e-002,1.3877788e-017 },
              {-6.0869565e-002,-4.0316206e-002,-2.1343874e-002,-3.9525692e-003,1.1857708e-002,2.6086957e-002,3.8735178e-002,4.9802372e-002,5.9288538e-002,6.7193676e-002,7.3517787e-002,7.8260870e-002,8.1422925e-002,8.3003953e-002,8.3003953e-002,8.1422925e-002,7.8260870e-002,7.3517787e-002,6.7193676e-002,5.9288538e-002,4.9802372e-002,3.8735178e-002,2.6086957e-002 },
              {-5.2173913e-002,-3.5968379e-002,-2.0835686e-002,-6.7758329e-003,6.2111801e-003,1.8125353e-002,2.8966685e-002,3.8735178e-002,4.7430830e-002,5.5053642e-002,6.1603614e-002,6.7080745e-002,7.1485037e-002,7.4816488e-002,7.7075099e-002,7.8260870e-002,7.8373800e-002,7.7413890e-002,7.5381141e-002,7.2275551e-002,6.8097120e-002,6.2845850e-002,5.6521739e-002 },
              {-3.9130435e-002,-2.8458498e-002,-1.8238283e-002,-8.4697911e-003,8.4697911e-004,9.7120271e-003,1.8125353e-002,2.6086957e-002,3.3596838e-002,4.0654997e-002,4.7261434e-002,5.3416149e-002,5.9119142e-002,6.4370412e-002,6.9169960e-002,7.3517787e-002,7.7413890e-002,8.0858272e-002,8.3850932e-002,8.6391869e-002,8.8481084e-002,9.0118577e-002,9.1304348e-002 },
              {-2.1739130e-002,-1.7786561e-002,-1.3551666e-002,-9.0344438e-003,-4.2348955e-003,8.4697911e-004,6.2111801e-003,1.1857708e-002,1.7786561e-002,2.3997741e-002,3.0491248e-002,3.7267081e-002,4.4325240e-002,5.1665726e-002,5.9288538e-002,6.7193676e-002,7.5381141e-002,8.3850932e-002,9.2603049e-002,1.0163749e-001,1.1095426e-001,1.2055336e-001,1.3043478e-001 },
              {4.1633363e-017,-3.9525692e-003,-6.7758329e-003,-8.4697911e-003,-9.0344438e-003,-8.4697911e-003,-6.7758329e-003,-3.9525692e-003,0.0000000e+000,5.0818746e-003,1.1293055e-002,1.8633540e-002,2.7103331e-002,3.6702428e-002,4.7430830e-002,5.9288538e-002,7.2275551e-002,8.6391869e-002,1.0163749e-001,1.1801242e-001,1.3551666e-001,1.5415020e-001,1.7391304e-001 },
              {2.6086957e-002,1.3043478e-002,2.0892151e-003,-6.7758329e-003,-1.3551666e-002,-1.8238283e-002,-2.0835686e-002,-2.1343874e-002,-1.9762846e-002,-1.6092603e-002,-1.0333145e-002,-2.4844720e-003,7.4534161e-003,1.9480519e-002,3.3596838e-002,4.9802372e-002,6.8097120e-002,8.8481084e-002,1.1095426e-001,1.3551666e-001,1.6216827e-001,1.9090909e-001,2.2173913e-001 },
              {5.6521739e-002,3.3201581e-002,1.3043478e-002,-3.9525692e-003,-1.7786561e-002,-2.8458498e-002,-3.5968379e-002,-4.0316206e-002,-4.1501976e-002,-3.9525692e-002,-3.4387352e-002,-2.6086957e-002,-1.4624506e-002,1.3877788e-017,1.7786561e-002,3.8735178e-002,6.2845850e-002,9.0118577e-002,1.2055336e-001,1.5415020e-001,1.9090909e-001,2.3083004e-001,2.7391304e-001 },
              {9.1304348e-002,5.6521739e-002,2.6086957e-002,-1.3877788e-017,-2.1739130e-002,-3.9130435e-002,-5.2173913e-002,-6.0869565e-002,-6.5217391e-002,-6.5217391e-002,-6.0869565e-002,-5.2173913e-002,-3.9130435e-002,-2.1739130e-002,1.3877788e-017,2.6086957e-002,5.6521739e-002,9.1304348e-002,1.3043478e-001,1.7391304e-001,2.2173913e-001,2.7391304e-001,3.3043478e-001 }};

        //流量计算
        public static double MAX_Q_LIQUID = 150.0;   //最大液量,2020/6/29由100改为150
        public static double MIN_Q_LIQUID = 0.0;     //最小液量
        public static double MAX_Q_GAS = 300.0;   //最大气量,2020/6/29由150改为200
        public static double MIN_Q_GAS = 0.0;     //最小气量
        public double Q_Liquid;
        public double Q_Oil;
        public double Q_Water;
        public double Q_Gas;
        public double Q_GVF;


        public double Q_Liquid_ave;
        public double Q_Oil_ave;
        public double Q_Water_ave;
        public double Q_Gas_ave;

        public string calc_flow_phase = "oil_gas_water_mix"; //=油气水三相流（有气）  oil_water_mix=油水两相流（无气）
        //public string calc_flow_phase = "oil_water_mix";

        //油气水三相流系数
        //液量计算系数
        public double[] three_phase_coe_low_cw = new double[4] { 16.1034631293600, 0.0474009817446342, -0.0319423145006287, 43.4229523861356 };

        //  public double[] three_phase_coe = new double[10]{ 254.687355363655, 0.0329673120206655, -0.942371066099166,                             //小魏系数
        //                                                   -1008.03784167390, 1.39413769438167, 0.0124393880714639,
        //                                                    1686.08144356418, 0.0167978720446740,-6.01173073929066e-05, -886.036091605744 };


        // public double[] three_phase_coe = new double[10]{-291.879579341560, 0.057584070538, 10.335060498741, 237.200987118755, -0.891247500887,     //2019.10.24   大气大液系数
        //                                                   - 0.137566015033, -206.250018796945, 0.151154096309, 0.000598938670, 49.970448632050  };
        //public double[] three_phase_coe = new double[10]{-126.7502432087894, 0.0362877220280, 4.0467620058515,  183.8396501765060,0.9297735362950,     //2019.10.24   大气大液系数
        //                                                 -0.0563703751048, -62.4113417938935, 0.0996388603162, 0.0002546090874,  -66.8667842347271 };   //2019.11.10   大气大液系数

        //public double[] three_phase_coe = new double[10]{-231.907571754067, 0.0721221071851303, 7.17964539949068,
        //                                                 270.362330012127, -2.62236836290254, -0.0903462032516407,
        //                                                   -246.434702393541, -0.0675281996689324,0.000368247440918353, 62.4752157752240 };   //2020.1.8   大气大液系数

        public double[] three_phase_coe = new double[10];//2020.8.12液量分段系数

        //public double[] three_phase_coe_all = new double[10]{-17.1750634865015, 0.0385043711078720, -0.786860440017954,
        //                                                       216.547531201432, 0.0974195256416584, 0.01283737839941649,
        //                                                      -173.045545452586, -0.0229162786679157, -7.21522463302663e-05, 31.2132953478337 };   //2020.8.12   不分段液系数
        //public double[] three_phase_coe_all = new double[10]{20.8460686640470, 0.0271629243052287, -0.0983518761523217,
        //                                                        72.4770432515269, 1.28359566620248, -0.00127442945247802,
        //                                                       -32.2395659492606, -0.0244556871643688, 1.38020467733806e-05, -13.9682003119009 };   //2020.12.28   不分段液系数

        //public double[] three_phase_coe_all = new double[10]{1.34623648381544, 0.0268158639791481, -0.220006705564818,
        //                                                        164.515513536295, 1.69817759100966, 0.00250713959405815,
        //                                                       -168.400748830784, -0.0348946390393933, -1.30120629477603e-05, 42.9350793968238 };   //2021.1.27   不分段液系数
        //public double[] three_phase_coe_all = new double[10]{69.0523846505837, 0.0191505019192639, 0.136060456465980,
        //                                                        -123.413826058326, 1.07644351841122, 0.00299274948295223,
        //                                                       167.581267572624, -0.00613584147707697, -3.44317230266326e-05, -73.3263596797319 };   //2021.5.11   不分段液系数
        //public double[] three_phase_coe_all = new double[10]{-18.7984562430322,0.00649628216580705,0.683070004815190,90.1696417873438,0.413503235179841,
        //                                                      -0.0122770664588315,-96.1174661930230,-0.00369285255880578,6.55787225437962e-05,25.0183595547605 };   //2022.11.4   不分段液系数
        // public double[] three_phase_coe_s = new double[10]{-27.5904747547625,0.0171291396619229,-0.182508312969032,
        //                                                     198.447970459012,-0.165570477285799,0.00277688022078280,
        //                                                     -257.638726879377,-0.0109100175048565,-1.81110013321423e-05,103.915403001057};//2022.12.09  小液量系数 <20
        //public double[] three_phase_coe_m = new double[10]{-8.46994220098005,0.00441771742324012,0.828120199684782,
        //                                                     59.0845366729900,0.593778060043673,-0.0132849200265063,
        //                                                    -85.1138853702660,-0.00430999785292729,6.95943925945948e-05,27.8832716616133};//2022.12.09  中液量系数 >20
        //public double[] three_phase_coe_s = new double[10]{-56.9411250050150, 0.0358129088093013, 0.484522227362750,
        //                                                    315.066223152363, 0.343417459287242, -0.00897705425093958,
        //                                                    -348.415053828611, 0.0314242118975060, 4.62415989883398e-05, 125.005085545028 };   //2020.8.12   小液量系数

        //public double[] three_phase_coe_m = new double[10]{-13.3215062861406, 0.0301241570813222, -0.275890371895336,
        //                                                    212.163043790288, 1.01474177608503, 0.00203230508048760,
        //                                                    -202.441688947295, -0.0282568601855677,-4.99637632731535e-06, 53.1188632537192 };   //2020.12.28   中液量系数

        //public double[] three_phase_coe_m = new double[10]{-3.92697840286325, 0.0279219016300540, -0.195386694299339,
        //                                                    165.975712453628, 1.19382751098106, 0.00167324358809000,
        //                                                   -150.042287303672, -0.0327012178934860, -6.81844606444908e-06, 34.7679818035138 };   //2020.8.12   中液量系数

        //public double[] three_phase_coe_l = new double[10]{-300.291637680454,0.0197340584413826,11.8604789434203,
        //                                                    39.3063377092814,2.20423858766325,-0.141534093237599,
        //                                                    -18.3453966019521,-0.0324972012285103,0.000557941880387002,-12.7672441508233 };   //2020.8.12   大液量系数
        //public double[] three_phase_coe_l = new double[10]{154.799084421677,0.0113378757683842,-7.08694098287707,
        //                                                    219.895319284591,3.10394515226593,0.0852663362125191,
        //                                                    -190.378487416624,-0.0328510862358088,-0.000331041328458967,40.8548044174568 };   //2020.12.28   大液量系数


        //public double[] three_phase_coe_all = new double[10]{-13.3857636711310,0.00526557652877776,0.336560066229776,
        //                                                     82.7800095140734,0.354067286383202,-0.00563859629864542,
        //                                                     -81.7136908362927,-0.00224588047506721,2.92975938195656e-05,20.9318872366883};   //2022.12.26   不分段液系数 10<液量<60

        //public double[] three_phase_coe_s = new double[10]{11.5466293711934,0.00965010469280350,-0.0347227092042343,
        //                                                   21.9903561453010,0.106055741504886,0.000602333691281303,-20.2868525035424,
        //                                                  -0.00285010641706991,-8.20328616786279e-06,2.84171615961345};//2022.12.26  小液量系数 10<液量<25

        //public double[] three_phase_coe_m = new double[10]{-24.2746662449099,0.00335465423460025,0.280681804671941,
        //                                                    104.281919021844,0.558032688808571,-0.000896357251748706,
        //                                                   -110.211387472433,-0.00278288187340656,-4.92542952042871e-06,31.1586281938559};//2022.12.26  中液量系数 25<液量<60 


        //202-3-27大庆
        public double[] three_phase_coe_all = new double[10]{19.0892720359504,
0.00620653592771467,
0.369429337825646,
-44.1673538750937,
0.480006993750203,
-0.00336600693810385,
59.1901748447869,
-0.00252922084911561,
3.94871724761430e-06,
-32.1973309027294

};   

        public double[] three_phase_coe_s = new double[10]{18.0601732461718,
0.0171101030724692,
0.0904404254859923,
-46.8623913878691,
0.0896971341627946,
-0.00389378872870941,
137.477736576071,
-0.00921992184428078,
2.71926687261219e-05,
-104.445941009298
};//小液量系数 10-25

       public double[] three_phase_coe_m = new double[10]{-1.66154785651592,
0.0104156806289856,
0.116016854531879,
73.0044616420558,
0.185834052917638,
-0.00113307293639624,
-97.1011223784204,
-0.00108483778648472,
1.03615462905763e-06,
35.0883767287739
};//中液量系数 20-50 

       public double[] three_phase_coe_l = new double[10]{-22.4614915961878,
0.00382917071944774,
0.844315428341543,
78.3685029902490,
0.522820182798769,
-0.00665027979220078,
-102.523970023521,
-0.00180116195587838,
1.12224479063354e-05,
35.1649611874265
};   //大液量系数45-100



        //气量计算系数
        //public double[] calc_gas_coe = new double[8]{-2030.82422685030,   -2296.79394877368,    2.07306676318314,      -0.167626178978525,                     //小魏系数
        //                                              2.54751613523255,   -0.0418280389186899,  0.000254027895733244,  2167.42143845284 };
        //public double[] calc_gas_coe = new double[7] { - 82.1941499231982e+00,   138.652419309980e+00,   -28.5728572769663e-03,   5.17919036727506e-03,     //黄亚系数
        //                                                -15.3867320895613e-03,   12.1694682781302e-03,   82.9094042258434e-03};

        public double[] calc_gas_coe = new double[11];//2020.8.12气量分段系数

        //public double[] calc_gas_coe = new double[11] { 35.7673188568168, -3.76822075482524, 0.264897018620693, 279.932000927862,
        //                                                 3032.24684051718, 0.000866959142953601, 100.685656701638,
        //                                                 -124.732440291724, 27.7060120133453, -134.572177883096, -1.13502987895186e-05};//2021.3.10  气量系数


        //public double[] calc_gas_coe_s = new double[11] { 683.625047275966, -1.56207975168780, 0.156709507014582,-1486.73892636464,
        //                                                 -6931.32469216859, 0.00190679242909449, 5.49160000247264,
        //                                                 -18.2347263073803, -129.091467244179, 1263.16281925566, -1.12363745791284e-05};//2021.5.11  小液量系数

        //public double[] calc_gas_coe_s = new double[11] { -110.505719812176,0.909475543594838,-0.0624245369689094,
        //                                                  251.989920621541,-189.298227629564,0.000400663086582778,-4.37378629056046,
        //                                                  6.96840169779252,81.9652272779071,-83.0461512445238,2.43468148128352e-05 };//2022.12.09  小液量系数


        //public double[] calc_gas_coe_l = new double[11] {115.433265962874,-1.88052005911309,0.0436987026024894,
        //                                                -331.710472118535,3217.84515932195,0.000387052874766530,-0.229686819733725,
        //                                                0.483153108876743,-13333.4848921318,154.572209403961,-3.24172435020483e-06 };//2021.12.09 大液量系数
        //public double[] calc_gas_coe_s = new double[11] { 7.38723040385461,-2.19535443142660,0.0167755408934847,
        //                                                  85.9968143643920,-36.0712620531346,0.000272915235517852,-0.168925164772989,
        //                                                  0.320599203324440,-18.4329784750914,-17.3855370895602,-1.39636258259225e-06 };//2022.12.26  小液量系数 小液量系数 10<液量<25


        //public double[] calc_gas_coe_m = new double[11] {130.785638917531,-2.92970913509217,0.0319381945753077,
        //                                                 -155.701043995622,1334.14294230643,9.50595182729021e-05,0.406040766505901,
        //                                                 -0.487431940623156,-23622.0940901512,69.9284686483658,-1.08379793024864e-06 };//2022.12.26  中液量系数 25<液量<60 


        public double[] calc_gas_coe_s = new double[11] {3.80949256545465,
-2.10183424826250,
-0.107805136156523,
152.035338371697,
-1240.19250969913,
0.00289668945766332,
-4.64194809530054,
10.0631508176909,
-62.3155766947197,
-51.0437616088109,
7.20881310376876e-05
};//2022.12.26  小液量系数 小液量系数 10<液量<25
        public double[] calc_gas_coe_m = new double[11] {29.9448769305368,
-2.46613534310650,
0.0253049652094391,
137.755412615986,
-1210.84502602845,
0.00168264676899937,
-0.144508994418019,
0.178294430617492,
-721.087327028098,
-60.3655438640726,
-2.43268311367080e-06 };//2022.12.26  中液量系数 25<液量<60 
        public double[] calc_gas_coe_l = new double[11] {94.4116368718578,
-2.75609985926141,
0.0197719620554501,
-24.3752724880941,
4188.76660883538,
0.00115945829619729,
-0.156866287654886,
0.388259498202026,
-162852.925177664,
1.79851380834146,
-4.20503152050248e-07};//2021.12.09 大液量系数

        //USB
        USBDeviceList usbDevices = null;
        CyUSBDevice MyDevice = null;
        CyControlEndPoint CtrlEndPt = null;
        CyBulkEndPoint inEndPt = null;
        Thread tXfers;
        bool usb_status;
        bool usb_exist;
        long outCount;

        const int XFERSIZE = 4096;
        byte[] outData = new byte[XFERSIZE];
        byte[] inData = new byte[XFERSIZE];

        //端口
        public int com_index = 0;
        public SerialPort ComDevice = new SerialPort();
        public SerialPort port_Cw = new SerialPort();
        public SerialPort port_Cw2 = new SerialPort();
        public SerialPort port_Dp = new SerialPort();
        public SerialPort port_GasMeter = new SerialPort();
        public SerialPort port_OilMeter = new SerialPort();
        public SerialPort port_BigOilMeter = new SerialPort();
        public bool port_Cw_status = false;
        public bool port_Cw2_status = false;
        public bool port_Dp_status = false;
        public bool port_GasMeter_status = false;
        public bool port_OilMeter_status = false;
        public bool port_BigOilMeter_status = false;
        public bool port_exist = false;
        public List<SerialPort> ComDevice_list = new List<SerialPort>();
        public List<string> ComDevice_name_list = new List<string>();
        public bool receSuccess_cw = false;
        public bool receSuccess_cw2 = false;
        public bool receSuccess_dp = false;
        public int rece_i_cw = 0;
        public int rece_i_cw2 = 0;
        public int rece_i_dp = 0;
        public bool port_error_message1 = true;
        public bool port_error_message2 = true;

        //气表端口
        public List<byte> paraBuffer_gas = new List<byte>();
        //油表端口
        public List<byte> paraBuffer_oil = new List<byte>();
        public List<byte> paraBuffer_bigoil = new List<byte>();

        //含水端口1
        public bool save_path_flag = false;
        public string filename_path;
        public string filepath_path;
        public StreamWriter sw_path;
        public int path_num;
        public int path_count = 0;

        private List<byte> paraBuffer_cw = new List<byte>();
        public bool greaterone_cw = false;
        public bool starflag_cw = false;
        public bool starflag2_cw = false;
        public int identifer_cw;
        public string datatime_cw;
        public string datatimelabel_cw;
        public int ii_cw = 1;
        public int ii_path = 1;
        public int pointindex_cw;
        public double seccw;
        public double mincw;
        public double dpre_cw;
        public double temp_cw;
        public double pathfir;
        public double pathsec;
        public double paththr;
        public double pathfor;

        //含水端口2
        private List<byte> paraBuffer_cw2 = new List<byte>();
        public bool greaterone_cw2 = false;
        public bool starflag_cw2 = false;
        public bool starflag2_cw2 = false;
        public int identifer_cw2;
        public string datatime_cw2;
        public string datatimelabel_cw2;
        public int ii_cw2 = 1;
        public int pointindex_cw2;
        public double seccw2;
        public double mincw2;
        public double dpre_cw2;
        public double temp_cw2;
        public double pathfir2;
        public double pathsec2;
        public double paththr2;
        public double pathfor2;

        public int startScroll_cw = 1;
        public int startScroll_path = 1;
        public static int pointThres_cw = 150;
        public string[] timearray_cw = new string[pointThres_cw];
        public double[] seccwarray_cw = new double[pointThres_cw];
        public double[] mincwarray_cw = new double[pointThres_cw];
        public double[] QLarray_cw = new double[pointThres_cw];
        public double[] QGarray_cw = new double[pointThres_cw];

        public string[] timearray_path = new string[pointThres_cw];
        public double[] yarray_cw1_path1 = new double[pointThres_cw];
        public double[] yarray_cw1_path2 = new double[pointThres_cw];
        public double[] yarray_cw2_path1 = new double[pointThres_cw];
        public double[] yarray_cw2_path2 = new double[pointThres_cw];

        public string light_status = "closed";

        //压差
        public static int VREF = 4096;
        public static int DP_MAX = 20000;
        public static int DP_MIN = -2000;
        public static int DP_MAX_2 = 5000;
        public static int DP_MIN_2 = -5000;
        public static int DP_MAX_3 = 9000;
        public static int DP_MIN_3 = -1000;

        public static int FT_MAX = 150;
        public static int FT_MIN = 0;
        public static double FP_MAX = 20000.0;
        public static double FP_MIN = 0.0;
        public static int AT_MAX = 85;
        public static int AT_MIN = -45;
        private List<byte> paraBuffer_dp = new List<byte>();
        public bool greaterone_dp = false;
        public bool starflag_dp = false;
        public bool starflag2_dp = false;
        public int identifer_dp;
        public string datatime_dp;
        public string datatimelabel_dp;
        public int ii_dp = 1;
        public int pointindex_dp;
        public int ft_buf;
        public int at_buf;
        public int fp_buf;
        public int dp_buf;
        public double wm_buf;
        public double wm_value;
        public double swm_buf;
        public double swm_value;
        public double gm_buf;
        public double gm_value;
        public int om_buf;
        public double om_value;
        public int bom_buf;
        public double bom_value;
        public static int TP_SMOOTH_SPAN = 16 * 90;   //压差平滑窗口长度=16s （90为一个单位秒，压差下位机每秒约发90次数据）
        public static int TP_SMOOTH_STEP = 4 * 90;    //压差平滑频率=4s
        public int tp_span_index = 0;
        public int tp_step_index = 0;
        public int[] ft_buf_win = new int[TP_SMOOTH_SPAN];
        public int[] at_buf_win = new int[TP_SMOOTH_SPAN];
        public int[] fp_buf_win = new int[TP_SMOOTH_SPAN];
        public int[] dp_buf_win = new int[TP_SMOOTH_SPAN];
        public int ft_buf_sum = 0;
        public int at_buf_sum = 0;
        public int fp_buf_sum = 0;
        public int dp_buf_sum = 0;

        public double dp_volt, ft_volt, fp_volt, at_volt;
        public double dp_value_pa, fp_value_pa, at_value_c, ft_value_c;
        public static int dp_wim_length = 80;
        public double[] dp_win = new double[dp_wim_length];
        public double dp_ave;
        public double dp_sum = 0.0;
        public int dp_count = 0;
        public bool star_slide = false;
        public double dp_display;


        public int startScroll_dp = 1;
        public static int pointThres_dp = 150;
        public string[] timearray_dp = new string[pointThres_dp];
        public double[] dparray_dp = new double[pointThres_dp];

        //网格数据处理参数（求相关速度和面积）
        //每帧数据包含2个网格的64个点的数据，大小为64*2*2=256字节，前128字节为网格A数据，后128字节为网格B数据
        public static int ANA_LENGTH = 16;   //每次读取16帧数据（64*2*2*16=4096字节数据），每秒读取8次数据，每秒读取16*8=128帧数据
        public static int XCORR_SPAN = 16 * 8 * 4;   //16 * 8 * 4=512帧,计算互相关时,A,B序列长度,根据实际计算速度调整,4S的序列长度	16 
        public static int XCORR_STEP = 16 * 8;   //每更新8*16=128帧，计算一次相关，根据实际计算速度调整，必须是16的整数倍，1秒更新一次
        //public static int MAX_LAG = 61;   //相关时，最大滞后量，根据实际气泡速度范围确定
        public static int MAX_LAG = 70; //202211-08
        public static int MIN_LAG = 10;   //相关时，最小滞后量 ，合理选择有助于减小计算量，提高计算速度   2020.1.2改
        public static int XCORR_MIN_PEAK = 10;
        //两个网格的距离
        public static double MESH_DIST = 0.146; //两个网格传感器距离，单位：米
        //网格传感器两帧数据的时间间隔
        public static double TIME_INTERVAL = 0.008192; //时间间隔，单位：秒

        //气面积计算参数
        public static int WG_VAR_A_MAX = 10;
        public static int WG_VAR_B_MAX = 7;
        public static int WG_MIN_A_AREA = WG_VAR_A_MAX * 16;
        public static int WG_MIN_B_AREA = WG_VAR_B_MAX * 16;

        public int speed_idx = 0;
        public int speed_ctr = 0;
        public double peak;
        public int[] Sa_xcorr = new int[XCORR_SPAN];
        public int[] Sb_xcorr = new int[XCORR_SPAN];
        public static double[] sa_xcorr_tmp = new double[XCORR_SPAN];
        public static double[] sb_xcorr_tmp = new double[XCORR_SPAN];
        public static double[] sa_xcorr_sm = new double[XCORR_SPAN];
        public static double[] sb_xcorr_sm = new double[XCORR_SPAN];
        public static double[] sa_xcorr_sm_area = new double[XCORR_SPAN];
        public static double[] sb_xcorr_sm_area = new double[XCORR_SPAN];

        //排序并平滑后的微分序列，可以直接做相关运算
        public static double[] sa_diff = new double[XCORR_SPAN];
        public static double[] sa_diff_area = new double[XCORR_SPAN];
        public static double[] sa_diff_sm = new double[XCORR_SPAN];
        public static double[] sa_diff_sm_area = new double[XCORR_SPAN];
        public static double[] sb_diff = new double[XCORR_SPAN];
        public static double[] corra_b = new double[2 * XCORR_SPAN - 1];

        //气泡面积计算相关变量
        public static double threshold_p = 4.0;               // 导数正门限
        public static double threshold_n = -4.0;              // 导数负门限

        //public static double threshold_p = 5.0;               // 导数正门限
        //public static double threshold_n = -5.0;              // 导数负门限2023-1-20

        //public static double threshold_p = 6.0;               // 导数正门限 202211-8
        //public static double threshold_n = -6.0;              // 导数负门限 202211-8

        public static int gas_bubble_width_max = 80;       // 气泡最大宽度
        public static int threshold_max_hl = 20;           // 气泡起始位置幅度与结束位置幅度最大差值

        public static int gas_bubble_width = 0;            //气泡宽度        
        public static int gas_bubble_num = 0;               //气泡个数 
        public static double start_data = 0.0;              //气泡起始位置数据幅度  
        public static int gas_bubble_flag = 0;              //气泡标志，0表示目前数据不是气泡，1表示目前数据是气泡 
        public static double base_value = 0.0;             //起始点基准值
        public static double slope = 0.0;                       //加权平均值
        public static double gas_area_sum;                  //气泡面积和
        public static int count_speed;					  //气泡看门狗计数

        public int idx = 0;
        public int ctr = 0;
        public int start_flag = 0;
        public static double[] Sa_Regime = new double[XCORR_SPAN];
        public static double[] Sb_Regime = new double[XCORR_SPAN];
        public static double[] Sa_Regime_se = new double[XCORR_SPAN];
        public static double[] Sb_Regime_se = new double[XCORR_SPAN];
        public static double[] sa_diff_re = new double[XCORR_SPAN];
        public static double[] sb_diff_re = new double[XCORR_SPAN];
        public static double[] sa_diff_ns = new double[XCORR_SPAN];
        public static double[] sa_diff_mse = new double[XCORR_SPAN];
        public static int uplimit = 0;
        public static int gas_num = 0;
        public string Flow_Regime;

        //气面积平滑参数
        public static int AREA_SMOOTH_SPAN = 32;   //面积平滑长度，4秒
        public static int AREA_SMOOTH_STEP = 8;   //每秒刷新一次面积值，面积值为4秒的平均
        //气速度平滑参数
        public static int SMOOTH_SPAN = 16;   //速度和面积平滑窗口长度=16s
        public static int SMOOTH_STEP = 1;    //窗口滑动频率=1s
        public int span_index = 0;
        public int step_index = 0;
        public double[] speed_win = new double[SMOOTH_SPAN];
        public double[] area_win = new double[SMOOTH_SPAN];
        public double speed_sum = 0.0;
        public double area_sum = 0.0;
        public double GasSpeed = 0.0;
        public double GasArea = 0.0;
        public bool span_end_flag = false;
        public int span_count = 0;
        public double speed_sum_count = 0.0;
        public double area_sum_count = 0.0;

        public double speed_check;
        public double area_check;
        public bool new_file = false;

        //网格图像参数
        int[] mesh_a_16 = new int[1024];
        int[] mesh_b_16 = new int[1024];//16帧网格数据（64*16），64个点依次排列（1,2,3,...,64, 65,66,...）
        int[] mesh_a_ave = new int[64];
        int[] mesh_b_ave = new int[64];//16帧平均后的64个点的网格数据（（1,2,3,...,64）
        int[] meah_a_ave_64 = new int[16];
        int[] meah_b_ave_64 = new int[16];//每帧64个点的平均数据
        int mesh_a;
        int mesh_b;

        static Bitmap image_mesh_a = new Bitmap(224, 224); //新建画布
        static Bitmap image_mesh_b = new Bitmap(224, 224); //新建画布
        Graphics ga = Graphics.FromImage(image_mesh_a);
        Graphics gb = Graphics.FromImage(image_mesh_b);

        Pen p = new Pen(Color.Transparent, 1);//定义画笔

        SolidBrush mesh_error_black = new SolidBrush(Color.Black);//数据出错-黑色
        SolidBrush mesh_gas_yellow = new SolidBrush(Color.Yellow);//气-黄色
        SolidBrush mesh_oil_gas_mix_orange = new SolidBrush(Color.Orange);//油气混合-橙色
        SolidBrush mesh_oil_red = new SolidBrush(Color.Red);//油-红色
        SolidBrush mesh_oil_water_mix1_indianred = new SolidBrush(Color.IndianRed);//油水混合-油多-印度红
        SolidBrush mesh_oil_water_mix2_purple = new SolidBrush(Color.Purple);//油水混合-油水相当-紫色
        SolidBrush mesh_oil_water_mix3_blueviolet = new SolidBrush(Color.BlueViolet);//油水混合-水多-蓝紫
        SolidBrush mesh_water_blue = new SolidBrush(Color.Blue);//水-蓝色

        SolidBrush[] mesh_a_color = new SolidBrush[64];
        SolidBrush[] mesh_b_color = new SolidBrush[64];

        public int startScroll_net = 1;
        public static int pointThres_net = 150;
        public string[] timearray_net = new string[pointThres_net];
        public double[] seccwarray_net = new double[pointThres_net];
        public double[] mincwarray_net = new double[pointThres_net];

        public int frame_count = 0;
        public bool save_data = false;
        public string file_name;
        public string ql_qg_filename;
        public string ql_qg_filepath;
        public FileStream mesh_fs;
        public StreamWriter mesh_value_sw;
        public StreamWriter cw_dp_sw;
        public StreamWriter ql_qg_sw;
        public bool create_new_file = true;
        public bool save_data_flag = false;
        public int data_count = 0;
        public string data_file_name;
        public StreamWriter data_sw;
        public int file_row_count = 0;

        public int mesh_a_sum = 0;
        public int mesh_b_sum = 0;

        public double cw_ave_save = 0.0, dp_ave_save = 0.0, speed_ave_save = 0.0, area_ave_save = 0.0;
        public double cw_sum_save = 0.0, dp_sum_save = 0.0, speed_sum_save = 0.0, area_sum_save = 0.0;
        public double ql_ave_save = 0.0, qo_ave_save = 0.0, qw_ave_save = 0.0, qg_ave_save = 0.0;
        public double ql_sum_save = 0.0, qo_sum_save = 0.0, qw_sum_save = 0.0, qg_sum_save = 0.0;

        public string speed_check_file_name;
        public StreamWriter speed_check_sw;

        public bool save_from_txt = false; //保存速度数据至txt文件的标志

        //含水曲线刷新委托
        public delegate void drawCurveDelegate_cw(string xl, double seccw_y, double mincw_y, double ql_y, double qg_y);
        drawCurveDelegate_cw drawChart_cw;

        //通道值曲线刷新委托
        public delegate void drawCurveDelegate_path(string path_xl, double paht11_y, double path12_y, double path21_y, double path22_y);
        drawCurveDelegate_path drawChart_path;

        //网格均值曲线刷新委托
        public delegate void drawCurveDelegate(string xl, int[] mesh_ay, int[] mesh_by);
        drawCurveDelegate drawChart_mesh;

        //
        //ClassIniFile inifile =new ClassIniFile();

        //------------------------------------------------------------------------------------------
        public string[] dir_file_his = Directory.GetLogicalDrives();
        public string query_dir_his;
        private void hisCw_serialdataGrid2_initizlize()
        {
            //获取电脑磁盘
            if (dir_file_his.Length >= 2)
                query_dir_his = dir_file_his[1];
            else
                query_dir_his = dir_file_his[0];
            bool query_path_exist1 = Directory.Exists(query_dir_his + "地面计量测试数据\\");
            if (query_path_exist1 == false)
            {
                Directory.CreateDirectory(query_dir_his + "地面计量测试数据\\");              //创建文件夹
            }
        }

        //inifile
        string path = System.IO.Directory.GetCurrentDirectory() + "\\coe.ini";
        string[] coe_name = new string[7] { "three_phase_coe_all", "three_phase_coe_s", "three_phase_coe_m", "three_phase_coe_l", "calc_gas_coe_s", "calc_gas_coe_m", "calc_gas_coe_l" };
        private void inifile_initizlize()
        {
            ClassIniFile.CreateFile(path);
            
            User_Sm_Time = Convert.ToDouble(ClassIniFile.ReadIniData("setting", "smoothtime", "30", path));
            textBox_sm_time.Text = ClassIniFile.ReadIniData("setting", "smoothtime", "30", path);
            comboBox_portCw.Text = ClassIniFile.ReadIniData("port", "port_Cw", " ", path);
            comboBox_portGasMeter.Text = ClassIniFile.ReadIniData("port", "port_GasMeter", " ", path);
            comboBox_portDp.Text = ClassIniFile.ReadIniData("port", "port_Dp", " ", path);

            string[] coe = new string[7];
            for (int i =0;i<7;i++)
            {
                coe[i] = ClassIniFile.ReadIniData("coefficienct", coe_name[i], "0", path);
            }
            string[] strArray0 = coe[0].Split(',');
            for (int i = 0; i < strArray0.Length; i++)
            {
                three_phase_coe_all[i] = Convert.ToDouble(strArray0[i]);
            }
            string[] strArray1 = coe[1].Split(',');
            for (int i = 0; i < strArray1.Length; i++)
            {
                three_phase_coe_s[i] = Convert.ToDouble(strArray1[i]);
            }
            string[] strArray2 = coe[2].Split(',');
            for (int i = 0; i < strArray2.Length; i++)
            {
                three_phase_coe_m[i] = Convert.ToDouble(strArray2[i]);
            }
            string[] strArray3 = coe[3].Split(',');
            for (int i = 0; i < strArray3.Length; i++)
            {
                three_phase_coe_l[i] = Convert.ToDouble(strArray3[i]);
            }
            string[] strArray4 = coe[4].Split(',');
            for (int i = 0; i < strArray4.Length; i++)
            {
                calc_gas_coe_s[i] = Convert.ToDouble(strArray4[i]);
            }
            string[] strArray5 = coe[5].Split(',');
            for (int i = 0; i < strArray5.Length; i++)
            {
                calc_gas_coe_m[i] = Convert.ToDouble(strArray5[i]);
            }
            string[] strArray6 = coe[6].Split(',');
            for (int i = 0; i < strArray6.Length; i++)
            {
                calc_gas_coe_l[i] = Convert.ToDouble(strArray6[i]);
            }
        }

        //public double[] seccw_second_buf = new double[60];
        //public double[] seccw_minute_buf = new double[60];
        //public double[] seccw_hour_buf = new double[24];
        //public double[] seccw_day_buf = new double[7];
        

        //public double[] Q_Liquid_second_buf = new double[60];
        //public double[] Q_Liquid_minute_buf = new double[60];
        //public double[] Q_Liquid_hour_buf = new double[24];
        //public double[] Q_Liquid_day_buf = new double[7];


        //public double[] Q_Gas_second_buf = new double[60];
        //public double[] Q_Gas_minute_buf = new double[60];
        //public double[] Q_Gas_hour_buf = new double[24];
        //public double[] Q_Gas_day_buf = new double[7];


        //public int sec_sm_idx = 0;
        //public int min_sm_idx = 0;
        //public int hour_sm_idx = 0;
        //public int day_sm_idx = 0;
        //public int week_sm_idx = 0;


        //public int seccw_sec_sm_idx = 0;
        //public int seccw_min_sm_idx = 0;
        //public int seccw_hour_sm_idx = 0;
        //public int seccw_day_sm_idx = 0;
        //public int seccw_week_sm_idx = 0;
        //public int seccw_user_sm_idx = 0;

        //public int Q_Liquid_sec_sm_idx = 0;
        //public int Q_Liquid_min_sm_idx = 0;
        //public int Q_Liquid_hour_sm_idx = 0;
        //public int Q_Liquid_day_sm_idx = 0;
        //public int Q_Liquid_week_sm_idx = 0;
        //public int Q_Liquid_user_sm_idx = 0;

        //public int Q_Gas_sec_sm_idx = 0;
        //public int Q_Gas_min_sm_idx = 0;
        //public int Q_Gas_hour_sm_idx = 0;
        //public int Q_Gas_day_sm_idx = 0;
        //public int Q_Gas_week_sm_idx = 0;
        //public int Q_Gas_user_sm_idx = 0;

        //public double watercut_sec = 0;     // 秒平均含水，即瞬时含水
        //public double seccw_min = 0;     // 分钟平均含水
        //public double seccw_hour = 0;        // 小时平均含水
        //public double seccw_day = 0;     // 日平均含水
        //public double seccw_week = 0;        // 周平均含水


        //public double Q_Gas_sec = 0;     // 秒平均含水，即瞬时
        //public double Q_Gas_min = 0;     // 分钟平均
        //public double Q_Gas_hour = 0;        // 小时平均
        //public double Q_Gas_day = 0;     // 日平均
        //public double Q_Gas_week = 0;        // 周平均


        //public double Q_Liquid_sec = 0;     // 秒平均，即瞬时
        //public double Q_Liquid_min = 0;     // 分钟平均
        //public double Q_Liquid_hour = 0;        // 小时平均
        //public double Q_Liquid_day = 0;     // 日平均
        //public double Q_Liquid_week = 0;        // 周平均

        public double[] seccw_user_buf = new double[120];
        public double[] Q_Liquid_user_buf = new double[120];
        public double[] Q_Gas_user_buf = new double[120];

        public int user_sm_idx = 0;

        public double seccw_user = 0;      //自定义
        public double Q_Gas_user = 0;      //自定义
        public double Q_Liquid_user = 0;      //自定义
        public double Q_Qil_user;
        public double Q_Water_user;

        public double Q_seccw_total = 0;      //自定义
        public double Q_Gas_total = 0;      //自定义
        public double Q_Liquid_total = 0;      //自定义
        public double Q_Oil_total = 0;
        public double Q_Water_total = 0;

        public double User_Sm_Time = 30;
        public double User_idx = 0;
        public int User_idx_lock = 1;

        private static double ArithmeticMean(double[] arr, double count)
        {
            double result = 0;
            for (int i = 0; i < count; i++)
            {
                result += arr[i];           //累加求和
            }
            return result / count;
        }
        /* 
        private void flow_calc()
        {
            seccw
            Q_Oil
            Q_Water
            Q_Gas
            Q_Liquid
        
        //seccw_second_buf[sec_sm_idx] = seccw;
        seccw_user_buf[user_sm_idx] = seccw;

            //Q_Gas_second_buf[sec_sm_idx] = Q_Gas;
            Q_Gas_user_buf[user_sm_idx] = Q_Gas;

            //Q_Liquid_second_buf[sec_sm_idx++] = Q_Liquid;
            Q_Liquid_user_buf[user_sm_idx++] = Q_Liquid;

            if (User_idx_lock == 1)
            {
                if (User_idx++ >= User_Sm_Time)
                {
                    User_idx = User_Sm_Time;
                    User_idx_lock = 0;
                }
            }
            //自定义平均 自定义平滑时间（秒）更新一次
            seccw_user = ArithmeticMean(seccw_user_buf, User_idx);
            Q_Gas_user = ArithmeticMean(Q_Gas_user_buf, User_idx);
            Q_Liquid_user = ArithmeticMean(Q_Liquid_user_buf, User_idx);
            if (user_sm_idx >= User_Sm_Time)
            {              
                user_sm_idx = 0;
            }
            /*
            //分钟平均 一分钟更新一次
            if (sec_sm_idx >= 60)
            {
                seccw_min = ArithmeticMean(seccw_second_buf, sec_sm_idx);
                seccw_minute_buf[min_sm_idx] = seccw_min;

                Q_Gas_min = ArithmeticMean(Q_Gas_second_buf, sec_sm_idx);
                Q_Gas_minute_buf[min_sm_idx] = seccw_min;

                Q_Liquid_min = ArithmeticMean(Q_Liquid_second_buf, sec_sm_idx);
                Q_Liquid_minute_buf[min_sm_idx++] = seccw_min;

                sec_sm_idx = 0;
            }

            //小时平均 一小时更新一次
            if (min_sm_idx >= 60)
            {
                seccw_hour = ArithmeticMean(seccw_minute_buf, min_sm_idx);
                seccw_hour_buf[hour_sm_idx] = seccw_hour;

                Q_Gas_hour = ArithmeticMean(Q_Gas_minute_buf, min_sm_idx);
                Q_Gas_hour_buf[hour_sm_idx] = seccw_hour;

                Q_Liquid_hour = ArithmeticMean(Q_Liquid_minute_buf, min_sm_idx);
                Q_Liquid_hour_buf[hour_sm_idx++] = seccw_hour;

                min_sm_idx = 0;
            }

            //天平均 一天更新一次
            if (hour_sm_idx >= 24)
            {
                seccw_day = ArithmeticMean(seccw_hour_buf, seccw_hour_sm_idx);
                seccw_day_buf[seccw_day_sm_idx++] = seccw_day;

                hour_sm_idx = 0;
            }

            //周平均 一周更新一次
            if (day_sm_idx >= 7)
            {
                seccw_week = ArithmeticMean(seccw_day_buf, day_sm_idx);
                day_sm_idx = 0;
            }
            
        }*/

        #region 显示内容切换切换
        //调试模式界面
        private void 调试界面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel_work.Visible = false;
            panel_debug.Visible = true;
            panel_coef.Visible = false;
        }

        //工作模式界面
        private void 实时数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel_work.Visible = true;
            panel_debug.Visible = false;
            panel_coef.Visible = false;
        }

        private void 系数设定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel_coef.Visible = true;
            panel_work.Visible = false;
            panel_debug.Visible = false;
        }
        //显示或者隐藏原始字节数据
        private void checkBox_show_bytedata_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_show_bytedata.Checked == true)
            {
                textBox_DpPortReceive.Visible = true;
                textBox_CwPortReceive.Visible = true;
                chart_cw_path.Visible = false;
            }
            else if (checkBox_show_bytedata.Checked == false)
            {
                textBox_DpPortReceive.Visible = false;
                textBox_CwPortReceive.Visible = false;
                chart_cw_path.Visible = true;
            }
        }

        #endregion

        #region 控制气阀门值
        //手动设置气阀门值
        private void skinButton_gasDegree_Click(object sender, EventArgs e)
        {
            string tgstr = textBox_gasDegree.Text.ToString();

            //获取设置气阀门门限值的命令字符串，获取失败则为空字符串
            string gas_order_send = get_gas_degree_str(tgstr);
            if (gas_order_send != "")
            {
                port_Cw_Write(port_GasMeter, gas_order_send);
            }
        }

        //获取设置气阀门门限值的命令字符串，获取失败则为空字符串
        private string get_gas_degree_str(string str)
        {
            string gas_order = "";
            float gfloat;
            int gint;

            if (float.TryParse(str, out gfloat))
            {
                if (gfloat >= 0.0 & gfloat <= 100.0)
                {
                    gfloat = gfloat * 10;
                    gint = (int)gfloat + 1999;  //按指定格式转换（先乘以10再加1999）
                    string gstr = gint.ToString();
                    string gbytestr = uintTOstring(gstr).Substring(6);
                    string gbytestr_1 = gbytestr.Substring(0, 2);
                    string gbytestr_2 = gbytestr.Substring(3, 2);

                    string[] str16 = { "03", "06", "00", "02", gbytestr_1, gbytestr_2 };
                    int bl = str16.Length;
                    byte[] strbyte = new byte[bl];

                    for (int i = 0; i < str16.Length; i++)
                    {
                        strbyte[i] = Convert.ToByte(str16[i], 16);
                    }
                    short code16 = CRC16(strbyte, strbyte.Length);
                    string code16str = Convert.ToString(code16, 16).ToUpper();

                    gas_order = "03 06 00 02" + " " + gbytestr_1 + " " + gbytestr_2 + " " + code16str.Substring(0, 2) + " " + code16str.Substring(2, 2);
                }
                else
                {
                    MessageBox.Show("设置失败！请输入0-100内的数值");
                }
            }
            else
            {
                MessageBox.Show("设置失败！请输入0-100内的数值");
            }
            return gas_order;
        }

        #endregion

        #region 网格USB,读取网格数据并显示
        //（事件）打开网格USB
        private void skinButton_startUSB_Click(object sender, EventArgs e)
        {            
            set_mesh_chart();//网格均值图属性设置
            drawChart_mesh = new drawCurveDelegate(drawcurve_mesh);//委托

            //USB
            usbDevices = new USBDeviceList(CyConst.DEVICES_CYUSB);
            //MessageBox.Show(usbDevices.Count.ToString());
            //MessageBox.Show(usbDevices[0].FriendlyName);
            MyDevice = usbDevices[0] as CyUSBDevice;
            if (MyDevice != null)
            {
                CtrlEndPt = MyDevice.ControlEndPt;
                //inEndPt = MyDevice.BulkInEndPt;
                inEndPt = MyDevice.EndPointOf(0x82) as CyBulkEndPoint;
            }

            usb_exist = usb_start_or_stop(0x01); //0x01，打开usb命令
            usb_status = true;
            label_mesh_flag.BackColor = Color.Red;   //红色指示

            frame_count = 0;
            if (tXfers == null)
            {
                tXfers = new Thread(new ThreadStart(TransfersThread_Mesh));
                tXfers.IsBackground = true;
                tXfers.Start();
            }
        }

        //(方法)读取网格数据线程
        public void TransfersThread_Mesh()
        {
            int xferLen = XFERSIZE;
            bool bResult = false;

            // Loop stops if either an IN or OUT transfer fails
            while (usb_exist)
            {
                while (usb_status)
                {
                    xferLen = XFERSIZE;
                    outCount += xferLen;

                    //calls the XferData function for bulk transfer(OUT/IN) in the cyusb.dll
                    try
                    {
                        bResult = inEndPt.XferData(ref inData, ref xferLen);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "USB设备异常断开", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        usb_status = false;
                    }

                    if (bResult)
                    {
                        //保存数据---------------------------------------------------------------------
                        if (save_data == true)
                        {
                            if (frame_count == 0)
                            {
                                file_name = textBox_filename.Text;
                                string mesh_bufferPath = query_dir_his + "地面计量测试数据\\" + file_name + ".mfd";
                                string mesh_valuePath = query_dir_his + "地面计量测试数据\\" + file_name + "_网格均值.txt";
                                string cw_dp_txtPath = query_dir_his + "地面计量测试数据\\" + file_name + ".txt";
                                mesh_fs = new FileStream(mesh_bufferPath, FileMode.Create);   //原始字节网格数据
                                mesh_value_sw = new StreamWriter(mesh_valuePath, false, Encoding.Default);//均值txt网格数据
                                cw_dp_sw = new StreamWriter(cw_dp_txtPath, false, Encoding.Default);   //含水-压差-气速度-气面积数据
                            }
                            //保存网格数据
                            mesh_fs.Write(inData, 0, inData.Count());
                            mesh_fs.Flush();

                            //保存含水压差数据
                            //string cw_dp_str = seccw.ToString("f4").PadLeft(8) + "    " + dp_value_pa.ToString("f4").PadLeft(9) + "    " + GasSpeed.ToString("f4").PadLeft(6) + "    " + GasArea.ToString("f4").PadLeft(10) + "    " + Q_Liquid.ToString("f4").PadLeft(8)；
                            string cw_dp_str = seccw.ToString("f4").PadLeft(8) + "    " + dp_value_pa.ToString("f4").PadLeft(9) + "    " + GasSpeed.ToString("f4").PadLeft(6) + "    " + GasArea.ToString("f4").PadLeft(10) + "    " + Q_Liquid.ToString("f4").PadLeft(8) + "    " + Q_Gas.ToString("f4").PadLeft(8);
                            cw_dp_sw.WriteLine(cw_dp_str);
                            cw_dp_sw.Flush();

                            frame_count = frame_count + 1; //帧计数

                            Invoke(new MethodInvoker(delegate
                            {
                                textBox_frame_count.Text = (frame_count * 16).ToString();
                            }));

                            if (frame_count * 16 == int.Parse(textBox_frame_num.Text))
                            {
                                //保存的固定长度的网格均值数据的平均值
                                int mesh_a_ave = mesh_a_sum / (frame_count - 1);
                                int mesh_b_ave = mesh_b_sum / (frame_count - 1);
                                string mesh_value_str = frame_count.ToString().PadLeft(4) + "    " + mesh_a_ave.ToString() + "    " + mesh_b_ave.ToString();
                                mesh_value_sw.WriteLine(mesh_value_str);
                                mesh_value_sw.Flush();

                                mesh_fs.Close();
                                mesh_value_sw.Close();
                                cw_dp_sw.Close();
                                frame_count = 0;
                                mesh_a_sum = 0;
                                mesh_b_sum = 0;
                                save_data = false;
                                BeginInvoke(new MethodInvoker(delegate
                                {
                                    checkBox1.Checked = false;
                                }));
                            }
                        }
                        //-------------------------------------------------------------------------------

                        //按指定格式对原始数据进行处理
                        for (int i = 0; i < 4096; i = i + 128)
                        {
                            for (int k = 1; k < 6; k = k + 2)
                            {
                                //inData[i + k] = (byte)(inData[i + k] << 4);
                                //inData[i + k] = (byte)(inData[i + k] >> 4);
                                inData[i + k] = (byte)(inData[i + k] & 0x0f);
                            }
                        }

                        int ia = 0;
                        int ib = 0;
                        //数据格式：网格B的1-64个点数据 + 网格A的1-64个点数据 + 网格B的1-64个点数据，依次排列，以此类推
                        //数据格式：网格A和B数据不能对应错，否则后面计算速度和面积将会出错
                        //数据格式：以128字节为一个长度单位（1个点数据占2个字节，64个点数据占128个字节）
                        //第1、3、5、7...个128字节数据为网格A数据
                        for (int j = 1; j < 32; j = j + 2)
                        {
                            for (int i = 1; i < 128; i = i + 2)
                            {
                                mesh_a_16[ia] = (inData[j * 128 + (i - 1)] + inData[j * 128 + i] * 256);   //16帧网格A数据，共长64*16=1024（一列）
                                ia = ia + 1;
                            }
                        }

                        //第0、2、4、6..个128字节数据为网格B数据
                        for (int j = 0; j < 32; j = j + 2)
                        {
                            for (int i = 1; i < 128; i = i + 2)
                            {
                                mesh_b_16[ib] = (inData[j * 128 + (i - 1)] + inData[j * 128 + i] * 256);   //16帧网格B数据，共长64*16=1024（一列）
                                ib = ib + 1;
                            }
                        }

                        //将每帧数据的64个点平均为一个点，变为1*16
                        for (int k = 0; k < 16; k++)
                        {
                            int sum_a_64 = 0;
                            int sum_b_64 = 0;
                            for (int i = 0; i < 64; i++)
                            {
                                sum_a_64 = sum_a_64 + mesh_a_16[k * 64 + i];
                                sum_b_64 = sum_b_64 + mesh_b_16[k * 64 + i];
                            }
                            meah_a_ave_64[k] = (int)(sum_a_64 / 64.0);
                            meah_b_ave_64[k] = (int)(sum_b_64 / 64.0);
                        }

                        //计算-------------------------------------------------------------------------------
                        Gas_Liquid_Init();   //气面积计算参数初始化
                        Flow_Regime_Identify(meah_a_ave_64, meah_b_ave_64);   //获得滑动窗口数据，流型识别
                        Gas_Speed_Area_Calc();   //计算气速度和气面积
                        gas_liquid_calc();   //计算液量、油量和水量
                        //-----------------------------------------------------------------------------------

                        //将16帧数据平均，网格A、B分别变成64个点的数据
                        int sum_a_all = 0;
                        int sum_b_all = 0;
                        for (int k = 0; k < 64; k++)
                        {
                            int sum_a = 0;
                            int sum_b = 0;
                            for (int i = k; i < 1024; i = i + 64)
                            {
                                sum_a = sum_a + mesh_a_16[i];
                                sum_b = sum_b + mesh_b_16[i];
                            }
                            mesh_a_ave[k] = (int)(sum_a / 16.0);
                            mesh_b_ave[k] = (int)(sum_b / 16.0);
                            sum_a_all = sum_a_all + mesh_a_ave[k];
                            sum_b_all = sum_b_all + mesh_b_ave[k];
                        }

                        //将64个点数据平均，网格A、B的64个点的均值
                        mesh_a = (int)(sum_a_all / 64.0);
                        mesh_b = (int)(sum_b_all / 64.0);
                        string xlabel_str = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss").Substring(11);

                        //保存均值网格数据（txt格式）
                        if (save_data == true)
                        {
                            mesh_a_sum = mesh_a_sum + mesh_a;
                            mesh_b_sum = mesh_b_sum + mesh_b;

                            string mesh_value_str = frame_count.ToString().PadLeft(4) + "    " + mesh_a.ToString() + "    " + mesh_b.ToString();
                            mesh_value_sw.WriteLine(mesh_value_str);
                            mesh_value_sw.Flush();
                        }

                        BeginInvoke(drawChart_mesh, new object[] { xlabel_str, meah_a_ave_64, meah_b_ave_64 });//刷新网格均值曲线
                        method_refresh_mesh_color(mesh_a_ave, mesh_b_ave);//刷新网格图像
                    }
                }
                Thread.Sleep(100);
            }
        }

        //保存数据开关
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                save_data = true;
                mesh_a_sum = 0;
                mesh_b_sum = 0;
                frame_count = 0;
            }
            else if (checkBox1.Checked == false)
            {
                if (mesh_fs != null & cw_dp_sw != null & mesh_value_sw != null)
                {
                    mesh_fs.Close();
                    cw_dp_sw.Close();
                    mesh_value_sw.Close();
                }
                save_data = false;
            }
        }

        //（事件）停止网格USB
        private void skinButton_stopUSB_Click(object sender, EventArgs e)
        {
            usb_exist = usb_start_or_stop(0x00);//0x00，停止usb命令
            usb_status = false;
            label_mesh_flag.BackColor = Color.Black;
        }

        /// <summary>
        /// (方法)开始或者停止USB； order=0x01,表示开始；order=0x00,表示结束
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private bool usb_start_or_stop(ushort order)
        {
            bool write_bool;
            if (inEndPt != null)
            {
                CtrlEndPt.Target = CyConst.TGT_DEVICE;
                CtrlEndPt.ReqType = CyConst.REQ_VENDOR;
                CtrlEndPt.Direction = CyConst.DIR_TO_DEVICE;
                CtrlEndPt.ReqCode = 0xB0; // Some vendor-specific request code
                CtrlEndPt.Value = order;   //0x01 开始；0x00 停止；0x03 硬件配置模式
                CtrlEndPt.Index = order;
                int len = 0;
                byte[] buf = new byte[2];
                try
                {
                    write_bool = CtrlEndPt.Write(ref buf, ref len);
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message, "USB设备异常断开1", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        //（方法）刷新网格图像
        private void method_refresh_mesh_color(int[] mesh_a_value, int[] mesh_b_value)
        {
            //g.DrawLine(p, 20, 20, 100, 200);//在画板上画直线,起始坐标为(10,10),终点坐标为(100,100)
            //g.DrawEllipse(p, 10, 10, 100, 100);//在画板上画椭圆,起始坐标为(10,10),外接矩形的宽为100,高为100
            //g.DrawRectangle(p, 0, 0, 30, 30);//在画板上画矩形,起始坐标为(10,10),宽为100,高为100
            //HatchBrush hBrush_mesh_oil= new HatchBrush(HatchStyle., Color.White, Color.Red);

            //网格颜色分配
            mesh_a_color = mesh_color_set(mesh_a_value);
            mesh_b_color = mesh_color_set(mesh_b_value);

            //网格颜色对应
            for (int i = 7; i > -1; i--)
            {
                for (int k = 0; k < 8; k++)
                {
                    int col_row = 63 - (i * 8 + (7 - k));
                    ga.FillRectangle(mesh_a_color[col_row], k * 28, i * 28, 28, 28);
                    gb.FillRectangle(mesh_b_color[col_row], k * 28, i * 28, 28, 28);
                }
            }
            pictureBox1_upMesh.Image = image_mesh_b;
            pictureBox2_downMesh.Image = image_mesh_a;
        }

        int mesh_error = 1;
        //(方法)颜色分配
        private SolidBrush[] mesh_color_set(int[] mesh_value)
        {
            SolidBrush[] mesh_color = new SolidBrush[64];
            //网格颜色分配
            for (int mi = 0; mi < 64; mi++)
            {
                //网格A 颜色分配
                if (mesh_value[mi] < 2538)
                {
                    mesh_color[mi] = mesh_gas_yellow;
                }
                else if (mesh_value[mi] >= 2538 & mesh_value[mi] < 2638)
                {
                    mesh_color[mi] = mesh_oil_gas_mix_orange;
                }
                else if (mesh_value[mi] >= 2638 & mesh_value[mi] < 2808)
                {
                    mesh_color[mi] = mesh_oil_red;
                }
                else if (mesh_value[mi] >= 2808 & mesh_value[mi] < 2978)
                {
                    mesh_color[mi] = mesh_oil_water_mix1_indianred;
                }
                else if (mesh_value[mi] >= 2978 & mesh_value[mi] < 3148)
                {
                    mesh_color[mi] = mesh_oil_water_mix2_purple;
                }
                else if (mesh_value[mi] >= 3148 & mesh_value[mi] < 3319)
                {
                    mesh_color[mi] = mesh_oil_water_mix3_blueviolet;
                }
                else if (mesh_value[mi] >= 3319 & mesh_value[mi] < 3800)
                {
                    mesh_color[mi] = mesh_water_blue;
                }
                else if (mesh_value[mi] >= 3800)
                {
                    //网格重启
                    mesh_color[mi] = mesh_error_black;
                    mesh_error = 1;
                    /*
                    if (mesh_flag == 1)
                    {
                        skinButton_stopUSB.PerformClick();
                        mesh_flag = 0;
                        timer_mesh_restart.Start();
                    }
                    */
                }
            }
            return mesh_color;
        }

        //(方法)网格均值图
        public void drawcurve_mesh(string xlabel, int[] ysecValue, int[] yminValue)
        {
            //X序列
            for (int j = 0; j < pointThres_net - 16; j++)
            {
                timearray_net[j] = timearray_net[j + 1];
            }
            for (int j = pointThres_net - 16; j < pointThres_net; j++)
            {
                timearray_net[j] = xlabel;
            }

            //Y序列
            for (int j = 0; j < pointThres_net - 16; j++)
            {
                seccwarray_net[j] = seccwarray_net[j + 1];
                mincwarray_net[j] = mincwarray_net[j + 1];
            }
            for (int j = pointThres_net - 16; j < pointThres_net; j++)
            {
                int yi = 16 - (150 - j);
                seccwarray_net[j] = ysecValue[yi];
                mincwarray_net[j] = yminValue[yi];
            }

            chart1.Series["mesh_a"].Points.DataBindXY(timearray_net, seccwarray_net);
            chart1.Series["mesh_b"].Points.DataBindXY(timearray_net, mincwarray_net);

            if (startScroll_net < 3)
            {
                chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.Scroll(ScrollType.Last);
                startScroll_net = startScroll_net + 1;
            }
        }

        //(事件)设置网格均值图Y轴显示范围
        private void skinButton_set_mesh_chart_Y_Click(object sender, EventArgs e)
        {
            chart1.ChartAreas["ChartArea1"].AxisY.Maximum = int.Parse(textBox_mesh_chart_max.Text);
            chart1.ChartAreas["ChartArea1"].AxisY.Minimum = int.Parse(textBox_mesh_chart_min.Text);
            chart1.ChartAreas["ChartArea1"].AxisY.Interval = int.Parse(textBox_Y_interval.Text);
        }

        //(方法)网格均值chart属性设置
        private void set_mesh_chart()
        {
            chart1.ChartAreas["ChartArea1"].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.None;
            //设置适应全部数据点
            chart1.ChartAreas["ChartArea1"].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
            //设置当前X轴Label的双行显示格式 = 关闭
            chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.IsStaggered = false;
            //设置X轴不从0开始
            chart1.ChartAreas["ChartArea1"].AxisX.IsStartedFromZero = false;
            //chart1.ChartAreas["ChartArea1"].AxisY.IsStartedFromZero = false;

            chart1.ChartAreas["ChartArea1"].AxisX.ScrollBar.Enabled = true;
            chart1.ChartAreas["ChartArea1"].AxisX.ScrollBar.IsPositionedInside = true;//设置滚动条是在外部显示
            chart1.ChartAreas["ChartArea1"].AxisX.ScrollBar.Size = 10;//设置滚动条的宽度
            chart1.ChartAreas["ChartArea1"].AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.SmallScroll;//滚动条只显示向前的按钮，主要是为了不显示取消显示的按钮

            chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.Size = 150;//设置图表可视区域数据点数，即一次可以看到多少个X轴区域
            chart1.ChartAreas["ChartArea1"].AxisX.Interval = 30;
            chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.Scroll(ScrollType.Last);
        }

        #endregion

        #region 红外含水、压差端口，读取红外含水、压差数据并显示
        //（事件）查找红外含水、压差端口
        private void skinButton_findPort_Click(object sender, EventArgs e)
        {
            string[] port_findornot = SerialPort.GetPortNames();//查找可用串口
            string portstr = string.Join(",", port_findornot);  //字符串数组转字符串
            int port_count = port_findornot.Length;
            string[] port_list1 = new string[port_count];
            string[] port_list2 = new string[port_count];
            string[] port_list3 = new string[port_count];
            string[] port_list4 = new string[port_count];
            string[] port_list5 = new string[port_count];
            port_findornot.CopyTo(port_list1, 0);
            port_findornot.CopyTo(port_list2, 0);
            port_findornot.CopyTo(port_list3, 0);
            port_findornot.CopyTo(port_list4, 0);
            port_findornot.CopyTo(port_list5, 0);

            if (port_findornot.Length > 0)
            {
                port_exist = true;
                //textBox3_statearea.Text = "查找到" + port_count.ToString() + "个可用串口" + portstr;
                //comboBox_portCw.Text = findornot[0];
                //comboBox_portPre.Text = findornot[0];
                comboBox_portCw.DataSource = port_list1;
                //comboBox_portCw2.DataSource = port_list2;
                comboBox_portDp.DataSource = port_list3;
                comboBox_portGasMeter.DataSource = port_list2;
                comboBox_portOilMeter.DataSource = port_list4;
                comboBox_portBigOilMeter.DataSource = port_list5;

                MessageBox.Show("查找到" + port_count.ToString() + "个可用端口" + portstr + ", 请按需从中选择对应端口！");
            }
            else
            {
                port_exist = false;
                //comboBox_portCw.DataSource = port_list1;
                //comboBox_portCw2.DataSource = port_list2;
                //comboBox_portDp.DataSource = port_list3;
                comboBox_portCw.Text = "";
                comboBox_portCw2.Text = "";
                comboBox_portDp.Text = "";
                MessageBox.Show("未查找到可用端口，请检查设备后重试！");
            }
        }

        //（事件）打开红外含水、压差端口
        private void skinButton_measure_Click(object sender, EventArgs e)
        {
            set_cw_chart();   //含水曲线chart属性设置
            drawChart_cw = new drawCurveDelegate_cw(drawcurve_cw);   //含水曲线刷新委托
            drawChart_path = new drawCurveDelegate_path(drawcurve_ptah);   //通道值图刷新委托

            open_port();   //打开端口
        }

        //（方法）打开红外含水、压差端口
        private void open_port()
        {
            port_error_message1 = true;
            port_error_message2 = true;

            open_GasMeterPort();
//            open_OilMeterPort();
//            open_BigOilMeterPort();

            //配置红外含水端口，并打开
            if (port_Cw.IsOpen == false)
            {
                if (comboBox_portCw.Text != "")
                {
                    port_Cw.PortName = comboBox_portCw.Text;   //端口名
                    ClassIniFile.WriteIniData("port", "port_Cw", port_Cw.PortName, path);

                    port_Cw.BaudRate = 115200;   //选择波特率
                    port_Cw.Parity = Parity.None;    //选择奇偶校验位
                    port_Cw.DataBits = 8;   //选择数据位
                    port_Cw.StopBits = StopBits.One;   //选择停止位
                    port_Cw.Encoding = Encoding.Default;   //选择编码方式

                    try
                    {
                        //打开红外含水端口
                        port_Cw.Open();
                        port_Cw.DiscardInBuffer();
                        port_Cw.DiscardOutBuffer();
                        port_Cw_status = true;   //指示红外含水端口为打开状态
                        label_cw1_flag.BackColor = Color.Red;   //红色指示
                        port_Cw.DataReceived += new SerialDataReceivedEventHandler(Com_Cw_DataReceived);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "未能成功打开红外含水端口", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else if (comboBox_portCw.Text == "")
                {
                    MessageBox.Show("未选择红外含水端口，请选择后重新打开");
                }
            }
            else
            {
                MessageBox.Show("红外含水端口已打开，重复打开无效！");
            }
            /*
            //配置红外含水端口2，并打开
            if (false)//(port_Cw2.IsOpen == false)
            {
                if (comboBox_portCw2.Text != "")
                {
                    if (comboBox_portCw2.Text != comboBox_portCw.Text)
                    {
                        port_Cw2.PortName = comboBox_portCw2.Text;   //端口名

                        port_Cw2.BaudRate = 115200;   //选择波特率
                        port_Cw2.Parity = Parity.None;    //选择奇偶校验位
                        port_Cw2.DataBits = 8;   //选择数据位
                        port_Cw2.StopBits = StopBits.One;   //选择停止位
                        port_Cw2.Encoding = Encoding.Default;   //选择编码方式

                        try
                        {
                            //打开红外含水端口
                            port_Cw2.Open();
                            port_Cw2.DiscardInBuffer();
                            port_Cw2.DiscardOutBuffer();
                            port_Cw2_status = true;   //指示红外含水端口为打开状态
                            label_cw2_flag.BackColor = Color.Red;
                            port_Cw2.DataReceived += new SerialDataReceivedEventHandler(Com_Cw2_DataReceived);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "未能成功打开红外含水端口2", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else if (comboBox_portCw2.Text == comboBox_portCw.Text)
                    {
                        MessageBox.Show("红外含水端口2与红外含水端口1重复，端口2打开无效！");
                        comboBox_portCw2.Text = "";
                    }
                }
                else if (comboBox_portCw2.Text == "")
                {
                    //MessageBox.Show("未选择红外含水端口2，请选择后重新打开");
                }
                
            }
            else
            {
                MessageBox.Show("红外含水端口已打开，重复打开无效！");
            }
            */
            //配置压差变送端口，并打开
            if (port_Dp.IsOpen == false)
            {
                if (comboBox_portDp.Text != "")
                {
                    if (comboBox_portDp.Text != comboBox_portCw.Text & comboBox_portDp.Text != comboBox_portCw2.Text)
                    {
                        port_Dp.PortName = comboBox_portDp.Text;   //端口名
                        ClassIniFile.WriteIniData("port", "port_Dp", port_Dp.PortName, path);

                        port_Dp.BaudRate = 115200;   //选择波特率
                        port_Dp.Parity = Parity.None;    //选择奇偶校验位
                        port_Dp.DataBits = 8;   //选择数据位
                        port_Dp.StopBits = StopBits.One;   //选择停止位
                        port_Dp.Encoding = Encoding.Default;   //选择编码方式

                        try
                        {
                            //打开压差变送端口
                            port_Dp.Open();
                            port_Dp.DiscardInBuffer();
                            port_Dp.DiscardOutBuffer();
                            port_Dp_status = true;   //指示压差变送端口为打开状态
                            label_dp_flag.BackColor = Color.Red;
                            port_Dp.DataReceived += new SerialDataReceivedEventHandler(Com_Dp_DataReceived);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "未能成功打开压差变送端口", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else if (comboBox_portDp.Text == comboBox_portCw.Text | comboBox_portDp.Text == comboBox_portCw2.Text)
                    {
                        MessageBox.Show("压差端口与红外含水端口2或者红外含水端口1重复，压差端口打开无效！");
                        comboBox_portDp.Text = "";
                    }
                }
                else if (comboBox_portDp.Text == "")
                {
                    MessageBox.Show("未选择压差变送端口，请选择后重新打开");
                }
            }
            else
            {
                MessageBox.Show("压差变送端口已打开，重复打开无效！");
            }
        }

        //（方法）关闭红外含水、压差端口
        private void close_port()
        {
            //关闭红外含水端口1
            if (port_Cw.PortName == comboBox_portCw.Text)
            {
                if (port_Cw.IsOpen == true)
                {
                    try
                    {
                        port_Cw.Close();
                        port_Cw_status = false;
                        label_cw1_flag.BackColor = Color.Black;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "红外含水端口关闭错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else if (port_Cw.PortName != comboBox_portCw.Text)
            {
                MessageBox.Show("红外含水端口关闭失败");
            }

            //关闭红外含水端口2
            if (port_Cw2.PortName == comboBox_portCw2.Text)
            {
                if (port_Cw2.IsOpen == true)
                {
                    try
                    {
                        port_Cw2.Close();
                        port_Cw2_status = false;
                        label_cw2_flag.BackColor = Color.Black;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "红外含水端口关闭错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else if (port_Cw2.PortName != comboBox_portCw2.Text)
            {
                MessageBox.Show("红外含水端口2关闭失败");
            }

            //关闭压差变送端口
            if (port_Dp.PortName == comboBox_portDp.Text)
            {
                if (port_Dp.IsOpen == true)
                {
                    try
                    {
                        port_Dp.Close();
                        port_Dp_status = false;
                        label_dp_flag.BackColor = Color.Black;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "压差变送端口关闭错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else if (port_Dp.PortName != comboBox_portDp.Text)
            {
                MessageBox.Show("压差变送端口关闭失败");
            }

        }

        //（事件）关闭红外含水、压差端口
        private void skinButton_closePort_Click(object sender, EventArgs e)
        {
            close_port();  //关闭压差端口
        }

        //打开或者关闭红外含水率计灯泡
        private void skinButton_openLight_Click(object sender, EventArgs e)
        {
            if (light_status == "closed")
            {
                port_Cw_Write(port_Cw, "53 AA 01 BB 0D 0A");
                port_Cw_Write(port_Cw2, "53 AA 01 BB 0D 0A");
                light_status = "opened";
                skinButton_openLight.Text = "关闭灯泡";
            }
            else if (light_status == "opened")
            {
                port_Cw_Write(port_Cw, "53 AA 00 BB 0D 0A");
                port_Cw_Write(port_Cw2, "53 AA 00 BB 0D 0A");
                light_status = "closed";
                skinButton_openLight.Text = "打开灯泡";
            }
        }

        //保存通道值数据开关
        private void checkBox_save_path_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_save_path.Checked == true)
            {
                save_path_flag = true;
                path_count = 0;
            }
            else if (checkBox_save_path.Checked == false)
            {
                if (sw_path != null)
                {
                    sw_path.Close();
                }
                save_path_flag = false;
            }
        }

        //(方法)红外含水端口 通信，接收数据(含水下位机发送数据频率：每秒1条)
        public void Com_Cw_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            System.IO.Ports.SerialPort sp_cw = sender as System.IO.Ports.SerialPort;

            if (sp_cw != null)
            {
                // 临时缓冲区将保存串口缓冲区的所有数据
                int bytesToRead_cw = sp_cw.BytesToRead;
                byte[] tempBuffer_cw = new byte[bytesToRead_cw];

                // 将缓冲区所有字节读取出来
                sp_cw.Read(tempBuffer_cw, 0, bytesToRead_cw);

                //获取实时含水数据，根据字节头52 50判断（若一次只发了一个字节52，不作判断，直接舍弃掉该条数据，对结果的影响可忽略不计）
                if (bytesToRead_cw > 1)
                {
                    if (tempBuffer_cw[0].ToString("X2") == "52" & tempBuffer_cw[1].ToString("X2") == "50")
                    {
                        greaterone_cw = true;
                        starflag2_cw = true;
                    }
                    else if (tempBuffer_cw[0].ToString("X2") == "54" & tempBuffer_cw[1].ToString("X2") == "50")
                    {
                        if (port_error_message1 == true)   //以下弹出对话框仅提示一次
                        {
                            MessageBox.Show("含水和压差端口对应错误！ " + sp_cw.PortName + "为压差端口，请关闭端口并重新选择后打开！");
                            port_error_message1 = false;
                        }
                    }
                }

                if (greaterone_cw == true & starflag2_cw == true)
                {
                    paraBuffer_cw.AddRange(tempBuffer_cw);
                    //保存数据
                    if (paraBuffer_cw.Count == 19 | paraBuffer_cw.Count > 19)
                    {
                        if (paraBuffer_cw[0].ToString("X2") == "52" & paraBuffer_cw[1].ToString("X2") == "50" & paraBuffer_cw[18].ToString("X2") == "0D")
                        {
                            datatime_cw = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");   //时间格式为 2018 / 07 / 01 12:12:12
                            datatimelabel_cw = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss").Substring(11);   //时间标签，格式为 12:12:12

                            pathfir = paraBuffer_cw[10] * 256 + paraBuffer_cw[11];   //通道1
                            pathsec = paraBuffer_cw[12] * 256 + paraBuffer_cw[13];   //通道2
                            paththr = paraBuffer_cw[14] * 256 + paraBuffer_cw[15];   //通道3
                            pathfor = paraBuffer_cw[16] * 256 + paraBuffer_cw[17];   //通道4

                            double temp = (paraBuffer_cw[2] * 256 + paraBuffer_cw[3]) / 100.0;   //瞬时含水
                            if (temp > 10)
                            {
                                seccw = temp;
                            }
                            mincw = (paraBuffer_cw[4] * 256 + paraBuffer_cw[5]) / 100.0;   //平均含水
                            dpre_cw = (paraBuffer_cw[6] * 256 + paraBuffer_cw[7]) / 10.0;   //压力

                            ////20190729修正
                            //if (seccw < 55)
                            //{
                            //    seccw = seccw + 10;
                            //}
                            //else if (seccw >= 55 & seccw <= 70)
                            //{
                            //    seccw = seccw + 5;
                            //}
                            //else if (seccw > 70 & seccw <= 80)
                            //{
                            //    seccw = seccw + 3;
                            //}
                            //else if (seccw > 80)
                            //{
                            //    seccw = seccw - 4;
                            //}

                            identifer_cw = paraBuffer_cw[8] * 256 + paraBuffer_cw[9];   //含水仪器编号
                            pointindex_cw = ii_cw;   //序列号

                            ii_cw = ii_cw + 1;
                            if (ii_cw == 1001)
                            {
                                ii_cw = 1;
                            }

                            string x_path_time = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss").Substring(11);
                            BeginInvoke(drawChart_path, new object[] { x_path_time, pathsec, pathfir, pathsec2, pathfir2 });

                            string str_path = (path_count + 1).ToString().PadLeft(6) + pathfir.ToString().PadLeft(10) + pathsec.ToString().PadLeft(10) + pathfir2.ToString().PadLeft(10) + pathsec2.ToString().PadLeft(10);


                            if (save_path_flag == true)
                            {
                                if (path_count == 0)
                                {
                                    filename_path = textBox_filename.Text + "_path.txt";
                                    filepath_path = query_dir_his + "地面计量测试数据\\" + filename_path;
                                    sw_path = new StreamWriter(filepath_path, false, Encoding.Default);
                                }
                                sw_path.WriteLine(str_path);
                                sw_path.Flush();
                                path_count = path_count + 1;

                                Invoke(new MethodInvoker(delegate
                                {
                                    textBox_path_count.Text = path_count.ToString();
                                }));

                                if (path_count == int.Parse(textBox_path_num.Text))
                                {
                                    sw_path.Close();
                                    path_count = 0;
                                    save_path_flag = false;
                                    BeginInvoke(new MethodInvoker(delegate
                                    {
                                        checkBox_save_path.Checked = false;
                                    }));
                                }
                            }

                            greaterone_cw = false;
                            starflag2_cw = false;
                            paraBuffer_cw.Clear();
                        }
                    }
                }

                //实现数据的解码与显示
                rece_i_cw = rece_i_cw + 1;
                if (bytesToRead_cw != 0)
                {
                    receSuccess_cw = true;
                    string file_name = query_dir_his + "地面计量测试数据\\" + textBox_filename.Text + "_CW1.txt";
                    AddData(tempBuffer_cw, file_name, textBox_CwPortReceive, rece_i_cw);
                }
                else
                {
                    receSuccess_cw = false;
                }
            }
        }

        //(方法)红外含水端口2 通信，接收数据(含水下位机发送数据频率：每秒1条)
        public void Com_Cw2_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            System.IO.Ports.SerialPort sp_cw2 = sender as System.IO.Ports.SerialPort;

            if (sp_cw2 != null)
            {
                // 临时缓冲区将保存串口缓冲区的所有数据
                int bytesToRead_cw2 = sp_cw2.BytesToRead;
                byte[] tempBuffer_cw2 = new byte[bytesToRead_cw2];

                // 将缓冲区所有字节读取出来
                sp_cw2.Read(tempBuffer_cw2, 0, bytesToRead_cw2);

                //获取实时含水数据，根据字节头52 50判断（若一次只发了一个字节52，不作判断，直接舍弃掉该条数据，对结果的影响可忽略不计）
                if (bytesToRead_cw2 > 1)
                {
                    if (tempBuffer_cw2[0].ToString("X2") == "52" & tempBuffer_cw2[1].ToString("X2") == "50")
                    {
                        greaterone_cw2 = true;
                        starflag2_cw2 = true;
                    }
                    else if (tempBuffer_cw2[0].ToString("X2") == "54" & tempBuffer_cw2[1].ToString("X2") == "50")
                    {
                        if (port_error_message2 == true)   //以下弹出对话框仅提示一次
                        {
                            MessageBox.Show("含水和压差端口对应错误！ " + sp_cw2.PortName + "为压差端口，请关闭端口并重新选择后打开！");
                            port_error_message2 = false;
                        }
                    }
                }

                if (greaterone_cw2 == true & starflag2_cw2 == true)
                {
                    paraBuffer_cw2.AddRange(tempBuffer_cw2);
                    //保存数据
                    if (paraBuffer_cw2.Count == 19 | paraBuffer_cw2.Count > 19)
                    {
                        if (paraBuffer_cw2[0].ToString("X2") == "52" & paraBuffer_cw2[1].ToString("X2") == "50")
                        {

                            datatime_cw2 = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");   //时间格式为 2018 / 07 / 01 12:12:12
                            datatimelabel_cw2 = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss").Substring(11);   //时间标签，格式为 12:12:12

                            pathfir2 = paraBuffer_cw2[10] * 256 + paraBuffer_cw2[11];   //通道1
                            pathsec2 = paraBuffer_cw2[12] * 256 + paraBuffer_cw2[13];   //通道2
                            paththr2 = paraBuffer_cw2[14] * 256 + paraBuffer_cw2[15];   //通道3
                            pathfor2 = paraBuffer_cw2[16] * 256 + paraBuffer_cw2[17];   //通道4

                            seccw2 = (paraBuffer_cw2[2] * 256 + paraBuffer_cw2[3]) / 100.0;   //瞬时含水
                            mincw2 = (paraBuffer_cw2[4] * 256 + paraBuffer_cw2[5]) / 100.0;   //平均含水
                            dpre_cw2 = (paraBuffer_cw2[6] * 256 + paraBuffer_cw2[7]) / 10.0;   //压力

                            identifer_cw2 = paraBuffer_cw2[8] * 256 + paraBuffer_cw2[9];   //含水仪器编号
                            pointindex_cw2 = ii_cw2;   //序列号

                            ii_cw2 = ii_cw2 + 1;
                            if (ii_cw2 == 1001)
                            {
                                ii_cw2 = 1;
                            }

                            greaterone_cw2 = false;
                            starflag2_cw2 = false;
                            paraBuffer_cw2.Clear();
                        }
                    }
                }

                //实现数据的解码与显示
                rece_i_cw2 = rece_i_cw2 + 1;
                if (bytesToRead_cw2 != 0)
                {
                    receSuccess_cw2 = true;
                    string file_name = query_dir_his + "地面计量测试数据\\" + textBox_filename.Text + "_CW2.txt";
                    AddData(tempBuffer_cw2, file_name, textBox_CwPortReceive, rece_i_cw2);
                }
                else
                {
                    receSuccess_cw2 = false;
                }
            }
        }

        //(方法)压差变送端口通信，接收数据(压差下位机发送数据频率：每秒90条左右)
        public void Com_Dp_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            System.IO.Ports.SerialPort sp_dp = sender as System.IO.Ports.SerialPort;

            if (sp_dp != null)
            {
                // 临时缓冲区将保存串口缓冲区的所有数据
                int bytesToRead_dp = sp_dp.BytesToRead;
                byte[] tempBuffer_dp = new byte[bytesToRead_dp];

                // 将缓冲区所有字节读取出来
                sp_dp.Read(tempBuffer_dp, 0, bytesToRead_dp);

                //获取实时压差数据，根据字节头54 50判断（若一次只发了一个字节54，不作判断，直接舍弃掉该条数据，对结果的影响可忽略不计）
                if (bytesToRead_dp > 1)
                {
                    if (tempBuffer_dp[0].ToString("X2") == "54" & tempBuffer_dp[1].ToString("X2") == "50")
                    {
                        greaterone_dp = true;
                        starflag2_dp = true;
                    }
                }

                if (greaterone_dp == true & starflag2_dp == true)
                {
                    paraBuffer_dp.AddRange(tempBuffer_dp);
                    //保存数据
                    if (paraBuffer_dp.Count == 11 | paraBuffer_dp.Count > 11)
                    {
                        if (paraBuffer_dp[0].ToString("X2") == "54" & paraBuffer_dp[1].ToString("X2") == "50")
                        {

                            datatime_dp = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");   //时间格式为 2018 / 07 / 01 12:12:12
                            datatimelabel_dp = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss").Substring(11);   //时间标签，格式为 12:12:12

                            //信号值
                            //dp_buf_win[tp_span_index] = paraBuffer_dp[2] * 256 + paraBuffer_dp[3];   //压差信号值
                            //fp_buf_win[tp_span_index] = paraBuffer_dp[4] * 256 + paraBuffer_dp[5];   //流压信号值    //第二个压差计信号值
                            //at_buf_win[tp_span_index] = paraBuffer_dp[6] * 256 + paraBuffer_dp[7];   //环境温度信号值
                            //ft_buf_win[tp_span_index] = paraBuffer_dp[8] * 256 + paraBuffer_dp[9];   //流温信号值
                            //实际错误接线：压差 小水表 大水表

                            //ft ch1 fp ch2 dp ch3 at ch4 硬件正确接线
                            ft_buf_win[tp_span_index] = paraBuffer_dp[2] * 256 + paraBuffer_dp[3];   //流温信号值
                            fp_buf_win[tp_span_index] = paraBuffer_dp[4] * 256 + paraBuffer_dp[5];   //流压信号值    //第二个压差计信号值
                            dp_buf_win[tp_span_index] = paraBuffer_dp[6] * 256 + paraBuffer_dp[7];   //压差信号值
                            at_buf_win[tp_span_index] = paraBuffer_dp[8] * 256 + paraBuffer_dp[9];   //环境温度信号值

                            tp_span_index = tp_span_index + 1;   //环形写入滑动窗口数组
                            if (tp_span_index == TP_SMOOTH_SPAN)
                            {
                                tp_span_index = 0;
                            }

                            tp_step_index = tp_step_index + 1;
                            //注意：窗口长度累积期间的滑动平均后的值有错，但忽略此错误。
                            //因为窗口长度还未累积满时，未累积到的值为0，但平均时仍是以窗口长度。直接忽略此错误，窗口长度期间的结果不影响最终结果

                            if (tp_step_index == TP_SMOOTH_STEP)   //滑动
                            {
                                tp_step_index = 0;
                                ft_buf_sum = 0;
                                fp_buf_sum = 0;
                                at_buf_sum = 0;
                                dp_buf_sum = 0;
                                for (int di = 0; di < TP_SMOOTH_SPAN; di++)   //累加求和
                                {
                                    ft_buf_sum = ft_buf_sum + ft_buf_win[di];
                                    fp_buf_sum = fp_buf_sum + fp_buf_win[di];
                                    at_buf_sum = at_buf_sum + at_buf_win[di];
                                    dp_buf_sum = dp_buf_sum + dp_buf_win[di];
                                }

                                ft_buf = ft_buf_sum / TP_SMOOTH_SPAN;
                                fp_buf = fp_buf_sum / TP_SMOOTH_SPAN;
                                at_buf = at_buf_sum / TP_SMOOTH_SPAN;
                                dp_buf = dp_buf_sum / TP_SMOOTH_SPAN;   //平均
                            }

                            //工程值
                            //ft_volt = ft_buf * (FT_MAX - FT_MIN) / 4096 + FT_MIN;   //流温
                            //fp_volt = fp_buf * (FP_MAX - FP_MIN) / 4096 + FP_MIN;   //流压

                            //ft_volt = (paraBuffer_dp[2] * 256 + paraBuffer_dp[3]) * (FT_MAX - FT_MIN) / 4096 + FT_MIN;   //流温
                            //fp_volt = (paraBuffer_dp[4] * 256 + paraBuffer_dp[5]) * (FP_MAX - FP_MIN) / 4096 + FP_MIN;   //流压

                            //ft_volt = 0.002925969 * ft_buf - 87.16414117;   //流温  
                            ft_volt = 0.002930911 * ft_buf -87.21365716;   //流温                  

                            fp_volt = 0.195394038 * fp_buf - 2480.910477;   //压值

                            //at_volt = VREF * at_buf / 4096 / 10.0;   //环境温度
                            // dp_volt = (float)(VREF * dp_buf / 65535.0);   //压差

                            swm_buf = ft_buf;      //小量程水表采集值
                            wm_buf  = fp_buf;      //大量程水表采集值
                            //swm_value = 0.002 * swm_buf - 25.747;      //小量程水表数值
                            //wm_value = 0.0033 * wm_buf - 42.174;      //大量程水表数值

                            swm_value = ft_volt;//ft_volt;      //小量程水表数值
                            wm_value  = fp_volt;//fp_volt;      //大量程水表数值


                            if (dp_volt < 1)
                            {
                                dp_volt = 1;
                            }

                            //真实值
                            at_value_c = at_volt;   //环境温度，单位℃
                            ft_value_c = ft_volt;//ft_volt;   //流动温度，单位℃
                            fp_value_pa = fp_volt;//fp_volt;   //流动压力，单位Pa
                                                  //  dp_value_pa = (DP_MAX_2 - DP_MIN_2) * (dp_volt) / (4096) + DP_MIN_2;   // 压差，单位Pa              
                                                  //  dp_value_pa = dp_value_pa * 1.2652 - 204.2533;
                                                  // dp_value_pa = 0.23535149 * dp_buf - 8997.06579312;   //新压差表     

                            //dp_value_pa = dp_buf * 3.067496819 - 3520.052858;
                            //dp_value_pa = dp_buf * 0.195313 - 7470.43;                           //将计算值转换成压差(-1000_9000)
                            //dp_value_pa = dp_buf * 0.68935875 - 13809.07349;                       //将计算值转换成压差(-5000_30000)

                            /*压差电流转换
                            dp = (test_mA - 4) / (20 - 4) * (max_dp - min_dp) + min_dp
                            dp = (0.000310080 * path_value + 0.069888091 - 4) /(20 - 4) * (max_dp - min_dp) + min_dp
                            dp_value_pa = (0.00001938 * dp_buf - 0.2456319943125) * (max_dp - min_dp) + min_dp
                            */
                            //dp_value_pa = dp_buf * 0.427116470 - 7432.473608564;    //将计算值转换成压差(-2000_20000)
                            //dp_value_pa = 32000 * (0.000310080 * dp_buf + 0.069888091 - 4) / 16 - 2000;// 将计算值转换成压差(-2000_30000)
                            dp_value_pa = 0.62016 * dp_buf - 9860.223878;// 将计算值转换成压差(-2000_30000)
                            

                            if (dp_value_pa > DP_MAX)
                                dp_value_pa = DP_MAX;
                            if (dp_value_pa < DP_MIN)
                                dp_value_pa = DP_MIN;

                            dp_ave = dp_value_pa;   //省去对压差值的滑动平均
                            /*前面已对原始信号值做了滑动平均处理，对压差值的滑动平均省去（如需要可再次对压差值进行滑动平均）
                            //窗口长度dp_wim_length累积期间
                            if (dp_count < dp_wim_length)
                            {
                                dp_win[dp_count] = dp_value_pa;

                                dp_sum = dp_sum + dp_win[dp_count];
                                dp_count = dp_count + 1;
                                dp_ave = dp_sum / dp_count;
                            }
                            else if (dp_count >= dp_wim_length)
                            {
                                star_slide = true;
                            }

                            //窗口长度累积完成之后，一个点滑动一次,求滑动平均
                            double dp_sum_slide = 0.0;
                            if (star_slide == true)
                            {
                                for (int di = 0; di < dp_wim_length - 1; di++)
                                {
                                    dp_win[di] = dp_win[di + 1];
                                }
                                dp_win[dp_wim_length - 1] = dp_value_pa;

                                for (int da = 0; da < dp_wim_length; da++)
                                {
                                    dp_sum_slide = dp_sum_slide + dp_win[da];
                                }
                                dp_ave = dp_sum_slide / 80.0;
                            }
                            */

                            //压差曲线显示值，原始范围-1000.0 - 1000.0换算到0.0 - 100.0范围显示，显示值=真实值/20+50 (换算后，50以下为负，50以上为正)
                            //dp_display = dp_ave / 20.0 + 50.0;
                            //dp_display = dp_ave / 120.0 + 50.0;
                            //压差曲线显示值，原始范围-2000.0 - 22000.0换算到0.0 - 100.0范围
                            dp_display = dp_ave / 220 + 9.1;

                            pointindex_dp = ii_dp;   //序列号
                            ii_dp = ii_dp + 1;
                            if (ii_dp == 1001)
                            {
                                ii_dp = 1;
                            }

                            greaterone_dp = false;
                            starflag2_dp = false;
                            paraBuffer_dp.Clear();
                        }
                    }
                }

                rece_i_dp = rece_i_dp + 1;
                //实现数据的解码与显示
                if (bytesToRead_dp != 0)
                {
                    string file_name = query_dir_his + "地面计量测试数据\\"+"压差数据 -1213.txt";
                    AddData(tempBuffer_dp, file_name, textBox_DpPortReceive, rece_i_dp);
                }
                else
                {
                    receSuccess_dp = false;
                }
            }
        }

        //解码,暂时仅适用默认编码，十六进制
        private void AddData(byte[] data, string txt_file_name1, TextBox textbox1, int index1)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sb.AppendFormat("{0:x2}" + " ", data[i]);
            }
            AddContent(sb.ToString().ToUpper(), txt_file_name1, textbox1, index1);
        }

        //数据接收区显示
        private void AddContent(string content, string txt_file_name2, TextBox textbox2, int index2)
        {
            if (this.IsHandleCreated)
            {
                BeginInvoke(new MethodInvoker(delegate
                {
                    textbox2.AppendText(content + "\r\n");
                    if (index2 != 0 & index2 % 300 == 0)
                    {
                        textbox2.Text = "";
                    }

                    /*//保存原始字节数据
                    if (save_path_flag == true)
                    {
                        //string str = index2.ToString() + "    " + DateTime.Now.ToString() + "    " + content;
                        string str = content;
                        StreamWriter swbuffer = new StreamWriter(txt_file_name2, true, Encoding.Default);
                        swbuffer.WriteLine(str);
                        swbuffer.Flush();
                        swbuffer.Close();
                    }*/

                }));
            }
        }

        //(方法)红外端口发送数据
        public void port_Cw_Write(SerialPort alive_port, string textData)
        {
            if (alive_port.IsOpen == false)
            {
                //MessageBox.Show("红外端口未打开，打开灯泡失败");
            }
            else if (alive_port.IsOpen == true)
            {
                try
                {
                    string[] grp = textData.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    List<byte> list = new List<byte>();

                    foreach (var item in grp)
                    {
                        list.Add(Convert.ToByte(item, 16));
                    }
                    alive_port.Write(list.ToArray(), 0, list.Count);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //(方法)含水曲线图
        public void drawcurve_cw(string xlabel, double ysecValue, double yminValue, double yQLValue, double yQGValue)
        {
            for (int j = 0; j < pointThres_cw - 1; j++)
            {
                timearray_cw[j] = timearray_cw[j + 1];
            }
            timearray_cw[pointThres_cw - 1] = xlabel;

            for (int j = 0; j < pointThres_cw - 1; j++)
            {
                seccwarray_cw[j] = seccwarray_cw[j + 1];
                mincwarray_cw[j] = mincwarray_cw[j + 1];
                QLarray_cw[j] = QLarray_cw[j + 1];
                QGarray_cw[j] = QGarray_cw[j + 1];
            }
            seccwarray_cw[pointThres_cw - 1] = ysecValue;
            mincwarray_cw[pointThres_cw - 1] = yminValue;
            QLarray_cw[pointThres_cw - 1] = yQLValue;
            QGarray_cw[pointThres_cw - 1] = yQGValue;

            chart2.Series["瞬时含水"].Points.DataBindXY(timearray_cw, seccwarray_cw);
            chart2.Series["压差"].Points.DataBindXY(timearray_cw, mincwarray_cw);
            chart2.Series["液流量"].Points.DataBindXY(timearray_cw, QLarray_cw);
            chart2.Series["气流量"].Points.DataBindXY(timearray_cw, QGarray_cw);

            if (startScroll_cw < 3)
            {
                chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.Scroll(ScrollType.Last);
                startScroll_cw = startScroll_cw + 1;
            }
        }

        //(方法)含水曲线chart属性设置
        private void set_cw_chart()
        {
            //含水曲线图设置---------------------------------------------------------------------
            chart2.ChartAreas["ChartArea1"].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.None;
            //设置适应全部数据点
            chart2.ChartAreas["ChartArea1"].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
            //设置当前X轴Label的双行显示格式 = 关闭
            chart2.ChartAreas["ChartArea1"].AxisX.LabelStyle.IsStaggered = false;
            //设置X轴不从0开始
            chart2.ChartAreas["ChartArea1"].AxisX.IsStartedFromZero = false;
            //chart1.ChartAreas["ChartArea1"].AxisY.IsStartedFromZero = false;

            chart2.ChartAreas["ChartArea1"].AxisX.ScrollBar.Enabled = true;
            chart2.ChartAreas["ChartArea1"].AxisX.ScrollBar.IsPositionedInside = true;//设置滚动条是在外部显示
            chart2.ChartAreas["ChartArea1"].AxisX.ScrollBar.Size = 10;//设置滚动条的宽度
            chart2.ChartAreas["ChartArea1"].AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.SmallScroll;//滚动条只显示向前的按钮，主要是为了不显示取消显示的按钮

            chart2.ChartAreas["ChartArea1"].AxisX.ScaleView.Size = 150;//设置图表可视区域数据点数，即一次可以看到多少个X轴区域
            //chart2.ChartAreas["ChartArea1"].AxisX.Interval = 30;
            chart2.ChartAreas["ChartArea1"].AxisX.ScaleView.Scroll(ScrollType.Last);

            //通道值图设置----------------------------------------------------------------------
            chart_cw_path.ChartAreas["ChartArea1"].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.None;
            //设置适应全部数据点
            chart_cw_path.ChartAreas["ChartArea1"].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
            //设置当前X轴Label的双行显示格式 = 关闭
            chart_cw_path.ChartAreas["ChartArea1"].AxisX.LabelStyle.IsStaggered = false;
            //设置X轴不从0开始
            chart_cw_path.ChartAreas["ChartArea1"].AxisX.IsStartedFromZero = false;
            //chart1.ChartAreas["ChartArea1"].AxisY.IsStartedFromZero = false;

            chart_cw_path.ChartAreas["ChartArea1"].AxisX.ScrollBar.Enabled = true;
            chart_cw_path.ChartAreas["ChartArea1"].AxisX.ScrollBar.IsPositionedInside = true;//设置滚动条是在外部显示
            chart_cw_path.ChartAreas["ChartArea1"].AxisX.ScrollBar.Size = 10;//设置滚动条的宽度
            chart_cw_path.ChartAreas["ChartArea1"].AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.SmallScroll;//滚动条只显示向前的按钮，主要是为了不显示取消显示的按钮

            chart_cw_path.ChartAreas["ChartArea1"].AxisX.ScaleView.Size = 150;//设置图表可视区域数据点数，即一次可以看到多少个X轴区域
            //chart2.ChartAreas["ChartArea1"].AxisX.Interval = 30;
            chart_cw_path.ChartAreas["ChartArea1"].AxisX.ScaleView.Scroll(ScrollType.Last);
        }

        //(方法)含水仪通道值图
        public void drawcurve_ptah(string xlabel_path, double yValue_Cw1Path1, double yValue_Cw1Path2, double yValue_Cw2Path1, double yValue_Cw2Path2)
        {
            //X轴序列
            for (int j = 0; j < pointThres_cw - 1; j++)
            {
                timearray_path[j] = timearray_path[j + 1];
            }
            timearray_path[pointThres_cw - 1] = xlabel_path;

            //Y轴序列
            for (int j = 0; j < pointThres_cw - 1; j++)
            {
                yarray_cw1_path1[j] = yarray_cw1_path1[j + 1];
                yarray_cw1_path2[j] = yarray_cw1_path2[j + 1];
                yarray_cw2_path1[j] = yarray_cw2_path1[j + 1];
                yarray_cw2_path2[j] = yarray_cw2_path2[j + 1];
            }
            yarray_cw1_path1[pointThres_cw - 1] = yValue_Cw1Path1;
            yarray_cw1_path2[pointThres_cw - 1] = yValue_Cw1Path2;
            yarray_cw2_path1[pointThres_cw - 1] = yValue_Cw2Path1;
            yarray_cw2_path2[pointThres_cw - 1] = yValue_Cw2Path2;

            chart_cw_path.Series["cw1_path1"].Points.DataBindXY(timearray_path, yarray_cw1_path1);
            chart_cw_path.Series["cw2_path1"].Points.DataBindXY(timearray_path, yarray_cw1_path2);
            //chart_cw_path.Series["cw1_path2"].Points.DataBindXY(timearray_path, yarray_cw1_path2);
            //chart_cw_path.Series["cw2_path2"].Points.DataBindXY(timearray_path, yarray_cw2_path2);

            if (startScroll_path < 3)
            {
                chart_cw_path.ChartAreas["ChartArea1"].AxisX.ScaleView.Scroll(ScrollType.Last);
                startScroll_path = startScroll_path + 1;
            }
        }

        #endregion

        #region 网格数据处理:流型识别、计算气速度、计算气面积
        //初始化气面积计算参数
        public void Gas_Liquid_Init()
        {
            //面积计算变量	
            gas_bubble_width = 0;                       //气泡宽度        
            gas_bubble_num = 0;                         //气泡个数 
            start_data = 0.0;                           //气泡起始位置数据幅度  
            gas_bubble_flag = 0;                        //气泡标志，0表示目前数据不是气泡，1表示目前数据是气泡 
            base_value = 0.0;
            slope = 0;
        }

        //流型识别
        public string Flow_Regime_Identify(int[] meah_a_ave_64_para1, int[] meah_b_ave_64_para2)
        {
            int j = 0;
            int i = 0;
            int uplimit = 4;
            int dplimit = -4; //可根据实际调节门上下限值的大小
            int gas_num_flag;
            int end_gas = 0;
            int start_gas = 0;

            //将截面均值写入队列,每次长度为16
            for (i = 0; i < ANA_LENGTH; i++) //除去第一个点，还有15个点
            {
                Sa_xcorr[idx] = meah_a_ave_64_para1[i];
                Sb_xcorr[idx] = meah_b_ave_64_para2[i];
                idx = idx + 1;
                ctr = ctr + 1;
            }

            if (idx == XCORR_SPAN) //XCORR_SPAN=512，等到512帧时开始检测是否有气产生，一共等待4s
            {
                start_flag = 1;
                idx = 0;
            }
            if (ctr == XCORR_STEP)  //XCORR_STEP=128，每秒滑动一次数据
            {
                ctr = 0;
            }

            if (ctr == 0 && start_flag == 1)
            {
                j = 0;
                //idx=0,128,256,384
                for (i = idx; i < XCORR_SPAN; i++)     //将数据进行排列因为128帧数据的平滑，前面的数据应当丢弃，
                {                                  //   后面的128帧数据接在512帧数据后面 排序
                    Sa_Regime[j] = Sa_xcorr[i];
                    Sb_Regime[j] = Sb_xcorr[i];
                    j++;
                }
                for (i = 0; i < idx; i++)
                {
                    Sa_Regime[j] = Sa_xcorr[i];
                    Sb_Regime[j] = Sb_xcorr[i];   //512帧窗口数据
                    j++;
                }

                /*----------------平滑滤波---------------------------------*/
                Sa_Regime_se = smooth_sgolay_5x(Sa_Regime, XCORR_SPAN - 1);
                Sb_Regime_se = smooth_sgolay_5x(Sb_Regime, XCORR_SPAN - 1);

                for (i = 0; i < XCORR_SPAN - 1; i++)   //求导=后一个减去前一个，变为长度为511的微分序列
                {
                    sa_diff_re[i] = Sa_Regime_se[i + 1] - Sa_Regime_se[i]; //对a网格微分求导
                    sb_diff_re[i] = Sb_Regime_se[i + 1] - Sb_Regime_se[i]; //对b网格微分求导 
                }

                gas_num_flag = 0; gas_num = 0;   //每隔512帧数据   标志及气泡量清零   从新计算下一个512帧数据
                for (i = 0; i < XCORR_SPAN - 1; i++)      //进行4s的判断   如果判断有气   则为气相
                {

                    if ((sa_diff_re[i] > dplimit && sa_diff_re[i + 1] < dplimit))   // 如果求导值大于设置的门限值，说明有气泡存在
                    {
                        start_gas = i;
                        gas_num_flag = 1;   //发现气泡置为1
                    }
                    if (gas_num_flag == 1 && sa_diff_re[i] > uplimit && sa_diff_re[i + 1] < uplimit)
                    {
                        end_gas = i;
                    }

                    if ((end_gas - start_gas) > 6 && (end_gas - start_gas) < 300)   //结束位置与起始位置距离大于一定值才认定可能有气泡
                    {
                        gas_num++;   //4秒512帧内的气泡个数累加
                        end_gas = 0; start_gas = 0;

                        if (gas_num >= 1)   //当4s中有气泡的帧数大于1个，则认为在这4s内有气泡
                        {
                            gas_num = 0;
                            Flow_Regime = "Gsa_Liquid_Flow";
                        }
                    }
                    else        //如果没气，则为纯液相
                    {
                        Flow_Regime = "Pure_Liquid_Flow";
                    }
                }
            }
            return Flow_Regime;
        }

        //计算气速度和气面积
        //相关长度：XCORR_SPAN=512个点=4s
        //只在MIN_LAG~MAX_LAG范围内进行搜寻,因为气的速度不会太慢，也不会太快
        //每XCORR_STEP=128个点=1s触发一次计算
        void Gas_Speed_Area_Calc()
        {
            int i, j;
            double multi_add, max_val;
            int max_lag;
            int lag;
            int tmp;
            int[] gas_start = Enumerable.Repeat(0, XCORR_SPAN).ToArray();         //气泡起始位置缓存  
            int[] gas_end = Enumerable.Repeat(0, XCORR_SPAN).ToArray();      //气泡结束位置缓存  


            if (ctr == 0 && start_flag == 1)  //当新采集的数据足够触发一次相关计算，并避开刚开始队列中补零的那段时间。
            {
                //------------------------------------------算速度-------------------------------------		
                //求微分
                for (i = 1; i < XCORR_SPAN; i++)  //从1开始，微分序列比原始序列长度少1
                {
                    sa_xcorr_sm[i] = Sa_Regime[i] - Sa_Regime[i - 1];
                    sb_xcorr_sm[i] = Sb_Regime[i] - Sb_Regime[i - 1];
                }

                for (i = 0; i < XCORR_SPAN - 1; i++)  //从0开始，微分序列前移一位，最后一位不变，sa_xcorr_sm[510]=sa_xcorr_sm[511], sa_xcorr_sm[511]未变
                {
                    sa_xcorr_sm[i] = sa_xcorr_sm[i + 1];
                    sb_xcorr_sm[i] = sb_xcorr_sm[i + 1];
                }

                //平滑滤波
                sa_diff = smooth_sgolay_23x(sa_xcorr_sm, XCORR_SPAN - 1);
                sb_diff = smooth_sgolay_23x(sb_xcorr_sm, XCORR_SPAN - 1);

                //求相关
                //计算卷积，并找相关结果中最大值点
                max_val = 0; max_lag = 0;
                for (lag = MIN_LAG; lag <= MAX_LAG; lag++)   //只在合理范围内搜索相关峰值，速度合理范围0.89m/s ~ 0.296m/s ，MIN_LAG =20，MAX_LAG=60
                {
                    multi_add = 0;   //累加和  
                    for (i = 1; i < XCORR_SPAN; i++)    //XCORR_SPAN 范围内乘加
                    {
                        tmp = Math.Abs(lag) + i;
                        if ((tmp) >= XCORR_SPAN)
                            multi_add += 0;     //B=0,A*B=0;	 
                        else
                            multi_add += (sa_diff[i] * sb_diff[tmp]); //乘加和
                    }
                    if (multi_add > max_val)   //寻找最大max_val和max_lag
                    {
                        max_val = multi_add;
                        max_lag = lag;   //最大max_lag即表示两个气泡相隔点数
                    }
                }
                peak = max_val;

                //------------------------------------------算面积-------------------------------------			
                sa_xcorr_sm_area = smooth_sgolay_5x(Sa_Regime, XCORR_SPAN);       //先平滑

                for (i = 1; i < XCORR_SPAN; i++)
                {
                    sa_diff_area[i - 1] = sa_xcorr_sm_area[i] - sa_xcorr_sm_area[i - 1];   //求微分     
                }

                sa_diff_sm = smooth_sgolay_5x(sa_diff_area, XCORR_SPAN - 1);          //再平滑

                gas_area_sum = 0;
                gas_bubble_width = 0;
                //查找并标注气泡位置,状态机
                for (i = 0; i < (XCORR_SPAN - 3); i++)
                {
                    //检测到气泡起始位置，条件：目前数据不是气泡 && 下一个数据导数小于负门限 && 当前数据导数大于等于负门限
                    if ((gas_bubble_flag == 0) && (sa_diff_sm[i] >= threshold_n) && (sa_diff_sm[i + 1] < threshold_n))
                    {
                        gas_start[gas_bubble_num] = i;
                        gas_bubble_flag = 1;
                        start_data = Sa_Regime[i];
                    }
                    //已检测到气泡起始，还未检测到气泡结束
                    if (gas_bubble_flag == 1)    //如果当前数据是气泡数据，气泡宽度加1   
                    {
                        gas_bubble_width = gas_bubble_width + 1;
                    }
                    //已检测到气泡开始，气泡宽度超过最大限制，将回到检测气泡起始
                    if (gas_bubble_width > gas_bubble_width_max)         //如果气泡宽度大于最大气泡宽度，舍弃当前数据   
                    {
                        gas_bubble_flag = 0;
                        gas_bubble_width = 0;
                    }
                    //已检测到气泡起始，也检测到气泡结束位置，条件：当前数据是气泡 && 当前数据导数大于正门限 && 下一个数据导数小于等于正门限 && 气泡起始位置幅度与结束位置幅度差小于幅度最大差值
                    if ((gas_bubble_flag == 1) && (sa_diff_sm[i] > threshold_p) && (sa_diff_sm[i + 1] <= threshold_p) && (Math.Abs(start_data - Sa_Regime[i]) < threshold_max_hl) && (sa_diff_sm[i + 2] < sa_diff_sm[i + 1]))
                    {
                        gas_end[gas_bubble_num] = i + 1;
                        gas_bubble_num++;
                        gas_bubble_flag = 0;
                        gas_bubble_width = 0;
                    }
                }

                for (i = 0; i < gas_bubble_num; i++)   //求气泡面积新算法 
                {
                    if (gas_end[i] > 0 && gas_start[i] > 0)
                    {
                        slope = (Sa_Regime[gas_end[i]] - Sa_Regime[gas_start[i]]) / (gas_end[i] - gas_start[i]);   //获取每个气泡中点数据  

                        base_value = Sa_Regime[gas_start[i]];
                        for (j = gas_start[i]; j <= gas_end[i]; j++)   //求每个气泡面积并累加          
                        {
                            base_value += slope;
                            if (base_value > Sa_Regime[j])
                                gas_area_sum += (base_value - Sa_Regime[j]);
                        }
                    }
                }

                if (gas_area_sum < 0)  //气面积最小为0
                    gas_area_sum = 0;

                //当气速度在合理范围时，将气的速度和面积写入平滑队列，并计算平滑值 
                // 速度与面积的平滑参数一致
                if (max_val > XCORR_MIN_PEAK)//如果相关峰值大于XCORR_MIN_PEAK，则本次相关结果有效，主要是避免错误的相关结果影响计算
                {
                    //当读取txt数据时才执行以下操作
                    if (save_from_txt == true)
                    {
                        //直接通过计算保存的网格数据检查速度是否计算正确
                        speed_check = MESH_DIST / (TIME_INTERVAL * max_lag);
                        area_check = gas_area_sum;

                        string check_txtPath = query_dir_his + "地面计量测试数据\\" + speed_check_file_name + "_速度.txt";
                        speed_check_sw = new StreamWriter(check_txtPath, true, Encoding.Default);   //气速度
                        new_file = true;

                        //保存速度
                        string speed_check_str = speed_check.ToString("f4").PadLeft(8) + "    " + area_check.ToString("f4").PadLeft(10);
                        speed_check_sw.WriteLine(speed_check_str);
                        speed_check_sw.Flush();
                        speed_check_sw.Close();
                    }

                    //将速度和面积缓存至窗口滑动数组中
                    speed_win[span_index] = MESH_DIST / (TIME_INTERVAL * max_lag);   //速度
                    area_win[span_index] = gas_area_sum;  //面积

                    //窗口长度累积期间，直接求和平均
                    if (span_end_flag == false)
                    {
                        speed_sum_count = speed_sum_count + speed_win[span_index];
                        area_sum_count = area_sum_count + area_win[span_index];
                        span_count = span_count + 1;
                        GasSpeed = speed_sum_count / span_count;
                        GasArea = area_sum_count / span_count;
                    }

                    span_index = span_index + 1;
                    if (span_index == SMOOTH_SPAN)
                    {
                        span_index = 0;
                        span_end_flag = true;
                    }

                    //窗口长度累积完成之后，开始滑动平均
                    if (span_end_flag == true)
                    {
                        step_index = step_index + 1;
                        if (step_index == SMOOTH_STEP)
                        {
                            step_index = 0;
                            speed_sum = 0.0;
                            area_sum = 0.0;
                            for (int si = 0; si < SMOOTH_SPAN; si++)
                            {
                                speed_sum = speed_sum + speed_win[si];   //累加求和
                                area_sum = area_sum + area_win[si];
                            }
                            GasSpeed = speed_sum / SMOOTH_SPAN;   //平均
                            GasArea = area_sum / SMOOTH_SPAN;
                            GasArea = GasArea * 4;
                        }
                    }
                }
                else
                {
                    GasSpeed = 0.0;
                    GasArea = 0.0;
                }

                //实时显示气速度和气面积
                BeginInvoke(new MethodInvoker(delegate
                {
                    label_gas_area.Text = GasArea.ToString("f2");
                    label_gas_speed.Text = GasSpeed.ToString("f2");
                    label_sg2.Text = GasArea.ToString("f2");
                    label_vg2.Text = GasSpeed.ToString("f2");
                }));

            }
        }

        private void label_dp2_Click(object sender, EventArgs e)
        {

        }

        private void label28_Click(object sender, EventArgs e)
        {

        }
        #endregion


        #region 液量、气量计算

        //手动保存固定行数的数据开关
        private void checkBox_save_result_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_save_result.Checked == true)
            {
                save_data_flag = true;
                data_count = 0;
                cw_sum_save = 0.0;
                dp_sum_save = 0.0;
                speed_sum_save = 0.0;
                area_sum_save = 0.0;
                qo_sum_save = 0.0;
                qw_sum_save = 0.0;
                ql_sum_save = 0.0;
                qg_sum_save = 0.0;
            }
            else if (checkBox_save_result.Checked == false)
            {
                //最后一行保存之上所有数据的平均值
                string data_ave_str = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "    " + (data_count + 1).ToString().PadLeft(4)
                         + "    " + cw_ave_save.ToString("f4").PadLeft(8) + "    " + dp_ave_save.ToString("f4").PadLeft(10) + "    " + speed_ave_save.ToString("f4").PadLeft(8) + "    " + area_ave_save.ToString("f4").PadLeft(10)
                         + "    " + qo_ave_save.ToString("f4").PadLeft(8) + "    " + qw_ave_save.ToString("f4").PadLeft(8) + "    " + ql_ave_save.ToString("f4").PadLeft(8) + "    " + qg_ave_save.ToString("f4").PadLeft(8);
                data_sw.WriteLine(data_ave_str);
                data_sw.Flush();
                data_sw.Close();
                save_data_flag = false;
            }
        }

        //保存含水-压差-速度-面积-液量-气量数据，保存开关打开后每0.125秒保存一次,time_interval=0.125s
        private void timer_save_data_Tick(object sender, EventArgs e)
        {
            //后台自动保存数据
            if (create_new_file == true)
            {
                ql_qg_filename = "地面计量-" + standardTime(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));

                //ql_qg_filepath = "E:\\地面计量测试数据-1229\\" + ql_qg_filename + ".txt";
                ql_qg_filepath = query_dir_his + "地面计量测试数据\\" + ql_qg_filename + ".txt"; 
                ql_qg_sw = new StreamWriter(ql_qg_filepath, true, Encoding.Default);
                ql_qg_sw.WriteLine("      日期         时间        序号    含水率         压差        气速度        气面积        油量         水量         液量         气量         平均含水        平均油量      平均水量       平均液量       平均气量       累计油量       累计水量       累计液量       累计气量    通道一    通道二");
                create_new_file = false;
            }
            file_row_count = file_row_count + 1;
            string ql_qg_str = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "    " + file_row_count.ToString().PadLeft(4)
                              + "    " + seccw.ToString("f4").PadLeft(8) + "    " + dp_value_pa.ToString("f4").PadLeft(10) + "    " + GasSpeed.ToString("f4").PadLeft(8) + "    " + GasArea.ToString("f4").PadLeft(10)
                              + "    " + Q_Oil.ToString("f4").PadLeft(8) + "    " + Q_Water.ToString("f4").PadLeft(8) + "    " + Q_Liquid.ToString("f4").PadLeft(8) + "    " + Q_Gas.ToString("f4").PadLeft(8)
                              + "    " + seccw_user.ToString("f4").PadLeft(8) + "        " + Q_Qil_user.ToString("f4").PadLeft(8) + "        " + Q_Water_user.ToString("f4").PadLeft(8) + "        " + Q_Liquid_user.ToString("f4").PadLeft(8) + "        " + Q_Gas_user.ToString("f4").PadLeft(8)
                              + "        " + Q_Oil_total.ToString("f4").PadLeft(8) + "        " + Q_Water_total.ToString("f4").PadLeft(8) + "        " + Q_Liquid_total.ToString("f4").PadLeft(8) + "        " + Q_Gas_total.ToString("f4").PadLeft(8) + pathfir.ToString().PadLeft(10) + pathsec.ToString().PadLeft(10);
            ql_qg_sw.WriteLine(ql_qg_str);
            ql_qg_sw.Flush();

            //string day_clock_str = DateTime.Now.Hour.ToString().PadLeft(2, '0') + DateTime.Now.Minute.ToString().PadLeft(2, '0') + DateTime.Now.Second.ToString().PadLeft(2, '0');
            string hour_clock_str = DateTime.Now.Minute.ToString().PadLeft(2, '0') + DateTime.Now.Second.ToString().PadLeft(2, '0');
            if (hour_clock_str == "0000")
            {
                ql_qg_sw.Close();
                file_row_count = 0;
                create_new_file = true;
            }

            //手动保存固定行数的数据
            if (save_data_flag == true)
            {
                if (data_count == 0)
                {
                    data_file_name = textBox_result_fliename.Text;
                    string data_txtPath = query_dir_his + "地面计量测试数据\\" + data_file_name + "_data.txt";
                    data_sw = new StreamWriter(data_txtPath, false, Encoding.Default);   //含水-压差-气速度-气面积数据
                }

                //保存测试结果数据
                data_count = data_count + 1; //数据行数计数
                string data_str = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "    " + data_count.ToString().PadLeft(4)
                             + "    " + seccw.ToString("f4").PadLeft(8) + "    " + dp_value_pa.ToString("f4").PadLeft(10) + "    " + GasSpeed.ToString("f4").PadLeft(8) + "    " + GasArea.ToString("f4").PadLeft(10)
                             + "    " + Q_Oil.ToString("f4").PadLeft(8) + "    " + Q_Water.ToString("f4").PadLeft(8) + "    " + Q_Liquid.ToString("f4").PadLeft(8) + "    " + Q_Gas.ToString("f4").PadLeft(8)
                             + "    " + wm_buf.ToString("f4").PadLeft(8) + "    " + wm_value.ToString("f4").PadLeft(8) + "    " + gm_buf.ToString("f4").PadLeft(8) + "    " + gm_value.ToString("f4").PadLeft(8)
                             + "    " + swm_buf.ToString("f4").PadLeft(8) + "    " + swm_value.ToString("f4").PadLeft(8) + "    " + om_buf.ToString("x").PadLeft(8) + "    " + om_value.ToString("f4").PadLeft(8)
                             + "    " + bom_buf.ToString("X").PadLeft(8) + "    " + bom_value.ToString("f4").PadLeft(8);
                data_sw.WriteLine(data_str);
                data_sw.Flush();

                //累加求和
                cw_sum_save = seccw + cw_sum_save;
                dp_sum_save = dp_value_pa + dp_sum_save;
                speed_sum_save = GasSpeed + speed_sum_save;
                area_sum_save = GasArea + area_sum_save;
                qo_sum_save = Q_Oil + qo_sum_save;
                qw_sum_save = Q_Water + qw_sum_save;
                ql_sum_save = Q_Liquid + ql_sum_save;
                qg_sum_save = Q_Gas + qg_sum_save;

                //实时平均
                cw_ave_save = cw_sum_save / data_count;
                dp_ave_save = dp_sum_save / data_count;
                speed_ave_save = speed_sum_save / data_count;
                area_ave_save = area_sum_save / data_count;
                qo_ave_save = qo_sum_save / data_count;
                qw_ave_save = qw_sum_save / data_count;
                ql_ave_save = ql_sum_save / data_count;
                qg_ave_save = qg_sum_save / data_count;

                Invoke(new MethodInvoker(delegate
                {
                    textBox_data_count.Text = data_count.ToString();
                    // label32.Text= cw_ave_save.ToString("f4").PadLeft(8) + "    " + dp_ave_save.ToString("f4").PadLeft(10) + "    " + speed_ave_save.ToString("f4").PadLeft(8) + "    " + area_ave_save.ToString("f4").PadLeft(10)
                    //       + "    " + ql_ave_save.ToString("f4").PadLeft(8) + "    " + qg_ave_save.ToString("f4").PadLeft(8);
                    Cw_textBox1.Text = cw_ave_save.ToString("f4").PadLeft(8);
                    Dp_textBox2.Text = dp_ave_save.ToString("f4").PadLeft(10);
                    Sp_textBox3.Text = speed_ave_save.ToString("f4").PadLeft(8);
                    Area_textBox4.Text = area_ave_save.ToString("f4").PadLeft(10);
                    Q_textBox5.Text = ql_ave_save.ToString("f4").PadLeft(8);
                    G_textBox6.Text = qg_ave_save.ToString("f4").PadLeft(8);
                }));

                if (data_count == int.Parse(textBox_data_num.Text))
                {
                    save_data_flag = false;
                    BeginInvoke(new MethodInvoker(delegate
                    {
                        checkBox_save_result.Checked = false;
                    }));
                }
            }
        }

        public Int16 total_flag = 0;
        //(定时事件)每秒刷新含水、压差、气液流量曲线
        private void timer_refresh_cw_dp_Tick(object sender, EventArgs e)
        {


            //向气表端口每秒发送功能码，并接收返回的字节数据
            //气表端口打开时执行发送和接收操作
            /*
            if (port_GasMeter_status == true)
            {
                // port_Cw_Write(port_GasMeter, "01 03 00 00 00 14 45 C5");
                port_Cw_Write(port_GasMeter, "01 03 00 02 00 02 65 CB");
                port_GasMeter.DataReceived += new SerialDataReceivedEventHandler(Com_GasMeter_DataReceived);
            }
            */
            //向油表端口每秒发送功能码，并接收返回的字节数据
            //油表端口打开时执行发送和接收操作
            if (port_OilMeter_status == true)
            {
                // port_Cw_Write(port_GasMeter, "01 03 00 00 00 14 45 C5");
                port_Cw_Write(port_OilMeter, "01 03 01 02 00 02 64 37");
                port_OilMeter.DataReceived += new SerialDataReceivedEventHandler(Com_OilMeter_DataReceived);
            }
            //向大量程油表端口每秒发送功能码，并接收返回的字节数据
            //大量程油表端口打开时执行发送和接收操作
            if (port_BigOilMeter_status == true)
            {
                // port_Cw_Write(port_GasMeter, "01 03 00 00 00 14 45 C5");
                port_Cw_Write(port_BigOilMeter, "01 03 01 02 00 02 64 37");
                port_BigOilMeter.DataReceived += new SerialDataReceivedEventHandler(Com_BigOilMeter_DataReceived);
            }
            //Random rd = new Random();
            //Q_Gas = rd.Next(50, 100);
            //平均累计
            //seccw_second_buf[sec_sm_idx] = seccw;
            seccw_user_buf[user_sm_idx] = seccw;

            //Q_Gas_second_buf[sec_sm_idx] = Q_Gas;
            Q_Gas_user_buf[user_sm_idx] = Q_Gas;

            //Q_Liquid_second_buf[sec_sm_idx++] = Q_Liquid;
            Q_Liquid_user_buf[user_sm_idx++] = Q_Liquid;

            if (User_idx_lock == 1)
            {
                if (User_idx++ >= User_Sm_Time)
                {
                    User_idx = User_Sm_Time;
                    User_idx_lock = 0;
                }
            }
            //自定义平均 自定义平滑时间（秒）更新一次
            
            seccw_user = ArithmeticMean(seccw_user_buf, User_idx);
            Q_Gas_user = ArithmeticMean(Q_Gas_user_buf, User_idx);

            Q_Liquid_user = ArithmeticMean(Q_Liquid_user_buf, User_idx);
            Q_Water_user = Q_Liquid_user * seccw_user / 100;
            Q_Qil_user = Q_Liquid_user - Q_Water_user;

            /*
            seccw_user = 80;
            Q_Gas_user = 10000;

            Q_Liquid_user = 10000;
            Q_Water_user = Q_Liquid_user * seccw_user / 100;
            Q_Qil_user = Q_Liquid_user - Q_Water_user;
            */

            if (user_sm_idx >= User_Sm_Time)
            {
                user_sm_idx = 0;
            }
            
            if (++total_flag == 10)
            {
                Q_Gas_total = Q_Gas_total + Q_Gas_user / 8640;
                Q_Liquid_total = Q_Liquid_total + Q_Liquid_user / 8640;
                Q_Water_total = Q_Water_total + Q_Water_user / 8640;
                Q_Oil_total = Q_Oil_total + Q_Qil_user / 8640;
                total_flag = 0;
            }
            if (Q_Gas_total == 1000000 || Q_Liquid_total == 100000)
            {
                Q_Gas_total = 0;
                Q_Liquid_total = 0;
                Q_Water_total = 0;
                Q_Oil_total = 0;
            }

            //实时显示含水、压差数据
            //红外含水和压差端口中有一个为打开状态，则每秒刷新
            if (port_Cw_status == true | port_Dp_status == true)
            {
                //gas_liquid_calc();   //计算液量、油量和水量

                //实时刷新瞬时含水、压差曲线
                string x_time = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss").Substring(11);
                //BeginInvoke(drawChart_cw, new object[] { x_time, seccw, dp_display, Q_Liquid, Q_Gas });
                BeginInvoke(drawChart_cw, new object[] { x_time, seccw_user, dp_display, Q_Liquid_user, Q_Gas_user });

                //实时刷新瞬时含水、压差、流量等数据
                BeginInvoke(new MethodInvoker(delegate
                {
                    //label_sg2.Text = GasArea.ToString("f2");
                    //label_vg2.Text= GasSpeed.ToString("f2");
                    label_dp3.Text = fp_value_pa.ToString("f2"); //压差计2
                    label_dp2.Text = dp_value_pa.ToString("f2"); //压差计1

                    textBox4.Text = ft_value_c.ToString();
                    textBox5.Text = ft_buf.ToString();
                    textBox3.Text = fp_buf.ToString();
                    textBox6.Text = dp_buf.ToString();

                    label_cw_display.Text = seccw.ToString("f2");         //瞬时含水
                    label_dp_display.Text = dp_value_pa.ToString("f2");   //压差
                    //label_gas_area.Text = GasArea.ToString("f2");         //气面积
                    //label_gas_speed.Text = GasSpeed.ToString("f2");       //气速度

                    label_qLiquid.Text = Q_Liquid.ToString("f2");   //液流量
                    label_qWater.Text = Q_Water.ToString("f2");     //水流量
                    label_qOil.Text = Q_Oil.ToString("f2");         //油流量
                    label_qGas.Text = Q_Gas.ToString("f2");         //气流量

                    label_cw_avg.Text = seccw_user.ToString("f2");
                    label_L_avg.Text = Q_Liquid_user.ToString("f2");
                    label_G_avg.Text = Q_Gas_user.ToString("f2");
                    label_W_avg.Text = Q_Water_user.ToString("f2");
                    label_O_avg.Text = Q_Qil_user.ToString("f2");

                    label_L_total.Text = Q_Liquid_total.ToString("f2");
                    label_G_total.Text = Q_Gas_total.ToString("f2");
                    label_W_total.Text = Q_Water_total.ToString("f2");
                    label_O_total.Text = Q_Oil_total.ToString("f2");

                }));
            }
        }

        //液量、气量计算
        public void gas_liquid_calc()
        {
            //油水两相
            if (calc_flow_phase == "oil_water_mix")
            {
                //修正密度对压差的影响
                double density_mix = seccw * 1000 / 100.0 + (100 - seccw) * 832 / 100.0;
                double dp_m = dp_ave + (1000 - density_mix) * 9.8 * 0.142;
                if (dp_m < 0.0)
                {
                    dp_m = 0.0;
                }
                dp_m = Math.Sqrt(dp_m);
                Q_Liquid = -5.7449 * seccw / 100.0 + 2.6885 * dp_m + 5.2341;
                //最大液量限制
                if (Q_Liquid > MAX_Q_LIQUID)
                {
                    Q_Liquid = MAX_Q_LIQUID;
                }
                //最小液量限制
                if (Q_Liquid < MIN_Q_LIQUID)
                {
                    Q_Liquid = MIN_Q_LIQUID;
                }

                Q_Water = Q_Liquid * seccw / 100.0;
                Q_Oil = Q_Liquid - Q_Water;

                Q_Gas = 0.0;   //油水两相时气量为0.0
            }//油气水三相
            else if (calc_flow_phase == "oil_gas_water_mix")
            {

                //2020.1.12不分段公式
                Q_Liquid = three_phase_coe_all[0] + three_phase_coe_all[1] * dp_ave + three_phase_coe_all[2] * seccw + three_phase_coe_all[3] * GasSpeed + three_phase_coe_all[4] * dp_ave / (10 + seccw)
                      + three_phase_coe_all[5] * seccw * seccw + three_phase_coe_all[6] * GasSpeed * GasSpeed + three_phase_coe_all[7] * dp_ave / (10 + seccw) * dp_ave / (10 + seccw)
                      + three_phase_coe_all[8] * seccw * seccw * seccw + three_phase_coe_all[9] * GasSpeed * GasSpeed * GasSpeed;
                ////2021.2.1液量分段
                if (Q_Liquid <= 22.5)
                {
                    calc_gas_coe = calc_gas_coe_s;
                    Q_Liquid = three_phase_coe_s[0] + three_phase_coe_s[1] * dp_ave + three_phase_coe_s[2] * seccw + three_phase_coe_s[3] * GasSpeed + three_phase_coe_s[4] * dp_ave / (10 + seccw)
                          + three_phase_coe_s[5] * seccw * seccw + three_phase_coe_s[6] * GasSpeed * GasSpeed + three_phase_coe_s[7] * dp_ave / (10 + seccw) * dp_ave / (10 + seccw)
                          + three_phase_coe_s[8] * seccw * seccw * seccw + three_phase_coe_s[9] * GasSpeed * GasSpeed * GasSpeed;
                }
                else if (Q_Liquid > 22.5 && Q_Liquid <= 47.5)
                {
                    calc_gas_coe = calc_gas_coe_m;
                    Q_Liquid = three_phase_coe_m[0] + three_phase_coe_m[1] * dp_ave + three_phase_coe_m[2] * seccw + three_phase_coe_m[3] * GasSpeed + three_phase_coe_m[4] * dp_ave / (10 + seccw)
                          + three_phase_coe_m[5] * seccw * seccw + three_phase_coe_m[6] * GasSpeed * GasSpeed + three_phase_coe_m[7] * dp_ave / (10 + seccw) * dp_ave / (10 + seccw)
                          + three_phase_coe_m[8] * seccw * seccw * seccw + three_phase_coe_m[9] * GasSpeed * GasSpeed * GasSpeed;
                }
                else
                {
                    calc_gas_coe = calc_gas_coe_l;
                    Q_Liquid = three_phase_coe_l[0] + three_phase_coe_l[1] * dp_ave + three_phase_coe_l[2] * seccw + three_phase_coe_l[3] * GasSpeed + three_phase_coe_l[4] * dp_ave / (10 + seccw)
                          + three_phase_coe_l[5] * seccw * seccw + three_phase_coe_l[6] * GasSpeed * GasSpeed + three_phase_coe_l[7] * dp_ave / (10 + seccw) * dp_ave / (10 + seccw)
                          + three_phase_coe_l[8] * seccw * seccw * seccw + three_phase_coe_l[9] * GasSpeed * GasSpeed * GasSpeed;
                }
                //计算液量-----------------
                //普通含水情况下，采用以下公式和系数计算液量
                //Q_Liquid = three_phase_coe[0] + three_phase_coe[1] * dp_ave + three_phase_coe[2] * seccw + three_phase_coe[3] * GasSpeed + three_phase_coe[4] * dp_ave / (10 + seccw)
                //          + three_phase_coe[5] * seccw * seccw + three_phase_coe[6] * GasSpeed * GasSpeed + three_phase_coe[7] * dp_ave / (10 + seccw) * dp_ave / (10 + seccw)
                //         + three_phase_coe[8] * seccw * seccw * seccw + three_phase_coe[9] * GasSpeed * GasSpeed * GasSpeed;

                //低含水情况下，采用以下公式和系数计算液量
                //Q_Liquid = three_phase_coe_low_cw[0] + three_phase_coe_low_cw[1] * dp_ave + three_phase_coe_low_cw[2] * seccw + three_phase_coe_low_cw[3] * GasSpeed;
                //进一步修正
                //Q_Liquid = Q_Liquid / (0.975 + 0.0001 * (seccw - 65) * (seccw - 65));

                //最大液量限制
                if (Q_Liquid > MAX_Q_LIQUID)
                {
                    Q_Liquid = MAX_Q_LIQUID;
                }
                //最小液量限制
                if (Q_Liquid < MIN_Q_LIQUID)
                {
                    Q_Liquid = MIN_Q_LIQUID;
                }

                Q_Water = Q_Liquid * seccw / 100.0;
                Q_Oil = Q_Liquid - Q_Water;

                //计算气量------------------
                //Q_Gas = calc_gas_coe[0] + calc_gas_coe[1] * GasSpeed + calc_gas_coe[2] * seccw + calc_gas_coe[3] * dp_ave
                //      + calc_gas_coe[4] * Q_Liquid + calc_gas_coe[5] * seccw * seccw + calc_gas_coe[6] * seccw * seccw * seccw + calc_gas_coe[7] * Math.Pow(2, GasSpeed);     //小魏计算气量公式

                //   Q_Gas = calc_gas_coe[0] + calc_gas_coe[1] * GasSpeed + calc_gas_coe[2] * dp_ave + calc_gas_coe[3] * GasArea
                //       + calc_gas_coe[4] * GasArea * GasSpeed + calc_gas_coe[5] * GasArea * GasSpeed * GasSpeed + calc_gas_coe[6] * seccw;             //黄亚计算气量公式 

                // Q_GVF = calc_gas_coe[0] + calc_gas_coe[1] * dp_ave + calc_gas_coe[2] * Q_Liquid + calc_gas_coe[3] * (dp_ave * Q_Liquid)
                //         + calc_gas_coe[4] * Q_Liquid * seccw + calc_gas_coe[5] * Q_Liquid * Q_Liquid + calc_gas_coe[6] * dp_ave* dp_ave + calc_gas_coe[7] *(dp_ave * Q_Liquid)*(dp_ave * Q_Liquid)
                //         + calc_gas_coe[8] * GasSpeed + calc_gas_coe[9] * GasSpeed * GasSpeed + calc_gas_coe[10] * seccw;                                   //张磊算含气率

                // Q_Gas = (Q_GVF * Q_Liquid) / (1- Q_GVF);                                                                                                  //张磊算气公式

                //Q_Gas = calc_gas_coe[0] + calc_gas_coe[1] * Q_Liquid + calc_gas_coe[2] * dp_ave + calc_gas_coe[3] * GasSpeed
                //      + calc_gas_coe[4] * GasArea + calc_gas_coe[5] * GasSpeed * dp_ave + calc_gas_coe[6] * dp_ave * dp_ave
                //      + calc_gas_coe[7] * dp_ave * dp_ave * GasSpeed + calc_gas_coe[8] * GasSpeed * Q_Liquid
                //      + calc_gas_coe[9] * GasSpeed * GasSpeed + calc_gas_coe[10] * GasSpeed * GasArea + calc_gas_coe[11] * GasArea / GasSpeed;//2020.8.12改

                //Q_Gas = calc_gas_coe[0] + calc_gas_coe[1] * Q_Liquid + calc_gas_coe[2] * dp_ave + calc_gas_coe[3] * GasSpeed
                //    + calc_gas_coe[4] * GasSpeed * dp_ave + calc_gas_coe[5] * dp_ave * dp_ave
                //    + calc_gas_coe[6] * dp_ave * dp_ave * GasSpeed + calc_gas_coe[7] * GasSpeed * Q_Liquid
                //    + calc_gas_coe[8] * GasSpeed * GasSpeed + calc_gas_coe[9] * GasSpeed * GasArea + calc_gas_coe[10] * GasArea / GasSpeed;//2020.12.28改

                //Q_Gas = calc_gas_coe[0] + calc_gas_coe[1] * Q_Liquid + calc_gas_coe[2] * dp_ave + calc_gas_coe[3] * GasSpeed / Q_Liquid
                //    + calc_gas_coe[4] * GasSpeed * GasArea + calc_gas_coe[5] * dp_ave / seccw
                //    + calc_gas_coe[6] * dp_ave / (10 + seccw) + calc_gas_coe[7] * GasSpeed / dp_ave
                //    + calc_gas_coe[8] * GasSpeed * GasSpeed + calc_gas_coe[9] * seccw * seccw + calc_gas_coe[10] * dp_ave * dp_ave;//2021.1.27改

                Q_Gas = calc_gas_coe[0] + calc_gas_coe[1] * Q_Liquid + calc_gas_coe[2] * dp_ave + calc_gas_coe[3] * GasSpeed
                    + calc_gas_coe[4] * GasSpeed / Q_Liquid + calc_gas_coe[5] * GasSpeed * GasArea
                    + calc_gas_coe[6] * dp_ave / seccw + calc_gas_coe[7] * dp_ave / (10 + seccw)
                    + calc_gas_coe[8] * GasSpeed / dp_ave + calc_gas_coe[9] * GasSpeed * GasSpeed + calc_gas_coe[10] * dp_ave * dp_ave;//2021.2.1改
                 if (double.IsNaN(Q_Gas))
                 {
                    Q_Gas = 0.0;
                 }

                //温度压力补偿  压力：ft_volt  温度：fp_volt
                //Q_Gas = Q_Gas * ft_volt * 293.15 / (101.325 * (ft_volt + 273.15));


                //最大气量限制
                if (Q_Gas > MAX_Q_GAS)
                {
                    Q_Gas = MAX_Q_GAS;
                }
                //最小气量限制
                if (Q_Gas < MIN_Q_GAS)
                {
                    Q_Gas = MIN_Q_GAS;
                }
            }
        }

        //油水两相和油气水三相计算模式切换
        private void radioButton_oil_gas_water_mix_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_oil_gas_water_mix.Checked == true)
            {
                calc_flow_phase = "oil_gas_water_mix";
            }
            else if (radioButton_oil_water_mix.Checked == true)
            {
                calc_flow_phase = "oil_water_mix";
            }
        }


        //规范时间格式，用于设置文件名，将"/",":"字符以"."代替,将" "以"-"代替
        private string standardTime(string time)
        {
            string s = time;
            String[] ss = s.Split(new char[] { '/', ' ', ':', '-' });
            return string.Format(ss[0] + "." + ss[1] + "." + ss[2] + "-" + ss[3] + "." + ss[4] + "." + ss[5]);
        }

        #endregion

        #region 平滑方法3种(普通平滑方法，5点sgolay 平滑方法，23点sgolay 平滑方法)
        //普通滑动平均
        public double[] smooth(double[] in_array, int span, int num_of_elements)
        {
            double[] out_array = new double[num_of_elements];
            double sum;
            int i;
            int c;

            //序列开头
            for (i = 0; i < span / 2; i++)
            {
                sum = 0;
                for (c = 1; c <= span / 2; c++)         //第i点数据右边半个span求和
                    sum += in_array[i + c];
                for (c = 1; c <= span / 2; c++)     // 第i点数据左边半个span求和，由于左边索引将小于0，所以求绝对值，回卷
                    sum += in_array[Math.Abs(i - c)];

                sum = sum + in_array[i];//再加中间点		
                out_array[i] = sum / (span + 1);  //求平均	
            }

            //序列中间	
            for (i = span / 2; i < (num_of_elements - span / 2); i++)//共num_of_elements个数据点
            {
                sum = 0;

                for (c = 1; c <= span / 2; c++) //第i点数据右边半个span求和
                    sum += in_array[i + c];
                for (c = 1; c <= span / 2; c++)// 第i点数据左边半个span求和
                    sum += in_array[i - c];

                sum = sum + in_array[i];//再加中间点		
                out_array[i] = sum / (span + 1);  //求平均
            }

            //序列尾部	   
            for (i = (num_of_elements - span / 2); i < num_of_elements; i++)//共num_of_elements个数据点
            {
                sum = 0;
                for (c = 1; c <= span / 2; c++)// 左边半个span求和
                    sum += in_array[i - c];
                sum = sum + in_array[i];//再加中间点		
                out_array[i] = sum / (span / 2 + 1);  //求平均
            }
            return out_array;
        }

        //5点sgolay 平滑
        public double[] smooth_sgolay_5x(double[] in_array, int num_of_elements)
        {
            int length = in_array.Length;
            double[] out_array = new double[length];
            int i, j;

            for (i = 0; i < 2; i++)
            {
                out_array[i] = 0.0;
                for (j = 0; j < 5; j++)
                {
                    out_array[i] += q_s_5x[i, j] * in_array[j];
                }
            }

            for (i = 2; i < (num_of_elements - 2); i++)
            {
                out_array[i] = 0.0;
                for (j = 0; j < 5; j++)
                {
                    out_array[i] += q_m_5x[j] * in_array[i + 2 - j];
                }
            }

            for (i = (num_of_elements - 2); i < num_of_elements; i++)
            {
                out_array[i] = 0.0;
                for (j = 0; j < 5; j++)
                {
                    out_array[i] += q_e_5x[i - (num_of_elements - 2), j] * in_array[num_of_elements - 5 + j];
                }
            }
            out_array[length - 1] = in_array[length - 1];
            return out_array;
        }

        private void label32_Click(object sender, EventArgs e)
        {

        }

        private void textBox_GasPortReceive_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel_work_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox_path_num_TextChanged(object sender, EventArgs e)
        {

        }

        private void label54_Click(object sender, EventArgs e)
        {

        }

        private void button_L_get_Click(object sender, EventArgs e)
        {

        }

        //获取气量系数
        private void button_G_get_Click(object sender, EventArgs e)
        {
            string aa = null;
            string bb = null;
            string cc = null;
            for (int i = 0; i < calc_gas_coe_s.Length; i++)
            {
                aa = aa + calc_gas_coe_s[i].ToString() + ',';
                bb = bb + calc_gas_coe_m[i].ToString() + ',';
                cc = cc + calc_gas_coe_l[i].ToString() + ',';
            }
            aa = aa.Remove(aa.Length - 1);
            bb = bb.Remove(bb.Length - 1);
            cc = cc.Remove(cc.Length - 1);
            textBox_G_low.Text = aa;
            textBox_G_middle.Text = bb;
            textBox_G_high.Text = cc;
        }

        //获取液量系数
        private void button_L_get_Click_1(object sender, EventArgs e)
        {
            string aa = null;
            string bb = null;
            string cc = null;
            string dd = null;
            for (int i = 0; i < three_phase_coe_all.Length; i++)
            {
                aa = aa + three_phase_coe_all[i].ToString() + ',';
                bb = bb + three_phase_coe_s[i].ToString() + ',';
                cc = cc + three_phase_coe_m[i].ToString() + ',';
                dd = dd + three_phase_coe_l[i].ToString() + ',';
            }
            aa = aa.Remove(aa.Length - 1);
            bb = bb.Remove(bb.Length - 1);
            cc = cc.Remove(cc.Length - 1);
            dd = dd.Remove(dd.Length - 1);
            textBox_L_all.Text = aa;
            textBox_L_low.Text = bb;
            textBox_L_middle.Text = cc;
            textBox_L_high.Text = dd;
        }

        //修改气量系数
        private void button_G_set_Click(object sender, EventArgs e)
        {
            string[] value = new string[3];
            value[0] = textBox_G_low.Text;
            value[1] = textBox_G_middle.Text;
            value[2] = textBox_G_high.Text;

            for (int i = 0; i < 3; i++)
            {
                bool WriteIniDat = ClassIniFile.WriteIniData("coefficienct", coe_name[i+4], value[i], path);
            }

            string[] strArray0 = value[0].Split(',');
            for (int i = 0; i < strArray0.Length; i++)
            {
                calc_gas_coe_s[i] = Convert.ToDouble(strArray0[i]);
            }
            string[] strArray1 = value[1].Split(',');
            for (int i = 0; i < strArray1.Length; i++)
            {
                calc_gas_coe_m[i] = Convert.ToDouble(strArray1[i]);
            }
            string[] strArray2 = value[2].Split(',');
            for (int i = 0; i < strArray2.Length; i++)
            {
                calc_gas_coe_l[i] = Convert.ToDouble(strArray2[i]);
            }
            //MessageBox.Show("系数修改完成");
        }

        //修改液量系数
        private void button_L_set_Click(object sender, EventArgs e)
        {
            string[] value = new string[4];
            value[0] = textBox_L_all.Text;
            value[1] = textBox_L_low.Text;
            value[2] = textBox_L_middle.Text;
            value[3] = textBox_L_high.Text;

            for (int i = 0; i < 4; i++)
            {
                bool WriteIniDat = ClassIniFile.WriteIniData("coefficienct", coe_name[i], value[i], path);
            }
            string[] strArray0 = value[0].Split(',');
            for (int i = 0; i < strArray0.Length; i++)
            {
                three_phase_coe_all[i] = Convert.ToDouble(strArray0[i]);
            }
            string[] strArray1 = value[1].Split(',');
            for (int i = 0; i < strArray1.Length; i++)
            {
                three_phase_coe_s[i] = Convert.ToDouble(strArray1[i]);
            }
            string[] strArray2 = value[2].Split(',');
            for (int i = 0; i < strArray2.Length; i++)
            { 
                three_phase_coe_m[i] = Convert.ToDouble(strArray2[i]);
            }
            string[] strArray3 = value[3].Split(',');
            for (int i = 0; i < strArray3.Length; i++)
            {
                three_phase_coe_l[i] = Convert.ToDouble(strArray3[i]);
            }
            //MessageBox.Show("系数修改完成");
        }

        private void button_L_coe_clear_Click(object sender, EventArgs e)
        {
            textBox_L_all.Text =" ";
            textBox_L_low.Text = " ";
            textBox_L_middle.Text = " ";
            textBox_L_high.Text = " ";
        }

        private void button_G_clear_Click(object sender, EventArgs e)
        {
            textBox_G_low.Text = " ";
            textBox_G_middle.Text = " ";
            textBox_G_high.Text = " ";
        }

        private void textBox_data_num_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void skinButton_sm_time_Click(object sender, EventArgs e)
        {
            User_Sm_Time = Convert.ToDouble(textBox_sm_time.Text);
            User_idx = User_Sm_Time;
            ClassIniFile.WriteIniData("setting", "smoothtime", textBox_sm_time.Text, path);
        }

        //定时器自动重启网格
        
        //int mesh_restart_flag = 1;
        //private void timer_mesh_restart_Tick(object sender, EventArgs e)
        //{  
            /*
            if (mesh_restart_flag++ == 2)
            {
                mesh_restart_flag = 1;
                skinButton_startUSB.PerformClick();
                mesh_flag = 1;
                timer_mesh_restart.Stop();
            }
            */
        //}

        private void 管理员模式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (button2.Enabled == false)
            {
                button2.Enabled = true;
                button5.Enabled = true;
                button1.Enabled = true;
                checkBox_save_result.Enabled = true;
                skinButton_sm_time.Enabled = true;
            }
            else
            {
                button2.Enabled = false;
                button5.Enabled = false;
                button1.Enabled = false;
                checkBox_save_result.Enabled = false;
                skinButton_sm_time.Enabled = false;
            }
        }


        //23点sgolay 平滑
        public double[] smooth_sgolay_23x(double[] in_array, int num_of_elements)
        {
            int length = in_array.Length;
            double[] out_array = new double[length];
            int i, j;

            for (i = 0; i < 11; i++)
            {
                out_array[i] = 0.0;
                for (j = 0; j < 23; j++)
                {
                    out_array[i] += q_s_23x[i, j] * in_array[j];
                }
            }

            for (i = 11; i < (num_of_elements - 11); i++)
            {
                out_array[i] = 0.0;
                for (j = 0; j < 23; j++)
                {
                    out_array[i] += q_m_23x[j] * in_array[i + 11 - j];
                }
            }

            for (i = (num_of_elements - 11); i < num_of_elements; i++)
            {
                out_array[i] = 0.0;
                for (j = 0; j < 23; j++)
                {
                    out_array[i] += q_e_23x[i - (num_of_elements - 11), j] * in_array[num_of_elements - 23 + j];
                }
            }
            out_array[length - 1] = in_array[length - 1];
            return out_array;
        }
        #endregion

        //临时功能：读取txt数据计算气速度和气面积
        private void skinButton_calc_speed_Click(object sender, EventArgs e)
        {
            save_from_txt = true;   //保存速度数据至txt文件的标志
            //speed_check_file_name = "check_1";
            string tbstr = textBox_filename.Text;
            speed_check_file_name = tbstr.Substring(0, tbstr.LastIndexOf('.'));
            string mesh_bufferPath = query_dir_his + "地面计量测试数据\\" + speed_check_file_name + ".mfd";

            FileStream fs = new FileStream(mesh_bufferPath, FileMode.Open);

            //获取文件大小
            long size = fs.Length;   //size=64*2*2*8192=2097152
            int frame_count = (int)size / 128;   //=16348=8192*2,将64个点平均后的总长度
            int count = frame_count / 2;   //=8192,单独一个网格数据的总长度
            int win_count = count / 16;   //=512,以16个数据点为一个单位窗口划分后的长度


            byte[] array = new byte[size];
            //将文件读到byte数组中
            fs.Read(array, 0, array.Length);
            fs.Close();
            int[] ave_64 = new int[frame_count];

            //按指定格式对数据进行处理
            for (int i = 0; i < size; i = i + 128)
            {
                for (int k = 1; k < 6; k = k + 2)
                {
                    array[i + k] = (byte)(array[i + k] & 0x0f);
                }
            }

            //将每帧数据的64个点平均为1个点
            for (int k = 0; k < frame_count; k++)
            {
                int buf_sum = 0;
                for (int i = 1; i < 128; i = i + 2)
                {
                    int buf = array[k * 128 + i] * 256 + array[k * 128 + i - 1];
                    buf_sum = buf_sum + buf;
                }
                ave_64[k] = buf_sum / 64;
            }

            int[] sa_array = new int[count];
            int[] sb_array = new int[count];
            int sai = 0;
            int sbi = 0;

            //奇数位置为sa
            for (int k = 1; k < frame_count; k = k + 2)
            {
                sa_array[sai] = ave_64[k];
                sai = sai + 1;
            }

            //偶数位置为sb
            for (int k = 0; k < frame_count; k = k + 2)
            {
                sb_array[sbi] = ave_64[k];
                sbi = sbi + 1;
            }

            int[] sa_array_win = new int[16];
            int[] sb_array_win = new int[16];

            for (int wi = 0; wi < win_count; wi++)
            {
                for (int j = 0; j < 16; j++)
                {
                    sa_array_win[j] = sa_array[wi * 16 + j];
                    sb_array_win[j] = sb_array[wi * 16 + j];
                }

                Gas_Liquid_Init();   //气面积计算参数初始化
                Flow_Regime_Identify(sa_array_win, sb_array_win);   //获得滑动窗口数据，流型识别
                Gas_Speed_Area_Calc();   //计算气速度和气面积
            }
            save_from_txt = false;

        }

        //unit整型的字符串转换为16进制字节数据，1个整型对应4个字节(高位→低位)
        public string uintTOstring(string para)
        {
            string txt = string.Empty;
            uint x;
            int count = 4;

            unsafe
            {
                if (uint.TryParse(para, out x))
                {
                    byte* p = (byte*)&x;

                    while (count > 0)
                    {
                        if (txt != string.Empty)
                        {
                            txt = " " + txt;
                        }
                        txt = (*p).ToString("X2") + txt;
                        //txt = txt + (*p).ToString("X2");
                        // if (*p == '\0')
                        //  {
                        //      txt = txt + "0";
                        //  }

                        p++;
                        count--;
                    }
                    return txt;
                }
                else
                {
                    return txt;
                }
            }
        }

        //获取CRC校验码
        public short CRC16(byte[] CRCArray, int arrayLength)
        {
            byte CRCHigh = 0xFF;
            byte CRCLow = 0xFF;
            byte index;
            int i = 0;
            while (arrayLength-- > 0)
            {
                index = (byte)(CRCHigh ^ CRCArray[i++]);
                CRCHigh = (byte)(CRCLow ^ ArrayCRCHigh[index]);
                CRCLow = ArrayCRCLow[index];
            }
            return (short)(CRCHigh << 8 | CRCLow);
        }

        //检查CRC校验的正确性
        public void check_crc16()
        {
            string[] _lstInt16 = { "01","03","28" ,"33", "C8" ,"C0", "F6" ,"D5" ,"6E" ,"46","41","FF", "4B", "44", "79", "50", "35", "41", "58", "00",
                "00", "41","40","32","78","42","9C","00","00","00","00","00","00","00","00","46",
                "33","41","22","01","00","00","00" };//待测16进制数组
            byte[] _lstByte = new byte[43];

            string[] _lstInt16_2 = { "03", "06", "00", "02", "08", "33" };
            int bl = _lstInt16_2.Length;
            byte[] _lstByte_2 = new byte[bl];

            for (int i = 0; i < _lstInt16_2.Length; i++)
            {
                _lstByte_2[i] = Convert.ToByte(_lstInt16_2[i], 16);
            }
            short _returnValue = CRC16(_lstByte_2, _lstByte_2.Length);
            string _str = Convert.ToString(_returnValue, 16).ToUpper();
            MessageBox.Show(_str);
        }

        private void timer_mesh_error_check_Tick(object sender, EventArgs e)
        {
            if (mesh_error == 1)
            {
                button1.PerformClick();
                mesh_error = 0;
            } 

        }

        #region 继电器端口收发数据
        //网格断电上电重启
        /*
        * mesh_error_flag
        private static System.Timers.Timer runonce_open_usb;
        private void skinButton_startUSB_Click(object sender, EventArgs e)
        {
        port_Cw_Write(port_GasMeter, "A0 01 00 A1");

        runonce_open_usb = new System.Timers.Timer(1000);
        runonce_open_usb.Elapsed += new ElapsedEventHandler(OnTimedEvent);

        runonce_open_usb.AutoReset = false;
        runonce_open_usb.Start();
        }
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
        Console.WriteLine("触发的事件发生在： {0}", e.SignalTime);
        }
        */
        private static System.Timers.Timer runonce_close_relay;
        private static System.Timers.Timer runonce_start_usb;
        private void button1_Click(object sender, EventArgs e)
        {
            //check_crc16();
            //open_GasMeterPort();
            //if (mesh_flag == 1)
            //{
            //skinButton_stopUSB.PerformClick();
            //mesh_flag = 0;
            //timer_mesh_restart.Start();
            //}
            skinButton_stopUSB.PerformClick();
            port_Cw_Write(port_GasMeter, "A0 01 01 A2");
            runonce_close_relay = new System.Timers.Timer(1000);
            runonce_close_relay.Elapsed += new ElapsedEventHandler(OnTimedEvent_close_relay);
            runonce_close_relay.AutoReset = false;
            runonce_close_relay.Start();
        }
        private void OnTimedEvent_close_relay(object source, ElapsedEventArgs e)
        {
            port_Cw_Write(port_GasMeter, "A0 01 00 A1");
            runonce_start_usb = new System.Timers.Timer(1000);
            runonce_start_usb.Elapsed += new ElapsedEventHandler(OnTimedEvent_start_usb);
            runonce_start_usb.AutoReset = false;
            runonce_start_usb.Start();
        }
        private void OnTimedEvent_start_usb(object source, ElapsedEventArgs e)
        {
            // skinButton_startUSB.PerformClick();
            set_mesh_chart();//网格均值图属性设置
            drawChart_mesh = new drawCurveDelegate(drawcurve_mesh);//委托

            //USB
            usbDevices = new USBDeviceList(CyConst.DEVICES_CYUSB);
            //MessageBox.Show(usbDevices.Count.ToString());
            //MessageBox.Show(usbDevices[0].FriendlyName);
            MyDevice = usbDevices[0] as CyUSBDevice;
            if (MyDevice != null)
            {
                CtrlEndPt = MyDevice.ControlEndPt;
                //inEndPt = MyDevice.BulkInEndPt;
                inEndPt = MyDevice.EndPointOf(0x82) as CyBulkEndPoint;
            }

            usb_exist = usb_start_or_stop(0x01); //0x01，打开usb命令
            usb_status = true;
            label_mesh_flag.BackColor = Color.Red;   //红色指示

            frame_count = 0;
            if (tXfers == null)
            {
                tXfers = new Thread(new ThreadStart(TransfersThread_Mesh));
                tXfers.IsBackground = true;
                tXfers.Start();
            }

        }

        //网格断电
        private void button2_Click(object sender, EventArgs e)
        {
            // port_Cw_Write(port_GasMeter, "01 03 00 00 00 14 45 C5");
            port_Cw_Write(port_GasMeter, "A0 01 01 A2");
            //port_GasMeter.DataReceived += new SerialDataReceivedEventHandler(Com_GasMeter_DataReceived);
        }

        //网格上电
        private void button5_Click(object sender, EventArgs e)
        {
            port_Cw_Write(port_GasMeter, "A0 01 00 A1");
        }

        //(方法)打开继电器端口
        public void open_GasMeterPort()
        {
            if (port_GasMeter.IsOpen == false)
            {
                if (comboBox_portGasMeter.Text != "")
                {
                    port_GasMeter.PortName = comboBox_portGasMeter.Text;   //端口名
                    ClassIniFile.WriteIniData("port", "port_GasMeter", port_GasMeter.PortName, path);

                    port_GasMeter.BaudRate = 9600;   //选择波特率
                    port_GasMeter.Parity = Parity.None;    //选择奇偶校验位
                    port_GasMeter.DataBits = 8;   //选择数据位
                    port_GasMeter.StopBits = StopBits.One;   //选择停止位
                    port_GasMeter.Encoding = Encoding.Default;   //选择编码方式

                    try
                    {
                        //打开气表端口
                        port_GasMeter.Open();
                        port_GasMeter.DiscardInBuffer();
                        port_GasMeter.DiscardOutBuffer();
                        port_GasMeter_status = true;   //指示气表端口为打开状态
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "未能成功打开气表端口", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else if (comboBox_portGasMeter.Text == "")
                {
                    MessageBox.Show("未选择气表端口，请选择后重新打开");
                }
            }
            else
            {
                MessageBox.Show("气表端口已打开，重复打开无效！");
            }
        }

        //(方法)气表端口 通信，接收数据
        public void Com_GasMeter_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            System.IO.Ports.SerialPort sp_gas = sender as System.IO.Ports.SerialPort;

            if (sp_gas != null)
            {
                // 临时缓冲区将保存串口缓冲区的所有数据
                int bytesToRead_gas = sp_gas.BytesToRead;
                byte[] tempBuffer_gas = new byte[bytesToRead_gas];

                // 将缓冲区所有字节读取出来
                sp_gas.Read(tempBuffer_gas, 0, bytesToRead_gas);
                paraBuffer_gas.AddRange(tempBuffer_gas);


                if (bytesToRead_gas == 9)
                {
                    byte[] ParaBuffer_gas_byte = paraBuffer_gas.ToArray();
                    //接收到CRC校验码字符串
                    string CRCStr_Send = ParaBuffer_gas_byte[7].ToString("X2").PadLeft(2, '0').ToUpper() + " " + ParaBuffer_gas_byte[8].ToString("X2").PadLeft(2, '0').ToUpper();

                    byte[] Crc_byte_array = new byte[7];
                    Array.Copy(ParaBuffer_gas_byte, 0, Crc_byte_array, 0, 7);  //截取前7个字节进行CRC校验计算
                    short CRCCheckValue = CRC16(Crc_byte_array, 7);   //CRC校验码计算，并将校验码转化为字符串
                    string CRCCheckValue_str = Convert.ToString(CRCCheckValue, 16).PadLeft(4, '0').ToUpper();
                    string CRCStr_rece = CRCCheckValue_str.Substring(0, 2) + " " + CRCCheckValue_str.Substring(2, 2);

                    //如果接收到的CRC校验码与根据字节数据计算出的CRC校验码相同，表明数据接收正确，进行下一步处理，反之不予处理
                    if (CRCStr_Send == CRCStr_rece)
                    {

                        gm_buf = ((tempBuffer_gas[3] * 256 + tempBuffer_gas[4]) * 65536) + tempBuffer_gas[5] * 256 + tempBuffer_gas[6];  //气表采集值
                        gm_value = gm_buf / 1000;   //气表数据
                        BeginInvoke(new MethodInvoker(delegate
                        {
                            textBox_GasPortReceive.Clear();
                            textBox_GasPortReceive.AppendText(gm_value.ToString());
                        }));
                        
                    }
                }
                //实现数据的解码与显示
                if (bytesToRead_gas != 0)
                {
                   // AddData(tempBuffer_gas, file_name, textBox_GasPortReceive, rece_i_cw);
                }

                //接收到一帧数据的长度为45时才进行处理，否则数据接收有误，不予处理
                if (paraBuffer_gas.Count >= 45)
                {
                    byte[] paraBuffer_gas_byte = paraBuffer_gas.ToArray();
                    //接收到CRC校验码字符串
                    string CRCStr_send = paraBuffer_gas_byte[43].ToString("X2").PadLeft(2, '0').ToUpper() + " " + paraBuffer_gas_byte[44].ToString("X2").PadLeft(2, '0').ToUpper();

                    byte[] crc_byte_array = new byte[43];
                    Array.Copy(paraBuffer_gas_byte, 0, crc_byte_array, 0, 43);  //截取前43个字节进行CRC校验计算
                    short CRCCheckValue = CRC16(crc_byte_array, 43);   //CRC校验码计算，并将校验码转化为字符串
                    string CRCCheckValue_str = Convert.ToString(CRCCheckValue, 16).PadLeft(4, '0').ToUpper();
                    string CRCStr_rece = CRCCheckValue_str.Substring(0, 2) + " " + CRCCheckValue_str.Substring(2, 2);

                    //如果接收到的CRC校验码与根据字节数据计算出的CRC校验码相同，表明数据接收正确，进行下一步处理，反之不予处理
                    if (CRCStr_send == CRCStr_rece)
                    {
                        byte[] temp_byte_array = new byte[4];
                        byte[] temp_byte_array_re = new byte[4];
                        Array.Copy(paraBuffer_gas_byte, 3, temp_byte_array, 0, 4);
                        temp_byte_array_re = recovery_squence(temp_byte_array);
                        float temp = BitConverter.ToSingle(temp_byte_array_re, 0);

                        BeginInvoke(new MethodInvoker(delegate
                        {
                            textBox_GasPortReceive.AppendText(temp.ToString() + "\r\n");
                        }));
                    }
                    paraBuffer_gas.Clear();
                }
            }
        }

        //恢复低位-高位排列次序（气表端口发送回来的4字节数据顺序为3 4 1 2）
        public byte[] recovery_squence(byte[] array_in)
        {
            byte[] array_return = new byte[4];
            byte[] array_return_tmp = new byte[4];
            int length = array_in.Length;
            if (length == 4)
            {
                array_return[0] = array_in[2];
                array_return[1] = array_in[3];
                array_return[2] = array_in[0];
                array_return[3] = array_in[1];

                array_return_tmp[0] = array_return[0];
                array_return_tmp[1] = array_return[1];
                array_return_tmp[2] = array_return[2];
                array_return_tmp[3] = array_return[3];

                array_return[0] = array_return_tmp[3];
                array_return[1] = array_return_tmp[2];
                array_return[2] = array_return_tmp[1];
                array_return[3] = array_return_tmp[0];
            }
            return array_return;
        }
        #endregion

        #region 油表端口处理
        // 打开油表端口
        private void button3_Click(object sender, EventArgs e)
        {
            open_OilMeterPort();
        }

        //发送并接收数据
        private void button4_Click(object sender, EventArgs e)
        {
            // port_Cw_Write(port_GasMeter, "01 03 00 00 00 14 45 C5");
            port_Cw_Write(port_OilMeter, "01 03 01 02 00 02 64 37");
            port_OilMeter.DataReceived += new SerialDataReceivedEventHandler(Com_OilMeter_DataReceived);
        }
        //(方法)打开油表端口
        public void open_OilMeterPort()
        {
            if (port_OilMeter.IsOpen == false)
            {
                if (comboBox_portOilMeter.Text != "")
                {
                    port_OilMeter.PortName = comboBox_portOilMeter.Text;   //端口名

                    port_OilMeter.BaudRate = 9600;   //选择波特率
                    port_OilMeter.Parity = Parity.None;    //选择奇偶校验位
                    port_OilMeter.DataBits = 8;   //选择数据位
                    port_OilMeter.StopBits = StopBits.One;   //选择停止位
                    port_OilMeter.Encoding = Encoding.Default;   //选择编码方式

                    try
                    {
                        //打开油表端口
                        port_OilMeter.Open();
                        port_OilMeter.DiscardInBuffer();
                        port_OilMeter.DiscardOutBuffer();
                        port_OilMeter_status = true;   //指示油表端口为打开状态
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "未能成功打开油表端口", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else if (comboBox_portOilMeter.Text == "")
                {
                    MessageBox.Show("未选择油表端口，请选择后重新打开");
                }
            }
            else
            {
                MessageBox.Show("油表端口已打开，重复打开无效！");
            }
        }

        //(方法)油表端口 通信，接收数据
        public void Com_OilMeter_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            System.IO.Ports.SerialPort sp_oil = sender as System.IO.Ports.SerialPort;

            if (sp_oil != null)
            {
                // 临时缓冲区将保存串口缓冲区的所有数据
                int bytesToRead_oil = sp_oil.BytesToRead;
                byte[] tempBuffer_oil = new byte[bytesToRead_oil];

                // 将缓冲区所有字节读取出来
                sp_oil.Read(tempBuffer_oil, 0, bytesToRead_oil);
                paraBuffer_oil.AddRange(tempBuffer_oil);


                if (bytesToRead_oil == 9)
                {
                    byte[] ParaBuffer_oil_byte = paraBuffer_oil.ToArray();
                    //接收到CRC校验码字符串
                    string CRCStr_Send = ParaBuffer_oil_byte[7].ToString("X2").PadLeft(2, '0').ToUpper() + " " + ParaBuffer_oil_byte[8].ToString("X2").PadLeft(2, '0').ToUpper();

                    byte[] Crc_byte_array = new byte[7];
                    Array.Copy(ParaBuffer_oil_byte, 0, Crc_byte_array, 0, 7);  //截取前7个字节进行CRC校验计算
                    short CRCCheckValue = CRC16(Crc_byte_array, 7);   //CRC校验码计算，并将校验码转化为字符串
                    string CRCCheckValue_str = Convert.ToString(CRCCheckValue, 16).PadLeft(4, '0').ToUpper();
                    string CRCStr_rece = CRCCheckValue_str.Substring(0, 2) + " " + CRCCheckValue_str.Substring(2, 2);

                    //如果接收到的CRC校验码与根据字节数据计算出的CRC校验码相同，表明数据接收正确，进行下一步处理，反之不予处理
                    if (CRCStr_Send == CRCStr_rece)
                    {
                        byte[] temp_byte_array = new byte[4];           //-----------------------测试使用--------------------------
                        Array.Copy(tempBuffer_oil, 3, temp_byte_array, 0, 4);
                        om_buf = tempBuffer_oil[3] * 16777216 + tempBuffer_oil[4] * 65536 + tempBuffer_oil[5] * 256 + tempBuffer_oil[6];  //油表采集值
                        int om_buf_exchange = tempBuffer_oil[5] * 16777216 + tempBuffer_oil[6] * 65536 + tempBuffer_oil[3] * 256 + tempBuffer_oil[4];
                        /* byte[] bytes = new byte[4];
                         bytes[0] = tempBuffer_oil[3];
                         bytes[1] = tempBuffer_oil[4];
                         bytes[2] = tempBuffer_oil[5];
                         bytes[3] = tempBuffer_oil[6];
                         */
                        /*
                        bytes[0] = 0x40;
                        bytes[1] = 0x10;
                        bytes[2] = 0x00;
                        bytes[3] = 0x00;
                       float m = BitConverter.ToSingle(bytes, 0);
                       */
                        /*
                                                string ydl = "40100000";//16进制字符串
                                                UInt32 x = Convert.ToUInt32(ydl, 16);//字符串转16进制32位无符号整数
                                                float fy = BitConverter.ToSingle(BitConverter.GetBytes(x), 0);//IEEE754 字节转换float
                        *//*
                        string s1 = "3e051eb8";
                        UInt32 x = Convert.ToUInt32(s1, 16);//字符串转16进制32位无符号整数
                        byte[] doubleBuff = BitConverter.GetBytes(x);
                        float fy = BitConverter.ToSingle(doubleBuff, 0);//IEEE754 字节转换float
                        *//*
                        byte[] f = BitConverter.GetBytes(om_buf);
                        float fy = BitConverter.ToSingle(f, 0);//IEEE754 字节转换float
                        */
                          //s1 = om_buf.ToString();
                        UInt32 om_buf_exchangeUInt32 = (UInt32)om_buf_exchange;
                        float fy = BitConverter.ToSingle(BitConverter.GetBytes(om_buf_exchangeUInt32), 0);//IEEE754 字节转换float
                        om_value = fy;   //油表数据
                        BeginInvoke(new MethodInvoker(delegate
                        {
                            textBox1.Clear();
                            textBox1.AppendText(om_value.ToString());
                        }));
                    }
                }/*
                //实现数据的解码与显示
                if (bytesToRead_oil != 0)
                {
                    AddData(tempBuffer_oil, file_name, textBox_GasPortReceive, rece_i_cw);
                }

                //接收到一帧数据的长度为45时才进行处理，否则数据接收有误，不予处理
                if (paraBuffer_oil.Count >= 45)
                {
                    byte[] paraBuffer_oil_byte = paraBuffer_oil.ToArray();
                    //接收到CRC校验码字符串
                    string CRCStr_send = paraBuffer_oil_byte[43].ToString("X2").PadLeft(2, '0').ToUpper() + " " + paraBuffer_oil_byte[44].ToString("X2").PadLeft(2, '0').ToUpper();

                    byte[] crc_byte_array = new byte[43];
                    Array.Copy(paraBuffer_oil_byte, 0, crc_byte_array, 0, 43);  //截取前43个字节进行CRC校验计算
                    short CRCCheckValue = CRC16(crc_byte_array, 43);   //CRC校验码计算，并将校验码转化为字符串
                    string CRCCheckValue_str = Convert.ToString(CRCCheckValue, 16).PadLeft(4, '0').ToUpper();
                    string CRCStr_rece = CRCCheckValue_str.Substring(0, 2) + " " + CRCCheckValue_str.Substring(2, 2);

                    //如果接收到的CRC校验码与根据字节数据计算出的CRC校验码相同，表明数据接收正确，进行下一步处理，反之不予处理
                    if (CRCStr_send == CRCStr_rece)
                    {
                        byte[] temp_byte_array = new byte[4];
                        byte[] temp_byte_array_re = new byte[4];
                        Array.Copy(paraBuffer_oil_byte, 3, temp_byte_array, 0, 4);
                        temp_byte_array_re = recovery_oilsquence(temp_byte_array);
                        float temp = BitConverter.ToSingle(temp_byte_array_re, 0);

                        BeginInvoke(new MethodInvoker(delegate
                        {
                            textBox_GasPortReceive.AppendText(temp.ToString() + "\r\n");
                        }));
                    }
                    paraBuffer_gas.Clear();
                }*/
            }
        }

        //恢复低位-高位排列次序（油表端口发送回来的4字节数据顺序为3 4 1 2转为1 2 3 4）
        public byte[] recovery_oilsquence(byte[] array_in)
        {
            byte[] array_return = new byte[4];
            // byte[] array_return_tmp = new byte[4];
            int length = array_in.Length;
            if (length == 4)
            {
                array_return[0] = array_in[2];
                array_return[1] = array_in[3];
                array_return[2] = array_in[0];
                array_return[3] = array_in[1];
                /*
                array_return_tmp[0] = array_return[0];
                array_return_tmp[1] = array_return[1];
                array_return_tmp[2] = array_return[2];
                array_return_tmp[3] = array_return[3];

                array_return[0] = array_return_tmp[3];
                array_return[1] = array_return_tmp[2];
                array_return[2] = array_return_tmp[1];
                array_return[3] = array_return_tmp[0];*/
            }
            return array_return;
        }
        #endregion

        #region 大量程油表端口处理
        //打开大量程油表端口
        private void button_OpenBigOilMeterPort_Click(object sender, EventArgs e)
        {
            open_BigOilMeterPort();
        }

        //(方法)打开大量程油表端口
        public void open_BigOilMeterPort()
        {
            if (port_BigOilMeter.IsOpen == false)
            {
                if (comboBox_portBigOilMeter.Text != "")
                {
                    port_BigOilMeter.PortName = comboBox_portBigOilMeter.Text;   //端口名

                    port_BigOilMeter.BaudRate = 9600;   //选择波特率
                    port_BigOilMeter.Parity = Parity.None;    //选择奇偶校验位
                    port_BigOilMeter.DataBits = 8;   //选择数据位
                    port_BigOilMeter.StopBits = StopBits.One;   //选择停止位
                    port_BigOilMeter.Encoding = Encoding.Default;   //选择编码方式

                    try
                    {
                        //打开大量程油表端口
                        port_BigOilMeter.Open();
                        port_BigOilMeter.DiscardInBuffer();
                        port_BigOilMeter.DiscardOutBuffer();
                        port_BigOilMeter_status = true;   //指示大量程油表表端口为打开状态
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "未能成功打开大量程油表端口", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else if (comboBox_portBigOilMeter.Text == "")
                {
                    MessageBox.Show("未选择大量程油表端口，请选择后重新打开");
                }
            }
            else
            {
                MessageBox.Show("大量程油表端口已打开，重复打开无效！");
            }
        }
        //(方法)大量程油表端口 通信，接收数据
        public void Com_BigOilMeter_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            System.IO.Ports.SerialPort sp_bigoil = sender as System.IO.Ports.SerialPort;

            if (sp_bigoil != null)
            {
                // 临时缓冲区将保存串口缓冲区的所有数据
                int bytesToRead_bigoil = sp_bigoil.BytesToRead;
                byte[] tempBuffer_bigoil = new byte[bytesToRead_bigoil];

                // 将缓冲区所有字节读取出来
                sp_bigoil.Read(tempBuffer_bigoil, 0, bytesToRead_bigoil);
                paraBuffer_bigoil.AddRange(tempBuffer_bigoil);


                if (bytesToRead_bigoil == 9)
                {
                    byte[] ParaBuffer_bigoil_byte = paraBuffer_bigoil.ToArray();
                    //接收到CRC校验码字符串
                    string CRCStr_Send = ParaBuffer_bigoil_byte[7].ToString("X2").PadLeft(2, '0').ToUpper() + " " + ParaBuffer_bigoil_byte[8].ToString("X2").PadLeft(2, '0').ToUpper();

                    byte[] Crc_byte_array = new byte[7];
                    Array.Copy(ParaBuffer_bigoil_byte, 0, Crc_byte_array, 0, 7);  //截取前7个字节进行CRC校验计算
                    short CRCCheckValue = CRC16(Crc_byte_array, 7);   //CRC校验码计算，并将校验码转化为字符串
                    string CRCCheckValue_str = Convert.ToString(CRCCheckValue, 16).PadLeft(4, '0').ToUpper();
                    string CRCStr_rece = CRCCheckValue_str.Substring(0, 2) + " " + CRCCheckValue_str.Substring(2, 2);

                    //如果接收到的CRC校验码与根据字节数据计算出的CRC校验码相同，表明数据接收正确，进行下一步处理，反之不予处理
                    if (CRCStr_Send == CRCStr_rece)
                    {
                        byte[] temp_byte_array = new byte[4];           //-----------------------测试使用--------------------------
                        Array.Copy(tempBuffer_bigoil, 3, temp_byte_array, 0, 4);
                        bom_buf = tempBuffer_bigoil[3] * 16777216 + tempBuffer_bigoil[4] * 65536 + tempBuffer_bigoil[5] * 256 + tempBuffer_bigoil[6];  //大量程油表采集值
                        int bom_buf_exchange = tempBuffer_bigoil[5] * 16777216 + tempBuffer_bigoil[6] * 65536 + tempBuffer_bigoil[3] * 256 + tempBuffer_bigoil[4];
                        /* byte[] bytes = new byte[4];
                         bytes[0] = tempBuffer_oil[3];
                         bytes[1] = tempBuffer_oil[4];
                         bytes[2] = tempBuffer_oil[5];
                         bytes[3] = tempBuffer_oil[6];
                         */
                        /*
                        bytes[0] = 0x40;
                        bytes[1] = 0x10;
                        bytes[2] = 0x00;
                        bytes[3] = 0x00;
                       float m = BitConverter.ToSingle(bytes, 0);
                       */
                        /*
                                                string ydl = "40100000";//16进制字符串
                                                UInt32 x = Convert.ToUInt32(ydl, 16);//字符串转16进制32位无符号整数
                                                float fy = BitConverter.ToSingle(BitConverter.GetBytes(x), 0);//IEEE754 字节转换float
                        *//*
                        string s1 = "3e051eb8";
                        UInt32 x = Convert.ToUInt32(s1, 16);//字符串转16进制32位无符号整数
                        byte[] doubleBuff = BitConverter.GetBytes(x);
                        float fy = BitConverter.ToSingle(doubleBuff, 0);//IEEE754 字节转换float
                        *//*
                        byte[] f = BitConverter.GetBytes(om_buf);
                        float fy = BitConverter.ToSingle(f, 0);//IEEE754 字节转换float
                        */
                          //s1 = om_buf.ToString();
                        UInt32 bom_buf_exchangeUInt32 = (UInt32)bom_buf_exchange;
                        float fy = BitConverter.ToSingle(BitConverter.GetBytes(bom_buf_exchangeUInt32), 0);//IEEE754 字节转换float
                        bom_value = fy;   //大量程油表数据
                        BeginInvoke(new MethodInvoker(delegate
                        {
                            textBox2.Clear();
                            textBox2.AppendText(bom_value.ToString());
                        }));
                    }
                }/*
                //实现数据的解码与显示
                if (bytesToRead_oil != 0)
                {
                    AddData(tempBuffer_oil, file_name, textBox_GasPortReceive, rece_i_cw);
                }

                //接收到一帧数据的长度为45时才进行处理，否则数据接收有误，不予处理
                if (paraBuffer_oil.Count >= 45)
                {
                    byte[] paraBuffer_oil_byte = paraBuffer_oil.ToArray();
                    //接收到CRC校验码字符串
                    string CRCStr_send = paraBuffer_oil_byte[43].ToString("X2").PadLeft(2, '0').ToUpper() + " " + paraBuffer_oil_byte[44].ToString("X2").PadLeft(2, '0').ToUpper();

                    byte[] crc_byte_array = new byte[43];
                    Array.Copy(paraBuffer_oil_byte, 0, crc_byte_array, 0, 43);  //截取前43个字节进行CRC校验计算
                    short CRCCheckValue = CRC16(crc_byte_array, 43);   //CRC校验码计算，并将校验码转化为字符串
                    string CRCCheckValue_str = Convert.ToString(CRCCheckValue, 16).PadLeft(4, '0').ToUpper();
                    string CRCStr_rece = CRCCheckValue_str.Substring(0, 2) + " " + CRCCheckValue_str.Substring(2, 2);

                    //如果接收到的CRC校验码与根据字节数据计算出的CRC校验码相同，表明数据接收正确，进行下一步处理，反之不予处理
                    if (CRCStr_send == CRCStr_rece)
                    {
                        byte[] temp_byte_array = new byte[4];
                        byte[] temp_byte_array_re = new byte[4];
                        Array.Copy(paraBuffer_oil_byte, 3, temp_byte_array, 0, 4);
                        temp_byte_array_re = recovery_oilsquence(temp_byte_array);
                        float temp = BitConverter.ToSingle(temp_byte_array_re, 0);

                        BeginInvoke(new MethodInvoker(delegate
                        {
                            textBox_GasPortReceive.AppendText(temp.ToString() + "\r\n");
                        }));
                    }
                    paraBuffer_gas.Clear();
                }*/
            }
        }
        #endregion
    }
}
