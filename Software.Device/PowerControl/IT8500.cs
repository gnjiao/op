//using System;
//using System.IO.Ports;
//using System.Text;
//using System.Threading;
//using Microsoft.VisualBasic;

//namespace SvHardware
//{
//	// Token: 0x0200003F RID: 63
//	public class IT8500
//	{
//		// Token: 0x0600071F RID: 1823 RVA: 0x0002F208 File Offset: 0x0002D608
//		public IT8500()
//		{
//			this.IT8500_ReceiveData = new byte[26];
//			this.IT8500_SendData = new byte[26];
//		}

//		// Token: 0x06000720 RID: 1824 RVA: 0x0002F22C File Offset: 0x0002D62C
//		public void SerialPortInit()
//		{
//			bool isOpen = this.SerialPort.IsOpen;
//			if (isOpen)
//			{
//				this.SerialPort.Close();
//			}
//			try
//			{
//				SerialPort serialPort = this.SerialPort;
//				serialPort.PortName = "COM4";
//				serialPort.BaudRate = this.ComPort.BandRate;
//				serialPort.Parity = Parity.None;
//				serialPort.DataBits = this.ComPort.BitsNum;
//				serialPort.StopBits = (StopBits)this.ComPort.StopBit;
//				serialPort.DiscardNull = false;
//				serialPort.DtrEnable = false;
//				serialPort.Encoding = Encoding.Unicode;
//				serialPort.RtsEnable = false;
//				serialPort.ReceivedBytesThreshold = 1;
//				this.SerialPort.Open();
//			}
//			catch (Exception ex)
//			{
//				Interaction.MsgBox("没有发现此串口或串口被占用！", MsgBoxStyle.OkOnly, null);
//			}
//		}

