using System;
using System.Collections.Generic;
using System.Linq;

using System;

namespace func.brainfuck
{
	public class BrainfuckBasicCommands
	{
		public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
		{
			AddDecreaseInput(vm);
			ReadWriteInput(vm, read, write);
			RegisterMovementInput(vm);
			RegisterSymbolInput(vm);

			static void AddDecreaseInput(IVirtualMachine vm)
			{
				vm.RegisterCommand('+', b =>
				{
					if (b.Memory[b.MemoryPointer] == 255) b.Memory[b.MemoryPointer] = 0;
					else b.Memory[b.MemoryPointer]++;
				});
				vm.RegisterCommand('-', b =>
				{
					if (b.Memory[b.MemoryPointer] == 0) b.Memory[b.MemoryPointer] = 255;
					else b.Memory[b.MemoryPointer]--;
				});
			}

			static void ReadWriteInput(IVirtualMachine vm, Func<int> read, Action<char> write)
			{
				vm.RegisterCommand('.', b => { write(Convert.ToChar(b.Memory[b.MemoryPointer])); });
				vm.RegisterCommand(',', b => { b.Memory[b.MemoryPointer] = Convert.ToByte(read()); });
			}

			static void RegisterMovementInput(IVirtualMachine vm)
			{
				vm.RegisterCommand('>', b =>
				{
					if (b.MemoryPointer == b.Memory.Length - 1) b.MemoryPointer = 0;
					else b.MemoryPointer++;
				});
				vm.RegisterCommand('<', b =>
				{
					if (b.MemoryPointer == 0) b.MemoryPointer = b.Memory.Length - 1;
					else b.MemoryPointer--;
				});
			}

			static void RegisterSymbolInput(IVirtualMachine vm)
			{
				char[] symbols = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890".ToCharArray();
				foreach (var chars in symbols)
				{
					vm.RegisterCommand(chars, machine => machine.Memory[machine.MemoryPointer] = (byte)chars);
				}
			}
		}
	}
}