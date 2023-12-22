// Вставьте сюда финальное содержимое файла VirtualMachine.cs
using System;
using System.Collections.Generic;

namespace func.brainfuck
{
    public class VirtualMachine : IVirtualMachine
    {
        Dictionary<char, Action<IVirtualMachine>> commandDict = new Dictionary<char, Action<IVirtualMachine>>();
        public string Instructions { get; }
        public int InstructionPointer { get; set; }
        public byte[] Memory { get; }
        public int MemoryPointer { get; set; }

        public VirtualMachine(string program, int memorySize)
        {
            Instructions = program;
            Memory = new byte[memorySize];
        }

        public void RegisterCommand(char symbol, Action<IVirtualMachine> execute)
        {
            commandDict.Add(symbol,execute);
        }

        public void Run()
        {
            for (; InstructionPointer < Instructions.Length; InstructionPointer++)
            {
                var command = Instructions[InstructionPointer];
                if (commandDict.ContainsKey(command))
                {
                    commandDict[command](this);
                }
            }
        }
    }
}