//		// Token: 0x06000721 RID: 1825 RVA: 0x0002F314 File Offset: 0x0002D714
//		public void IT85ComTx()
//		{
//			bool flag = this.IT85TxNewFlage;
//			checked
//			{
//				if (flag)
//				{
//					this.IT8500_SendData[0] = 170;
//					this.IT8500_SendData[1] = 0;
//					this.IT8500_SendData[2] = (byte)this.IT8500_Command;
//					int i = 3;
//					int num;
//					int num2;
//					do
//					{
//						this.IT8500_SendData[i] = 0;
//						i++;
//						num = i;
//						num2 = 25;
//					}
//					while (num <= num2);
//					this.IT8500_SendDataNum = 26;
//					switch (this.IT8500_Command)
//					{
//					case 32:
//						this.IT8500_SendData[3] = this.FrontRear;
//						goto IL_4B8;
//					case 33:
//						this.IT8500_SendData[3] = this.OutputEn;
//						goto IL_4B8;
//					case 40:
//						this.IT8500_SendData[3] = this.OperationMode;
//						goto IL_4B8;
//					case 41:
//						goto IL_4B8;
//					case 42:
//						this.IT8500_SendData[3] = (byte)(this.CC_Current % 256);
//						this.IT8500_SendData[4] = (byte)((this.CC_Current >> 8) % 256);
//						this.IT8500_SendData[5] = (byte)((this.CC_Current >> 16) % 256);
//						this.IT8500_SendData[6] = (byte)((this.CC_Current >> 24) % 256);
//						goto IL_4B8;
//					case 43:
//						goto IL_4B8;
//					case 44:
//						this.IT8500_SendData[3] = (byte)(this.CC_Current % 256);
//						this.IT8500_SendData[4] = (byte)((this.CV_Voltage >> 8) % 256);
//						this.IT8500_SendData[5] = (byte)(this.CV_Voltage >> 16);
//						this.IT8500_SendData[6] = (byte)(this.CV_Voltage >> 24);
//						goto IL_4B8;
//					case 45:
//						goto IL_4B8;
//					case 46:
//						this.IT8500_SendData[3] = (byte)(this.CC_Current % 256);
//						this.IT8500_SendData[4] = (byte)((this.CW_Power >> 8) % 256);
//						this.IT8500_SendData[5] = (byte)((this.CW_Power >> 16) % 256);
//						this.IT8500_SendData[6] = (byte)((this.CW_Power >> 24) % 256);
//						goto IL_4B8;
//					case 47:
//						goto IL_4B8;
//					case 48:
//						this.IT8500_SendData[3] = (byte)(this.CC_Current % 256);
//						this.IT8500_SendData[4] = (byte)((this.CR_Register >> 8) % 256);
//						this.IT8500_SendData[5] = (byte)((this.CR_Register >> 16) % 256);
//						this.IT8500_SendData[6] = (byte)((this.CR_Register >> 24) % 256);
//						goto IL_4B8;
//					case 49:
//						goto IL_4B8;
//					case 50:
//						this.IT8500_SendData[3] = (byte)(this.DYN_CC_CurrentA % 256);
//						this.IT8500_SendData[4] = (byte)((this.DYN_CC_CurrentA >> 8) % 256);
//						this.IT8500_SendData[5] = (byte)((this.DYN_CC_CurrentA >> 16) % 256);
//						this.IT8500_SendData[6] = (byte)((this.DYN_CC_CurrentA >> 24) % 256);
//						this.IT8500_SendData[7] = (byte)(this.DYN_TimeA % 256);
//						this.IT8500_SendData[8] = (byte)((this.DYN_TimeA >> 8) % 256);
//						this.IT8500_SendData[9] = (byte)(this.DYN_CC_CurrentB % 256);
//						this.IT8500_SendData[10] = (byte)((this.DYN_CC_CurrentB >> 8) % 256);
//						this.IT8500_SendData[11] = (byte)((this.DYN_CC_CurrentB >> 16) % 256);
//						this.IT8500_SendData[12] = (byte)((this.DYN_CC_CurrentB >> 24) % 256);
//						this.IT8500_SendData[13] = (byte)(this.DYN_TimeB % 256);
//						this.IT8500_SendData[14] = (byte)((this.DYN_TimeB >> 8) % 256);
//						this.IT8500_SendData[15] = (byte)this.DYN_Mode;
//						goto IL_4B8;
//					case 51:
//						goto IL_4B8;
//					case 52:
//						goto IL_4B8;
//					case 53:
//						goto IL_4B8;
//					case 54:
//						goto IL_4B8;
//					case 55:
//						goto IL_4B8;
//					case 56:
//						goto IL_4B8;
//					case 57:
//						goto IL_4B8;
//					case 88:
//						this.IT8500_SendData[3] = this.TriggerMode;
//						goto IL_4B8;
//					case 89:
//						goto IL_4B8;
//					case 90:
//						goto IL_4B8;
//					case 93:
//						this.IT8500_SendData[3] = this.WorkMode;
//						goto IL_4B8;
//					case 94:
//						goto IL_4B8;
//					case 95:
//						goto IL_4B8;
//					}
//					this.IT8500_SendDataNum = 0;
//					IL_4B8:
//					flag = (this.IT8500_SendDataNum != 0);
//					if (flag)
//					{
//						int sum = 0;
//						int j = 0;
//						int num3;
//						do
//						{
//							sum += (int)this.IT8500_SendData[j];
//							j++;
//							num3 = j;
//							num2 = 24;
//						}
//						while (num3 <= num2);
//						this.IT8500_SendData[25] = (byte)(sum % 256);
//					}
//					flag = (this.IT8500_SendDataNum > 0);
//					if (flag)
//					{
//						bool isOpen = this.SerialPort.IsOpen;
//						if (isOpen)
//						{
//							this.SerialPort.Write(this.IT8500_SendData, 0, 26);
//							Thread.Sleep(500);
//						}
//					}
//				}
//			}
//		}

//		// Token: 0x06000722 RID: 1826 RVA: 0x0002F85C File Offset: 0x0002DC5C
//		public void IT85ComRx()
//		{
//			int sum = 0;
//			int i = 0;
//			checked
//			{
//				int num;
//				int num2;
//				do
//				{
//					sum += (int)this.IT8500_ReceiveData[i];
//					i++;
//					num = i;
//					num2 = 24;
//				}
//				while (num <= num2);
//				sum %= 256;
//				bool flag = sum == (int)this.IT8500_ReceiveData[25];
//				if (flag)
//				{
//					this.IT8500_Command = (int)this.IT8500_ReceiveData[2];
//					int it8500_Command = this.IT8500_Command;
//					flag = (it8500_Command == 43);
//					if (!flag)
//					{
//						flag = (it8500_Command == 51);
//						if (!flag)
//						{
//							flag = (it8500_Command == 95);
//							if (flag)
//							{
//								this.CC_Load_Current = (double)((int)this.IT8500_ReceiveData[7] + (int)this.IT8500_ReceiveData[8] * 256 + (int)this.IT8500_ReceiveData[9] * 65536) / 10000.0;
//							}
//						}
//					}
//				}
//			}
//		}

