#define DEBUG_ON
using MotorTest.Devices;
using System;
using System.Collections.Generic;
using System.Text;
using VoidLibrary.Utils;

namespace VoidLibrary.Devices
{

    public class DeviceTopasLap321 : Device
    {
        private const int _mb_index = 2;
        private const int _cur_config_classes_num = 58;
        // 0:256 1:128 2:64 3:32 4:16 Channels
        private const int _channel_parameters_value = 2;

        private const int _frame_lrc_char_num = 3;
        private const int _frame_terminator_len = 1;

        private string _cmd_config_channel_parameters = ":00#0201" + _channel_parameters_value;
        private string _cmd_config_unkown_function = ":00#0211" + "7"; // 到这就应该可以了 

        public string _cmd_action_independent_counter_reset = ":00#0107";

        public string _cmd_action_measurement_start = ":00#0101" + _mb_index + ",1";
        public string _cmd_action_measurement_reset = ":00#0105";
        public string _cmd_action_measurement_stop = ":00#01010,1";

        public string _cmd_query_channel_data = ":00#0305";
        public string _cmd_query_work_state = ":00#0311";

        private Dictionary<string, byte[]> _unpacked_cmd_map_packed_cmd;
        private Dictionary<string, string> _unpacked_cmd_map_func_code;

        int defaultWaitTimeMilliSeconds = 50;

        public DeviceTopasLap321(string name)
            : base(name)
        {
            this.name = name;
            _unpacked_cmd_map_packed_cmd = new Dictionary<string, byte[]>();
            _unpacked_cmd_map_func_code = new Dictionary<string, string>();
            InitPackedCmd();
        }

        internal void InitPackedCmd()
        {
            MakeUpackedCmdMapPackedCmd(_cmd_config_channel_parameters);
            MakeUpackedCmdMapPackedCmd(_cmd_config_unkown_function);
            MakeUpackedCmdMapPackedCmd(_cmd_action_independent_counter_reset);

            MakeUpackedCmdMapPackedCmd(_cmd_action_measurement_start);
            MakeUpackedCmdMapPackedCmd(_cmd_action_measurement_reset);
            MakeUpackedCmdMapPackedCmd(_cmd_query_channel_data);
            MakeUpackedCmdMapPackedCmd(_cmd_action_measurement_stop);

            MakeUpackedCmdMapPackedCmd(_cmd_query_work_state);

#if DEBUG_ON
            string frame = ":00#03054,0,1088,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0238\r";
            string no_cr = ":00#03054,0,1088,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0238";
            string no_lrc = ":00#03054,0,1088,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0237\r";
            string two_frame = ":00#03054,0,1088,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0238\r:00#03054,0,2083,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0242\r:00#0305,0,2083,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0242\r";
            string first_no_cr_second_has_cr = ":00#03054,0,1088,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0238:00#03054,0,1088,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0238\r";
            Console.WriteLine("Demo");
            PickFrameList(frame, "03054");
            Console.WriteLine("no_cr");
            PickFrameList(no_cr, "03054");
            Console.WriteLine("no_lrc");
            PickFrameList(no_lrc, "03054");
            Console.WriteLine("two");
            PickFrameList(two_frame, "03054");
            Console.WriteLine("first_no_cr_second_has_cr");
            PickFrameList(first_no_cr_second_has_cr, "03054");

            PickFrameList("0,0,0,0,0,0,0,0,0,0,0,0,0,0#03416,1,1,1,1,1,0#03211,8486#03241,1941177\r;00#031159,2,0\r:00#0105125\r", "0105");
#endif
        }

        internal byte[] PackedCmd(string unpacked_cmd)
        {
            return (byte[]) _unpacked_cmd_map_packed_cmd[unpacked_cmd];
        }

        internal string FuncCode(string unpacked_cmd)
        {
            return _unpacked_cmd_map_func_code[unpacked_cmd];
        }

        internal void MakeUpackedCmdMapPackedCmd(string unpacked_cmd)
        {
            _unpacked_cmd_map_packed_cmd.Add(unpacked_cmd, CmdWithLRC(unpacked_cmd));
            _unpacked_cmd_map_func_code.Add(unpacked_cmd, unpacked_cmd.Substring(4, 4));
        }

