﻿using Mesen.GUI.Debugger.Controls;
using Mesen.GUI.Debugger.Integration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesen.GUI.Debugger.Code
{
	public class Sa1DisassemblyManager : CpuDisassemblyManager
	{
		public override CpuType CpuType { get { return CpuType.Sa1; } }
		public override SnesMemoryType RelativeMemoryType { get { return SnesMemoryType.Sa1Memory; } }
		public override bool AllowSourceView { get { return false; } }

		public override void RefreshCode(DbgImporter symbolProvider, DbgImporter.FileInfo file)
		{
			this._provider = new CodeDataProvider(CpuType.Sa1);
		}

		protected override int GetFullAddress(int address, int length)
		{
			CpuState state = DebugApi.GetState().Sa1;
			if(length == 4) {
				//Append current DB register to 2-byte addresses
				return (state.DBR << 16) | address;
			} else if(length == 2) {
				//Add direct register to 1-byte addresses
				return (state.D + address);
			}

			return address;
		}
	}
}