//		// Token: 0x06000723 RID: 1827 RVA: 0x0002F91C File Offset: 0x0002DD1C
//		public void RmtMode()
//		{
//			this.FrontRear = 1;
//			this.IT8500_Command = 32;
//			this.IT85TxNewFlage = true;
//			this.IT85ComTx();
//		}

//		// Token: 0x06000724 RID: 1828 RVA: 0x0002F940 File Offset: 0x0002DD40
//		public void OutputDisable()
//		{
//			this.OutputEn = 0;
//			this.IT8500_Command = 33;
//			this.IT85TxNewFlage = true;
//			this.IT85ComTx();
//		}

//		// Token: 0x06000725 RID: 1829 RVA: 0x0002F964 File Offset: 0x0002DD64
//		public void OutputEnable()
//		{
//			this.OutputEn = 1;
//			this.IT8500_Command = 33;
//			this.IT85TxNewFlage = true;
//			this.IT85ComTx();
//		}

//		// Token: 0x06000726 RID: 1830 RVA: 0x0002F988 File Offset: 0x0002DD88
//		public void WorkModeSet(int mode)
//		{
//			this.WorkMode = 0;
//			this.IT8500_Command = 93;
//			this.IT85TxNewFlage = true;
//			this.IT85ComTx();
//		}

//		// Token: 0x06000727 RID: 1831 RVA: 0x0002F9AC File Offset: 0x0002DDAC
//		public void OperationModeSet(int mode)
//		{
//			this.OperationMode = 0;
//			this.IT8500_Command = 40;
//			this.IT85TxNewFlage = true;
//			this.IT85ComTx();
//		}

//		// Token: 0x06000728 RID: 1832 RVA: 0x0002F9D0 File Offset: 0x0002DDD0
//		public void CurrentSet(int current_mA)
//		{
//			this.CC_Current = checked(current_mA * 10);
//			this.IT8500_Command = 42;
//			this.IT85TxNewFlage = true;
//			this.IT85ComTx();
//		}

//		// Token: 0x06000729 RID: 1833 RVA: 0x0002F9F4 File Offset: 0x0002DDF4
//		public void DYN_CC_Set(int currentA, int timeA, int currentB, int timeB)
//		{
//			checked
//			{
//				this.DYN_CC_CurrentA = currentA * 10;
//				this.DYN_CC_CurrentB = currentB * 10;
//				this.DYN_TimeA = timeA * 10;
//				this.DYN_TimeB = timeB * 10;
//				this.DYN_Mode = 2;
//				this.IT8500_Command = 50;
//				this.IT85TxNewFlage = true;
//				this.IT85ComTx();
//			}
//		}

//		// Token: 0x0600072A RID: 1834 RVA: 0x0002FA4C File Offset: 0x0002DE4C
//		public void TrigModeSet()
//		{
//			this.TriggerMode = 1;
//			this.IT8500_Command = 88;
//			this.IT85TxNewFlage = true;
//			this.IT85ComTx();
//		}

//		// Token: 0x0600072B RID: 1835 RVA: 0x0002FA70 File Offset: 0x0002DE70
//		public void BusTrig()
//		{
//			this.IT8500_Command = 90;
//			this.IT85TxNewFlage = true;
//			this.IT85ComTx();
//		}

//		// Token: 0x0600072C RID: 1836 RVA: 0x0002FA8C File Offset: 0x0002DE8C
//		public double ReadCurrent()
//		{
//			this.IT8500_Command = 43;
//			this.IT85TxNewFlage = true;
//			this.IT85ComTx();
//			return this.CC_Load_Current;
//		}

//		// Token: 0x04000585 RID: 1413
//		public const int IT85_SYNC_HEAD = 170;

