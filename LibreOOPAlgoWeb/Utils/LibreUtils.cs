using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LibreOOPWeb.Utils
{
    public class LibreUtils
    {
        private static readonly long[] crc16table ={
                    0, 4489, 8978, 12955, 17956, 22445, 25910, 29887, 35912,
            40385, 44890, 48851, 51820, 56293, 59774, 63735, 4225, 264,
            13203, 8730, 22181, 18220, 30135, 25662, 40137, 36160, 49115,
            44626, 56045, 52068, 63999, 59510, 8450, 12427, 528, 5017,
            26406, 30383, 17460, 21949, 44362, 48323, 36440, 40913, 60270,
            64231, 51324, 55797, 12675, 8202, 4753, 792, 30631, 26158,
            21685, 17724, 48587, 44098, 40665, 36688, 64495, 60006, 55549,
            51572, 16900, 21389, 24854, 28831, 1056, 5545, 10034, 14011,
            52812, 57285, 60766, 64727, 34920, 39393, 43898, 47859, 21125,
            17164, 29079, 24606, 5281, 1320, 14259, 9786, 57037, 53060,
            64991, 60502, 39145, 35168, 48123, 43634, 25350, 29327, 16404,
            20893, 9506, 13483, 1584, 6073, 61262, 65223, 52316, 56789,
            43370, 47331, 35448, 39921, 29575, 25102, 20629, 16668, 13731,
            9258, 5809, 1848, 65487, 60998, 56541, 52564, 47595, 43106,
            39673, 35696, 33800, 38273, 42778, 46739, 49708, 54181, 57662,
            61623, 2112, 6601, 11090, 15067, 20068, 24557, 28022, 31999,
            38025, 34048, 47003, 42514, 53933, 49956, 61887, 57398, 6337,
            2376, 15315, 10842, 24293, 20332, 32247, 27774, 42250, 46211,
            34328, 38801, 58158, 62119, 49212, 53685, 10562, 14539, 2640,
            7129, 28518, 32495, 19572, 24061, 46475, 41986, 38553, 34576,
            62383, 57894, 53437, 49460, 14787, 10314, 6865, 2904, 32743,
            28270, 23797, 19836, 50700, 55173, 58654, 62615, 32808, 37281,
            41786, 45747, 19012, 23501, 26966, 30943, 3168, 7657, 12146,
            16123, 54925, 50948, 62879, 58390, 37033, 33056, 46011, 41522,
            23237, 19276, 31191, 26718, 7393, 3432, 16371, 11898, 59150,
            63111, 50204, 54677, 41258, 45219, 33336, 37809, 27462, 31439,
            18516, 23005, 11618, 15595, 3696, 8185, 63375, 58886, 54429,
            50452, 45483, 40994, 37561, 33584, 31687, 27214, 22741, 18780,
            15843, 11370, 7921, 3960 };

        public static readonly byte[] TestPatchAlwaysReturning63 = {
            0x3a, 0xcf, 0x10, 0x16, 0x03, 0x00, 0x00, 0x00, // 0x00 Begin of header
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, // 0x01
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, // 0x02 End of header
            0x4f, 0x11, 0x08, 0x10, 0xad, 0x02, 0xc8, 0xd4, // 0x03 Begin of body. CRC shoud be 4f 11. trendIndex: 8, historyIndex: 16
            0x5b, 0x00, 0xaa, 0x02, 0xc8, 0xb4, 0x1b, 0x80, // 0x04
            0xa9, 0x02, 0xc8, 0x9c, 0x5b, 0x00, 0xa9, 0x02, // 0x05
            0xc8, 0x8c, 0x1b, 0x80, 0xb0, 0x02, 0xc8, 0x30, // 0x06
            0x5c, 0x80, 0xb0, 0x02, 0x88, 0xe6, 0x9c, 0x80, // 0x07
            0xb8, 0x02, 0xc8, 0x3c, 0x9d, 0x80, 0xb8, 0x02, // 0x08
            0xc8, 0x60, 0x9d, 0x80, 0xa1, 0x02, 0xc8, 0xdc, // 0x09
            0x9e, 0x80, 0xab, 0x02, 0xc8, 0x14, 0x9e, 0x80, // 0x0A
            0xa9, 0x02, 0xc8, 0xc0, 0x9d, 0x80, 0xab, 0x02, // 0x0B
            0xc8, 0x78, 0x9d, 0x80, 0xaa, 0x02, 0xc8, 0x40, // 0x0C
            0x9d, 0x80, 0xa8, 0x02, 0xc8, 0x08, 0x9d, 0x80, // 0x0D
            0xa8, 0x02, 0xc8, 0x2c, 0x5c, 0x80, 0xad, 0x02, // 0x0E
            0xc8, 0xf8, 0x5b, 0x00, 0x29, 0x06, 0xc8, 0xf4, // 0x0F
            0x9b, 0x80, 0xc9, 0x05, 0xc8, 0x8c, 0xde, 0x80, // 0x10
            0xc3, 0x05, 0xc8, 0x28, 0x9e, 0x80, 0x2c, 0x06, // 0x11
            0xc8, 0xd0, 0x9e, 0x80, 0x7b, 0x06, 0x88, 0xa6, // 0x12
            0x9e, 0x80, 0xf9, 0x05, 0xc8, 0xb0, 0x9e, 0x80, // 0x13
            0x99, 0x05, 0xc8, 0xf0, 0x9e, 0x80, 0x2e, 0x05, // 0x14
            0xc8, 0x00, 0x9f, 0x80, 0x81, 0x04, 0xc8, 0x48, // 0x15
            0xa0, 0x80, 0x5d, 0x04, 0xc8, 0x38, 0x9d, 0x80, // 0x16
            0x12, 0x04, 0xc8, 0x10, 0x9e, 0x80, 0xcf, 0x03, // 0x17
            0xc8, 0x4c, 0x9e, 0x80, 0x6f, 0x03, 0xc8, 0xb8, // 0x18
            0x9e, 0x80, 0x19, 0x03, 0xc8, 0x40, 0x9f, 0x80, // 0x19
            0xc5, 0x02, 0xc8, 0xf4, 0x9e, 0x80, 0xaa, 0x02, // 0x1A
            0xc8, 0xf8, 0x5b, 0x00, 0xa2, 0x04, 0xc8, 0x38, // 0x1B
            0x9a, 0x00, 0xd1, 0x04, 0xc8, 0x28, 0x9b, 0x80, // 0x1C
            0xe4, 0x04, 0xc8, 0xe0, 0x1a, 0x80, 0x8f, 0x04, // 0x1D
            0xc8, 0x20, 0x9b, 0x80, 0x22, 0x06, 0xc8, 0x50, // 0x1E
            0x5b, 0x80, 0xbc, 0x06, 0xc8, 0x54, 0x9c, 0x80, // 0x1F
            0x7f, 0x05, 0xc8, 0x24, 0x5c, 0x80, 0xc9, 0x05, // 0x20
            0xc8, 0x38, 0x5c, 0x80, 0x38, 0x05, 0xc8, 0xf4, // 0x21
            0x1a, 0x80, 0x37, 0x07, 0xc8, 0x84, 0x5b, 0x80, // 0x22
            0xfb, 0x08, 0xc8, 0x4c, 0x9c, 0x80, 0xfb, 0x09, // 0x23
            0xc8, 0x7c, 0x9b, 0x80, 0x77, 0x0a, 0xc8, 0xe4, // 0x24
            0x5a, 0x80, 0xdf, 0x09, 0xc8, 0x88, 0x9f, 0x80, // 0x25
            0x6d, 0x08, 0xc8, 0x2c, 0x9f, 0x80, 0xc3, 0x06, // 0x26
            0xc8, 0xb0, 0x9d, 0x80, 0xd9, 0x11, 0x00, 0x00, // 0x27 End of body. Time: 4569 (0xd911 -> bytes swapped -> 0x11d9 = 4569)
            0x72, 0xc2, 0x00, 0x08, 0x82, 0x05, 0x09, 0x51, // 0x28 Beginn of footer
            0x14, 0x07, 0x96, 0x80, 0x5a, 0x00, 0xed, 0xa6, // 0x29
            0x0e, 0x6e, 0x1a, 0xc8, 0x04, 0xdd, 0x58, 0x6d  // 0x2A End of footer
        };

        // first two bytes = crc16 included in data
        private static long computeCRC16(byte[] data, int start, int size)
        {
            long crc = 0xffff;
            for (int i = start + 2; i < start + size; i++)
            {
                crc = ((crc >> 8) ^ crc16table[(int)(crc ^ (data[i] & 0xFF)) & 0xff]);
            }

            long reverseCrc = 0;
            for (int i = 0; i < 16; i++)
            {
                reverseCrc = (reverseCrc << 1) | (crc & 1);
                crc >>= 1;
            }
            return reverseCrc;
        }

        private static bool CheckCRC16(byte[] data, int start, int size)
        {
            long crc = computeCRC16(data, start, size);
            return crc == ((data[start + 1] & 0xFF) * 256 + (data[start] & 0xff));
        }


        public static long CalculateHeaderCRCs(byte[] data) => computeCRC16(data, 0, 24); 
        public static long CalculateBodyCRCs(byte[] data) => computeCRC16(data, 24, 296);
        public static long CalculateFooterCRCs(byte[] data) => computeCRC16(data, 320, 24);
       

        public static byte[] bytesWithCorrectCRC(byte[] data)
        {
            var headerCrc = CalculateHeaderCRCs(data);
            var bodyCrc = CalculateBodyCRCs(data);
            var footerCrc = CalculateFooterCRCs(data);

            byte[] copy = new byte[data.Length];
            data.CopyTo(copy, 0);

            //swift code swapped these bytes in the compute function, but we don't
            //that's why this definition below is reverse of that in swift
            byte headerCorrectedByte0 = Convert.ToByte(headerCrc & 0x00FF);
            byte headerCorrectedByte1 = Convert.ToByte(headerCrc >> 8);

            byte bodyCorrectedByte0 = Convert.ToByte(bodyCrc & 0x00FF);
            byte bodyCorrectedByte1 = Convert.ToByte(bodyCrc >> 8);

            byte footerCorrectedByte0 = Convert.ToByte(footerCrc & 0x00FF);
            byte footerCorrectedByte1 = Convert.ToByte(footerCrc >> 8);

            copy[0] = headerCorrectedByte0;
            copy[1] = headerCorrectedByte1;

            copy[24] = bodyCorrectedByte0;
            copy[25] = bodyCorrectedByte1;

            copy[320] = footerCorrectedByte0;
            copy[321] = footerCorrectedByte1;

            return copy;
        }

        public static bool verify(byte[] data)
        {
            if (data.Length < 344)
            {
                //Log.e(TAG, "Must have at least 344 bytes for libre data");
                Console.WriteLine("Must have at least 344 bytes for libre data");
                return false;
            }
            bool checksum_ok = CheckCRC16(data, 0, 24);
            checksum_ok &= CheckCRC16(data, 24, 296);
            checksum_ok &= CheckCRC16(data, 320, 24);
            return checksum_ok;
        }

        public static byte[] Create_2018_10_07_Sensor(UInt16? raw_glucose = null, UInt16? raw_temp = null)
        {
            var b64 = "hNtgFwMAAAAAAAAAAAAAAAAAAAAAAAAA+SIECI4EyPRXAKIEyOxXALUEyORXAMgEyNxXADEEyJBYADEEyIBYADEEyGxYADIEyFxYADUEyFBYADoEyEBYAEEEyDRYAEgEyChYAFIEyCBYAF4EyBRYAG4EyAxYAH4EyABYANwEyIhZAEUEyHRZAJoDyExZAEwDyFBZAEsDyMRZAKUDyOwZgLADyAhaADMEyFxYACgFyKRZAC0FyExaAN0EyCQbgKMEyLxZAAQEyPgagK4DyChaAKwDyMRZAAkEyAhZACAEyGRZAOsDyGBZACQEyJhZAPQDyPwagP0DyPBZABMEyLwbgBoEyDgbgK8DyPBbAIkDiOYbgP4DiHJdgL0EyNwbgCIGyNQagLIHyDwagFYHyORZAGoGyMBZACoFyKhZACUGAACWLgABYwddURQHloBaAO2mEHoayATReG4=";

            var temp = Convert.FromBase64String(b64);
            return CreateFakePatch(patch: temp, raw_glucose: raw_glucose, raw_temp: raw_temp);

    
        }
        public static byte[] CreateOnePatch(byte[] patch, UInt16? raw_glucose = null, UInt16? raw_temp = null)
        {
            return Create_2018_10_07_Sensor( raw_glucose, raw_temp);
        }

        public static byte[] CreateFakePatch(byte[] patch, UInt16? raw_glucose=null, UInt16? raw_temp=null)
        {
            //glucoseByte2,glucoseByte1, flag1, tempByte2, tempByte1, flag2
            var value = new byte[] { 0xff, 0x3f, 0xc8, 0xfc, 0xd8, 0x00 };
        
            if (raw_glucose != null) {
                value[0] = Convert.ToByte(raw_glucose & 0xFF);
                value[1] = Convert.ToByte(raw_glucose >> 8);
            }
            if (raw_temp != null) {
                value[3] = Convert.ToByte(raw_temp & 0xFF);
                value[4] = Convert.ToByte(raw_temp >> 8);


            }

            var modifiedPatch = new List<byte[]>();
            //changes sensorstatusbyte to ready
            patch[4] = 0x03;

            //24 bytes of header 
            modifiedPatch.Add(new ArraySegment<byte>(patch,0,24).ToArray());

            // + 4 bytes of body(crc, trend and history index
            // this could be from the original patch, but wouldn't work for sensors
            // warming up or pre-initialixing,
            // so fill it with trend and history from a known good patch
            // no matter what we will fix the crcs with bytesWithCorrectCRC()
            // anyway

            //modifiedPatch.Add(new ArraySegment<byte>(patch, 24, 4).ToArray());
            modifiedPatch.Add(new ArraySegment<byte>(LibreUtils.TestPatchAlwaysReturning63, 24, 4).ToArray());



            //fake body values
            for (var i=0; i < 48; i++)
            {
                modifiedPatch.Add(value);
            }

            //rest of data, minute counter and two zeroes
            //modifiedPatch.Add(new ArraySegment<byte>(patch, 316, 4).ToArray());
            modifiedPatch.Add(new ArraySegment<byte>(LibreUtils.TestPatchAlwaysReturning63, 316, 4).ToArray());

            //footer
            modifiedPatch.Add(new ArraySegment<byte>(patch, 320, 24).ToArray());

            var flattened = modifiedPatch.SelectMany(x => x).ToArray();



            return LibreUtils.bytesWithCorrectCRC(flattened);
        }

    }
}