        internal byte[] CmdWithLRC(string ascii_str)
        {
            byte[] unpacked_cmd_bytes = Encoding.ASCII.GetBytes(ascii_str);
            int lrc = LRC.CalcLRC(unpacked_cmd_bytes);
            string ascii_lrc = lrc.ToString().PadLeft(_frame_lrc_char_num, '0');

            List<byte> packed_cmd_byte_list = new List<byte>(unpacked_cmd_bytes.Length + _frame_lrc_char_num + 1);
            packed_cmd_byte_list.AddRange(unpacked_cmd_bytes);
            packed_cmd_byte_list.AddRange(Encoding.ASCII.GetBytes(ascii_lrc));
            packed_cmd_byte_list.Add((byte)('\r'));

            byte[] packed_cmd_byte_array = packed_cmd_byte_list.ToArray();

#if DEBUG_ON
            string ascii = Encoding.ASCII.GetString(packed_cmd_byte_array, 0, packed_cmd_byte_array.Length);
            Console.WriteLine(ascii);
            Console.WriteLine(FrameIsRight(ascii.Remove(ascii.Length - 1), _frame_lrc_char_num));
#endif

            return packed_cmd_byte_array;
        }

        internal bool FrameIsRight(string ascii_str_with_lrc, int lrc_num)
        {
            bool result = false;
            if (ascii_str_with_lrc == null || ascii_str_with_lrc.Length < lrc_num)
            {
                return result;
            }

            byte[] packed_frame_bytes = Encoding.ASCII.GetBytes(ascii_str_with_lrc);
            string lrc_str = ascii_str_with_lrc.Substring(ascii_str_with_lrc.Length - lrc_num);
            int dest_lrc;
            if (!int.TryParse(lrc_str, out dest_lrc))
            {
                return result;
            }

            int real_lrc = LRC.CalcLRC(packed_frame_bytes, 0, packed_frame_bytes.Length - lrc_num);
            return real_lrc == dest_lrc;
        }

        public override bool Register()
        {
            return base.Register();
        }

        public override bool Open()
        {
            bool isOpen = base.Open();
            return isOpen;
        }

        public override bool Close()
        {
            return base.Close();
        }

        private List<string> EMPTY_LIST = new List<string>();

        public List<string> SendAndRead(string cmd, int wait_time_millis)
        {
            byte[] result = null;
            try
            {
                //this.driver.ClearInBuffer();
                //this.driver.ClearOutBuffer();
#if DEBUG_ON
                FileLog.WriteI("Send:" + Encoding.ASCII.GetString(PackedCmd(cmd)));
#endif
                this.driver.Send(PackedCmd(cmd));
                result = this.ReadWait(wait_time_millis);
            }
            catch (Exception e)
            {
                FileLog.WriteE(e.ToString());
                return EMPTY_LIST;
            }

            if (result == null || result.Length == 0)
            {
                return EMPTY_LIST;
            }
#if DEBUG_ON
            FileLog.WriteI("Result:" + Encoding.ASCII.GetString(result) + " FuncCode:" + FuncCode(cmd));
#endif
            return PickFrameList(Encoding.ASCII.GetString(result), FuncCode(cmd));
        }

        public string MeasurementStart()
        {
#if DEBUG_ON
            FileLog.WriteI("MeasurementStart:");
#endif
            List<string> result_list = SendAndRead(_cmd_action_measurement_start, 50);
            if (result_list == null || result_list.Count == 0)
            {
                return String.Empty;
            }
            return result_list[result_list.Count - 1];
        }

        public string MeasurementReset()
        {
            List<string> result_list = SendAndRead(_cmd_action_measurement_reset, 50);
            if (result_list == null || result_list.Count == 0)
            {
                return String.Empty;
            }
            return result_list[result_list.Count - 1];
        }

        public string MeasurementStop()
        {
            List<string> result_list = SendAndRead(_cmd_action_measurement_stop, 50);
            if (result_list == null || result_list.Count == 0)
            {
                return String.Empty;
            }
            return result_list[result_list.Count - 1];
        }

        public string QueryChannelData()
        {
            List<string> result_list = SendAndRead(_cmd_query_channel_data, 200);
            if (result_list == null || result_list.Count == 0)
            {
                return String.Empty;
            }
            return result_list[result_list.Count - 1];
        }

        public int QueryWorkState()
        {
            List<string> result_list = SendAndRead(_cmd_query_work_state, 50);
            if (result_list == null || result_list.Count == 0)
            {
                return -1;
            }
            string result = result_list[result_list.Count - 1];
            string str_state = result.Substring(8, result.IndexOf(',') - 8);
            int int_state;
            if (!int.TryParse(str_state, out int_state))
            {
                int_state = -1;
            }

            return int_state;
        }

        public void ActiveDeviceOrResetToStop()
        {
            int state = QueryWorkState();
            if (state == 1) // Need Init
            {
                // 没有检查是否成功
                List<string> result_list = SendAndRead(_cmd_config_channel_parameters, 50);
                result_list = SendAndRead(_cmd_config_unkown_function, 50);
                result_list = SendAndRead(_cmd_action_independent_counter_reset, 50);
            }
            else if (state != 57) // Stop
            {
                MeasurementStop();
            }
        }