//		// Token: 0x04000586 RID: 1414
//		public const int IT85_ADDRESS = 0;

//		// Token: 0x04000587 RID: 1415
//		public const int IT85_FRONT_REAR = 32;

//		// Token: 0x04000588 RID: 1416
//		public const int IT85_OUTPUT_EN = 33;

//		// Token: 0x04000589 RID: 1417
//		public const int IT85_OPERATION_MODE_W = 40;

//		// Token: 0x0400058A RID: 1418
//		public const int IT85_OPERATION_MODE_R = 41;

//		// Token: 0x0400058B RID: 1419
//		public const int IT85_CC_CURRENT_W = 42;

//		// Token: 0x0400058C RID: 1420
//		public const int IT85_CC_CURRENT_R = 43;

//		// Token: 0x0400058D RID: 1421
//		public const int IT85_CV_VOLTAGE_W = 44;

//		// Token: 0x0400058E RID: 1422
//		public const int IT85_CV_VOLTAGE_R = 45;

//		// Token: 0x0400058F RID: 1423
//		public const int IT85_CW_POWER_W = 46;

//		// Token: 0x04000590 RID: 1424
//		public const int IT85_CW_POWER_R = 47;

//		// Token: 0x04000591 RID: 1425
//		public const int IT85_CR_REGISTER_W = 48;

//		// Token: 0x04000592 RID: 1426
//		public const int IT85_CR_REGISTER_R = 49;

//		// Token: 0x04000593 RID: 1427
//		public const int IT85_CC_DYN_CURRENT_W = 50;

//		// Token: 0x04000594 RID: 1428
//		public const int IT85_CC_DYN_CURRENT_R = 51;

//		// Token: 0x04000595 RID: 1429
//		public const int IT85_CV_DYN_VOLTAGE_W = 52;

//		// Token: 0x04000596 RID: 1430
//		public const int IT85_CV_DYN_VOLTAGE_R = 53;

//		// Token: 0x04000597 RID: 1431
//		public const int IT85_CW_DYN_POWER_W = 54;

//		// Token: 0x04000598 RID: 1432
//		public const int IT85_CW_DYN_POWER_R = 55;

//		// Token: 0x04000599 RID: 1433
//		public const int IT85_CR_DYN_REGISTER_W = 56;

//		// Token: 0x0400059A RID: 1434
//		public const int IT85_CR_DYN_REGISTER_R = 57;

//		// Token: 0x0400059B RID: 1435
//		public const int IT85_TRIGGER_MODE_W = 88;

//		// Token: 0x0400059C RID: 1436
//		public const int IT85_TRIGGER_MODE_R = 89;

//		// Token: 0x0400059D RID: 1437
//		public const int IT85_TRIGGER_BUS = 90;

//		// Token: 0x0400059E RID: 1438
//		public const int IT85_WORK_MODE_W = 93;

//		// Token: 0x0400059F RID: 1439
//		public const int IT85_WORK_MODE_R = 94;

//		// Token: 0x040005A0 RID: 1440
//		public const int IT85_QUERY = 95;

//		// Token: 0x040005A1 RID: 1441
//		public const int IT85_OFF = 0;

//		// Token: 0x040005A2 RID: 1442
//		public const int IT85_ON = 1;

//		// Token: 0x040005A3 RID: 1443
//		public const int IT85_FRONT = 0;

//		// Token: 0x040005A4 RID: 1444
//		public const int IT85_REAR = 1;

//		// Token: 0x040005A5 RID: 1445
//		public const int IT85_CC = 0;

//		// Token: 0x040005A6 RID: 1446
//		public const int IT85_CV = 1;

//		// Token: 0x040005A7 RID: 1447
//		public const int IT85_CW = 2;

//		// Token: 0x040005A8 RID: 1448
//		public const int IT85_CR = 3;

//		// Token: 0x040005A9 RID: 1449
//		public const int IT85_ONCE = 0;

//		// Token: 0x040005AA RID: 1450
//		public const int IT85_REPEAT = 1;

//		// Token: 0x040005AB RID: 1451
//		public const int IT85_CONTINUES = 0;

//		// Token: 0x040005AC RID: 1452
//		public const int IT85_PULSE = 1;

