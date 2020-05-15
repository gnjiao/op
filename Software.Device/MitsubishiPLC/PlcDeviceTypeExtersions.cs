using System;
using System.Collections.Generic;
using System.Text;

namespace Software.Device.MitsubishiPLC
{
	/// <summary>
	/// 对PlcDeviceType的扩展：
	///     提供ASCII方式下的命令与二进制下的对照表
	/// </summary>
	public static class PlcDeviceTypeExtersions
	{
		private static Dictionary<PLCDeviceType, string> typeMapping = new Dictionary<PLCDeviceType, string>() {
			{PLCDeviceType.M,"M*"},
			{PLCDeviceType.X,"X*"},
			{PLCDeviceType.Y,"Y*"},
			{PLCDeviceType.D,"D*"},
			{PLCDeviceType.R,"R*"},
			{PLCDeviceType.TN,"TN"},
		};
		public static string ToAsciiName(this PLCDeviceType deviceType)
		{
			return typeMapping[deviceType];
		}

		public static byte[] ToAsciiNameBytes(this PLCDeviceType deviceType)
		{
			return ASCIIEncoding.ASCII.GetBytes(typeMapping[deviceType]);
		}

		public static PLCDeviceType ToPlcDeviceType(this string deviceTypeName)
		{
			foreach (var kv in typeMapping) {
				if (kv.Value.Equals(deviceTypeName, StringComparison.OrdinalIgnoreCase))
					return kv.Key;
			}
			return PLCDeviceType.M;
		}

	}
}