        //=========================================
        // Parse Data
        //=========================================
        private StringBuilder _msg_builder = new StringBuilder();

        private byte[] _cmd_start;
        private DataFrame[] _data_frame_array = new DataFrame[] {
            new DataFrame().Head(":").RemoveNum(":00#".Length),
        };

        public int GetRangeClassesNum()
        {
            return _cur_config_classes_num;
        }

        private int FindDataFrameIndex(string msg, int start_index, out DataFrame data_frame)
        {
#if DEBUG_ON
            Console.WriteLine(msg);
#endif
            data_frame = null;
            int find_index = -1;
            int index;
            for (index = _data_frame_array.Length - 1; index >= 0; --index)
            {
                find_index = msg.IndexOf(_data_frame_array[index]._head, start_index);
                if (find_index > -1)
                {
                    data_frame = _data_frame_array[index];
#if DEBUG_ON
                    if (index == 1)
                    {
                        Console.WriteLine("Index:" + index);
                    }
#endif
                    break;
                }
            }

            return find_index;
        }

        internal List<string> PickFrameList(string msg, string function_code)
        {
            _msg_builder.Append(msg);
            List<string> result_frame_list = new List<string>();

            while (true)
            {
                string msg_str = _msg_builder.ToString();

                DataFrame data_frame;
                int head_index = FindDataFrameIndex(msg_str, 0, out data_frame);
                if (head_index < 0)
                {
                    break;
                }

                string head_str = data_frame._head;
                string function_introduction_char = data_frame._function_introduction_char;
                string tail_str = data_frame._tail;

                int remove_num = data_frame._remove_num;
                int frame_terminator_len = tail_str.Length;

                int tail_index = msg_str.IndexOf(tail_str, head_index);
                if (tail_index < 0)
                {
                    int next_head_index = FindDataFrameIndex(msg_str, head_index + head_str.Length, out data_frame);
                    if (next_head_index >= 0)
                    {
                        _msg_builder = _msg_builder.Remove(0, next_head_index);
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    // string demo = ":00#03054,0,1088,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0238:00#03054,0,1088,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0238\r";
                    int next_head_index = FindDataFrameIndex(msg_str, head_index + head_str.Length, out data_frame);
                    if (next_head_index > head_index && next_head_index < tail_index)
                    {
                        _msg_builder = _msg_builder.Remove(0, next_head_index);
                        continue;
                    }
                }

                string pick_frame = msg_str.Substring(head_index, tail_index - head_index); // not contain '\r'
                if (!FrameIsRight(pick_frame, _frame_lrc_char_num))
                {
                    _msg_builder = _msg_builder.Remove(0, tail_index + frame_terminator_len);
                    continue;
                }

                int function_introduction_char_index = pick_frame.IndexOf(function_introduction_char);
                if (function_introduction_char_index + function_introduction_char.Length + function_code.Length > pick_frame.Length)
                {
                    _msg_builder = _msg_builder.Remove(0, tail_index + frame_terminator_len);
                    continue;
                }

                string find_function_code = pick_frame.Substring(function_introduction_char_index + function_introduction_char.Length, function_code.Length);
                if (!function_code.Equals(find_function_code))
                {
                    _msg_builder = _msg_builder.Remove(0, tail_index + frame_terminator_len);
                    continue;
                }

                _msg_builder = _msg_builder.Remove(0, tail_index + frame_terminator_len);

                int remove_function_introduction_char_and_pre_char_num = function_introduction_char_index + function_introduction_char.Length;
                string unpacked_frame = pick_frame.Substring(remove_function_introduction_char_and_pre_char_num, pick_frame.Length - remove_function_introduction_char_and_pre_char_num - _frame_lrc_char_num);
#if DEBUG_ON
                Console.WriteLine("unpacked_frame:" + unpacked_frame);
                Console.WriteLine("remain_frame:" + _msg_builder.ToString());
#endif
                result_frame_list.Add(pick_frame);

#if DEBUG_ON
                FileLog.WriteI("RemainFrame:" + pick_frame);
#endif
            }

            return result_frame_list;
        }

        public class DataFrame
        {
            public string _head;
            public string _function_introduction_char = "#";
            public int _remove_num;
            public string _tail = "\r";

            public DataFrame Head(string head)
            {
                _head = head;
                return this;
            }

            public DataFrame RemoveNum(int remove_num)
            {
                _remove_num = remove_num;
                return this;
            }
        }

    }

    public class PackageFrame
    {
        public string frame;

        public string function_code;
        public string function_flag;
        public string msg;
        public string lrc;
    }

}