//		// Token: 0x040005AD RID: 1453
//		public const int IT85_TOGGLED = 2;

//		// Token: 0x040005AE RID: 1454
//		public const int IT85_IMMEDIATE = 0;

//		// Token: 0x040005AF RID: 1455
//		public const int IT85_EXTERNAL = 1;

//		// Token: 0x040005B0 RID: 1456
//		public const int IT85_BUS = 2;

//		// Token: 0x040005B1 RID: 1457
//		public const int IT85_FIXED = 0;

//		// Token: 0x040005B2 RID: 1458
//		public const int IT85_SHORT = 1;

//		// Token: 0x040005B3 RID: 1459
//		public const int IT85_TRAN = 2;

//		// Token: 0x040005B4 RID: 1460
//		public const int IT85_LIST = 3;

//		// Token: 0x040005B5 RID: 1461
//		public const int IT85_BATTERY = 4;

//		// Token: 0x040005B6 RID: 1462
//		public byte Address;

//		// Token: 0x040005B7 RID: 1463
//		public byte OutputEn;

//		// Token: 0x040005B8 RID: 1464
//		public byte FrontRear;

//		// Token: 0x040005B9 RID: 1465
//		public byte OperationMode;

//		// Token: 0x040005BA RID: 1466
//		public byte TriggerMode;

//		// Token: 0x040005BB RID: 1467
//		public byte RemoteTest;

//		// Token: 0x040005BC RID: 1468
//		public byte WorkMode;

//		// Token: 0x040005BD RID: 1469
//		public int MaxCurrent;

//		// Token: 0x040005BE RID: 1470
//		public int MaxVoltage;

//		// Token: 0x040005BF RID: 1471
//		public int MaxPower;

//		// Token: 0x040005C0 RID: 1472
//		public int CC_Current;

//		// Token: 0x040005C1 RID: 1473
//		public int CV_Voltage;

//		// Token: 0x040005C2 RID: 1474
//		public int CW_Power;

//		// Token: 0x040005C3 RID: 1475
//		public int CR_Register;

//		// Token: 0x040005C4 RID: 1476
//		public double CC_Load_Current;

//		// Token: 0x040005C5 RID: 1477
//		public int DYN_CC_CurrentA;

//		// Token: 0x040005C6 RID: 1478
//		public int DYN_CV_VoltageA;

//		// Token: 0x040005C7 RID: 1479
//		public int DYN_CW_PowerA;

//		// Token: 0x040005C8 RID: 1480
//		public int DYN_CR_RegisterA;

//		// Token: 0x040005C9 RID: 1481
//		public int DYN_TimeA;

//		// Token: 0x040005CA RID: 1482
//		public int DYN_CC_CurrentB;

//		// Token: 0x040005CB RID: 1483
//		public int DYN_CV_VoltageB;

//		// Token: 0x040005CC RID: 1484
//		public int DYN_CW_PowerB;

//		// Token: 0x040005CD RID: 1485
//		public int DYN_CR_RegisterB;

//		// Token: 0x040005CE RID: 1486
//		public int DYN_TimeB;

//		// Token: 0x040005CF RID: 1487
//		public int DYN_UpRate;

//		// Token: 0x040005D0 RID: 1488
//		public int DYN_DownRate;

//		// Token: 0x040005D1 RID: 1489
//		public int DYN_Mode;

//		// Token: 0x040005D2 RID: 1490
//		public int DYN_Duty;

//		// Token: 0x040005D3 RID: 1491
//		public byte ListMode;

//		// Token: 0x040005D4 RID: 1492
//		public byte LoopMode;

//		// Token: 0x040005D5 RID: 1493
//		public byte[] IT8500_ReceiveData;

//		// Token: 0x040005D6 RID: 1494
//		public int IT8500_Command;

//		// Token: 0x040005D7 RID: 1495
//		public byte[] IT8500_SendData;

//		// Token: 0x040005D8 RID: 1496
//		public int IT8500_SendDataNum;

//		// Token: 0x040005D9 RID: 1497
//		public bool IT85TxNewFlage;

//		// Token: 0x040005DA RID: 1498
//		public COM_PORT ComPort;

//		// Token: 0x040005DB RID: 1499
//		public SerialPort SerialPort;
//	}
//}
